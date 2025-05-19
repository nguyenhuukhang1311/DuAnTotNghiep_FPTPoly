using MenShop_Assignment.Datas;
using MenShop_Assignment.Models.ColorModel;
using MenShop_Assignment.Models.FabricModel;
using MenShop_Assignment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricController : ControllerBase
    {
        private readonly FabricRepository _fabricRepository;

        public FabricController(FabricRepository fabricRepository)
        {
            _fabricRepository = fabricRepository;
        }

        [HttpGet]

        public async Task<List<FabricViewModel>> GetAllFabric()
        {
            return await _fabricRepository.GetAllFabric();
        }

        [HttpGet("{Id}")]
        public async Task<FabricViewModel> GetByIdFabric(int Id)
        {
            return await _fabricRepository.GetByIdFabric(Id);
        }

        [HttpPost]

        public async Task<ActionResult<Fabric>> CreateFabric(FabricCreateUpdateModel fabricCreateUpdateModel)
        {
            var checkColor = await _fabricRepository.GetAllFabric();

            if (checkColor.Any(s => s.Name.ToLower() == fabricCreateUpdateModel.Name.ToLower()))
            {
                return BadRequest(new { message = "Tên Loại đã tồn tại!" });
            }

            var fabric = new Fabric
            {
                Name = fabricCreateUpdateModel.Name,
            };

            var createFabric = await _fabricRepository.CreateFabric(fabric);

            return Ok(new { message = "Success add Loại", data = createFabric });
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateFabricById(int Id, FabricCreateUpdateModel updateFabric)
        {
            var checkFabric = await _fabricRepository.GetAllFabric();

            if (checkFabric.Any(s => s.Name.ToLower() == updateFabric.Name.ToLower()))
            {
                return BadRequest(new { message = "Tên loại đã tồn tại!" });
            }
            // Kiểm tra đối tượng sizeUpdate
            if (updateFabric == null || string.IsNullOrEmpty(updateFabric.Name))
            {
                return BadRequest(new { message = "Dữ liệu cập nhật không hợp lệ" });
            }

            var fabricCheck = await _fabricRepository.GetById(Id);
            if (fabricCheck == null)
            {
                return NotFound(new { message = "Không tìm thấy loại" });
            }

            fabricCheck.Name = updateFabric.Name;

            await _fabricRepository.UpdateFabric(Id, fabricCheck);

            return Ok(new { message = "Cập nhật loại thành công" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFabricById(int id)
        {
            var result = await _fabricRepository.DeleteFabric(id);
            if (!result)
            {
                return NotFound(new { message = "Không tìm thấy loại" });
            }

            return Ok(new { message = "Xóa loại thành công" });
        }
    }
}
