using System;

namespace Data.Payloads
{
    public class LoggedInUserInfoPayload
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public UserInfoPayload UserInfo { get; set; }
    }
}
