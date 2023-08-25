using Microsoft.IdentityModel.Tokens;
using PMA_Core.Models;
using PMA_Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Services.Services
{
    public class TokenHandlerService
    {
        private readonly ITokenHandlerRepository _tokenHandler;

        public TokenHandlerService(ITokenHandlerRepository tokenHandler)
        {
            _tokenHandler = tokenHandler;
        }

        public async Task<AuthResponse?> GenerateJwtToken(AuthRequest authRequest)
        {
            // You can delegate the token generation logic to the injected ITokenHandler
            return await _tokenHandler.GenerateJwtToken(authRequest);
        }
    }
}
