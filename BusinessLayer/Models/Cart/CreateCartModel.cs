using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Cart
{
    public class CreateCartModel
    {
        public List<int> BookIds { get; set; }
        public int? OrderId { get; set; }
    }
}
