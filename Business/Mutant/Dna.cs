using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mutants.Business.Mutant
{
    public class Dna
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DnaId { get; set; }
        public string[] DnaString { get; set; }
        public bool IsMutant { get; set; }
    }
}
