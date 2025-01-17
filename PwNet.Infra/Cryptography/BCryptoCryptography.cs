﻿using PwNet.Application.Interfaces.Infra;

namespace PwNet.Infra.Cryptography
{
    public class BCryptoCryptography : IPasswordCryptography
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
