using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRecipeBook.Application.UseCases.User.Register;

namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void Success()
        {
            var validator = new RegisterUserValidator();

            var request = new MyRecipeBook.Communication.Requests.RequestRegisterUserJson
            {
                Name = "Valid Name",
                Email = "email@gmail.com",
                Password = "ValidPassword123!"
            }
            var result = validator.Validate(request);

            // Assert
}
