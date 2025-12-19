using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        // private, só essa classe vai ver esse DbContext
        // readonly, porque só o construtor pode mudar algo desse dbcontext
        private readonly MyRecipeBookDbContext _dbContext;

        public UserRepository(MyRecipeBookDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(User user) => await _dbContext.Users.AddAsync(user);
        
        public async Task<bool> ExistActiveUserWithEmail(string email) =>  await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.IsActive);
    }
}