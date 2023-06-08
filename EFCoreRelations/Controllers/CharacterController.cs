using EFCoreRelations.DB;
using EFCoreRelations.DTO;
using EFCoreRelations.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DemoDbContext _dbContext;
        public CharacterController(DemoDbContext demoDbContext)
        {
            _dbContext=demoDbContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetCharacter(int userId) {

            var chars = await _dbContext.Character
                    .Where(x => x.UserId == userId)
                    .Include(x => x.Weapon)
                    .Include(y => y.Skill)
                    .ToListAsync();

            return chars;
        
        }


        [HttpPost]
        public async Task<ActionResult<List<Character>>> AddCharacter(CreateCharaterDTO request)
        {
            var user = await _dbContext.User.FindAsync(request.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var newCharacter = new Character
            {

                Name = request.Name,
                RpgClass = request.RgpClass,
                User = user,
            };

            _dbContext.Character.Add(newCharacter);
            await _dbContext.SaveChangesAsync();

            return await GetCharacter(newCharacter.UserId);

        }


        [HttpPost("Weapon")]
        public async Task<ActionResult<Character>> AddWeapon(AddWeaponDTO request)
        {
            var character = await _dbContext.Character.FindAsync(request.CharacterId);

            if (character == null)
            {
                return NotFound();
            }

            var newWeapon = new Weapon
            {

                Name = request.Name,
                Damage = request.Damage,
                Character = character,
            };

            _dbContext.Weapon.Add(newWeapon);
            await _dbContext.SaveChangesAsync();

            return character;

        }



        [HttpPost("Skill")]
        public async Task<ActionResult<Character>> AddSkill(AddCharacterSkillDTO request)
        {
            var character = await _dbContext.Character
                .Where(c=>c.Id == request.CharacterId)
                .Include(c => c.Skill)
                .FirstOrDefaultAsync();

            if (character == null)
            {
                return NotFound();
            }

            var skill = await _dbContext.Skill.FindAsync(request.SkillId);


            if (skill == null)
            {
                return NotFound();
            }

            character.Skill.Add(skill);
            await _dbContext.SaveChangesAsync();

            return character;

        }



    }
}
