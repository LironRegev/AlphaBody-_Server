using BL.NeededCaloriesCalculators;
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
    public class UserDataManager
    {
        private User _user;
        private UserInfoConverter _userInfoConverter;
        private MenuConverter _menuConverter;

        public UserDataManager(User user)
        {
            _user = user;
            _userInfoConverter = new UserInfoConverter();
            _menuConverter = new MenuConverter();
        }

        public bool CheckIfNewUser()
        {
            return _user.UserInfo == null;
        }

        public LoggedInUserInfoPayload GetLoggedInUserInfo()
        {
            return new LoggedInUserInfoPayload()
            {
                Id = _user.Id,
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                IsAdmin = IsAdmin(),
                UserInfo = _userInfoConverter.ToDataType(_user.UserInfo)
            };
        }

        public LoggedInUserInfoPayload SaveAndCompleteUserInfo(UserInfoPayload userInfoPayload)
        {
            var userInfo = _userInfoConverter.ToDBType(userInfoPayload);
            userInfo.UserId = _user.Id;

            var bmi = CalculateBMI(userInfo.Height, (int)userInfo.Weight);
            var bmr = CalculateBMR(userInfo.Gender, userInfo.Height, (int)userInfo.Weight, userInfo.Age);
            var ncc = NCCFactory.GetNCC(userInfo.Goal);
            var neededCalories = ncc.CalculateNeededCalories(userInfo.Age, userInfo.Gender, userInfo.Height, userInfo.Weight, userInfo.ActivityLevel);

            userInfo.UserId = _user.Id;
            userInfo.BMI = bmi;
            userInfo.BMR = bmr;
            userInfo.NeededCalories = neededCalories;
            _user.UserInfo = userInfo;

            DBOperations.SaveUserInfo(_user.UserInfo);

            _user.FirstName = userInfoPayload.FirstName;
            _user.LastName = userInfoPayload.LastName;

            DBOperations.SaveName(_user.Id, _user.FirstName, _user.LastName);

            return GetLoggedInUserInfo();
        }

        public void SaveDislikedFoods(List<int> dislikedFoods)
        {
            if (_user.Dislikes != null)
            {
                DBOperations.DeleteUserDislikedFoods(_user.Dislikes);
            }
            var transformed = dislikedFoods.Select(foodId => new UserDislike() { FoodId = foodId, UserId = _user.Id }).ToList();
            _user.Dislikes = transformed;
            DBOperations.SaveUserDislikedFoods(transformed);
        }

        private double CalculateBMR(Gender gender, int height, int weight, int age)
        {
            if (gender == Gender.Male)
            {
                return 66.47 + (13.75 * weight) + (5.003 * height) - (6.755 * age);
            }
            else
            {
                return 655.1 + (9.563 * weight) + (1.85 * height) - (4.676 * age);
            }
        }

        public MenuPayload GenerateMenu()
        {
            var userInfo = GetUserInfo();
            var userDislikes = GetUserDislike();
            var menu = MenuBuilder.BuildMenu(userInfo.NeededCalories, FoodsManager.Instance.GetAllDbFoods(), userDislikes, userInfo.MealsNum);
            return _menuConverter.ToDataType(menu);
        }

        public UserInfo GetUserInfo()
        {
            return _user.UserInfo;
        }

        private double CalculateBMI(double height, double weight)
        {
            double bmiHeight = height / 100;
            double bmi = weight / (bmiHeight * bmiHeight);
            return (int)bmi;
        }

        public MenuPayload GetUserMenu()
        {
            var menu = _user.UserMenuItems;
            return _menuConverter.ToDataType(menu);
        }

        public void SaveMenu(MenuPayload menuPayload)
        {
            var menu = _menuConverter.ToDBType(menuPayload);

            if (_user.UserMenuItems != null)
            {
                DBOperations.DeleteUserMenu(_user.UserMenuItems);
            }

            foreach (var item in menu)
            {
                item.UserId = _user.Id;
            }

            _user.UserMenuItems = DBOperations.SaveUserMenu(menu);
        }

        public List<int> GetUserDislike()
        {
            return _user.Dislikes.Select(D => D.FoodId).ToList();
        }

        public bool IsAdmin()
        {
            return _user.Role == Role.Admin;
        }
    }
}
