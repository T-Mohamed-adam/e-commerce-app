using Microsoft.AspNetCore.Mvc;
using TagerProject.ServiceContracts;

namespace TagerProject.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService) 
        {
            _emailService = emailService;
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(string receptor, string subject, string body)
        {
            await _emailService.SendEmail(receptor, subject, body);

            return Ok();
        }
    }
}
