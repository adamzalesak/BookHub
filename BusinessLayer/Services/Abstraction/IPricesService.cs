using BusinessLayer.Models.Price;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstraction
{
    public interface IPricesService : IBaseService
    {
        public Task<PriceModel?> GetPrice(int id);
        public Task<List<PriceModel>> GetAllPrices();
        public Task<PriceModel?> CreatePrice(CreatePriceModel priceDto);
        public Task<bool> DeletePrice(int id);
        public Task<List<PriceModel>> FindBookHistoryPrices(int bookId);
    }
}
