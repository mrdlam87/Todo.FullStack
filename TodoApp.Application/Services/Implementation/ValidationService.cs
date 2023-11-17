using System.ComponentModel.DataAnnotations;
using TodoApp.Application.Common.Exceptions;
using TodoApp.Application.Services.Interfaces;

namespace TodoApp.Application.Services.Implementation
{
    public class ValidationService : IValidationService
    {
        private readonly IValidator _validator;

        public ValidationService(IValidator validator)
        {
            _validator = validator;
        }

        public IValidation<T> Validate<T>(T model)
        {
            IValidation<T> validation = _validator.Validate(model);

            return validation;
        }

        public void ValidateAndThrow<T>(T model)
        {
            IValidation<T> validation = _validator.Validate(model);

            List<string> errors = new();

            if (!validation.IsValid)
            {
                errors.AddRange(validation.Errors);
            }

            if (errors.Count > 0)
            {
                throw ServiceException.Invalid(errors.ToArray());
            }
        }

        public bool IsValid<T>(T model)
        {
            IValidation<T> validation = _validator.Validate(model);

            return validation.IsValid;
        }
    }

    public class Validation<T> : IValidation<T>
    {
        public bool IsValid { get; }
        public string?[] Errors { get; }

        public Validation(T model)
        {
            ValidationContext validationContext = new(model);
            List<ValidationResult> validationResults = new();
            IsValid = Validator.TryValidateObject(model, validationContext, validationResults, true);
            Errors = validationResults.Select(vr => vr.ErrorMessage).ToArray();
        }
    }

    public class CustomValidator : IValidator
    {
        public IValidation<T> Validate<T>(T model)
        {
            return new Validation<T>(model);
        }
    }
}
