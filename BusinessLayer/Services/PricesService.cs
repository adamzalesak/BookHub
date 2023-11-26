using BusinessLayer.Mappers;
using BusinessLayer.Models.Price;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class PricesService : IPricesService
    {
        private readonly BookHubBdContext _dbContext;

        public PricesService(BookHubBdContext dbContext)
        {
            _dbContext = dbContext;
        }                

        public async Task<PriceModel?> CreatePrice(CreatePriceModel priceDto)
        {
            var book = await _dbContext.Books.FindAsync(priceDto.BookId);
            if (book == null)
            {
                return null;
            }
            var price = priceDto.MapCreatePriceModelToPrice();
            price.Book = book;
            var newPrice = await _dbContext.Prices.AddAsync(price);
            await SaveAsync();
            return newPrice.Entity.MapPriceToPriceModel();
        }

        public async Task<bool> DeletePrice(int id)
        {
            var price = await _dbContext.Prices.FindAsync(id);
            if (price == null)
            {
                return false;
            }

            _dbContext.Prices.Remove(price);
            await SaveAsync();
            return true;
        }

        public async Task<List<PriceModel>> FindBookHistoryPrices(int bookId)
        {
            var prices = await _dbContext.Prices
            .Include(p => p.Book)
            .Where(p => p.BookId == bookId).ToListAsync();
            if (prices == null || prices.Count == 0)
            {
                return new List<PriceModel>();
            }

            await SaveAsync();
            return prices.MapPriceListToPriceModelList();
        }

        public async Task<List<PriceModel>> GetAllPrices()
        {
            var prices = await _dbContext.Prices
            .Include(p => p.Book)
            .ToListAsync();
            return prices.MapPriceListToPriceModelList();
        }

        public async Task<PriceModel?> GetPrice(int id)
        {
            Price? price = await GetPriceObject(id);
            if (price == null)
            {
                return null;
            }
            return price.MapPriceToPriceModel();
        }

        private async Task<Price?> GetPriceObject(int id)
        {
            return await _dbContext.Prices
            .Include(p => p.Book)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}