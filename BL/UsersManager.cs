using Data.Payloads;
using BL.DBConverters;
using Dal;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class UsersManager
    {
        private Dictionary<int, UserDataManager> _loggedInUsersManagers { get; set; }

        private static UsersManager _instance;
        public static UsersManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UsersManager();
                }

                return _instance;
            }
        }

        private UsersManager()
        {
            _loggedInUsersManagers = new Dictionary<int, UserDataManager>();
        }

        public bool Register(string userName, string password,  string EmailAddress)
        {
            if (DBOperations.IsUserNameExist(userName))
            {
                return false;
            }

            DBOperations.AddUser(userName, password, EmailAddress);

            return true;
        }

        public LoggedInUserInfoPayload Login(string userName, string password)
        {
            var user = DBOperations.GetUser(userName, password);

            if (user == null)
            {
                return null;
            }

            var udm = new UserDataManager(user);
            _loggedInUsersManagers[user.Id] = udm;
            return udm.GetLoggedInUserInfo();
        }

        public void Logout(int userId)
        {
            if (_loggedInUsersManagers.ContainsKey(userId))
                _loggedInUsersManagers.Remove(userId);
        }

        public LoggedInUserInfoPayload SaveUserInfo(int userId, UserInfoPayload userInfo)
        {
            if (!_loggedInUsersManagers.TryGetValue(userId, out var udm))
            {
                return null;
            }

            return udm.SaveAndCompleteUserInfo(userInfo);
        }

        public bool SaveUserDislikes(int userId, List<int> userDislikes)
        {
            if (!_loggedInUsersManagers.TryGetValue(userId, out var udm))
            {
                return false;
            }

            udm.SaveDislikedFoods(userDislikes);
            return true;
        }

        public List<int> GetUserDislikes(int userId)
        {
            if (!_loggedInUsersManagers.TryGetValue(userId, out var udm))
            {
                return null;
            }

            return udm.GetUserDislike();
        }

        public MenuPayload GetUserMenu(int userId)
        {
            if (!_loggedInUsersManagers.TryGetValue(userId, out var udm))
            {
                return null;
            }

            return udm.GetUserMenu();
        }

        public MenuPayload GenerateNewUserMenu(int userId)
        {
            if (!_loggedInUsersManagers.TryGetValue(userId, out var udm))
            {
                return null;
            }

            return udm.GenerateMenu();
        }

        public bool SaveUserMenu(int userId, MenuPayload menu)
        {
            if (!_loggedInUsersManagers.TryGetValue(userId, out var udm))
            {
                return false;
            }

            udm.SaveMenu(menu);
            return true;
        }
    }
}
