using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Models.FileModel 
{
    public class FileClass 
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required"), StringLength(20, ErrorMessage = "Name length must be less than 20 characters")]
        public string Name { get; set; }
        
        public string FileContents { get; set; }

    }
}
