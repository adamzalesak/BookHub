using DataAccessLayer.Models;
using Riok.Mapperly.Abstractions;
using BusinessLayer.Models.User;

namespace BusinessLayer.Mappers;

[Mapper]
public static partial class UserMapper
{
    public static partial User MapCreateUserModelToUser(this CreateUserModel model);
    
    [MapProperty(nameof(User.Orders), nameof(UserModel.OrderIds))]
    public static partial UserModel MapUserToUserModel(this User user);

    private static List<int> OrdersToOrderIds(ICollection<Order> orders)
    {
        return orders.Select(o => o.Id).ToList();
    }
}