using App.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DAL.Context;
using DAL.DTO_s;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserRepository(AppDbContext dbContext , IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            return await _dbContext.User
                  .Include(p => p.Photos)
                  .ToListAsync();
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _dbContext.User.FindAsync(id);
        }

        public async Task<UserModel> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.User
                  .Include(p=>p.Photos)
                  .SingleOrDefaultAsync(x => x.Name == username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void Update(UserModel user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
        }


        public async Task<MemberDTO> GetMemberAsync(string username)
        {
            return await _dbContext.User
                .Where(x => x.Name == username)
                .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
        {
            return await _dbContext.User
                .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
