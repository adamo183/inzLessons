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
using System.Security.Cryptography;

namespace inzLessons.Server.Services
{
    public interface ILoginServices
    {
        string GenerateJwtToken(Users user, string role);
        LoginResponse Authenticate(LoginRequest model);
        Users GetById(int id);
        public string GenerateSalt(int nSalt);
        public string HashPassword(string password, string salt, int nIterations, int nHash);
        public string GetUserHash(string userName);
        public void InsertMembership(Membership membership);
        public void InsertUser(Users user);
        public bool CheckUsername(string username);
    }

    public class LoginServices : ILoginServices
    {
        UnitOfWork _unitOfWork = new UnitOfWork();

        public string GetUserHash(string login)
        {
            var hashToRet = _unitOfWork.MembershipRepository.Get(x => x.Login == login).FirstOrDefault();
            if (hashToRet != null)
                return hashToRet.PasswordSalt;

            return null;
        }

        public bool CheckUsername(string username)
        {
            var check = _unitOfWork.UsersRepository.Get(x => x.Username == username).FirstOrDefault();
            if (check == null)
                return false;

            return true;
        }

        public void InsertUser(Users user)
        {
            _unitOfWork.UsersRepository.Insert(user);
            _unitOfWork.Save();
        }

        public void InsertMembership(Membership membership)
        {
            _unitOfWork.MembershipRepository.Insert(membership);
            _unitOfWork.Save();
        }

        public string HashPassword(string password, string salt, int nIterations, int nHash)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations))
            {
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
            }
        }

        public string GenerateSalt(int nSalt)
        {
            var saltBytes = new byte[nSalt];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }

        public LoginResponse Authenticate(LoginRequest model)
        {
            var membership = _unitOfWork.MembershipRepository.Get(x => x.Login == model.Username && model.Password == x.Password).FirstOrDefault();

            if (membership == null)
                return null;

            var user = _unitOfWork.UsersRepository.Get(x => x.Id == membership.Id, includeProperties: "Role").FirstOrDefault();

            string role = user.Role.Name;
            var token = GenerateJwtToken(user, role);

            return new LoginResponse() { Id = user.Id, FirstName = user.Firstname, LastName = user.Lastname, Username = user.Username, Token = token, Role = (Shared.Role.RoleEnum)user.RoleId.Value };
        }

        public string GenerateJwtToken(Users user, string role)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("asdasjdokajfudinvdikosejwrk2i34r209");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim(ClaimTypes.Role, role) }),
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
