using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TimeSheet.Models;
using TimeSheet.Repsoitories;

namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly UserRepo _userRepo;
        public UsersController(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<List<UserModels>> Get()
        {
            return await _userRepo.GetAll();
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<UserModels>> Get(int Id)
        {
            var user = await _userRepo.GetById(Id);
            if (user == null) { return NotFound(); }
            return user;
        }

        [HttpPost]
        public async Task Post([FromBody] UserModels userModels)
        {
            await _userRepo.Insert(userModels);
        }

        [HttpPut]
        public async Task Put([FromBody] UserModels userModels, int Id)
        {
            if (Id != userModels.ID)
            {
                BadRequest();
            }
            try
            {
                await _userRepo.Update(userModels);
            }
            catch (Exception)
            {
                throw;
            }

            NoContent();
        }


        [HttpDelete("{Id}")]

        public async Task Delete(int Id)
        {
            await _userRepo.DeleteById(Id);
        }

    }
}
