using System;

namespace Data.Payloads
{
    public class RegisterPayload : LoginPayload
    {
        public string EmailAddress { get; set; }
    }
}
