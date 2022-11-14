﻿using Data.DataTypes;
using Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Payloads;

namespace BL
{
    public class MenuBuilder
    {
        static List<FoodKind> allFoodKinds = Enum.GetValues(typeof(FoodKind)).Cast<FoodKind>().ToList();
        static Dictionary<MealTime, List<FoodKind>> mealToKinds = new Dictionary<MealTime, List<FoodKind>>
        {
            {MealTime.Breakfast,new List<FoodKind>{ FoodKind.Bread,FoodKind.Egg,FoodKind.Baked,FoodKind.Vegetable,FoodKind.Fruit} },
            {MealTime.EarlySnack,new List<FoodKind>{ FoodKind.Fruit} },
            {MealTime.Lunch,allFoodKinds},
            {MealTime.LateSnack,new List<FoodKind>{ FoodKind.Fruit} },
            {MealTime.Dinner,allFoodKinds.Except(new List<FoodKind>{ FoodKind.Baked,FoodKind.Fruit,FoodKind.Snack}).ToList()},
            {MealTime.BeforeSleep,new List<FoodKind>{ FoodKind.Fruit} },
    };
        static Dictionary<MealTime, int> numOfFoodsPerMeal = new Dictionary<MealTime, int>
        {
            {MealTime.Breakfast,2 },
            {MealTime.EarlySnack,1 },
            {MealTime.Lunch,4 },
            {MealTime.LateSnack,1 },
            {MealTime.Dinner,3 },
            {MealTime.BeforeSleep,1 },

        };
        static Dictionary<MealsNum, List<MealTime>> mealsPerDay = new Dictionary<MealsNum, List<MealTime>>
        {
            {MealsNum.Meals1,new List<MealTime>{MealTime.Lunch  } }, 
            {MealsNum.Meals2,new List<MealTime>{MealTime.Breakfast,MealTime.Dinner  } },
            {MealsNum.Meals3,new List<MealTime>{ MealTime.Breakfast, MealTime.Lunch, MealTime.Dinner} },
            {MealsNum.Meals4,new List<MealTime>{ MealTime.Breakfast, MealTime.EarlySnack, MealTime.Lunch, MealTime.Dinner} },
            {MealsNum.Meals5,new List<MealTime>{ MealTime.Breakfast, MealTime.EarlySnack, MealTime.Lunch,MealTime.LateSnack, MealTime.Dinner} },
            {MealsNum.Meals6,new List<MealTime>{ MealTime.Breakfast, MealTime.EarlySnack, MealTime.Lunch,MealTime.LateSnack, MealTime.Dinner, MealTime.BeforeSleep} },
        };

        public static List<UserMenuItem> BuildMenu(int totalCalories,List<Food> foods,List<int> dislikes,MealsNum mealsNum)
        {
            var likedfoods = foods.Where(fd =>!dislikes.Contains(fd.Id)).ToList();
            var meals = mealsPerDay[mealsNum];
            var totalFoods = meals.Sum(ms => numOfFoodsPerMeal[ms]);
            var caloriesPerFood = (double)totalCalories / totalFoods;
            var menu = new List<UserMenuItem>();

            foreach (var meal in meals)
            {
                var numOfFoods = numOfFoodsPerMeal[meal];
                var mealKinds = mealToKinds[meal];
                mealKinds = mealKinds.Where(mk => likedfoods.Any(lk => lk.FoodKind == mk)).ToList();
                Random rnd = new Random();
                var selectedKinds = mealKinds.OrderBy(x => rnd.Next()).Take(numOfFoods).ToList();
                foreach (var kind in selectedKinds)
                {
                    var kindFoods = likedfoods.Where(fd => fd.FoodKind == kind).ToList();
                    var selectedFood = kindFoods.OrderBy(x => rnd.Next()).First();
                    likedfoods.Remove(selectedFood);
                    var grams = caloriesPerFood / selectedFood.Calories * selectedFood.Grams;
                    var menuiItem = new UserMenuItem
                    {
                        Food = selectedFood,
                        MealTime = meal,
                        FoodId = selectedFood.Id,
                        Grams = (int)grams
                    };
                    menu.Add(menuiItem);
                }
            }
            return menu;
        }
    }
}
