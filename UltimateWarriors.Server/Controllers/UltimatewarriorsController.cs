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
    }
}
