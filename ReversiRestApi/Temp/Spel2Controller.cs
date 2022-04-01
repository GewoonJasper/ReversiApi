//using Microsoft.AspNetCore.Mvc;
//using ReversieISpelImplementatie.Model;
//using ReversiRestApi.Models;
//using ReversiRestApi.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace ReversiRestApi.Controllers
//{
//    [Route("api/Spel2")]
//    [ApiController]
//    public class Spel2Controller : ControllerBase
//    {
//        private readonly ISpel2Repository iRepository;

//        public Spel2Controller(ISpel2Repository repository)
//        {
//            iRepository = repository;
//        }

//        //TODO ALLES ASYNC MAKEN

//        // GET: api/spel
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<string>>> GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler() 
//        {
//            return Ok(iRepository.GetSpellen()
//                .Where(s => string.IsNullOrEmpty(s.Speler2Token))
//                .Where(s => !string.IsNullOrEmpty(s.Omschrijving))
//                .Select(s => s.Omschrijving));
//        }

//        // GET: api/spel/Spellen
//        [HttpGet("Spellen")]
//        public async Task<ActionResult<List<SpelTbvJson>>> GetSpellenMetWachtendeSpeler()
//        {
//            List<Spel> spellen = iRepository.GetSpellen()
//                .Where(s => string.IsNullOrEmpty(s.Speler2Token)).ToList();

//            List<SpelTbvJson> spellenTbvJson = new List<SpelTbvJson>();

//            foreach (Spel spel in spellen) spellenTbvJson.Add(new SpelTbvJson(spel));

//            return Ok(spellenTbvJson);
//        }

//        // MISSCHIEN UITEINDELIJK NOG CHECK OF SPELER1TOKEN WEL BESTAAT
//        // MOET DAARVOOR WEL SPELER MODEL MAKEN

//        // POST: /api/Spel
//        [HttpPost]
//        public async Task<ActionResult> AddSpel([FromBody] SpelInfoTbvApi spelInfo)
//        {
//            if (string.IsNullOrEmpty(spelInfo.Speler1Token) || string.IsNullOrEmpty(spelInfo.Omschrijving)) return BadRequest();

//            Spel nieuwSpel = new Spel()
//            {
//                Speler1Token = spelInfo.Speler1Token,
//                Omschrijving = spelInfo.Omschrijving,
//            };

//            iRepository.AddSpel(nieuwSpel);

//            return CreatedAtAction("GetSpel", 
//                new { token = nieuwSpel.Token }, 
//                new SpelTbvJson(nieuwSpel));
//        }

//        // GET: /api/Spel/SpelToken?token=abcdef
//        [HttpGet("SpelToken")]
//        public async Task<ActionResult<SpelTbvJson>> GetSpel(string token)
//        {
//            Spel spel = iRepository.GetSpelFromToken(token);

//            if (spel == null) return NotFound();

//            return Ok(new SpelTbvJson(spel));
//        }

//        // GET: /api/Spel/SpelerToken?spelerToken=abcdef
//        [HttpGet("SpelerToken")]
//        public async Task<ActionResult<SpelTbvJson>> GetSpelFromSpelerToken(string spelerToken)
//        {
//            Spel spel = iRepository.GetSpelFromSpelerToken(spelerToken);

//            if (spel == null) return NotFound();
            
//            return Ok(new SpelTbvJson(spel));
//        }

//        // GET: /api/Spel/AanDeBeurt?spelToken=abcdef
//        [HttpGet("AanDeBeurt")]
//        public async Task<ActionResult<Kleur>> GetAanDeBeurt(string spelToken)
//        {
//            Spel spel = iRepository.GetSpelFromToken(spelToken);

//            if (spel == null) return BadRequest();

//            return Ok(spel.AandeBeurt);
//        }

//        // GET: /api/Spel/AantalFichesZwart?spelToken=abcdef
//        [HttpGet("AantalFichesZwart")]
//        public async Task<ActionResult<int>> GetAantalFichesZwart(string spelToken)
//        {
//            Spel spel = iRepository.GetSpelFromToken(spelToken);

//            if (spel == null) return BadRequest();

//            int aantalZwart = 0;

//            foreach (Kleur kleur in spel.Bord)
//            {
//                if (kleur.Equals(Kleur.Zwart)) aantalZwart++;
//            }

//            return Ok(aantalZwart);
//        }

//        // GET: /api/Spel/AantalFichesWit?spelToken=abcdef
//        [HttpGet("AantalFichesWit")]
//        public async Task<ActionResult<int>> GetAantalFichesWit(string spelToken)
//        {
//            Spel spel = iRepository.GetSpelFromToken(spelToken);

//            if (spel == null) return BadRequest();

//            int aantalWit = 0;

//            foreach (Kleur kleur in spel.Bord)
//            {
//                if (kleur.Equals(Kleur.Wit)) aantalWit++;
//            }

//            return Ok(aantalWit);
//        }

//        // PUT: api/Spel/DoeMee?spelToken=abcdef&spelerToken=abcdef
//        [HttpPut("DoeMee")]
//        public async Task<ActionResult<SpelTbvJson>> DoeMee(string spelToken, string spelerToken)
//        {
//            Spel spel = iRepository.GetSpelFromToken(spelToken);
//            if (spel == null || !string.IsNullOrEmpty(spel.Speler2Token)) return BadRequest();

