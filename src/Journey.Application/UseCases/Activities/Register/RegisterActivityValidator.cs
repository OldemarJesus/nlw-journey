using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Activities.Register
{
    public class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
    {
        public RegisterActivityValidator()
        {
            RuleFor(request => request.Name).NotNull().WithMessage(ResourceErrorMessages.NAME_NULL_OR_EMPTY);
        }
    }
}
