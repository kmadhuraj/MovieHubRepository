using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Movie.Application.Abstractions;
using Movie.Application.DTO;
using Movie.Domain.Entities;
using Movie.Infrastucture.Context;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Infrastucture.AuthService
{
    public class AuthService : IAuthService
    {

        private IConfiguration _config;
        private ApplicationDbContext _context;
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        public AuthService(IConfiguration config,ApplicationDbContext context)
        {
            _config = config;
            _context= context;
        }

     
        public string PasswordHashing(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hashPassword = Rfc2898DeriveBytes.Pbkdf2(password,salt,iterations, hashAlgorithm, keySize);
            return Convert.ToHexString(hashPassword);
        }

        public string RegisterUser(User user)
        {
            if (user == null) throw new ArgumentNullException("user cannot be empty");
            user.Role = false;
            user.PasswordHash = PasswordHashing(user.PasswordHash, out byte[] salt);
            user.Salt = Convert.ToHexString(salt); 
            try
            {
                _context.Add(user);             
                _context.SaveChanges();              
                return "Registration Successfull";   
            }
            catch (Exception ex)                                               
            {
                return ("An error occured while Registering the User" + ex.Message);
            }

        }

        public string RegisterOrUpdateAdmin(User adminDetails)
        {
            var existingAdmin = _context.Users.FirstOrDefault(x => x.Role == true);
            if (existingAdmin != null)
            {
                existingAdmin.Username= adminDetails.Username;
                existingAdmin.Email= adminDetails.Email;
                existingAdmin.PasswordHash= PasswordHashing(adminDetails.PasswordHash ,out byte[] salt);
                _context.SaveChanges();
                return ("Admin credentials updated successfully!");
            }
            else
            {
                // Register a new admin
                adminDetails.Role = true; // Ensure Role is set to Admin
                //new salt is created
                var salt = RandomNumberGenerator.GetBytes(keySize);
                adminDetails.Salt = Convert.ToHexString(salt);// Generate Salt
                adminDetails.PasswordHash = PasswordHashing(adminDetails.PasswordHash, out byte[] Salt);
                _context.Users.Add(adminDetails);
                _context.SaveChanges();
                return ("Admin registered successfully!");
            }
        }
        async Task<string>  IAuthService.JwtToken(User user)
        {
            var securutyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securutyKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                //new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim("Username",user.Username.ToString()),
                //new Claim("Email",user.Email.ToString()),
                //ading new claim. if Role is true Role will be admin else user
                new Claim(ClaimTypes.Role,user.Role?"Admin":"User")
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
               claims,
               expires: DateTime.Now.AddMinutes(1),
               signingCredentials: credentials);
            var finalToken = new JwtSecurityTokenHandler().WriteToken(token);
            return finalToken;
        }

        public bool VerifyPassword(string enteredPassword, string storedPassword, string salt)
        {
            var salt_decrypt=Convert.FromHexString(salt);
            var hashPasswordToCompare = Rfc2898DeriveBytes.Pbkdf2(enteredPassword, salt_decrypt, iterations, hashAlgorithm, keySize);
            var hashPasswordToCompareToHex = Convert.ToHexString(hashPasswordToCompare);
            return CryptographicOperations.Equals(hashPasswordToCompareToHex, storedPassword);
        }
        async Task<User> IAuthService.Authenticate(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                throw new ArgumentException("Email and password cannot be empty.");
            }
            if (user != null)
            {
                var loginUser = _context.Users.FirstOrDefault(x => x.Email == user.Email);
                if (loginUser != null)
                {
                    var authUser = VerifyPassword(user.PasswordHash, loginUser.PasswordHash,loginUser.Salt);
                    return loginUser;
                }
                return null;
            }
            throw new ArgumentNullException(nameof(user), "User object cannot be null.");

        }

        
    }
}
