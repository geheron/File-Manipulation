using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.FileModel;
using Data.DbData;
using Microsoft.Identity.Client;
using ControllerLibrary.ControllerInterface;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Microsoft.AspNetCore.Cors;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileMethods _obj;
        public FileController(ApplicationDbContext db, IFileMethods obj)
        {
            _db = db;
            _obj = obj;
        }


        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile(FileClassDTO fileDTO)
        {
            if(fileDTO.File == null)
            {
                return BadRequest("File not submitted");
            }
            return await _obj.UploadFile(fileDTO, _db);
        }

        [HttpGet]
        [Route("GetFiles")]
        public async Task<ActionResult<IEnumerable<FileClassDTO>>> GetFiles()
        {
            return await _obj.GetFiles(_db);
        }

        /*
        [HttpGet]
        [Route("GetFileById")]
        public async Task<ActionResult<FileClassDTO>> GetFileById(int id)
        {
            return await _obj.GetFileById(id, _db);
        }
        */

        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            return await _obj.DownloadFile(filename, _db);
        }

        [HttpDelete]
        [Route("DeleteFile")]
        public async Task<IActionResult> DeleteFile(string filename)
        {
            return await _obj.DeleteFile(filename, _db);
        }

        [HttpPut]
        [Route("UpdateFile")]
        public async Task<IActionResult> PutTodoItem(int id, FileClassDTO fileDTO)
        {
            return await _obj.PutFile(id, fileDTO, _db);
        }

        private bool fileExists(long id)
        {
            return _db.files.Any(e => e.Id == id);
        }
    }
}
