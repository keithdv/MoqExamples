using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{

    public interface IUniverse
    {
        ISuperHero WhoWins(ISuperHero a, ISuperHero b);
    }

    public abstract class Universe : IUniverse
    {

        public abstract ISuperHero WhoWins(ISuperHero a, ISuperHero b);

    }


    public class Marvel : Universe
    {


        public override ISuperHero WhoWins(ISuperHero a, ISuperHero b)
        {
            if (a.Race == b.Race)
            {
                throw new Exception("The races do not fight their own kind in the Marvel Universe!");
            }

            if (a.Race == enumRace.Human)
            {

                if (a.HasIronSuit && a.HasKryptonite)
                {
                    return a;
                }
                if (a.HasIronSuit && b.Race == enumRace.Mutant)
                {
                    return a;
                }
                if (a.HasKryptonite && b.Race == enumRace.Kryptonian)
                {
                    return a;
                }

                return b;

            }

            if (a.Race == enumRace.Mutant && b.Race == enumRace.Kryptonian)
            {
                return b;
            }

            if (a.Race == enumRace.Kryptonian && b.Race == enumRace.Mutant)
            {
                return a;
            }


            return WhoWins(b, a);
        }

    }

}
