using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Domain.Interfaces
{
    public interface IJobRepository
    {
        public Task<string> GetJobIdByCartId(Guid id);
    }
}
