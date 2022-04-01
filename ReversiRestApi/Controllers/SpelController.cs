using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReversiRestApi.DataAccess;
using ReversieISpelImplementatie.Model;
using ReversiRestApi.Repositories;
using ReversiRestApi.Models;

namespace ReversiRestApi.Controllers
{
    [Route("api/Spel")]
    [ApiController]
    public class SpelController : Controller
    {
        private readonly ISpelRepository _spelRepository;

        public SpelController(ISpelRepository repository)
        {
            _spelRepository = repository;
        }

        // GET: api/spel/Omschrijvingen
        [HttpGet("Omschrijvingen")]
        public async Task<ActionResult<IEnumerable<string>>> GetSpelOmschrijvingenVanSpellenMetWachtendeSpeler()
        {
            List<Spel> spellen = await _spelRepository.GetSpellen();

            return Ok(spellen
                .Where(s => string.IsNullOrEmpty(s.Speler2Token))
                .Where(s => !string.IsNullOrEmpty(s.Omschrijving))
                .Select(s => s.Omschrijving));
        }

        // GET: api/spel/BeschikbareSpellen
        [HttpGet("BeschikbareSpellen")]
        public async Task<ActionResult<List<SpelTbvJson>>> GetSpellenMetWachtendeSpeler()
        {
            List<Spel> spellen = await _spelRepository.GetSpellen();

            List<SpelTbvJson> spellenTbvJson = new List<SpelTbvJson>();

            foreach (Spel spel in spellen.Where(s => string.IsNullOrEmpty(s.Speler2Token) && s.Status == Status.Wachten).ToList())
                spellenTbvJson.Add(new SpelTbvJson(spel));

            return Ok(spellenTbvJson);
        }

        // GET: api/spel/Spellen
        [HttpGet("Spellen")]
        public async Task<ActionResult<List<SpelTbvJson>>> GetSpellen()
        {
            List<Spel> spellen = await _spelRepository.GetSpellen();

            List<SpelTbvJson> spellenTbvJson = new List<SpelTbvJson>();

            foreach (Spel spel in spellen)
                spellenTbvJson.Add(new SpelTbvJson(spel));

            return Ok(spellenTbvJson);
        }

        // GET: /api/Spel/SpelToken?token=abcdef
        [HttpGet("SpelToken")]
        public async Task<ActionResult<SpelTbvJson>> GetSpel(string token)
        {
            Spel spel = await _spelRepository.GetSpel(token);

            if (spel == null) return NotFound();

            return Ok(new SpelTbvJson(spel));
        }

        // GET: /api/Spel/SpelerToken?token=abcdef
        [HttpGet("SpelerToken")]
        public async Task<ActionResult<SpelTbvJson>> GetSpelFromSpeler(string token)
        {
            List<Spel> spellen = await _spelRepository.GetSpellen();
            Spel spel = spellen.Where(s => s.Speler1Token == token || s.Speler2Token == token).FirstOrDefault();

            if (spel == null) return NotFound();

            return Ok(new SpelTbvJson(spel));
        }

        // GET: /api/Spel/AanDeBeurt?spelToken=abcdef
        [HttpGet("AanDeBeurt")]
        public async Task<ActionResult<Kleur>> GetAanDeBeurt(string spelToken)
        {
            Spel spel = await _spelRepository.GetSpel(spelToken);

            if (spel == null) return BadRequest();

            return Ok(spel.AandeBeurt);
        }

        // GET: /api/Spel/AantalFichesZwart?spelToken=abcdef
        [HttpGet("AantalFichesZwart")]
        public async Task<ActionResult<int>> GetAantalFichesZwart(string spelToken)
        {
            Spel spel = await _spelRepository.GetSpel(spelToken);

            if (spel == null) return BadRequest();

            int aantalZwart = GetAantalFiches(Kleur.Zwart, spel);

            return Ok(aantalZwart);
        }

