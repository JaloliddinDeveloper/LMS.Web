//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using LMS.Web.Models.Foundations.Users;
using LMS.Web.Models.Foundations.Users.Exceptions;

namespace LMS.Web.Services.Foundations
{
    public partial class UserService
    {
        private void ValidateUserOnAdd(User user)
        {
            ValidationUserNotNull(user);

            Validate(
               (Rule: IsInvalid(user.Id), Parameter: nameof(user.Id)),
               (Rule: IsInvalid(user.FirstName), Parameter: nameof(user.FirstName)),
               (Rule: IsInvalid(user.LastName), Parameter: nameof(user.LastName)),
               (Rule: IsInvalid(user.Email), Parameter: nameof(user.Email)),
               (Rule: IsInvalid(user.Password), Parameter: nameof(user.Password)),
               (Rule: IsInvalid(user.CreatedDate), Parameter: nameof(user.CreatedDate)),
               (Rule: IsInvalid(user.UpdatedDate), Parameter: nameof(user.UpdatedDate)));
        }

        private void ValidationUserNotNull(User user)
        {
            if(user is null)
            {
                throw new NullUserException("User is null");
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition=id==Guid.Empty,
            Message="Id is required"
        };
        private static dynamic IsInvalid(string text) => new
        {
            Condition=string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };
        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date ==default,
            Message = "Date is required"
        };
        private static void Validate(params(dynamic Rule,string Parameter)[]validations)
        {
            InvalidUserException invalidUserException = 
                new InvalidUserException(message: "User is invalid");

            foreach((dynamic rule,string parametr) in validations)
            {
                if(rule.Condition)
                {
                    invalidUserException.UpsertDataList(parametr, rule.message);
                }
            }
            invalidUserException.ThrowIfContainsErrors();
        }
    }
}
