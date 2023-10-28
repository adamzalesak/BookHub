using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Price : BaseEntity
    {
        public decimal BookPrice { get; set; }
        public DateTime ValidFrom { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
