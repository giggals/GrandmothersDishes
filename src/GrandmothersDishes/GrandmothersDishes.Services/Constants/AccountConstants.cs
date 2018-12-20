using System;
using System.Collections.Generic;
using System.Text;

namespace GrandmothersDishes.Services.Constants
{
    public class AccountConstants
    {
        public const string ConfirmPasswordErrorMessage = "The password and confirmation password do not match";

        public const string PasswordErrorMessage =
            "The password must be atleast {2} characters long and max {1} characters";

        public const int MaxPasswordLenght = 30;

        public const int MinPasswordLenght = 3;
    }
}
