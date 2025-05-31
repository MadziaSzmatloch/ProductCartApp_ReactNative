using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Application.Commands.DeleteCart;
using Cart.Domain.Aggregates;
using Cart.Domain.Interfaces;
using Hangfire;
using MediatR;

namespace Cart.Application
{
    public class JobSchedulerHelper(IJobRepository jobRepository, IBackgroundJobClient backgroundJobClient, IMediator mediator)
    {
        private readonly IJobRepository _jobRepository = jobRepository;
        private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
        private readonly IMediator _mediator = mediator;

        public async Task RescheduleJob(Guid cartId)
        {
            var jobId = await _jobRepository.GetJobIdByCartId(cartId);
            _backgroundJobClient.Delete(jobId);
            TimeSpan delay = TimeSpan.FromMinutes(15);
            var job = _backgroundJobClient.Schedule(() => DeleteCart(cartId), delay);
        }

        public void ScheduleJob(Guid cartId)
        {
            TimeSpan delay = TimeSpan.FromMinutes(15);
            var job = _backgroundJobClient.Schedule(() => DeleteCart(cartId), delay);
        }

        public async Task DeleteCart(Guid cartId)
        {
            await _mediator.Send(new DeleteCartRequest(cartId));
        }

        public async Task DeleteJob(Guid cartId)
        {
            var jobId = await _jobRepository.GetJobIdByCartId(cartId);
            _backgroundJobClient.Delete(jobId);
        }
    }
}
