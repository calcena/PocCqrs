namespace PocCqrs.Domain.Interfaces
{
    using PocCqrs.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IProductRepository: IGenericRepository<Product>
    {
    }
}
