using Data.Payloads;
using BL;
using Data.Payloads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaBodyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : AlphaBodyControllerBase
    {
        public UsersController()
        {
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<LoggedInUserInfoPayload> Register([FromBody] RegisterPayload registerInfo)
        {
            var success = UsersManager.Instance.Register(registerInfo.UserName, registerInfo.Password, registerInfo.EmailAddress);

            if (!success)
                return null;

            var user = UsersManager.Instance.Login(registerInfo.UserName, registerInfo.Password);

            return OkOrBadRequest(user);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<LoggedInUserInfoPayload> Login([FromBody] LoginPayload loginInfo)
        {
            var user = UsersManager.Instance.Login(loginInfo.UserName, loginInfo.Password);

            return OkOrBadRequest(user);
        }

        [HttpGet]
        [Route("logout/{userId:int}")]
        public ActionResult<LoggedInUserInfoPayload> Logout(int userId)
        {
            UsersManager.Instance.Logout(userId);

            return Ok();
        }

        [HttpPut]
        [Route("userinfo/{userId:int}")]
        public ActionResult<LoggedInUserInfoPayload> PutUserInfo(int userId, [FromBody] UserInfoPayload userInfo)
        {
            var info = UsersManager.Instance.SaveUserInfo(userId, userInfo);

            return OkOrUnauthorized(info);
        }

        [HttpGet]
        [Route("dislikes/{userId:int}")]
        public ActionResult<List<int>> GetUserDislikes(int userId)
        {
            var dislikes = UsersManager.Instance.GetUserDislikes(userId);

            return OkOrUnauthorized(dislikes);
        }

        [HttpPut]
        [Route("dislikes/{userId:int}")]
        public ActionResult PutUserDislikes(int userId, [FromBody] List<int> userDislikes)
        {
            var success = UsersManager.Instance.SaveUserDislikes(userId, userDislikes);

            return OkOrUnauthorized(success);
        }

        [HttpGet]
        [Route("menu/{userId:int}")]
        public ActionResult<MenuPayload> GetUserMenu(int userId)
        {
            var menu = UsersManager.Instance.GetUserMenu(userId);

            return OkOrUnauthorized(menu);
        }

        [HttpPost]
        [Route("menu/{userId:int}")]
        public ActionResult<MenuPayload> GenerateNewUserMenu(int userId)
        {
            var menu = UsersManager.Instance.GenerateNewUserMenu(userId);

            return OkOrUnauthorized(menu);
        }

        [HttpPut]
        [Route("menu/{userId:int}")]
        public ActionResult PutUserMenu(int userId, [FromBody] MenuPayload menu)
        {
            var success = UsersManager.Instance.SaveUserMenu(userId, menu);

            return OkOrUnauthorized(success);
        }
    }
}
