using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mutant.Validators
{
    public class SecuenceValidator
    {
        private readonly ISecuenceValidator _secuenceValidator;

        public SecuenceValidator(ISecuenceValidator secuenceValidator)
        {
            _secuenceValidator = secuenceValidator;
        }
        public Task<int> CountAllSecuence(string[] dna)
        {
            return _secuenceValidator.CountAllSecuence(dna);
        }
    }
}
