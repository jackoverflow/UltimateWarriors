using Microsoft.AspNetCore.Mvc;
using UltimateWarriors.Server.Models;
using UltimateWarriors.Server.Repositories;

namespace UltimateWarriors.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UltimatewarriorsController : ControllerBase
    {
        private readonly IUltimateWarriorsRepository _repository;

        public UltimatewarriorsController(IUltimateWarriorsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("warriors")]
        public async Task<IActionResult> GetWarriors()
        {
            var warriors = await _repository.GetAllWarriors();
            return Ok(warriors);
        }

        [HttpGet("weapons")]
        public async Task<IActionResult> GetWeapons()
        {
            var weapons = await _repository.GetAllWeapons();
            return Ok(weapons);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWarrior([FromBody] Warrior warrior)
        {
            if (warrior == null || string.IsNullOrEmpty(warrior.Name))
            {
                return BadRequest("Warrior data is invalid.");
            }

            var createdWarrior = await _repository.CreateWarrior(warrior);
            return CreatedAtAction(nameof(GetWarriors), new { id = createdWarrior.Id }, createdWarrior);
        }

        // Removed the duplicate CreateWarrior method

        [HttpPost("weapons")]
        public async Task<IActionResult> CreateWeapon([FromBody] Weapon weapon)
        {
            if (weapon == null || string.IsNullOrEmpty(weapon.Name))
            {
                return BadRequest("Weapon data is invalid.");
            }

            var createdWeapon = await _repository.CreateWeapon(weapon);
            return CreatedAtAction(nameof(GetWeapons), new { id = createdWeapon.Id }, createdWeapon);
        }

        [HttpPost("warrior-weapon")]
        public async Task<IActionResult> AssociateWarriorWithWeapon([FromBody] WarriorWeapon warriorWeapon)
        {
            if (warriorWeapon == null || warriorWeapon.WarriorId <= 0 || warriorWeapon.WeaponId <= 0)
            {
                return BadRequest("WarriorWeapon data is invalid.");
            }

            await _repository.AssociateWarriorWithWeapon(warriorWeapon);
            return Ok("Warrior associated with weapon successfully.");
        }

        [HttpGet("warriors/{id}")]
        public async Task<ActionResult<Warrior>> GetWarriorById(int id)
        {
            var warrior = await _repository.GetWarriorById(id);
            if (warrior == null)
                return NotFound();
            return Ok(warrior);
        }

        [HttpDelete("warriors/{id}")]
        public async Task<IActionResult> DeleteWarrior(int id)
        {
            await _repository.DeleteWarrior(id);
            return NoContent();
        }

        [HttpPost("create-warrior-with-weapons")]
        public async Task<IActionResult> CreateWarriorWithWeapons([FromBody] CreateWarriorWithWeaponsDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Name))
            {
                return BadRequest("Warrior data is invalid.");
            }

            var createdWarrior = await _repository.CreateWarriorWithWeapons(dto);
            return CreatedAtAction(nameof(GetWarriorById), new { id = createdWarrior.Id }, createdWarrior);
        }
    }
}