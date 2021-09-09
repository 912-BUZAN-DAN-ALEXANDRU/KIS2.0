using KIS.Managers;
using KIS.Models;
using KIS.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KIS.Controllers
{
    public class UserController : Controller
    {
        private UnitOfWork _unitOfWork;

        public UserController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register()
        {
            var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            var user = JsonConvert.DeserializeObject<User>(body);
            user.Id = Guid.NewGuid();


            if (!_unitOfWork.userManager.AddUser(user))
            {
                var result = Content("User already exists!");
                result.StatusCode = (int)HttpStatusCode.Conflict;

                return result;
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("Login")]

        public async Task<ActionResult> Login()
        {
            var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            var loginCredentials = JsonConvert.DeserializeObject<LoginModel>(body);

            var response = _unitOfWork.userManager.Login(loginCredentials);

            if (response == null)
            {
                var result = Content("Username or password is incorrect");
                result.StatusCode = (int)HttpStatusCode.Unauthorized;

                return result;
            }


            return Ok(response);
        }

        [HttpDelete]
        [Route("User/Delete/{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {

            if (!_unitOfWork.userManager.DeleteUser(id))
                return Content("User does not exist!");
            return Ok();
        }
    }
}
