using App.Interface;
using AutoMapper;
using DAL.Context;
using DAL.DTO_s;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWebApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository , IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
        {
            var user = await _userRepository.GetMembersAsync();

            return Ok(user);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> GetUserByIdAsync(string username)
        {

            return await _userRepository.GetMemberAsync(username);

        }
    }
}
