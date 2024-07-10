using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Register
{
    public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
    {
        public RegisterTripValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_NULL_OR_EMPTY);
            RuleFor(request => request.StartDate.Date).GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage(ResourceErrorMessages.START_DATE_IN_PAST);
            RuleFor(request => request).Must(request => request.EndDate.Date >= request.StartDate.Date).WithMessage(ResourceErrorMessages.END_DATE_BEFORE_START_DATE);
        }
    }
}
