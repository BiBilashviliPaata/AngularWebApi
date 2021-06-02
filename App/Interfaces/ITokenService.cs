using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(UserModel user);
    }
}
