using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using shared;
using shared.DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserDetailsController : ControllerBase
    {
        private readonly ILogger<UserDetailsController> _logger;

        private readonly IStorageService storageMan;

        public UserDetailsController(ILogger<UserDetailsController> logger, IStorageService storageService) : base()
        {
           _logger = logger;
            storageMan = storageService;
            _logger.LogDebug("Storage manager initialised");

        }

        [HttpPost]
        public async Task<ActionResult> Post(UserTableEntity user)
        {
            _logger.LogDebug("Post() Saving details {0} {1}", user.name, user.email);
            string responseMessage = "";
            // write to az storage
            var tableEntity = new UserTableEntity(user.name, user.email);
            await storageMan.WriteTableData(tableEntity, "User");
            responseMessage = "User successfully saved";
            return Ok(responseMessage);
        }

        [HttpGet]
        public async Task<ActionResult> Get(string id = "")
        {
            _logger.LogDebug("Getting user: " + (string.IsNullOrEmpty(id) ? "all" : id ));
            // retrieve from az storage
            var result = await storageMan.GetEntity<UserTableEntity>("User", id, UserTableEntity.Columns);
            // TODO cast to public facing Model
            _logger.LogDebug("Get() " + result.Count + " User(s) successfully retrieved");
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            await storageMan.DeleteEntityAsync("User", id.Split(';')[0], id.Split(';')[1]);
            return Ok("User id: " + id + " deleted");
        }
    }
}