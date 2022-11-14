using Data.DataTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Payloads
{
    public class FoodPayload
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FoodKind FoodKind { get; set; }
        public int Grams { get; set; }
        public int Calories { get; set; }
        public double ProteinGrams { get; set; }
        public double CarbsGrams { get; set; }
        public double FatGrams { get; set; }
        public double CholesterolMilligram { get; set; }
        public double Fibers { get; set; }
    }
}
