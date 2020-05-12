using Mutants.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Mutants.Business.Dtos.Validators
{
    public class MatrixLen : ValidationAttribute
    {
        private readonly int _len;
        public MatrixLen(int Len)
        {
            _len = Len;
        }
        protected override ValidationResult IsValid(object value,
                        ValidationContext validationContext)
        {
            if (value!=null)
            {
                string[] data = (string[])value;
                if (data.Length!=_len)
                {
                    return new ValidationResult(MutantConstants.LenError);
                }
                foreach( string dnaString in data)
                {
                    if (dnaString.Length!= _len)
                    {
                        return new ValidationResult(MutantConstants.LenError);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
