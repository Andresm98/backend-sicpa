using System.Text.Json.Serialization;

namespace backend_sicpa.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = null!;

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly CreatedDate { get; set; }

        public string ModifiedBy { get; set; } = null!;

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly ModifiedDate { get; set; }

        public sbyte Status { get; set; }
        public int Age { get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}
