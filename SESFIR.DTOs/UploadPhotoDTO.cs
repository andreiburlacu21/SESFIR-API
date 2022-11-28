using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.DTOs
{
    public class UploadPhotoDTO
    {
        public IFormFile File { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
