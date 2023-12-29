using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

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

        [HttpGet]
        [Route("GetNextNumber")]
        public IActionResult GetNextNumber(int number)
        {
            return Ok(new {value = number+1});
        }

        [HttpGet]
        [Route("GetSortedList")]
        public IActionResult GetSortedList([FromQuery] string numbers)
        {
            var list = JsonConvert.DeserializeObject<List<int>>(numbers);
            var ordered = list.OrderBy(s=>s).ToList();
            return Ok(new {value = ordered });
        }

        [HttpGet]
        [Route("GetNormalizedStudent")]
        public IActionResult GetNormalizedStudent([FromQuery] string student, [FromHeader] string action)
        {
            var studentObject = JsonConvert.DeserializeObject<Student>(student);
            if(studentObject == null) { return BadRequest(); }
            studentObject.Name = action == "1"? studentObject.Name.ToUpper() : studentObject.Name.ToLower();
            studentObject.Description = action == "1" ? studentObject.Description.ToUpper() : studentObject.Name.ToLower();
            return Ok(new {  value = studentObject});
        }

        [HttpPost]
        [Route("SaveNumber")]
        public IActionResult SaveNumber([FromBody] int number)
        {
            return Ok(new { value = number });
        }

        [HttpPost]
        [Route("SaveNumberList")]
        public IActionResult SaveNumberList([FromBody] List<int> numbers)
        {
            return Ok(new {value = numbers});
        }

        [HttpPost]
        [Route("SaveObject")]
        public IActionResult SaveObject([FromBody] Student student)
        {
            return Ok(new {value = student});
        }
        [HttpPost]
        [Route("SaveObjectList")]
        public IActionResult SaveObjectList([FromBody] List<Student> students)
        {
            return Ok(new {value = students});
        }

        [HttpDelete]
        [Route("DeleteStudent")]
        public IActionResult DeleteStudent([FromQuery] int id)
        {
            return Ok(new {value = id});
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
