using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocCqrs.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt
        {
            get
            {
                return DateTime.Now;
            }
            set
            {
               this.CreatedAt = value;
            }
        }
        public DateTime ModifiedAt
        {
            get
            {
                return DateTime.Now;
            }
            set
            {
                this.CreatedAt = value;
            }
        }
        public string Audit { get; set; }

    }
}
