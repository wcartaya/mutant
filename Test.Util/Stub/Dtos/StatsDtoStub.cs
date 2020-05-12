using Mutants.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Util.Stub.Dtos
{
    public class StatsDtoStub
    {
        private IList<StatsDto> _data;

        public StatsDtoStub()
        {
            _data = new List<StatsDto>
            {
                new StatsDto()
                {
                    count_human_dna = 100,
                    count_mutant_dna = 40,
                    ratio = 0.4
                },
                new StatsDto()
                {
                    count_human_dna = 8,
                    count_mutant_dna = 10,
                    ratio = 1.25
                }
            };            
        }
        public StatsDto GetOnPosition(int position)
        {
            return _data[position];
        }
    }
}
