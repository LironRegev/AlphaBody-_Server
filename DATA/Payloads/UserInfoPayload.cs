using Data.DataTypes;
using System;

namespace Data.Payloads
{
    public class UserInfoPayload
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Goal Goal { get; set; }
        public ActivityLevel ActivityLevel { get; set; }
        public MealsNum MealsNum { get; set; }
        public double BMI { get; set; }
        public double BMR { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public int NeededCalories { get; set; }
    }
}
