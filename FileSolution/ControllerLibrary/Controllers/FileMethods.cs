using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Models.FileModel;
using Data.DbData;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using ControllerLibrary.ControllerInterface;
using Models.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ControllerLibrary.Controllers
{
    public class FileMethods : Controller, IFileMethods
    {
        public async Task<IActionResult> UploadFile(FileClassDTO fileDTO, ApplicationDbContext db)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await fileDTO.File.CopyToAsync(ms);
                    byte[] data = ms.ToArray();
                    db.files.Add(new FileClass()
                    {
                        Id = fileDTO.Id,
                        Name = fileDTO.Name,
                        FileContents = Convert.ToBase64String(data)
                    });
                    await db.SaveChangesAsync();
                }              
                return Ok("File has been stored in database");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<ActionResult<IEnumerable<FileClassDTO>>> GetFiles(ApplicationDbContext db)
        {
            try
            {

                var files = await db.files.Select(x => FileToDTO(x)).ToListAsync();
                return files;

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /*
        public async Task<ActionResult<FileClassDTO>> GetFileById(int id, ApplicationDbContext db)
        {
            try
            {
                var file = await db.files.FindAsync(id);
                if (file != null)
                {
                    var fileDTO = FileToDTO(file);
                    return fileDTO;
                }
                else
                {
                    return NotFound($"File with id=${id} is not present.");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        */

        public async Task<IActionResult> DownloadFile(string filename, ApplicationDbContext db)
        {
            try
            {
                var entity = await db.files.FirstOrDefaultAsync(files => files.Name == filename);
                
                if (entity == null)
                {
                    return NotFound("File not found");
                }

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filename, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var bytes = Convert.FromBase64String(entity.FileContents);
                return File(bytes, contentType, filename);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        public async Task<IActionResult> DeleteFile(string filename, ApplicationDbContext db)
        {
            try
            {
                var entity = await db.files.FirstOrDefaultAsync(files => files.Name == filename);
                if (entity == null)
                {
                    return NotFound("File not found");
                }
                db.files.Remove(entity);
                db.SaveChanges();
                return Ok("File has been deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        public async Task<IActionResult> PutFile(int id, FileClassDTO fileDTO, ApplicationDbContext db)
        {

            if (id != fileDTO.Id)
            {
                return BadRequest("Invalid Id");
            }

            var file = await db.files.FindAsync(id);
            if (file == null)
            {
                return NotFound("Id not found");
            }

            file.Name = fileDTO.Name;
            

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

            return Ok("Updated");
        }

        private bool fileExists(int id, ApplicationDbContext db)
        {
            return db.files.Any(e => e.Id == id);
        }

        private static FileClassDTO FileToDTO(FileClass fileClass) =>
        new FileClassDTO
        {
            Id = fileClass.Id,
            Name = fileClass.Name,
        };
    }
}
