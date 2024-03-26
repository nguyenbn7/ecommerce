using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ecommerce.Application.DbSqlite;

public class DecimalToDouble : ValueConverter<decimal, double>
{
    public DecimalToDouble() : base(dec => decimal.ToDouble(dec), doub => (decimal)doub)
    {
    }
}