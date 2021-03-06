﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arkitektum.Orden.Data;
using Arkitektum.Orden.Models;
using Microsoft.EntityFrameworkCore;

namespace Arkitektum.Orden.Services
{
    public interface ISuperUsersService
    {
        Task<SuperUser> Get(int id);

        Task<List<SuperUser>> GetSuperUsersForOrganization(int organizationId);

        Task<SuperUser> AddSuperUserToOrganization(SuperUser superUser);

        Task<SuperUser> UpdateSuperUser(SuperUser superUser);

        Task DeleteSuperUser(int superUserId);
        Task<List<SuperUser>> GetSuperUsersForApplication(int applicationId);
        Task AddSuperUserToApplication(int applicationId, int superUserId);
        Task RemoveSuperUserFromApplication(int applicationId, int superUserId);
    }

    public class SuperUsersService : ISuperUsersService
    {
        private readonly ApplicationDbContext _context;

        public SuperUsersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SuperUser>> GetSuperUsersForOrganization(int id)
        {
            return await _context.SuperUser.Where(s => s.OrganizationId == id).ToListAsync();
        }

        public async Task<SuperUser> AddSuperUserToOrganization(SuperUser superUser)
        {
            _context.SuperUser.Add(superUser);
            await _context.SaveChangesAsync();
            return superUser;
        }

        public async Task<SuperUser> UpdateSuperUser(SuperUser superUser)
        {
            var originalSuperUser = await Get(superUser.Id);

            _context.Entry(originalSuperUser).CurrentValues.SetValues(superUser);

            await _context.SaveChangesAsync();

            return originalSuperUser;
        }

        public Task<SuperUser> Get(int id)
        {
            return _context.SuperUser.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task DeleteSuperUser(int superUserId)
        {
            var superUser = await Get(superUserId);
            _context.SuperUser.Remove(superUser);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SuperUser>> GetSuperUsersForApplication(int applicationId)
        {
            List<SuperUser> result = await _context.Application
                .Where(a => a.Id == applicationId)
                .SelectMany(a => a.SuperUsers)
                .Select(s => s.SuperUser)
                .ToListAsync();

            return result;
        }

        public async Task AddSuperUserToApplication(int applicationId, int superUserId)
        {
            var application = await _context.Application
                .Include(a => a.SuperUsers)
                .SingleAsync(a => a.Id == applicationId);

            var superUser = await _context.SuperUser.SingleAsync(s => s.Id == superUserId);
            
            application.AddSuperUser(superUser);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSuperUserFromApplication(int applicationId, int superUserId)
        {
            var application = await _context.Application
                .Include(a => a.SuperUsers)
                .SingleAsync(a => a.Id == applicationId);

            application.RemoveSuperUser(superUserId);
            await _context.SaveChangesAsync();
        }
    }
}