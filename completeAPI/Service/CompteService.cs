using completeAPI.DAL;
using completeAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace completeAPI.Service
{
    /*couche contenant les traitements metiers*/
    public class CompteService : ICompteService
    {

        private readonly GeneralContext context;

        public CompteService(GeneralContext context)
        {
            this.context = context;
        }
        // la methode pour obtenir tous les comptes
        public async Task<IEnumerable<Compte>> touslescomptes()
        {
            if (context.Comptes == null)
            {
                return null;
            }
            return await context.Comptes.ToListAsync();
        }

        public async Task<Compte> uncompte(int id)
        {
            if (context.Comptes == null)
            {
                return null;
            }
            var compte = await context.Comptes.FindAsync(id);

            if (compte == null)
            {
                return null;
            }

            return compte;
        }


        public async Task<Compte?> creerCompte(Compte compte)
        {
            //on commence avec le traitement metier qui est de mettre en majuscule tous les noms
            compte.nom = compte.nom.ToUpper();

            context.Comptes.Add(compte);
            await context.SaveChangesAsync();
            //je suppose qu'apres le save si la validation a ete alors notre objet a maintenant un identifiant et peu etre return
            if (compte.CompteID == 0)
            {
                return null;
            }
            return compte;

        }

        public async Task<bool> supprimeCompte(int id)
        {
            //on definit un boolean qui dit si la suppresion s'est bien faite
            if (context.Comptes == null)
            {
                return true; //il n'y a rien a delete d'entree de jeu
            }
            var compte = await context.Comptes.FindAsync(id);
            if (compte == null)
            {
                return true; //il n'y a rien a delete d'entree de jeu
            }

            //on fait quelques controlles du cas ou l'objet est non null mais l'operation de modif en bdd echoue=> la donnee nest pas supprimee
            //on peut faire pareil sur un post
            try
            {
                context.Comptes.Remove(compte);
                await context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {

                return false;
            }


            catch (OperationCanceledException)
            {

                return false;
            }


            return true;
        }

        public async Task<int> MajCompte(int id, Compte compte)
        {
            if (id != compte.CompteID)
            {
                return 1;
            }

            context.Entry(compte).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompteExists(id))
                {
                    //MOTH: lorsque le savechanges fait appel au change tracker et que pour l'objet passe en argument
                    //on se rend compte et on lance l'exception entre autres raisons
                    //si le compte n'existe pas alors il est impossible de faire la MAJ
                    return 2;
                }
                else
                {
                    throw;
                }
            }

            return 3;
        }

        private bool CompteExists(int id)
        {
            return (context.Comptes?.Any(e => e.CompteID == id)).GetValueOrDefault();
        }


    }
}
