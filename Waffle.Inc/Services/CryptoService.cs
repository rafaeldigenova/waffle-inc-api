using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waffle.Inc.Settings;

namespace Waffle.Inc.Services
{
    public class CryptoService
    {
        private TokenConfigurations _tokenConfigurations;

        public CryptoService(TokenConfigurations tokenConfigurations)
        {
            _tokenConfigurations = tokenConfigurations;
        }

        public string Encrypt(string valueToBeEncrypted)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: valueToBeEncrypted,
                salt: Encoding.ASCII.GetBytes(_tokenConfigurations.Salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
