using Dapper;
using Microsoft.Extensions.Configuration;
using Mutants.Business.Dtos;
using Mutants.DataAccess.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Mutants.DataAccess.Repository
{
    public class StatsRepository : IStatsRepository
    {
        private readonly IConfiguration Configuration;

        public StatsRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<StatsDto> GetStats()
        {
            StatsDto statsDto = new StatsDto();
            using (IDbConnection con = new SqlConnection(Configuration["MyDbConnection"]))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                statsDto = con.Query<StatsDto>("GetStats", commandType: CommandType.StoredProcedure).FirstOrDefault();
            };
            return statsDto;
        }
    }
}
