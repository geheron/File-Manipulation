using Data.DbData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.FileModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTO;

namespace ControllerLibrary.ControllerInterface
{
    public interface IFileMethods
    {
        Task<IActionResult> UploadFile(FileClassDTO fileDTO, ApplicationDbContext db);
        Task<ActionResult<IEnumerable<FileClassDTO>>> GetFiles(ApplicationDbContext db);
        //Task<ActionResult<FileClassDTO>> GetFileById(int id, ApplicationDbContext db);
        Task<IActionResult> DownloadFile(string filename, ApplicationDbContext db);
        Task<IActionResult> DeleteFile(string filename, ApplicationDbContext db);
        Task<IActionResult> PutFile(int id, FileClassDTO fileDTOs, ApplicationDbContext db);
    }
}
