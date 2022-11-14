using Dal.Model;
using Data.DataTypes;
using Data.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.DBConverters
{
    public class MenuConverter : IDBConverter<List<UserMenuItem>, MenuPayload>
    {
        public MenuPayload ToDataType(List<UserMenuItem> dbType)
        {
            var menu = new MenuPayload();

            var mealGroups = dbType.GroupBy(dbType => dbType.MealTime);
            foreach (var mealGroup in mealGroups)
            {
                var mealTime = mealGroup.Key;
                var mealItems = mealGroup.Select(mi => new MealItemPayload() { FoodId = mi.FoodId, Grams = mi.Grams }).ToList();

                var meal = new MealPayload()
                {
                    MealTime = mealTime.ToString(),
                    MealItems = mealItems
                };

                menu.Meals.Add(meal);
            }

            return menu;
        }

        public List<UserMenuItem> ToDBType(MenuPayload dataType)
        {
            var menuItems = new List<UserMenuItem>();

            foreach (var meal in dataType.Meals)
            {
                var mealItems = meal.MealItems.Select(mi => new UserMenuItem() { MealTime = (MealTime)Enum.Parse(typeof(MealTime), meal.MealTime), FoodId = mi.FoodId, Grams = mi.Grams }).ToList();
                menuItems.AddRange(mealItems);
            }

            return menuItems;
        }
    }
}
