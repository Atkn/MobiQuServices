using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Application.Application.Password
{
    public class PasswordCryptology : IPasswordCryptology
    {
        public virtual string HashPassword(string password)
        {
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashPassword;
        }

        public virtual bool VerifiedPassword(string decodePassword, string encodePassword)
        {
            return BCrypt.Net.BCrypt.Verify(decodePassword, encodePassword);
        }

         
    }
}
