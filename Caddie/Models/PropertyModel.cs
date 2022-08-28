using System;
using System.Collections.Generic;
using PetaPoco;
using UIOMatic.Attributes;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Caddie.Models
{
    [UIOMatic("Indstillinger", "icon-users", "icon-user", ConnectionStringName = "vgcmsSqlServer")]
    [TableName("ms.Property")]
    public class PropertyModel  
    {
        public PropertyModel() { }

        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [UIOMaticField(Name = "Desce", Description = "Enter the persons first name")]
        public string Description { get; set; }

        [UIOMaticField(Name = "Værdi", Description = "Enter the persons first name")]
        public string Value { get; set; }

        [UIOMaticField(Name = "Gældende fra", Description = "Enter the persons last name")]
        public DateTime ValidFrom { get; set; }

        [UIOMaticField(Name = "Gældende til", Description = "Enter the persons last name")]
        public DateTime ValidTo { get; set; }

        public override string ToString()
        {
            return Description + ": " + Value;
        }

        public IEnumerable<Exception> Validate()
        {
            var exs = new List<Exception>();

            if (string.IsNullOrEmpty(Value))
                exs.Add(new Exception("Please provide a value"));

            return exs;
        }
    }
}
