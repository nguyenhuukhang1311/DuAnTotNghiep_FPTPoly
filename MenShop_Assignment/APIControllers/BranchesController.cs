using MenShop_Assignment.DTO;
using MenShop_Assignment.Repositories;
using MenShop_Assignment.Repositories.BranchesRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchRepository _branchRepo;

        public BranchesController(IBranchRepository branchRepo)
        {
            _branchRepo = branchRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetBranches()
        {
            var result = await _branchRepo.GetBranchesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranch(int id)
        {
            var result = await _branchRepo.GetBranchByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostBranch(BranchCreateDto dto)
        {
            var result = await _branchRepo.CreateBranchAsync(dto);
            if (result == null)
                return BadRequest("Địa chỉ không hợp lệ hoặc bị thiếu.");
            return CreatedAtAction(nameof(GetBranch), new { id = result.BranchId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBranch(int id, BranchUpdateDto dto)
        {
            var success = await _branchRepo.UpdateBranchAsync(id, dto);
            if (!success)
                return BadRequest("Không thể cập nhật. Địa chỉ không hợp lệ hoặc không tìm thấy chi nhánh.");
            return NoContent();
        }
    }
}
