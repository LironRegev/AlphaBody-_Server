using Dal;
using Data.DataTypes;
using Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Payloads;
using BL.DBConverters;

namespace BL
{
    public class FoodsManager
    {
        private FoodConverter _foodConverter;
        private static FoodsManager _instance;
        public static FoodsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FoodsManager();
                }
                return _instance;
            }
        }
        private FoodsManager()
        {
            _foodConverter = new FoodConverter();
        }

        public List<FoodKindPayload> GetFoodKinds()
        {
            var FKL = new List<FoodKindPayload>();
            foreach (var name in Enum.GetNames(typeof(FoodKind)))
            {
                var Id = (int)Enum.Parse(typeof(FoodKind), name);
                var foodKindPL = new FoodKindPayload(Id,name);
                FKL.Add(foodKindPL);
            }
            return FKL;
        }

        public List<FoodPayload> GetFoodOfKind(int selectedKind)
        {
            var enumValue = (FoodKind)(selectedKind);
            var allFood = DBOperations.GetAllFoodsOfKind(enumValue);
            return allFood.Select(F => _foodConverter.ToDataType(F)).ToList();
        }
        
        public List<FoodPayload> GetAllFoods()
        {
            var allFood = GetAllDbFoods();
            return allFood.Select(F => _foodConverter.ToDataType(F)).ToList();
        }
        
        public int SaveNewFood(FoodPayload newfood)
        {
            var NF =_foodConverter.ToDBType(newfood);
            return DBOperations.SaveNewFood(NF);
        }

        public bool DeleteFood(int Id)
        {
            return DBOperations.DeleteFood(Id);
        }

        internal List<Food> GetAllDbFoods()
        {
            return DBOperations.GetAllFoods();
        }
    }
}
