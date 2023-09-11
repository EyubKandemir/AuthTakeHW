using Business.Messages;
using Data.Results;

namespace Business.ValidationRules.User
{
    public class UserAddValidator
    {
        public Result Run(Data.Tables.User user)
        {
            if (string.IsNullOrEmpty(user.FirstName))
                return new ErrorResult(UserMessages.FirstNameMandatory);

            if (string.IsNullOrEmpty(user.LastName))
                return new ErrorResult(UserMessages.LastNameMandatory);

            if (string.IsNullOrEmpty(user.Email))
                return new ErrorResult(UserMessages.EmailMandatory);

            if (string.IsNullOrEmpty(user.Password))
                return new ErrorResult(UserMessages.PasswordMandatory);

            return new SuccessResult();
        }
    }
}
