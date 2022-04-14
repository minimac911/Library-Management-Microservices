using Member.Data;
using Member.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Member.Controllers
{
    [ApiController]
    [Route("api/v1/members")]
    public class MemberController : Controller
    {
        private readonly MemberContext _context;

        public MemberController(MemberContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IList<MemberDTO>> GetAllMembers()
        {
            return await _context.Members.Select(x => MemberToDTO(x)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<MemberDTO>> CreateMember(MemberDTO memberDto)
        {
            MemberModel member = new MemberModel
            {
                Name = memberDto.Name,
                Email = memberDto.Email,
            };
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllMembers), new {id = member.Id}, MemberToDTO(member));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDTO>> GetSingleMember(long id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return MemberToDTO(member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(long id, MemberDTO memberDto) 
        {
            if (id != memberDto.Id)
            {
                return BadRequest();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            member.Name = memberDto.Name;
            member.Email = memberDto.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!MemberExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberExists(long id)
        {
            return _context.Members.Any(e => e.Id == id);
        }

        private static MemberDTO MemberToDTO(MemberModel member) => new MemberDTO
        {
            Id = member.Id,
            Email = member.Email,
            Name = member.Name
        };
    }
}
