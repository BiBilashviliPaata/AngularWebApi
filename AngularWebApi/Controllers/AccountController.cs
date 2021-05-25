using DAL.Context;
using DAL.DTO_s;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _dbcontext;

        public AccountController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserModel>> Register(RegisterDTO registerDTO)
        {
            if (await Userexist(registerDTO.Username)) return BadRequest("UserName is Already Taken");

            using var hmac = new HMACSHA512();

            var user = new UserModel
            {
                Name = registerDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };


            _dbcontext.Add(user);
            await _dbcontext.SaveChangesAsync();

            return user;

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login(LoginDTO loginDTO)
        {
            var user = await _dbcontext.User
                .SingleOrDefaultAsync(u => u.Name == loginDTO.Username);
            if(user == null)
            {
                return BadRequest("Invaild Username");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computerhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for(int i = 0; i < computerhash.Length; i++)
            {
                if (computerhash[i] != user.PasswordHash[i]) return Unauthorized("Invaild Password");
            }

            return user;

        }

        private async Task<bool> Userexist(string username)
        {
            return await _dbcontext.User.AnyAsync(u => u.Name == username.ToLower());
        }


    }
}
