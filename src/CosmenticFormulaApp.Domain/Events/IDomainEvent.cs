using MediatR;

namespace CosmenticFormulaApp.Domain.Events
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
