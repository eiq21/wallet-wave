using Wallet.Application.Abstractions.Clock;

namespace Wallet.Infrastructure.Clock;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
