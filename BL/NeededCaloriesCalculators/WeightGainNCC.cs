﻿using Dal;
using Data.DataTypes;
using Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.NeededCaloriesCalculators
{
    public class WeightGainNCC : INeededCaloriesCalculator
    {
        List<GoalDefinition> goalDefinitions;
        Dictionary<ActivityLevel, int> activityCalories;
        public WeightGainNCC()
        {
            goalDefinitions = DBOperations.GetGoalDefinitionsByGoal(Goal.WeightGain);
            activityCalories = new Dictionary<ActivityLevel, int>()
            {
                {ActivityLevel.SuperLight, 300 },
                {ActivityLevel.Light, 500 },
                {ActivityLevel.Medium, 700 },
                {ActivityLevel.Heavy, 800 },
                {ActivityLevel.SuperHeavy, 900 },
            };
        }
        private int GetHeightCalaulation(Gender gender, int height)
        {
            if (gender == Gender.Male)
            {
                if (height >= 140 && height <= 149)
                {
                    return 100;
                }
                else if (height >= 150 && height <= 159)
                {
                    return 200;
                }
                else if (height >= 160 && height <= 169)
                {
                    return 300;
                }
                else if (height >= 170 && height <= 179)
                {
                    return 400;
                }
                else if (height >= 180 && height <= 189)
                {
                    return 500;
                }
                else if (height >= 190 && height <= 200)
                {
                    return 600;
                }
                else
                {
                    return 700;
                }

            }
            if (gender == Gender.Female)
            {
                if (height >= 140 && height <= 149)
                {
                    return 50;
                }
                else if (height >= 150 && height <= 159)
                {
                    return 125;
                }
                else if (height >= 160 && height <= 169)
                {
                    return 200;
                }
                else if (height >= 170 && height <= 179)
                {
                    return 275;
                }
                else if (height >= 180 && height <= 189)
                {
                    return 350;
                }
                else if (height >= 190 && height <= 200)
                {
                    return 425;
                }
                else
                {
                    return 500;
                }

            }

            return 0;
        }
        public int CalculateNeededCalories(int age, Gender gender, int height, double weight, ActivityLevel activityLevel)
        {
            var definition = goalDefinitions.FirstOrDefault(gd => gd.MinAge <= age && gd.MaxAge >= age && gd.Gender == gender);

            var activitycalculation = activityCalories[activityLevel];

            var heightcalaulation = GetHeightCalaulation(gender, height);

            return definition.Calories + activitycalculation + heightcalaulation;
        }
    }
}
