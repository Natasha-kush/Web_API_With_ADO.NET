using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPICRUD.Controllers
{
    // Ye attribute API ka route define karta hai
    // "api/" fixed part hai URL ka
    // [controller] automatically controller ka naam le lega (StudentAPIController -> StudentAPI)
    [Route("api/[controller]")]

    [ApiController]
    public class StudentAPIController : ControllerBase
    {
       
        private readonly StudentDbContext context;
        public StudentAPIController(StudentDbContext context)
        {
            this.context = context;
        }

        
        [HttpGet]

        public async Task<ActionResult<List<Student>>> GetStudents()
        {
           
            var data = await context.Studenttbl.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentsById(int id)
        {
            var student = await context.Studenttbl.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            return student;

        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student std)
        {
            await context.Studenttbl.AddAsync(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, Student std)
        {
            if (id != std.Id)
            {
                return BadRequest();
            }
            context.Entry(std).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(std);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var std = await context.Studenttbl.FindAsync(id);
            if(std == null)
            {
                return NotFound();
            }
            context.Studenttbl.Remove(std);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
