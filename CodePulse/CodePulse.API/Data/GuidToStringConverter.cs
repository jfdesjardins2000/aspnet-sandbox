using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodePulse.API.Data
{
    public class GuidToStringConverter : ValueConverter<Guid, string>
    {
        public GuidToStringConverter() : base(g => g.ToString(), s => new Guid(s))
        {
        }
    }
}
