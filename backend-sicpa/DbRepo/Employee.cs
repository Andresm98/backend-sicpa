using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend_sicpa.DbRepo
{
    public partial class Employee
    {
        public Employee()
        {
            DepartmentsEmployees = new HashSet<DepartmentsEmployee>();
        }

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

        public virtual ICollection<DepartmentsEmployee> DepartmentsEmployees { get; set; }
    }
}
