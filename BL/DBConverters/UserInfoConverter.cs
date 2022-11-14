using Data.Payloads;
using Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DBConverters
{
    public class UserInfoConverter : IDBConverter<UserInfo, UserInfoPayload>
    {
        public UserInfoPayload ToDataType(UserInfo dbType)
        {
            if (dbType == null)
                return null;

            return new UserInfoPayload()
            {
                Goal = dbType.Goal,
                ActivityLevel = dbType.ActivityLevel,
                MealsNum = dbType.MealsNum,
                BMI = dbType.BMI,
                BMR = dbType.BMR,
                Height = dbType.Height,
                Weight = dbType.Weight,
                Gender = dbType.Gender,
                Age = dbType.Age,
                NeededCalories = dbType.NeededCalories,
            };
        }

        public UserInfo ToDBType(UserInfoPayload dataType)
        {
            return new UserInfo()
            {
                Goal = dataType.Goal,
                ActivityLevel = dataType.ActivityLevel,
                MealsNum = dataType.MealsNum,
                BMI = dataType.BMI,
                BMR = dataType.BMR,
                Height = dataType.Height,
                Weight = dataType.Weight,
                Gender = dataType.Gender,
                Age = dataType.Age,
                NeededCalories = dataType.NeededCalories,
            };
        }
    }
}
