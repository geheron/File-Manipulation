using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class FileClassDTO
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required"), StringLength(20, ErrorMessage = "Name length must be less than 20 characters")]
        public string Name { get; set; }

        public IFormFile? File { get; set; }
    }
}
