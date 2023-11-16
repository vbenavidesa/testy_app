using System.Threading.Tasks;
using testy.Common;

namespace testy.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
