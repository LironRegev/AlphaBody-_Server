using Dal.Model;
using Data.Payloads;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DBConverters
{
    public class FoodConverter : IDBConverter<Food, FoodPayload>
    {
        public FoodPayload ToDataType(Food dbType)
        {
            return new FoodPayload()
            {
                Id = dbType.Id,
                Name = dbType.Name,
                FoodKind = dbType.FoodKind,
                Grams = dbType.Grams,
                Calories = dbType.Calories,
                ProteinGrams = dbType.ProteinGrams,
                CarbsGrams = dbType.CarbsGrams,
                FatGrams = dbType.FatGrams,
                CholesterolMilligram = dbType.CholesterolMilligram,
                Fibers = dbType.Fibers,
            };
        }

        public Food ToDBType(FoodPayload dataType)
        {
            return new Food()
            {
                Id = dataType.Id,
                Name = dataType.Name,
                FoodKind = dataType.FoodKind,
                Grams = dataType.Grams,
                Calories = dataType.Calories,
                ProteinGrams = dataType.ProteinGrams,
                CarbsGrams = dataType.CarbsGrams,
                FatGrams = dataType.FatGrams,
                CholesterolMilligram = dataType.CholesterolMilligram,
                Fibers = dataType.Fibers,
            };
        }
    }
}
