namespace Lister.Services.Models
{
    public class ExecutionResult
    {
        public string? ErrorMessage { get; protected set; }
        public bool IsSuccessful { get; protected set; }

        public static ExecutionResult Successful()
        {
            return new ExecutionResult
            {
                IsSuccessful = true
            };
        }

        public static ExecutionResult Failed(string errorMessage)
        {
            return new ExecutionResult
            {
                ErrorMessage = errorMessage,
                IsSuccessful = false
            };
        }
    }

    public class ExecutionResult<T> : ExecutionResult
    {
        public T? Data { get; protected set; }

        public static ExecutionResult<T> Successful(T data)
        {
            return new ExecutionResult<T>
            {
                Data = data,
                IsSuccessful = true,
            };
        }

        public new static ExecutionResult<T> Failed(string errorMessage)
        {
            return new ExecutionResult<T>
            {
                ErrorMessage = errorMessage,
                IsSuccessful = false
            };
        }
    }
}