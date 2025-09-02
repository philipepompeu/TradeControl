using TradeControl.Domain.Model;

namespace TradeControl.Domain.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TradeControlDbContext context) : base(context) { }
        
    }

}
