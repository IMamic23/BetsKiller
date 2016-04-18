using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.Helper.Operations
{
    public class RandomElement
    {
        public static int FromList(int listCount)
        {
            return new Random().Next(0, listCount);
        }

        public static List<int> FromList(int listCount, int numbersCount)
        {
            List<int> generatedNumbers = new List<int>();

            while (generatedNumbers.Count != numbersCount)
            {
                int randomNumber = new Random().Next(0, listCount);

                if (!generatedNumbers.Contains(randomNumber))
                    generatedNumbers.Add(randomNumber);
            }

            return generatedNumbers;
        }
    }
}
