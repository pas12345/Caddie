using System;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.Property")]
    [PrimaryKey("PropertyId", AutoIncrement = false)]
    [ExplicitColumns]
    public class Property
    {
        [Column("PropertyId")]
        public int Id { get; set; }

        [Column("DataValue")]
        public string Value { get; set; }

        [Column("SystemType")]
        public string SystemType { get; set; }

        [Column("ValidFrom")]
        public DateTime ValidFrom { get; set; }

        [Column("ValidTo")]
        public DateTime ValidTo { get; set; }

        [Column("Description")]
        public string Description { get; set; }
    }
}
