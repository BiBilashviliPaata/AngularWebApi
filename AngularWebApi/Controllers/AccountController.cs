using App.Interfaces;
using DAL.Context;
using DAL.DTO_s;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        private readonly ITokenService _tokenservice;

        public AccountController(AppDbContext dbcontext , ITokenService tokenService)
        {
            _dbcontext = dbcontext;
            _tokenservice = tokenService;
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await Userexist(registerDTO.Username)) return BadRequest("UserName is Already Taken");

            using var hmac = new HMACSHA512();

            var user = new UserModel
            {
                Username = registerDTO.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };


            _dbcontext.Add(user);
            await _dbcontext.SaveChangesAsync();

            return new UserDTO
            {
                Username = user.Username,
                Token = await _tokenservice.CreateToken(user)
            };

        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _dbcontext.User
                .SingleOrDefaultAsync(u => u.Username == loginDTO.Username.ToLower());
            if(user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computerhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for(int i = 0; i < computerhash.Length; i++)
            {
                if (computerhash[i] != user.PasswordHash[i]) return Unauthorized("Invaild Password");
            }

            return new UserDTO
            {
                Username = user.Username,
                Token = await _tokenservice.CreateToken(user)
            };

        }

        private async Task<bool> Userexist(string username)
        {
            return await _dbcontext.User.AnyAsync(u => u.Username == username.ToLower());
        }


    }
}
