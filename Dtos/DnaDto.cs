using Mutants.Business.Dtos.Validators;
using Mutants.Common.Constants;

namespace Mutants.Business.Dtos
{
    public class DnaDto
    {
        private string[] _dna;
        public DnaDto()
        {            
        }
        public DnaDto(string[] dna)
        {
            _dna=dna;
        }

        [MatrixLen(MutantConstants.MatrixLen)]
        public string[] dna
        {
            get
            {
                return _dna;
            }
            set
            {
                _dna = value;
            }
        }
    }
}
