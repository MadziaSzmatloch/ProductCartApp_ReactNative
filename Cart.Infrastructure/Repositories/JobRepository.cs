using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Domain.Interfaces;
using Dapper;
using Npgsql;

namespace Cart.Infrastructure.Repositories
{
    internal class JobRepository(string connectionString) : IJobRepository
    {
        private string _connectionString = connectionString;
        public async Task<string> GetJobIdByCartId(Guid id)
        {
            string argumentId = id.ToString();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // SQL query to find JobId by searching for argumentId in the Arguments column
                var query = $@"
                SELECT j.Id
                FROM Hangfire.Job j
                JOIN Hangfire.State s ON s.JobId = j.Id
                WHERE j.Arguments::text LIKE '%{argumentId}%'
                  AND s.Name != 'Deleted'
                ORDER BY s.Id DESC
                LIMIT 1";

                var jobId = await connection.QuerySingleOrDefaultAsync<string>(query);

                return jobId;
            }
        }
    }
}
