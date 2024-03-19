using System.Security.Cryptography;
using System.Text;
using ViaCepConsumer.Api.Services.Interfaces;

namespace ViaCepConsumer.Api.Services
{
    public class EncryptorService : IEncryptorService
    {
        private readonly IConfiguration _configuration;

        public EncryptorService(IConfiguration configuration)
            => _configuration = configuration;

        public string Encrypt(string password)
        {
            byte[] data = Encoding.UTF8.GetBytes(password);

            using MD5 md5 = MD5.Create();
            byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(_configuration.GetSection("HashKey").Value));

            using TripleDES encryption = TripleDES.Create();
            encryption.Key = key;
            encryption.Mode = CipherMode.ECB;
            encryption.Padding = PaddingMode.PKCS7;

            ICryptoTransform cryptoTransform = encryption.CreateEncryptor();
            byte[] result = cryptoTransform.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(result, 0, result.Length);
        }

        /// <summary>
        /// Validate if the given password and the encrypted password are the same.
        /// </summary>
        /// <param name="password">The user's entered password.</param>
        /// <param name="encryptedPassword">The encrypted password from database.</param>
        /// <returns>Returns true if the user's entered password is the same as the encrypted password from the database.</returns>
        public bool Validate(string password, string encryptedPassword)
            => Encrypt(password).Equals(encryptedPassword);
    }
}
