namespace ViaCepConsumer.Api.Services.Interfaces
{
    public interface IEncryptorService
    {
        string Encrypt(string password);
        bool Validate(string password, string encryptedPassword);
    }
}