//            Spel nieuwSpel = iRepository.Join(spelToken, spelerToken);
//            spel.Status = Status.Bezig;

//            return CreatedAtAction("GetSpel",
//                new { token = nieuwSpel.Token },
//                new SpelTbvJson(nieuwSpel));
//        }

//        // PUT: api/Spel/Zet?spelToken=abcdef&spelerToken=abcdef&rijzet=1&kolomzet=1
//        [HttpPut("Zet")]
//        public async Task<ActionResult> PutSpelSpelerZet(string spelToken, string spelerToken, int rijZet, int kolomZet) // Moeten de laatste 2 er ook bij?
//        {
//            if (string.IsNullOrEmpty(spelToken) || string.IsNullOrEmpty(spelerToken)) return BadRequest();

//            Spel spel = iRepository.GetSpelFromToken(spelToken);
            

//            if (spel == null) return NotFound();
//            if (!SpelerZitInGame(spel, spelerToken)) return BadRequest("Je zit niet in deze game");
//            if (!SpelerIsAanDeBeurt(spel, spelerToken)) return BadRequest("Je bent niet aan de beurt");


//            if (spel.IsErEenZetMogelijk(spel.AandeBeurt))
//            {
//                if (spel.ZetMogelijk(rijZet, kolomZet))
//                {
//                    spel.DoeZet(rijZet, kolomZet);

//                    if (spel.Afgelopen())
//                    {
//                        spel.Winnaar = spel.OverwegendeKleur();
//                        spel.AandeBeurt = Kleur.Geen;
//                        spel.Status = Status.Klaar;
//                    }

//                    iRepository.SaveChanges();

//                    return Ok(new SpelTbvJson(spel));
//                }
//            }

//            return BadRequest("Er is iets fout gegaan");
//        }

//        // PUT: api/Spel/Opgeven?spelToken=abcdef&spelerToken=abcdef
//        [HttpPut("Opgeven")]
//        public async Task<ActionResult> PutSpelSpelerGeeftOp(string spelToken, string spelerToken)
//        {
//            Spel spel = iRepository.GetSpelFromToken(spelToken);

//            //if (spel == null || !spel.AandeBeurt.Equals(spelerToken)) return BadRequest();
//            if (spel == null) return BadRequest();

//            // Wat moet je doen bij opgeven?
//            spel.Opgeven();            

//            return Ok(new SpelTbvJson(spel));
//        }

//        // PUT: api/Spel/Pas?spelToken=abcdef&spelerToken=abcdef
//        [HttpPut("Pas")]
//        public async Task<ActionResult> Pas(string spelToken, string spelerToken)
//        {
//            Spel spel = iRepository.GetSpelFromToken(spelToken);

//            if (spel == null) return NotFound();
//            if (!SpelerZitInGame(spel, spelerToken)) return BadRequest("Je zit niet in deze game");
//            if (!SpelerIsAanDeBeurt(spel, spelerToken)) return BadRequest("Je bent niet aan de beurt");

//            spel.Pas();

//            iRepository.UpdateSpel(spel);

//            return Ok(new SpelTbvJson(spel));
//        }

//        // PUT: api/Spel/RemoveSpeler?spelToken=abcdef&spelerToken=abcdef
//        [HttpPut("RemoveSpeler")]
//        public async Task<ActionResult> RemoveSpelerFromSpel(string spelToken, string spelerToken)
//        {
//            Spel spel = iRepository.GetSpelFromToken(spelToken);

//            if (spel == null) return NotFound();
//            if (!SpelerZitInGame(spel, spelerToken)) return BadRequest();

//            if (spel.Speler1Token == spelerToken) spel.Speler1Token = null;
//            else spel.Speler2Token = null;

//            iRepository.UpdateSpel(spel);

//            return Ok(new SpelTbvJson(spel));
//        }

//        // DELETE: /api/Spel/Delete?spelToken=abcdef
//        [HttpDelete("Delete")]
//        public async Task<ActionResult> DeleteSpel(string spelToken)
//        {
//            Spel spel = iRepository.GetSpelFromToken(spelToken);

//            if (spel == null) return NotFound();
//            if (!GeenSpelersInGame(spel)) return BadRequest();

//            iRepository.DeleteSpel(spel);

//            return Ok();
//        }

//        //PUT: /api/Spel/UpdateSpel?spelToken=abcdef
//        [HttpPut("UpdateSpel")]
//        public async Task<ActionResult> UpdateSpel(string spelToken)
//        {
//            Spel spel = iRepository.GetSpelFromToken(spelToken);
//            iRepository.UpdateSpel(spel);
//            return Ok(new SpelTbvJson(spel));
//        }


//        private bool SpelerZitInGame(Spel spel, string spelerToken)
//        {
//            return spel.Speler1Token == spelerToken || spel.Speler2Token == spelerToken;
//        }

//        private bool SpelerIsAanDeBeurt(Spel spel, string spelerToken)
//        {
//            return spel.Speler1Token == spelerToken && spel.AandeBeurt == Kleur.Wit || spel.Speler2Token == spelerToken && spel.AandeBeurt == Kleur.Zwart;
//        }

//        private bool GeenSpelersInGame(Spel spel)
//        {
//            return spel.Speler1Token == null && spel.Speler2Token == null;
//        }
//    }
//}
