using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPIC_.Data;
using webAPIC_.Models;

namespace webAPIC_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;
        public UserController(UserDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Index()
        {
            return Ok(await _context.User.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<User>>> Find(int id)
        {

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return BadRequest("NotFound");
            }
            return Ok(user);
        }


             [HttpPost]
        public async Task<ActionResult<List<User>>> Store(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

                 return Ok(_context.User.ToListAsync());
             }


        [HttpPut]
        public async Task<ActionResult<List<User>>> Update(User request)
        {
            var user = await _context.User.FindAsync(request.UserId);
            if (user == null)
                return BadRequest("Not Found");


            user.Name = request.Name;
            user.Email = request.Email;

            await _context.SaveChangesAsync();

            return Ok(await _context.User.ToListAsync());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
                return BadRequest("Not Found");
            
                _context.User.Remove(user);
            
            await _context.SaveChangesAsync();
            return Ok(await _context.User.ToListAsync());
        }


    }
}
