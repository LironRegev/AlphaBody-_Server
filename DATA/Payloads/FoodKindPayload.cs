using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Payloads
{
    public class FoodKindPayload
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public FoodKindPayload(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
