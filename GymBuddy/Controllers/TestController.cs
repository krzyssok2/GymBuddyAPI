using GymBuddyAPI.Entities;
using GymBuddyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymBuddyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private GymBuddyContext _context;
        public TestController(GymBuddyContext gymBuddyContext)
        {
            _context = gymBuddyContext;
        }
        // GET: api/<TestController>
        [HttpPost]
        public IActionResult PostEntity(TestEntity testEntity)
        {
            var item = _context.TestEntity.Add(testEntity);
            _context.SaveChanges();

            return Ok(item);
        }


        [HttpGet]
        public IActionResult GetEntity()
        {
            var items = _context.TestEntity.Select(i=> new ModelTest
            {
                Age=i.Age,
                Name= i.Name,
                Number= i.Number,
                Surname= i.Surname
            });

            return Ok(items);
        }



    }
}
