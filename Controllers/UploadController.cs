using Microsoft.AspNetCore.Mvc;

namespace ResumeAnalyzer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly TextExtractorService _textExtractorService;

        public UploadController(TextExtractorService textExtractorService)
        {
            _textExtractorService = textExtractorService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var extractedText = await _textExtractorService.ExtractTextAsync(file);

            return Ok(new { extractedText });
        }
    }
}
