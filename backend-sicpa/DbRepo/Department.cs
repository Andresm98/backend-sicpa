using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace backend_sicpa.DbRepo
{
    public partial class Department
    {
        public Department()
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
        public string Description { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int EnterprisesId { get; set; }

        public virtual Enterprise Enterprises { get; set; } = null!;
        public virtual ICollection<DepartmentsEmployee> DepartmentsEmployees { get; set; }
    }
}
