using Business.Messages;
using Data.Results;

namespace Business.ValidationRules.User
{
    public class UserUpdateValidator
    {
        public Result Run(Data.Tables.User user)
        {
            if (user.Id <= 0)
                return new ErrorResult(UserMessages.IdCantBeZeroOrNull);

            if (string.IsNullOrEmpty(user.FirstName))
                return new ErrorResult(UserMessages.FirstNameMandatory);

            if (string.IsNullOrEmpty(user.LastName))
                return new ErrorResult(UserMessages.LastNameMandatory);

            if (string.IsNullOrEmpty(user.Email))
                return new ErrorResult(UserMessages.EmailMandatory);

            return new SuccessResult();
        }
    }
}
