
using Microsoft.AspNetCore.Mvc;
using SimpleEmailApp.Models;
using SimpleEmailApp.Services;

namespace SimpleEmailApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {

        private IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendEmail(EmailDto request)
        {
            _emailService.SendEmail(request);

            return Ok();
        }
    }
}
