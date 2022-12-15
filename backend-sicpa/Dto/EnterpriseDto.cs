﻿using System.Text.Json.Serialization;

namespace backend_sicpa.Dto
{
    public class EnterpriseDto
    {

        public int Id { get; set; }
        public string CreatedBy { get; set; } = null!;

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly CreatedDate { get; set; }

        public string ModifiedBy { get; set; } = null!;

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly ModifiedDate { get; set; }
        public sbyte Status { get; set; }
        public string Address { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;

    }
}
