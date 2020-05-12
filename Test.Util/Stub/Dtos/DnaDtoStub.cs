using Mutants.Business.Dtos;
using System.Collections.Generic;

namespace Test.Util.Stub.Dtos
{
    public class DnaDtoStub
    {
        private IList<DnaDto> _data;
        
        public  DnaDtoStub()
        {
            _data = new List<DnaDto>()
            {
                new DnaDto(new string[]
                    {
                        "ATGGGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"
                    }),
                new DnaDto(new string[]
                    {
                        "ATGCGA","CAGTTC","TTGAGT","AGAAGG","ACCCTA","TCACTG"
                    })
            };
        }
        public DnaDto GetOnPosition(int position)
        {
            return _data[position];
        }
    }
}
