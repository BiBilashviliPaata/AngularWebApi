using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserModel user);
    }
}
