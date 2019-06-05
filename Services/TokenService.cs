using Axioma.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Axioma.Services
{
	public class TokenService
	{
		private IConfiguration _configuration;
		private UserService userService;
		public TokenService(UserService userService, IConfiguration configuration) {
			this.userService = userService;
			_configuration = configuration;
		}
		public string getToken(string username, string password)
		{
			User query = userService.Get().AsQueryable().Where(usr => usr.Username == username && usr.Password == password).SingleOrDefault();

			if (query == null)
				return null;

			byte[] signingKey = Convert.FromBase64String(_configuration["Jwt:SigningSecret"]);

			var expiryDuration = int.Parse(_configuration["Jwt:ExpiryDuration"]);
			SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = null,              // Not required as no third-party is involved
				Audience = null,            // Not required as no third-party is involved
				IssuedAt = DateTime.UtcNow,
				NotBefore = DateTime.UtcNow,
				Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
				Subject = new ClaimsIdentity(new List<Claim> {
						new Claim("userid", query.Id.ToString()),
						new Claim("role", query.Role)
				}),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
			};
			JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
			JwtSecurityToken jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
			string token = jwtTokenHandler.WriteToken(jwtToken);
			return token;
		}
	}
}
