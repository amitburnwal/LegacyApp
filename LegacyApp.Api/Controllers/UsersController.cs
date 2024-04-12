using LegacyApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LegacyApp.Api.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class UsersController : ControllerBase
	{
        private IClientRepository _clientRepository;
		private IUserService _userService;
		private ICreditServiceBL _creditServiceBL;
		private readonly ILogger<UsersController> _logger;
		public UsersController(ICreditServiceBL creditServiceBL, IUserService userService, IClientRepository clientRepository, ILogger<UsersController> logger)
		{
            _clientRepository = clientRepository;
            _creditServiceBL = creditServiceBL;
			_userService = userService;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser(UserDto request, CancellationToken token)
		{
			try
			{
                //Moved model validation in UI. If given more time I would have used Data Annotation in a DTO object to make validation intrinsic to Models. 
				string modelValidationMsg = ValidateInput(request.Firstname, request.Surname, request.EmailAddress, request.DateOfBirth);
                if (!String.IsNullOrEmpty(modelValidationMsg))
				{
                    _logger.LogError(modelValidationMsg);
                    return BadRequest(modelValidationMsg);
                }
                var client = await _clientRepository.GetByIdAsync(request.ClientId);
                
                if (client.Name == ClientNameType.VeryImportantClient)
                {
                    request.HasCreditLimit = false;
                }
                else
                {
                    request.HasCreditLimit = true;
                    request.CreditLimit = await _creditServiceBL.GetCreditLimit(request.ClientId, request);
                }
                if(_creditServiceBL.CreditLimitNotSufficient(request))
                {
                    _logger.LogError("Insufficient Credit Limit");
                    return BadRequest("Insufficient Credit Limit");
                }
                var result = await _userService.AddUser(request);
				var clientStatus = result.Client?.ClientStatus.ToString() ?? "Unknown";
				return Ok(new { result.Id, result.Firstname, result.Surname, clientStatus, result.CreditLimit, result.EmailAddress });
			}
			catch (Exception ex)
			{
				_logger.LogError("Error in saving User details. Error: " + ex);
				return BadRequest("Error in saving data.");
				throw;
			}
		}
        private string ValidateInput(string firstName, string surname, string email, DateTime dateOfBirth)
        {
			StringBuilder errorMsg = new StringBuilder();
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname))
            {				
                errorMsg.Append("User first name and surname are required.");
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
				errorMsg.Append("User email is invalid.");
            }

            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            if (age < 21)
            {
                errorMsg.Append("User should be older than 21 years.");
            }
			return errorMsg.ToString();
        }
    }
}
