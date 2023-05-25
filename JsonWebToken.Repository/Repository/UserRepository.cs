using JsonWebToken.Core.Entity;
using JsonWebToken.Core.IRepository;
using JsonWebToken.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Repository.Repository
{
    public class UserRepository : GenericRepository<Product>, IUserRepository
    {
        public UserRepository(AppDbContext appContext) : base(appContext)
        {
        }
    }
}
