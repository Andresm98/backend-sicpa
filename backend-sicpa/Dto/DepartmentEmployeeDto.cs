using System.Text.Json.Serialization;

namespace backend_sicpa.Dto
{
    public class DepartmentEmployeeDto
    {
        public int Id { get; set; }
        public int DepartmentsId { get; set; }
        public int EmployeesId { get; set; }
        public string CreatedBy { get; set; } = null!;

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly CreatedDate { get; set; }

        public string ModifiedBy { get; set; } = null!;

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly ModifiedDate { get; set; }
        public sbyte Status { get; set; }

    }
}
