using Microsoft.AspNetCore.Mvc;

namespace MenShop_Assignment.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        [HttpPost]
        public IActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Chưa có tệp nào được tải lên.");
                }

                // Kiểm tra loại tệp (ví dụ: chỉ cho phép ảnh)
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (Array.IndexOf(allowedExtensions, extension) < 0)
                {
                    return BadRequest("Loại tệp không hợp lệ. Chỉ cho phép các loại .jpg, .jpeg, .png, .gif.");
                }

                // Tạo thư mục lưu trữ nếu không tồn tại
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                var originalFileName = Path.GetFileName(file.FileName); // tránh lỗi FormatException
                var fileName = $"{Guid.NewGuid()}_{originalFileName}";
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                // Lưu tệp vào đĩa
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Trả về đường dẫn tệp đã tải lên
                return Ok(new { FilePath = dbPath });
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi tải lên tệp: {ex.Message}");
            }
        }
    }
}