        // GET: /api/Spel/AantalFichesWit?spelToken=abcdef
        [HttpGet("AantalFichesWit")]
        public async Task<ActionResult<int>> GetAantalFichesWit(string spelToken)
        {
            Spel spel = await _spelRepository.GetSpel(spelToken);

            if (spel == null) return BadRequest();

            int aantalWit = GetAantalFiches(Kleur.Wit, spel);

            return Ok(aantalWit);
        }



        // POST: /api/Spel
        [HttpPost]
        public ActionResult<SpelTbvJson> AddSpel([FromBody] SpelInfoTbvApi spelInfo)
        {
            if (string.IsNullOrEmpty(spelInfo.Speler1Token) || string.IsNullOrEmpty(spelInfo.Omschrijving)) return BadRequest();

            Spel nieuwSpel = new Spel()
            {
                Speler1Token = spelInfo.Speler1Token,
                Omschrijving = spelInfo.Omschrijving,
            };

            _spelRepository.AddSpel(nieuwSpel);
            _spelRepository.SaveSpellen();

            return CreatedAtAction("GetSpel",
                new { token = nieuwSpel.Token },
                new SpelTbvJson(nieuwSpel));
        }



        // PUT: api/Spel/DoeMee?spelToken=abcdef&spelerToken=abcdef
        [HttpPut("DoeMee")]
        public async Task<ActionResult<SpelTbvJson>> DoeMee(string spelToken, string spelerToken)
        {
            Spel spel = await _spelRepository.GetSpel(spelToken);
            if (spel == null || !string.IsNullOrEmpty(spel.Speler2Token)) return BadRequest();

            spel.Speler2Token = spelerToken;
            spel.Status = Status.Bezig;
            spel.AandeBeurt = Kleur.Wit;
            _spelRepository.SaveSpellen();

            return Ok(new SpelTbvJson(spel));
        }

        // PUT: api/Spel/Zet?spelToken=abcdef&spelerToken=abcdef&rijzet=1&kolomzet=1
        [HttpPut("Zet")]
        public async Task<ActionResult<SpelTbvJson>> DoeZet(string spelToken, string spelerToken, int rijZet, int kolomZet)
        {
            if (string.IsNullOrEmpty(spelToken) || string.IsNullOrEmpty(spelerToken)) return BadRequest();

            Spel spel = await _spelRepository.GetSpel(spelToken);

            if (spel == null) return NotFound();
            if (!SpelerZitInGame(spel, spelerToken)) return BadRequest("Je zit niet in deze game");
            if (!SpelerIsAanDeBeurt(spel, spelerToken)) return BadRequest("Je bent niet aan de beurt");

            if (spel.IsErEenZetMogelijk(spel.AandeBeurt))
            {
                if (spel.ZetMogelijk(rijZet, kolomZet))
                {
                    spel.DoeZet(rijZet, kolomZet);

                    if (spel.Afgelopen())
                    {
                        spel.Winnaar = spel.OverwegendeKleur();
                        spel.AandeBeurt = Kleur.Geen;
                        spel.Status = Status.Klaar;
                    }

                    if (spel.AandeBeurt == Kleur.Wit)
                    {
                        spel.Beurt++;

                        spel.AantalFichesZwartPerBeurt = spel.AantalFichesZwartPerBeurt + "-" + GetAantalFiches(Kleur.Zwart, spel);
                        spel.AantalFichesWitPerBeurt = spel.AantalFichesWitPerBeurt + "-" + GetAantalFiches(Kleur.Wit, spel);
                    }

                    _spelRepository.SaveSpellen();

                    return Ok(new SpelTbvJson(spel));
                }
            }

            return BadRequest("Er is iets fout gegaan");
        }

