using Microsoft.Extensions.Options;
using SBMS.src.Contracts;
using SBMS.src.Dtos;
using SBMS.src.Entitiies;
using System;
using System.Threading.Tasks;

namespace SBMS.src.Servicess
{
    public class AuthService : IAuthService
    {
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenOptions _tokenOptions;

        public AuthService(
            ISubscriberRepository subscriberRepository,
            IServiceRepository serviceRepository,
            IUnitOfWork unitOfWork,
            IOptions<TokenOptions> tokenOptions)
        {
            _subscriberRepository = subscriberRepository;
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
            _tokenOptions = tokenOptions.Value;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
        {
            // Due to the data model, we fetch a subscriber to verify service credentials
            var subscriber = await _subscriberRepository.GetSubsciberBy(request.service_id);
            if (subscriber == null)
            {
                return new AuthResponseDto("Service not found.", false);
            }

            // NOTE: This assumes plain text passwords. In a real app, use a hashing library like BCrypt.
            if (subscriber.PasswordHash != request.password)
            {
                return new AuthResponseDto("Invalid credentials.", false);
            }

            // Check for existing valid token using the correct ServiceId
            var existingToken = await _serviceRepository.GetServiceTokenBy(subscriber.ServiceId);
            if (existingToken != null)
            {
                return new AuthResponseDto("Login successful.", true, existingToken.Id, existingToken.ExpiresAt);
            }

            // Create new token if none exists, linking to the correct ServiceId
            var newToken = new ServiceToken
            {
                ServiceId = subscriber.ServiceId,
                IssuedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(_tokenOptions.ExpiryHours),
                Id = Guid.NewGuid().ToString() // The token itself
            };

            await _serviceRepository.CreateServiceToken(newToken);
            await _unitOfWork.SaveChangesAsync();

            return new AuthResponseDto("Login successful.", true, newToken.Id, newToken.ExpiresAt);
        }

        public async Task<(bool valid, string reason)> ValidateTokenAsync(string serviceId, string tokenId)
        {
            var token = await _serviceRepository.GetServiceTokenBy(serviceId);
            if (token == null)
            {
                return (false, "Token expired or invalid.");
            }

            if (token.Id != tokenId)
            {
                return (false, "Wrong Token ID.");
            }

            if (token.ExpiresAt <= DateTime.UtcNow)
            {
                return (false, "Token ID expired.");
            }

            return (true, "Token is valid.");
        }
    }
}