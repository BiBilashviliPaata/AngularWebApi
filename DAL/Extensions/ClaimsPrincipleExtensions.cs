using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace DAL.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
             return user.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
