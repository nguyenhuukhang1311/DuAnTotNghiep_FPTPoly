using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.SizeModel;
using MenShop_Assignment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
       private readonly SizeRepository _sizeRepository;
        private readonly ApplicationDbContext _context;

        public SizeController(SizeRepository sizeRepository, ApplicationDbContext context)
        {
            _sizeRepository = sizeRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<List<SizeViewModel>> GetAllSize()
        {
            return await _sizeRepository.GetAllSize();
        }
        [HttpGet("{Id}")]
        public async Task<SizeViewModel> GetByIdSize(int Id)
        {
            return await _sizeRepository.GetByIdSize(Id);
        }

        [HttpPost]
        public async Task<ActionResult<Size>> CreateSize(SizeCreateUpdateModel sizeCreateUpdateModel)
        {
            var checkSize = await _sizeRepository.GetAllSize();

            if (checkSize.Any(s => s.Name.ToLower() == sizeCreateUpdateModel.Name.ToLower()))
            {
                return BadRequest(new { message = "Tên size đã tồn tại!" });
            }

            var size = new Size
            {
                Name = sizeCreateUpdateModel.Name,
            };

            var createSize = await _sizeRepository.CreateSize(size);

            return Ok(new { message = "Success add size", data = createSize });
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateSizeById(int Id, SizeCreateUpdateModel sizeUpdate)
        {
            var checkSize = await _sizeRepository.GetAllSize();

            if (checkSize.Any(s => s.Name.ToLower() == sizeUpdate.Name.ToLower()))
            {
                return BadRequest(new { message = "Tên size đã tồn tại!" });
            }
            // Kiểm tra đối tượng sizeUpdate
            if (sizeUpdate == null || string.IsNullOrEmpty(sizeUpdate.Name))
            {
                return BadRequest(new { message = "Dữ liệu cập nhật không hợp lệ" });
            }

            var sizeCheck = await _sizeRepository.GetById(Id);
            if (sizeCheck == null)
            {
                return NotFound(new { message = "Không tìm thấy size" });
            }

            sizeCheck.Name = sizeUpdate.Name;

            await _sizeRepository.UpdateSizeAsync(Id, sizeCheck);

            return Ok(new { message = "Cập nhật size thành công" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSizeById(int id)
        {
            var result = await _sizeRepository.DeleteSize(id);
            if (!result)
            {
                return NotFound(new { message = "Không tìm thấy size" });
            }

            return Ok(new { message = "Xóa size thành công" });
        }

    }
}
