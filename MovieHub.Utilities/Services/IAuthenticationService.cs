using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieHub.Utilities.Services
{
    public interface IAuthenticationService
    {
        string IssueJwtToken(string id, string firstName, string lastName);

        Task<ClaimsIdentity> ValidateTokenAsync(HttpContext context, string token);

        void AddToHeaders(string token);
    }
}
