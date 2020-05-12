using Mutants.Business.Mutant;
using System.Collections.Generic;

namespace Test.Util.Stub.Entitys
{
    public class DnaStub
    {
        private IList<Dna> _data;
        public DnaStub()
        {
            _data = new List<Dna>()
            {
                new Dna()
                {
                    DnaId = 1,
                    DnaString = new string[] { "ATGGGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" }
                },
                new Dna()
                {
                    DnaId = 2,
                    DnaString = new string[] { "ATGCGA", "CAGTTC", "TTGAGT", "AGAAGG", "ACCCTA", "TCACTG" }
                }
            };
        }
        public Dna GetOnPosition(int position)
        {
            return _data[position];
        }
    }
}
