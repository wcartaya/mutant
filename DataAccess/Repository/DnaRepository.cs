using Mutants.Business.Mutant;
using Mutants.DataAccess.Interfaces;
using System;
using System.Collections.Generic;

namespace Mutants.DataAccess.Repository
{
    public class DnaRepository : IDataRepository<Dna>
    {
        readonly MutantDBContext _mutantDBContext;

        public DnaRepository(MutantDBContext mutantDBContext)
        {
            _mutantDBContext = mutantDBContext;
        }

        public void Add(Dna entity)
        {
            _mutantDBContext.Dnas.Add(entity);
            _mutantDBContext.SaveChanges();
        }

        public void Delete(Dna entity)
        {
            throw new NotImplementedException();
        }

        public Dna Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dna> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Dna dbEntity, Dna entity)
        {
            throw new NotImplementedException();
        }
    }
}
