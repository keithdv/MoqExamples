using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lib.Test
{
    [TestClass]
    public class MarvelUniverseMockTests
    {

        public IUniverse marvel = new Marvel();


        private Mock<ISuperHero> Human(bool suit, bool kryptonite)
        {
            var human = new Mock<ISuperHero>(MockBehavior.Strict);

            human.SetupGet(x => x.Race).Returns(enumRace.Human);
            human.SetupGet(x => x.HasIronSuit).Returns(suit);
            human.SetupGet(x => x.HasKryptonite).Returns(kryptonite);


            return human;
        }

        private Mock<ISuperHero> Mutant()
        {
            var mutant = new Mock<ISuperHero>(MockBehavior.Strict);

            mutant.SetupGet(x => x.Race).Returns(enumRace.Mutant);

            return mutant;

        }

        private Mock<ISuperHero> Kryptonian()
        {
            var kryptonian = new Mock<ISuperHero>(MockBehavior.Strict);

            kryptonian.SetupGet(r => r.Race).Returns(enumRace.Kryptonian);

            return kryptonian;
        }


        [TestMethod]
        [TestCategory("Moq"), TestCategory("Human"), TestCategory("Mutant")]
        public void Human_Nothing_Mutant()
        {
            // Human vs Mutant
            // Human has nothing
            // Mutant should win

            // Arrange
            var human = new Mock<ISuperHero>(MockBehavior.Strict);
            var mutant = new Mock<ISuperHero>(MockBehavior.Strict);
            
            human.SetupGet(x => x.Race).Returns(enumRace.Human);
            human.SetupGet(x => x.HasIronSuit).Returns(false);
            human.SetupGet(x => x.HasKryptonite).Returns(false);

            mutant.SetupGet(x => x.Race).Returns(enumRace.Mutant);
   
            // Act
            var r = marvel.WhoWins(human.Object, mutant.Object);

            // Assert
            Assert.AreSame(r, mutant.Object);

            human.VerifyAll();
            mutant.Verify(x => x.Race, Moq.Times.Once);

        }

        [TestMethod]
        [TestCategory("Moq"), TestCategory("Human"), TestCategory("Mutant")]
        public void Human_IronSuite_Mutant()
        {
            // Human vs Mutant
            // Human has an Iron Suite
            // Human should win in Marvel universe

            // Arrange
            var human = Human(true, false);
            var mutant = Mutant();

            // Act
            var result1 = marvel.WhoWins(human.Object, mutant.Object);
            var result2 = marvel.WhoWins(mutant.Object, human.Object);

            // Assert
            Assert.AreSame(human.Object, result1);
            Assert.AreSame(result1, result2);

        }


        [TestMethod]
        [TestCategory("Moq"), TestCategory("Human"), TestCategory("Mutant")]
        public void Human_Kryptonite_Mutant()
        {
            var human = Human(false, true);
            var mutant = Mutant();

            var result = marvel.WhoWins(human.Object, mutant.Object);

            Assert.AreSame(mutant.Object, result);

        }


        [TestMethod]
        [TestCategory("Moq"), TestCategory("Human"), TestCategory("Mutant")]
        public void Human_Both_Mutant()
        {
            var human = Human(true, true);
            var mutant = Mutant();

            var result = marvel.WhoWins(human.Object, mutant.Object);

            Assert.AreSame(human.Object, result);

        }

        [TestMethod]
        [TestCategory("Moq"), TestCategory("Human"), TestCategory("Kryptonian")]
        public void Human_Nothing_Kryptonian()
        {
            var human = Human(false, false);
            var kryptonian = Kryptonian();

            var result = marvel.WhoWins(human.Object, kryptonian.Object);

            Assert.AreSame(kryptonian.Object, result);

        }

        [TestMethod]
        [TestCategory("Moq"), TestCategory("Human"), TestCategory("Kryptonian")]
        public void Human_IronSuite_Kryptonian()
        {
            var human = Human(true, false);
            var kryptonian = Kryptonian();

            var result = marvel.WhoWins(human.Object, kryptonian.Object);

            Assert.AreSame(kryptonian.Object, result);

        }


        [TestMethod]
        [TestCategory("Moq"), TestCategory("Human"), TestCategory("Kryptonian")]
        public void Human_Kryptonite_Kryptonian()
        {
            var human = Human(false, true);
            var kryptonian = Kryptonian();

            var result = marvel.WhoWins(human.Object, kryptonian.Object);

            Assert.AreSame(human.Object, result);

        }


        [TestMethod]
        [TestCategory("Moq"), TestCategory("Human"), TestCategory("Kryptonian")]
        public void Human_Both_Kryptonian()
        {
            var human = Human(true, true);
            var kryptonian = Kryptonian();

            var result = marvel.WhoWins(human.Object, kryptonian.Object);

            Assert.AreSame(human.Object, result);

        }

        [TestMethod]
        [TestCategory("Moq"), TestCategory("Mutant"), TestCategory("Kryptonian")]
        public void Mutant_Kryptonian()
        {

            var mutant = Mutant();
            var kryptonian = Kryptonian();

            var result = marvel.WhoWins(mutant.Object, kryptonian.Object);

            Assert.AreSame(result, kryptonian.Object);

        }

        [TestMethod]
        [TestCategory("Moq")]
        public void SameRaceError()
        {

            Exception ex = null;


            try
            {
                marvel.WhoWins(Mutant().Object, Mutant().Object);
            }
            catch (Exception ex1)
            {

                ex = ex1;
            }

            Assert.IsNotNull(ex);

        }

    }
}
