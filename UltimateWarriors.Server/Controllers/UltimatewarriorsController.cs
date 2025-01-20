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

        [HttpGet]
        public async Task<IActionResult> GetWarriors()
        {
            var warriors = await _repository.GetAllWarriorsAsync();
            return Ok(warriors);
        }

        [HttpGet("weapons")]
        public async Task<IActionResult> GetWeapons()
        {
            var weapons = await _repository.GetAllWeaponsAsync();
            return Ok(weapons);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWarrior([FromBody] Warriors warrior)
        {
            if (warrior == null || string.IsNullOrEmpty(warrior.Name))
            {
                return BadRequest("Warrior data is invalid.");
            }

            var warriorId = await _repository.InsertWarriorAsync(warrior);
            return CreatedAtAction(nameof(GetWarriors), new { id = warriorId }, warrior);
        }

        [HttpPost("weapons")]
        public async Task<IActionResult> CreateWeapon([FromBody] Weapons weapon)
        {
            if (weapon == null || string.IsNullOrEmpty(weapon.Name))
            {
                return BadRequest("Weapon data is invalid.");
            }

            var weaponId = await _repository.InsertWeaponAsync(weapon);
            return CreatedAtAction(nameof(GetWeapons), new { id = weaponId }, weapon);
        }

        [HttpPost("warrior-weapon")]
        public async Task<IActionResult> AssociateWarriorWithWeapon([FromBody] WarriorWeapon warriorWeapon)
        {
            if (warriorWeapon == null || warriorWeapon.WarriorId <= 0 || warriorWeapon.WeaponId <= 0)
            {
                return BadRequest("WarriorWeapon data is invalid.");
            }

            await _repository.InsertWarriorWeaponAsync(warriorWeapon);
            return Ok("Warrior associated with weapon successfully.");
        }
    }
}
