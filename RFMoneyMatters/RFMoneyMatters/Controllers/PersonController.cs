using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.DTOs;
using RFMoneyMatters.Models;

namespace RFMoneyMatters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly RaiDbContext _context;
        private readonly IMapper _mapper;

        public PersonController(RaiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons()
        {
            var persons = await _context.Persons
                .Include(p => p.Goals)
                .Include(e => e.Expenses)
                .ToListAsync();

            var dtoList = _mapper.Map<List<PersonDto>>(persons);
            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetPerson(int id)
        {
            var person = await _context.Persons
                .Include(p => p.Goals)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
                return NotFound();

            var dto = _mapper.Map<PersonDto>(person);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> PostPerson(CreatePersonDto personDto)
        {
            var person = new Person
            {
                ClerckId = personDto.ClerckId,
                Name = personDto.Name,
                Age = personDto.Age,

                Streak = 0,
                CurrentStreak = 0,
                LongestStreak = 0,
                LastActiveDate = null,
                Coins = 0,

                Goals = new List<Goal>()
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<PersonDto>(person);

            return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, UpdatePersonDto dto)
        {
            if (dto == null)
                return BadRequest("Payload cannot be null!");

            var person = await _context.Persons
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
                return NotFound($"Person {id} not found.");

            person.Name = dto.Name ?? person.Name;
            person.Age = dto.Age ?? person.Age;
            person.Streak = dto.Streak ?? person.Streak;
            person.CurrentStreak = dto.CurrentStreak ?? person.CurrentStreak;
            person.LongestStreak = dto.LongestStreak ?? person.LongestStreak;
            person.LastActiveDate = dto.LastActiveDate ?? person.LastActiveDate;
            person.Coins = dto.Coins ?? person.Coins;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
                return NotFound();

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}