        // PUT: api/Spel/Opgeven?spelToken=abcdef&spelerToken=abcdef
        [HttpPut("Opgeven")]
        public async Task<ActionResult<SpelTbvJson>> GeefOp(string spelToken, string spelerToken)
        {
            Spel spel = await _spelRepository.GetSpel(spelToken);

            if (spel == null) return NotFound();
            if (!SpelerZitInGame(spel, spelerToken)) return BadRequest("Je zit niet in deze game");

            spel.Opgeven();

            spel.Beurt++;

            spel.AantalFichesZwartPerBeurt = spel.AantalFichesZwartPerBeurt + "-" + GetAantalFiches(Kleur.Zwart, spel);
            spel.AantalFichesWitPerBeurt = spel.AantalFichesWitPerBeurt + "-" + GetAantalFiches(Kleur.Wit, spel);

            _spelRepository.SaveSpellen();

            return Ok(new SpelTbvJson(spel));
        }

        // PUT: api/Spel/Pas?spelToken=abcdef&spelerToken=abcdef
        [HttpPut("Pas")]
        public async Task<ActionResult<SpelTbvJson>> Pas(string spelToken, string spelerToken)
        {
            Spel spel = await _spelRepository.GetSpel(spelToken);

            if (spel == null) return NotFound();
            if (!SpelerZitInGame(spel, spelerToken)) return BadRequest("Je zit niet in deze game");
            if (!SpelerIsAanDeBeurt(spel, spelerToken)) return BadRequest("Je bent niet aan de beurt");

            spel.Pas();
            _spelRepository.SaveSpellen();

            return Ok(new SpelTbvJson(spel));
        }

        // PUT: api/Spel/RemoveSpeler?spelToken=abcdef&spelerToken=abcdef
        [HttpPut("RemoveSpeler")]
        public async Task<ActionResult<SpelTbvJson>> RemoveSpelerFromSpel(string spelToken, string spelerToken)
        {
            Spel spel = await _spelRepository.GetSpel(spelToken);

            if (spel == null) return NotFound();
            if (!SpelerZitInGame(spel, spelerToken)) return BadRequest();

            if (spel.Speler1Token == spelerToken) spel.Speler1Token = null;
            else spel.Speler2Token = null;

            _spelRepository.SaveSpellen();

            return Ok(new SpelTbvJson(spel));
        }



        // DELETE: /api/Spel/Delete?spelToken=abcdef
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteSpel(string spelToken)
        {
            Spel spel = await _spelRepository.GetSpel(spelToken);

            if (spel == null) return NotFound();
            if (!GeenSpelersInGame(spel)) return BadRequest();

            _spelRepository.DeleteSpel(spel);
            _spelRepository.SaveSpellen();

            return Ok();
        }

        // DELETE: /api/Spel/ForceDelete?speltoken=abcdef
        [HttpDelete("ForceDelete")]
        public async Task<ActionResult> ForceDeleteSpel(string spelToken)
        {
            Spel spel = await _spelRepository.GetSpel(spelToken);
            if (spel == null) return NotFound();

            _spelRepository.DeleteSpel(spel);
            _spelRepository.SaveSpellen();

            return Ok();
        }




        private bool SpelerZitInGame(Spel spel, string spelerToken)
        {
            return spel.Speler1Token == spelerToken || spel.Speler2Token == spelerToken;
        }
        private bool SpelerIsAanDeBeurt(Spel spel, string spelerToken)
        {
            return spel.Speler1Token == spelerToken && spel.AandeBeurt == Kleur.Wit || spel.Speler2Token == spelerToken && spel.AandeBeurt == Kleur.Zwart;
        }
        private bool GeenSpelersInGame(Spel spel)
        {
            return spel.Speler1Token == null && spel.Speler2Token == null;
        }
        private int GetAantalFiches(Kleur teTellenKleur, Spel spel)
        {
            int aantal = 0;

            foreach (Kleur kleur in spel.Bord)
                if (kleur.Equals(teTellenKleur))
                    aantal++;

            return aantal;
        }
    }
}
