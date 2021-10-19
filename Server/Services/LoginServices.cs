using inzLessons.Shared.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DAL.UnitOfWork;
using inzLessons.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using inzLessons.Common.Models;

namespace inzLessons.Server.Services
{
    public interface ILoginServices
    {
        string GenerateJwtToken(Users user);
        LoginResponse Authenticate(LoginRequest model);
        Users GetById(int id);
    }

    public class LoginServices : ILoginServices
    {
        UnitOfWork _unitOfWork = new UnitOfWork();

        public LoginResponse Authenticate(LoginRequest model)
        {
            var membership = _unitOfWork.MembershipRepository.Get(x => x.Login == model.Username && model.Password == x.Password).FirstOrDefault();

            if (membership == null) 
                return null;

            var user = _unitOfWork.UsersRepository.Get(x => x.MembershipId == membership.Id).FirstOrDefault();
            var token = GenerateJwtToken(user);

            return new LoginResponse() { Id = user.Id, FirstName = user.Firstname, LastName = user.Lastname, Username = user.Username, Token = token } ;
        }

        public string GenerateJwtToken(Users user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("aaaaaululullululu");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Users GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
