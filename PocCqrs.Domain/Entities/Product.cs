

namespace PocCqrs.Domain
{
    using PocCqrs.Domain.Entities;
    public class Product : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
