using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUploadControl
{
    public interface IUploadableFile
    {
        void UploadFile(IFormFile file, String folder);
    }
}
