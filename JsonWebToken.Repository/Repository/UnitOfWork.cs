using JsonWebToken.Core.IUnitOfWorks;
using JsonWebToken.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebToken.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public void Commit()
        {
            appDbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await appDbContext.SaveChangesAsync();
        }
    }
}
