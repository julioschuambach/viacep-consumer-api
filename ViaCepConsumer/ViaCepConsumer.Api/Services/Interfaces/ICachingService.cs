namespace ViaCepConsumer.Api.Services.Interfaces
{
    public interface ICachingService
    {
        Task Set(string key, string value);
        Task<string> Get(string key);
    }
}
