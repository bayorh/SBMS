using SBMS.src.Contracts;
using SBMS.src.Dtos;
using SBMS.src.Entitiies;
using System;
using System.Threading.Tasks;

namespace SBMS.src.Servicess
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public SubscriptionService(ISubscriberRepository subscriberRepository, IUnitOfWork unitOfWork, IAuthService authService)
        {
            _subscriberRepository = subscriberRepository;
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<SubscriptionResponseDto> Subscribe(SubscriptionRequestDto request)
        {
            var tokenValidation = await _authService.ValidateTokenAsync(request.service_id, request.token_id);
            if (!tokenValidation.valid)
            {
                return new SubscriptionResponseDto(tokenValidation.reason, false);
            }

            var existingSubscriber = await _subscriberRepository.GetSubsciberBy(request.service_id, request.phone_number);

            if (existingSubscriber != null)
            {
                if (existingSubscriber.IsSubscribed)
                {
                    return new SubscriptionResponseDto("User is already subscribed.", false);
                }
                else
                {
                    existingSubscriber.IsSubscribed = true;
                    existingSubscriber.LastActionAt = DateTime.UtcNow;
                }
            }
            else
            {
                // Get service details (like password hash) from another subscriber record for the same service
                var serviceInfo = await _subscriberRepository.GetSubsciberBy(request.service_id);
                if (serviceInfo == null)
                {
                    return new SubscriptionResponseDto("Service not found.", false);
                }

                existingSubscriber = new Subscriber
                {
                    ServiceId = request.service_id,
                    PhoneNumber = request.phone_number,
                    IsSubscribed = true,
                    LastActionAt = DateTime.UtcNow,
                    PasswordHash = serviceInfo.PasswordHash // Re-using password hash from existing service entry
                };
                await _subscriberRepository.AddSubscriber(existingSubscriber);
            }

            await _unitOfWork.SaveChangesAsync();
            return new SubscriptionResponseDto("Subscription successful.", true, existingSubscriber.Id);
        }

        public async Task<UnSubscriptionResponseDto> UnSubscribe(UnSubscriptionRequestDto request)
        {
            var tokenValidation = await _authService.ValidateTokenAsync(request.service_id, request.token_id);
            if (!tokenValidation.valid)
            {
                return new UnSubscriptionResponseDto(tokenValidation.reason, false);
            }

            var subscriber = await _subscriberRepository.GetSubsciberBy(request.service_id, request.phone_number);

            if (subscriber == null || !subscriber.IsSubscribed)
            {
                return new UnSubscriptionResponseDto("User is not subscribed.", false);
            }

            subscriber.IsSubscribed = false;
            subscriber.LastActionAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
            return new UnSubscriptionResponseDto("Unsubscription successful.", true);
        }

        public async Task<StatusResponseDto> CheckStatus(StatusRequestDto request)
        {
            var tokenValidation = await _authService.ValidateTokenAsync(request.service_id, request.token_id);
            if (!tokenValidation.valid)
            {
                return new StatusResponseDto(tokenValidation.reason, false, null, null);
            }

            var subscriber = await _subscriberRepository.GetSubsciberBy(request.service_id, request.phone_number);

            if (subscriber == null)
            {
                return new StatusResponseDto("User not found for this service.", true, false, null);
            }

            return new StatusResponseDto("Status retrieved successfully.", true, subscriber.IsSubscribed, subscriber.LastActionAt);
        }
    }
}