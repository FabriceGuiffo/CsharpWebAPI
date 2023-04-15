using completeAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTests
{
    internal class FakeData
    {
        public async Task<IEnumerable<Compte>> setcomptes()
        {
            List<Compte> jeu = new List<Compte>();
            jeu.Add(new Compte() { CompteID = 1, dateouverture = DateTime.Today, email = "fhindn", nom = "Brice", solde = 143 });
            jeu.Add(new Compte() { CompteID = 2, dateouverture = DateTime.Today, email = "uiun", nom = "adi", solde = 4563 });
            jeu.Add(new Compte() { CompteID = 3, dateouverture = DateTime.Today, email = "nondidi", nom = "dciiw", solde = 76626 });

            return  jeu.AsEnumerable<Compte>();
        }

        public async Task<IEnumerable<Compte>> nocompte()
        {
            //return new List<Compte>().AsEnumerable<Compte>(); //by default list is supposed to be null
            return null;
        }

        public async Task<Compte> uncompte()
        {
            return new Compte();
        }

    }
}
