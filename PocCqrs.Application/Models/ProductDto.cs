using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocCqrs.Application.Models
{
    public class ProductDto
    {
        [Required]
        [StringLength(5,MinimumLength = 3)]
        public string Code { get; set; }
    }
}
