using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend_sicpa.DbRepo
{
    public partial class DepartmentsEmployee
    {
        public int DepartmentsId { get; set; }
        public int EmployeesId { get; set; }
        public string CreatedBy { get; set; } = null!;

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly CreatedDate { get; set; }

        public string ModifiedBy { get; set; } = null!;

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly ModifiedDate { get; set; }

        public sbyte Status { get; set; }

        public virtual Department Departments { get; set; } = null!;
        public virtual Employee Employees { get; set; } = null!;
    }
}
