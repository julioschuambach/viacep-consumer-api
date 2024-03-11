namespace ViaCepConsumer.Api.ViewModels
{
    public class ResultViewModel<T>
    {
        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = new();
        public string InformationMessage { get; private set; } = string.Empty;

        public ResultViewModel(T data, string informationMessage)
        {
            Data = data;
            InformationMessage = informationMessage;
        }

        public ResultViewModel(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public ResultViewModel(T data)
            => Data = data;

        public ResultViewModel(List<string> errors)
            => Errors = errors;

        public ResultViewModel(string error)
            => Errors.Add(error);
    }
}
