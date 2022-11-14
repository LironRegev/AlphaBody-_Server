using Data.DataTypes;
using System.Collections.Generic;

namespace Data.Payloads
{
    public class MealPayload
    {
        public string MealTime { get; set; }

        public List<MealItemPayload> MealItems { get; set; }
    }

    public class MealItemPayload
    {
        public int FoodId { get; set; }
        public int Grams { get; set; }
    }
}