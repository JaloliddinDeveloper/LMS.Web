//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using LMS.Web.Models.Foundations.Users.Exceptions;
using LMS.Web.Models.Foundations.Users;
using Xeptions;
using Microsoft.Data.SqlClient;
using EFxceptions.Models.Exceptions;

namespace LMS.Web.Services.Foundations
{
    public partial class UserService
    {
        private delegate ValueTask<User> ReturningUserFunction();
        private async ValueTask<User> TryCatch(ReturningUserFunction returningUserFunction)
        {
            try
            {
                return await returningUserFunction();
            }
            catch (NullUserException nullUserException)
            {
                throw CreateAndLogValidationException(nullUserException);
            }
            catch (InvalidUserException invalidUserException)
            {
                throw CreateAndLogValidationException(invalidUserException);
            }
            catch(SqlException sqlException)
            {
                var failedUserStorageException =
                    new FailedUserStorageException(
                        "User storage error occurred,please contact support",
                        innerException: sqlException);
                throw CreateAndLogCriticalDependencyException(failedUserStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistUserException =
                    new AlreadyExistUserException(
                       message: "User already exist,please try again",
                       innerException: duplicateKeyException);
                throw CreateAndLogDublicateKeyExcption(alreadyExistUserException);
            }
        }

        private UserDependencyValidationException CreateAndLogDublicateKeyExcption(Xeption exception)
        {
            var userDependencyValidationException =
                 new UserDependencyValidationException(
                     message: "User dependency error occurred,fix errors and try again",
                     innerException: exception);
            this.loggingBroker.LogError(userDependencyValidationException);
            return userDependencyValidationException;
        }

        private UserDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
           var userDependencyException =
                new UserDependencyException("User dependency exception error occurred,please contact support",
                innerException:exception);
            this.loggingBroker.LogCritical(userDependencyException);
            return userDependencyException;
        }

        private UserValidationException CreateAndLogValidationException(Xeption exception)
        {
            var userValidationException = new UserValidationException(
                message: "User Validation error occurred,fix the errors and try again"
                , innerException: exception);
            this.loggingBroker.LogError(userValidationException);
            return userValidationException;
        }
    }
}
