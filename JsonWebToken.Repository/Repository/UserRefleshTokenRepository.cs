using JsonWebToken.Core.Entity;
using JsonWebToken.Core.IRepository;
using JsonWebToken.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Repository.Repository
{
    public class UserRefleshTokenRepository : GenericRepository<UserRefleshToken>,IUserRefleshTokenRepository
    {
        public UserRefleshTokenRepository(AppDbContext appContext) : base(appContext)
        {
        }
    }
}
