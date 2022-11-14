using Data.DataTypes;
using Dal.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public static class DBOperations
    {
        public static User GetUser(string username, string pass)
        {
            using (var db = new MainDBContext())
            {
                return db.Users.Include(user => user.UserInfo).Include(user => user.Dislikes).Include(user => user.UserMenuItems).ThenInclude(menuitem => menuitem.Food)
                               .FirstOrDefault(user => user.UserName == username && user.Password == pass);
            }
        }

        public static int AddUser(string userName, string password, string email)
        {
            var user = new User() { UserName = userName, Password = password, Email = email, Role = Role.User };

            using (var db = new MainDBContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

            return user.Id;
        }

        public static void SaveUserInfo(UserInfo userInfo)
        {
            using (var db = new MainDBContext())
            {
                var existinginfo = db.UserInfos.Any(ui => ui.UserId == userInfo.UserId);
                if (!existinginfo)
                {
                    db.UserInfos.Add(userInfo);
                }
                else
                {
                    db.UserInfos.Update(userInfo);
                }

                db.SaveChanges();
            }
        }

        public static bool DeleteFood(int Id)
        {
            using (var db = new MainDBContext())
            {
                var foodToDelete = db.Foods.FirstOrDefault(F => F.Id == Id);
                
                if (foodToDelete == null)
                {
                    return false;
                }

                db.Foods.Remove(foodToDelete);
                db.SaveChanges();
            }

            return true;
        }

        public static List<Food> GetAllFoodsOfKind(FoodKind kind)
        {
            using (var db = new MainDBContext())
            {
                return db.Foods.Where(fd => fd.FoodKind == kind).ToList();
            }
        }

        public static void SaveName(int id, string firstName, string lastName)
        {
            using (var db = new MainDBContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                user.FirstName = firstName;
                user.LastName = lastName;
                db.Update(user);
                db.SaveChanges();
            }
        }

        public static void SaveUserDislikedFoods(List<UserDislike> dislikes)
        {
            using (var db = new MainDBContext())
            {
                db.UserDislikes.AddRange(dislikes);
                db.SaveChanges();
            }
        }
        public static void DeleteUserDislikedFoods(List<UserDislike> dislikes)
        {
            using (var db = new MainDBContext())
            {
                db.UserDislikes.RemoveRange(dislikes);
                db.SaveChanges();
            }
        }
        public static List<GoalDefinition> GetGoalDefinitionsByGoal(Goal goal)
        {
            using (var db = new MainDBContext())
            {
                return db.GoalDefinitions.Where(GD => GD.Goal == goal).ToList();
            }
        }
        public static List<Food> GetAllFoods()
        {
            using (var db = new MainDBContext())
            {
                return db.Foods.ToList();
            }
        }
        public static List<UserMenuItem> SaveUserMenu(List<UserMenuItem> menu)
        {
            var menuCopy = menu.Select(m => new UserMenuItem { FoodId = m.FoodId, Grams = m.Grams, MealTime = m.MealTime, UserId = m.UserId}).ToList();
            using (var db = new MainDBContext())
            {
                db.UserMenuItems.AddRange(menuCopy);
                db.SaveChanges();
                menuCopy.ForEach(f => db.Entry(f).Reference(r => r.Food).Load());
            }
            return menuCopy;
        }
        public static void DeleteUserMenu(List<UserMenuItem> menu)
        {
            var menuCopy = menu.Select(m => new UserMenuItem { FoodId = m.FoodId, Grams = m.Grams, MealTime = m.MealTime, UserId = m.UserId, Id =m.Id });
            using (var db = new MainDBContext())
            {
                db.UserMenuItems.RemoveRange(menuCopy);
                db.SaveChanges();
            }
        }
        public static bool IsUserNameExist(string username)
        {
            using (var db = new MainDBContext())
            {

                return db.Users.Any(u => username == u.UserName);
            }
            
        }
        public static int SaveNewFood (Food newfood)
        {
            using (var db = new MainDBContext())
            {
                db.Foods.Add(newfood);
                db.SaveChanges();
            }

            return newfood.Id;
        }
    }
}
