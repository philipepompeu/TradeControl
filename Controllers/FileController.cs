using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradeControl.Domain.Model;
using TradeControl.Domain.Repository;

namespace TradeControl.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Base + "/files")]
    public class FileController : ControllerBase
    {
        private readonly IFileDocumentRepository _fileDocumentRepository;

        public FileController(IFileDocumentRepository fileDocumentRepository)
        {
            _fileDocumentRepository = fileDocumentRepository;
        }
        [HttpPost]        
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Arquivo inválido.");
            if (!FileValidator.IsValid(file, out var errorMessage))
                return BadRequest(errorMessage);

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var compressed = GZipUtils.Compress(memoryStream.ToArray());

            var fileDoc = new FileDocument
            {
                Id = Guid.NewGuid(),
                FileName = file.FileName,
                ContentType = file.ContentType ?? "application/octet-stream",
                Content = compressed
            };


            await _fileDocumentRepository.AddAsync(fileDoc);
            await _fileDocumentRepository.SaveChanges();

            return CreatedAtAction(nameof(GetFile), new { id = fileDoc.Id }, fileDoc.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(Guid id)
        {
            var fileDoc = await _fileDocumentRepository.GetByIdAsync(id);                

            if (fileDoc == null)
                return NotFound();

            var decompressedBytes = GZipUtils.Decompress(fileDoc.Content);

            return File(decompressedBytes, fileDoc.ContentType, fileDoc.FileName);
        }

    }
}
