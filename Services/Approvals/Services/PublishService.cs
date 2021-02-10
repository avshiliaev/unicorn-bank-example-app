using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Approvals.Services
{
    public class PublishService : IPublishService<AAccountState>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public PublishService(
            IPublishEndpoint publishEndpoint,
            IMapper mapper
        )
        {
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }
        
        public async Task<AAccountState> Publish<TEvent>(AAccountState entityState) where TEvent: class, IEvent
        {
            var newEvent = _mapper.Map<TEvent>(entityState);
            await _publishEndpoint.Publish(newEvent);
            return entityState;
        }
    }
}