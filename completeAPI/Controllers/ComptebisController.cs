using completeAPI.Model;
using completeAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace completeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComptebisController:ControllerBase
    {

        private readonly ICompteService service;

        public ComptebisController(ICompteService service)
        {
            this.service = service;
        }

        // GET: Comptebis/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compte>>> GetComptes()
        {
            List<Compte> compteList = (List<Compte>) await service.touslescomptes();
            if (compteList == null)
            {
                return NotFound();
            }
            return compteList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Compte>> GetCompte(int id)
        {
            Compte account = await service.uncompte(id);
            if (account==null)
            {
                return NotFound();
            }
            return account;          
            
        }

        [HttpPost]
        public async Task<ActionResult<Compte>> PostCompte(Compte compte)
        {
            Compte addedcompte= await service.creerCompte(compte);

            if (addedcompte == null)
            {
                return Problem("la creation a echouee");
            }
            return StatusCode(201, addedcompte);

          //return CreatedAtAction("GetCompte", new { id = compte.CompteID }, compte);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompte(int id)
        {
            bool etat = await service.supprimeCompte(id);

            if (etat==false)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompte(int id, Compte compte)
        {
            
            int etat = await service.MajCompte(id, compte);
            switch (etat) //le swith case ici n'aura pas de break vu quon fait des returns
            {
                case 1: return BadRequest(); 
                case 2: return NotFound(); 
                default: return NoContent(); 
            }
            
        }


    }
}
