using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NET_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        [HttpGet]
        public IActionResult getAllStudent()
        {
            string[] studentsName = new string[] { "bao", "binh", "Chi", "Duong" };
            return Ok(studentsName);
        }
    }
}
