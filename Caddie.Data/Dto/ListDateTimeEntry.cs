using System;
using PetaPoco;

namespace Caddie.Data.Dto
{
    public class ListDateTimeEntry
    {
        [Column("Key")]
        public int Key { get; set; }
        [Column("Value")]
        public DateTime Value { get; set; }
    }
}
