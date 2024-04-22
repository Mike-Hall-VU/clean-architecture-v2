using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Events
{
    public class SubscrptionDeletedEventHandler(IGymsRepository gymsRepository, IUnitOfWork unitOfWork) : INotificationHandler<SubscriptionDeletedEvent>
    {
        private readonly IGymsRepository _gymsRepository = gymsRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(SubscriptionDeletedEvent notification, CancellationToken cancellationToken)
        {
            var gyms = _gymsRepository.ListBySubscriptionIdAsync(notification.SubscriptionId);

            await _gymsRepository.RemoveRangeAsync(gyms.Result);
            await _unitOfWork.CommitChangesAsync();
        }
    }
}
