namespace TodoApp.Application.Services.Interfaces
{
    public interface IValidationService
    {
        IValidation<T> Validate<T>(T model);
        void ValidateAndThrow<T>(T model);
        bool IsValid<T>(T model);
    }

    public interface IValidation<T>
    {
        public bool IsValid { get; }
        public string?[] Errors { get; }
    }

    public interface IValidator
    {
        public IValidation<T> Validate<T>(T model);
    }
}
