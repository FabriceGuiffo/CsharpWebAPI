using completeAPI.Controllers;
using completeAPI.Model;
using completeAPI.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ProjectTests
{
    [TestClass]
    public class UnitTestSuite
    {
        [TestMethod]
        [Description("on cherche a tester la methode getcomptes du controller pour savoir si elle marche lorsqu'il y a des elems")]
        public void LamethodeGetComptes_renvoilebonnomdupremierelementdelalisteetlebonnombredelements()
        {
            //Arrange

            //on commence par creer un simulacre du Icompteservice
            var fauxservice = Mock.Of<ICompteService>();
            //ensuite on definit le comportement sur la methode touslescomptes
            Mock.Get(fauxservice).Setup(m => m.touslescomptes()).Returns(new FakeData().setcomptes());
            var controller = new ComptebisController(fauxservice);

            //Act
            var result = controller.GetComptes();

            //Assert
            //result.GetAwaiter().GetResult().Value est l'attribut du actionresult qui l'ouvre pour exposer sont contenu en occurence un Ienumerable ici qu'on converti en list
            result.GetAwaiter().GetResult().Value.ToList<Compte>().First().nom.Should().Be("Brice");
            result.GetAwaiter().GetResult().Value.ToList<Compte>().Count.Should().Be(3);

        }

        [TestMethod]
        [Description("on cherche a savoir si la methode getcomptes renvoi un 404 lorsque la liste est vide")]
        public void Etantdonneunappelalamethodegetcomptes_lorsquelalisterenvoyeeestnulle_onobtientunstatut404()
        {
            //Arrange
            var fauxservice= Mock.Of<ICompteService>();
            Mock.Get(fauxservice).Setup(m => m.touslescomptes()).Returns(new FakeData().nocompte());            
            var controller = new ComptebisController(fauxservice);

            //act
            var resultat = controller.GetComptes();
            var actionresult = resultat.GetAwaiter().GetResult();// j'ouvre la task et j'expose le actionresult qui est l'equivalent
                                                                 //mvc de IHTTPActionResult. 

            //assert

            Assert.IsInstanceOfType(actionresult.Result, typeof(NotFoundResult));
            
        }


        [TestMethod]
        [Description("on teste la methode PostCompte du controller qui doit renvoyer le bon statut")]
        public void EtantdonnelamethodePostCompte_lorsqueonluipasseuncompte_alorslestatuscodeestbon()
        {
            //Arrange
            var fauxservice = Mock.Of<ICompteService>();
            Mock.Get(fauxservice).Setup(m => m.creerCompte(It.IsAny<Compte>())).Returns(new FakeData().uncompte());
            var controller = new ComptebisController(fauxservice);

            //
            //act
            var resultat = controller.PostCompte(new Compte());
            var actionresult = resultat.GetAwaiter().GetResult();
            var elem = ((ObjectResult)(actionresult.Result)).StatusCode;

            //assert

            elem.Should().Be(201);

        }


    }
}