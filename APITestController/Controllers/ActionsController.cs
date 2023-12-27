using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APITestController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        [HttpGet]
        [Route("GetValue")]
        public IActionResult GetValue()
        {
            return Ok(new { value = 10 });
        }
        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList()
        {
            List<int> list = new List<int>() { 1, 2, 3 };
            return Ok(new { value = list });
        }
        [HttpGet]
        [Route("GetObject")]
        public IActionResult GetObject()
        {
            return Ok(new
            {
                value = new Student
                {
                    Description = "Test",
                    Id = 1,
                    Name = "Irfan"
                }
            });
        }

        [HttpGet]
        [Route("GetObjectList")]
        public IActionResult GetObjectList()
        {
            List<Student> list = new List<Student>() { GetStudentObject(), GetStudentObject(), GetStudentObject()};
            return Ok(new {value = list});
        }











        private Student GetStudentObject()
        {
            var number = new Random().Next(100, 1000);
            return new Student
            {
                Description = "Test" + number,
                Id = number,
                Name = "Name" + number
            };
        }
    }
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
