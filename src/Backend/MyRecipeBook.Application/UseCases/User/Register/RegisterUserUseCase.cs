using AutoMapper;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordEncrypt _passwordEncrypt;
        public RegisterUserUseCase(
            IUserWriteOnlyRepository writeOnlyRepository,
            IUserReadOnlyRepository readOnlyRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            PasswordEncrypt passwordEncrypt)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _passwordEncrypt = passwordEncrypt;
        }
        
        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
        {            

            // Validar a request (verificar se o e-mail já existe é uma regra de negócio, esse validate é só pra propriedades
            await Validate(request);

            // Mapear a request em uma entidad

            var user = _mapper.Map<Domain.Entities.User>(request);
            // Criptografia da senha

            user.Password = _passwordEncrypt.Encrypt(request.Password);

            // Salvar no banco de dados

            await _writeOnlyRepository.Add(user);
            await _unitOfWork.Commit();
            return new ResponseRegisterUserJson { Nome  = request.Name };


        }

        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);

            if (emailExist)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
