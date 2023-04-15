using System.ComponentModel.DataAnnotations;

namespace completeAPI.Model
{
    public class Compte
    {
        public int CompteID { get; set; }
      
        public string nom { get; set; }
        
        public DateTime dateouverture { get; set; }
       
        public float solde { get; set; }
        public string email { get; set; }
    }
}
