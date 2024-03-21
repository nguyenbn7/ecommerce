using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ecommerce.Application.DbPostgre;

public class DateTimeToDateTimeUtc : ValueConverter<DateTime, DateTime>
{
    public DateTimeToDateTimeUtc() : base(c => DateTime.SpecifyKind(c, DateTimeKind.Utc), c => c)
    {
    }
}