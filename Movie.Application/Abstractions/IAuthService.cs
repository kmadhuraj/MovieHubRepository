using Movie.Application.DTO;
using Movie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Application.Abstractions
{
    public interface IAuthService
    {
        public string RegisterUser(User user);

        public string RegisterOrUpdateAdmin(User user);
        public string PasswordHashing(string password,out byte[] salt);

        public bool VerifyPassword(string enteredPassword, string storedPassword,string salt);
        public Task<User> Authenticate(User user);

        public  Task<string> JwtToken (User user);


    }

}