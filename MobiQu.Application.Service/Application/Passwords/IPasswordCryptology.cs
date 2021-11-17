using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Application.Application.Password
{
    public interface IPasswordCryptology
    {
        public string HashPassword(string password);

        public bool VerifiedPassword(string decodePassword, string encodePassword);
    }
}
