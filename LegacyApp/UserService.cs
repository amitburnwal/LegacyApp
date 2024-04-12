using LegacyApp.Api.Models;
using System;
using System.Threading.Tasks;

namespace LegacyApp
{
    public interface IUserService
    {
        Task<User> AddUser(UserDto dto);
    }
    public class UserService : IUserService
    {
        public async Task<User> AddUser(UserDto userDto)
        {            
            
            var user = CreateUser(userDto);
            UserDataAccess.AddUser(user);
            return user;
        }
        //Given time I could have implemented Automapper here to transform dto to User object.
        # region privateFunction
        

        private User CreateUser(UserDto userDto)
        {
            var user = new User
            {
                Client = userDto.Client,
                DateOfBirth = userDto.DateOfBirth,
                EmailAddress = userDto.EmailAddress,
                Firstname = userDto.Firstname,
                Surname = userDto.Surname
                
            };
            return user;
        }

        #endregion
        
        // Commented old code
        //public async Task<User> AddUser(string firstName, string surname, string email, DateTime dateOfBirth, int clientId)
        //{
        //    if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname))
        //    {
        //        throw new InvalidOperationException("user firstname / surname is required ");
        //    }

        //    if (!email.Contains("@") && !email.Contains("."))
        //    {
        //        throw new InvalidOperationException("user email is invalid ");
        //    }

        //    var now = DateTime.Now;
        //    int age = now.Year - dateOfBirth.Year;
        //    if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

        //    if (age < 21)
        //    {
        //         throw new InvalidOperationException("user should be older than 21 years");
        //    }

        //    var clientRepository = new ClientRepository();
        //    var client = await clientRepository.GetByIdAsync(clientId);

        //    var user = new User
        //    {
        //        Client = client,
        //        DateOfBirth = dateOfBirth,
        //        EmailAddress = email,
        //        Firstname = firstName,
        //        Surname = surname
        //    };

        //    if (client.Name == "VeryImportantClient")
        //    {
        //        // Skip credit check
        //        user.HasCreditLimit = false;
        //    }
        //    else if (client.Name == "ImportantClient")
        //    {
        //        // Do credit check and double credit limit
        //        user.HasCreditLimit = true;
        //        using (var userCreditService = new UserCreditServiceClient())
        //        {
        //            var creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
        //            creditLimit = creditLimit * 2;
        //            user.CreditLimit = creditLimit;
        //        }
        //    }
        //    else
        //    {
        //        // Do credit check
        //        user.HasCreditLimit = true;
        //        using (var userCreditService = new UserCreditServiceClient())
        //        {
        //            var creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
        //            user.CreditLimit = creditLimit;
        //        }
        //    }

        //    if (user.HasCreditLimit && user.CreditLimit < 500)
        //    {
        //        throw new InvalidOperationException("insufficient credit limit");
        //    }

        //    UserDataAccess.AddUser(user);

        //    return user;
        //}
    }
}
