using System.Collections.Generic;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetUsers();
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ISecurityService _securityService;

        public UserService(ApplicationDbContext applicationDbContext, ISecurityService securityService)
        {
            _applicationDbContext = applicationDbContext;
            _securityService = securityService;
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await _applicationDbContext.ApplicationUser.ToListAsync();
        }
    }
}