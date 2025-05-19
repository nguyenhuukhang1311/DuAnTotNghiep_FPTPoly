using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.ColorModel;
using MenShop_Assignment.Models.SizeModel;
using MenShop_Assignment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly ColorRepository _colorRepository;

        public ColorController(ColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        [HttpGet]

        public async Task<List<ColorViewModel>> GetAllColor()
        {
            return await _colorRepository.GetAllColor();
        }

        [HttpGet("{Id}")]
        public async Task<ColorViewModel> GetByIdColor(int Id)
        {
            return await _colorRepository.GetByIdColor(Id);
        }

        [HttpPost]

        public async Task<ActionResult<Color>> CreateColor(ColorCreateUpdateModel colorCreateUpdateModel)
        {
            var checkColor = await _colorRepository.GetAllColor();

            if (checkColor.Any(s => s.Name.ToLower() == colorCreateUpdateModel.Name.ToLower()))
            {
                return BadRequest(new { message = "Tên size đã tồn tại!" });
            }

            var color = new Color
            {
                Name = colorCreateUpdateModel.Name,
            };

            var createColor = await _colorRepository.CreateColor(color);

            return Ok(new { message = "Success add size", data = createColor });
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateSizeById(int Id, ColorCreateUpdateModel colorUpdate)
        {
            var checkColor = await _colorRepository.GetAllColor();

            if (checkColor.Any(s => s.Name.ToLower() == colorUpdate.Name.ToLower()))
            {
                return BadRequest(new { message = "Tên màu đã tồn tại!" });
            }
            // Kiểm tra đối tượng sizeUpdate
            if (colorUpdate == null || string.IsNullOrEmpty(colorUpdate.Name))
            {
                return BadRequest(new { message = "Dữ liệu cập nhật không hợp lệ" });
            }

            var colorrCheck = await _colorRepository.GetById(Id);
            if (colorrCheck == null)
            {
                return NotFound(new { message = "Không tìm thấy màu" });
            }

            colorrCheck.Name = colorUpdate.Name;

            await _colorRepository.UpdateColor(Id, colorrCheck);

            return Ok(new { message = "Cập nhật màu thành công" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSizeById(int id)
        {
            var result = await _colorRepository.DeleteColor(id);
            if (!result)
            {
                return NotFound(new { message = "Không tìm thấy màu" });
            }

            return Ok(new { message = "Xóa màu thành công" });
        }
    }
}
