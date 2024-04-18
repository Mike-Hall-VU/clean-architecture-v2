using ErrorOr;
using GymManagement.Domain.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.CreateGym
{
    public class CreateGymCommandBehavior : IPipelineBehavior<CreateGymCommand, ErrorOr<Gym>>
    {
        public async Task<ErrorOr<Gym>> Handle(CreateGymCommand request, RequestHandlerDelegate<ErrorOr<Gym>> next, CancellationToken cancellationToken)
        {
            var validator = new CreateGymCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            return await next();    
        }
    }
}
