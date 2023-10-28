using AutoMapper;
using DataAccessLayer.Models;
using WebAPI.Models;
using WebAPI.Models.Ordering;

namespace WebAPI.Config
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cart, CartModel>()
                    .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.Id))
                    .ForMember(dest => dest.BookIds, opt => opt.MapFrom(src => src.Books.Select(b => b.Id)));
                cfg.CreateMap<Order, OrderModel>();
                cfg.CreateMap<Price, PriceModel>();

                cfg.CreateMap<OrderModel, Order>();
                cfg.CreateMap<PriceModel, Price>();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
