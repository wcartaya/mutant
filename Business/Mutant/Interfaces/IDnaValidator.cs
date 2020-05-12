using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mutants.Business.Mutant
{
    public interface IDnaValidator
    {
        Task<bool> IsMutant(string[] dna);
    }
}
