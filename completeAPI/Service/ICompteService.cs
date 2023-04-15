using completeAPI.Model;

namespace completeAPI.Service
{
    public interface ICompteService
    {
        Task<Compte?> creerCompte(Compte compte);
        Task<int> MajCompte(int id, Compte compte);
        Task<bool> supprimeCompte(int id);
        Task<IEnumerable<Compte>> touslescomptes();
        Task<Compte> uncompte(int id);
    }
}