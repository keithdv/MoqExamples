using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{


    public enum enumRace
    {
        Kryptonian,
        Human,
        Mutant
    }

    public interface ISuperHero
    {
        enumRace Race { get; }
        bool HasKryptonite { get; }
        bool HasIronSuit { get; }


    }

    public abstract class SuperHero: ISuperHero
    {

        protected SuperHero(enumRace r)
        {
            this.Race = r;
        }

        public enumRace Race
        {
            get; private set;
        }

        public bool HasKryptonite { get; private set; }

        public bool HasIronSuit { get; private set; }

        
    }

    public class Batman : SuperHero
    {

        public Batman(Robin robin, bool hasKryptonite, bool hasIronSuit) : base(enumRace.Human)
        {
            this.Robin = robin;
        }

        public Robin Robin { get; private set; }

    }

    public class Robin : SuperHero
    {
        public Robin() : base(enumRace.Human) { }
    }

    public class Superman : SuperHero
    {

        public Superman() : base(enumRace.Kryptonian) { }


    }

    public class Spiderman : SuperHero
    {

        public Spiderman() : base(enumRace.Mutant) { }
    }

}
