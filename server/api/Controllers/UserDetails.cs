using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserDetailsController : ControllerBase
    {
        private readonly ILogger<UserDetailsController> _logger;

        public UserDetailsController(ILogger<UserDetailsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [HttpGet]
        public void Details(string name, string email)
        {
            _logger.LogInformation("Saving details {0} {1}", name, email);
            // TODO write to az storage
        }
    }
}