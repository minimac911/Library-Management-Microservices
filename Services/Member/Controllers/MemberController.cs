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
        private ILogger<MemberController> _logger;

        public MemberController(MemberContext context, ILogger<MemberController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IList<MemberDTO>> GetAllMembers()
        {
            _logger.LogInformation("Get all memebers");

            List<MemberDTO> memberDTOs = await _context.Members.Select(x => MemberToDTO(x)).ToListAsync();
            
            _logger.LogInformation(memberDTOs.Count+" members found");

            return memberDTOs;
        }

        [HttpPost]
        public async Task<ActionResult<MemberDTO>> CreateMember(MemberDTO memberDto)
        {
            _logger.LogInformation("Create new memeber");

            MemberModel member = new MemberModel
            {
                Name = memberDto.Name,
                Email = memberDto.Email,
            };
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Member "+member.Id+" created");

            return CreatedAtAction(nameof(GetAllMembers), new {id = member.Id}, MemberToDTO(member));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDTO>> GetSingleMember(long id)
        {
            _logger.LogInformation("Get a single member");

            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                _logger.LogWarning("Member with id: "+id+" does not exist");

                return NotFound();
            }

            _logger.LogInformation("Member found");

            return MemberToDTO(member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(long id, MemberDTO memberDto) 
        {
            _logger.LogInformation("Update member: "+id);

            if (id != memberDto.Id)
            {
                _logger.LogError("Member id and uri path id do not match");

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
