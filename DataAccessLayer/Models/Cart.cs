using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Cart : BaseEntity
    {
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public Order? Order { get; set; }
    }
}
