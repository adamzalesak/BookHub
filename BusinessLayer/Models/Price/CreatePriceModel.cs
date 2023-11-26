using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Price
{
    public class CreatePriceModel
    {
        public decimal BookPrice { get; set; }
        public DateTime ValidFrom { get; set; }
        public int BookId { get; set; }
    }
}
