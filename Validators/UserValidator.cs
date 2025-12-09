using System.Net.Mail;


namespace MyGraphqlApp.Validators.UserValidator
{



    public class UserValidator
    {

        public bool userNameCheck(string userName)
        {



            for (int i = 0; i < userName.Length; i++)
            {
                if (userName[i] >= 'a' && userName[i] <= 'z')
                {
                    continue;
                }
                else if (userName[i] >= 'A' && userName[i] <= 'Z')
                {
                    continue;
                }
                else if (userName[i] >= '0' && userName[i] <= '9')
                {
                    continue;
                }
                else if (userName[i] == '_')
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }


            return true;
        }



        // email is correct or not check

        public bool emailCheck(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }

        }


        // password check goes here 

        public bool passwordCheck(string password)
        {


            bool case1 = password.Length >= 8 ? true : false;
            int lowerCase = 0;
            int upperCase = 0;
            int numbers = 0;
            int specialSymbol = 0;

            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] >= 'a' && password[i] <= 'z')
                {
                    lowerCase++;
                }
                if (password[i] >= 'A' && password[i] <= 'Z')
                {
                    upperCase++;
                }
                if (password[i] >= '0' && password[i] <= '9')
                {
                    numbers++;
                }
                if (password[i] == '!' || password[i] == '@' || password[i] == '#' || password[i] == '$' || password[i] == '%')
                {
                    specialSymbol++;
                }


            }

            if (case1 && lowerCase >= 1 && upperCase >= 1 && numbers >= 1 && specialSymbol >= 1)
            {
                return true;
            }


            return false;
        }



        // phone no validations 

        public bool phoneNuberCheck(string phoneNumber)
        {
            if (phoneNumber.Length != 10)
            {
                return false;
            }
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                if (phoneNumber[i] < '0' || phoneNumber[i] > '9')
                {
                    return false;
                }
            }

            return true;
        }



    }


}