using BusinessLayer.Models.Price;
using DataAccessLayer.Models;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappers
{
    [Mapper]
    public static partial class PriceMapper
    {
        public static partial PriceModel MapPriceToPriceModel(this Price price);
        public static partial List<PriceModel> MapPriceListToPriceModelList(this List<Price> price);
        public static partial Price MapCreatePriceModelToPrice(this CreatePriceModel createPriceModel);
    }
}
