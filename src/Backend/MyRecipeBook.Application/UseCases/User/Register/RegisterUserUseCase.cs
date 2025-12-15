using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisterUserJson Execute(RequestRegisterUserJson request)
        {
            // Validar a request (verificar se o e-mail já existe é uma regra de negócio, esse validate é só pra propriedades
            Validate(request);

            // Mapear a request em uma entidade
            var user = new Domain.Entities.User;
            // Criptografia da senha

            // Salvar no banco de dados

            return new ResponseRegisterUserJson { Nome  = request.Name };


        }

        private void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
