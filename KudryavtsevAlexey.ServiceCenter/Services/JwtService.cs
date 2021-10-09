using KudryavtsevAlexey.ServiceCenter.Data;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace KudryavtsevAlexey.ServiceCenter.Services
{
	public class JwtService : IJwtService
	{
		private readonly SymmetricSecurityKey _key;

		public JwtService()
		{
			var secret = "secret_for_generate_jwt_token";
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
		}

		public string CreateToken(ApplicationUser user)
		{
			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
			};

			var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

			var tokenDescriptor = new SecurityTokenDescriptor() 
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddMinutes(30),
				SigningCredentials = credentials
			};

			var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
