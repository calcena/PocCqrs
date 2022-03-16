using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocCqrs.Domain.Interfaces
{
    public interface IGenericRepository<T> where T: class
    {
        DbConnection GetDbconnection();
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
    }
}
