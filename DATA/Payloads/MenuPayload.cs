using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Payloads
{
    public class MenuPayload
    {
        public List<MealPayload> Meals { get; set; }

        public MenuPayload()
        {
            Meals = new List<MealPayload>();
        }
    }
}
