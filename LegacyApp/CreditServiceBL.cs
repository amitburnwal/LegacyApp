using LegacyApp.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LegacyApp
{
    public interface ICreditServiceBL
    {
        public Task<int> GetCreditLimit(int clientId, UserDto user);
        public bool CreditLimitNotSufficient(UserDto user);

    }
    public class CreditServiceBL : ICreditServiceBL
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserCreditService _userCreditService;

        public CreditServiceBL(IUserCreditService userCreditService, IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            _userCreditService = userCreditService;
        }

        public async Task<int> GetCreditLimit(int clientId, UserDto user)
        {

            var client = await _clientRepository.GetByIdAsync(clientId);
            int creditLimit = 0;
            if (client.Name == ClientNameType.VeryImportantClient)
            {
                
            }
            else
            {
                user.HasCreditLimit = true;
                creditLimit = _userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
                if (client.Name == ClientNameType.ImportantClient)
                {
                    creditLimit *= 2;
                }
                user.CreditLimit = creditLimit;
            }
            return creditLimit;
        }

        public bool CreditLimitNotSufficient(UserDto user)
        {
            return (user.HasCreditLimit && user.CreditLimit < 500);
        }
    }
}
