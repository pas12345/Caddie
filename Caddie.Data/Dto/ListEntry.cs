using System;
using PetaPoco;

namespace Caddie.Data.Dto
{
    public class ListEntry
    {
        [Column("Key")]
        public int Key { get; set; }
        [Column("Value")]
        public string Value { get; set; }
    }
}
