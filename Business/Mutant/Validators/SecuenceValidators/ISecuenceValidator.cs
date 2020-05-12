using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mutant.Validators
{
    public interface ISecuenceValidator
    {
        Task<int> CountAllSecuence(string[] dna);
    }
}
