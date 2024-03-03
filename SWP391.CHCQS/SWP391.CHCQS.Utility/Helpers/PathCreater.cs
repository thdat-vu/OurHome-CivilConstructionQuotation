using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Utility.Helpers
{
    public class PathCreater
    {
        private readonly IWebHostEnvironment _environment;
        public PathCreater(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public string CreateFilePathInRoot(string fileName, string? newFolder = null)
        {
            //lấy đường dẫn staic file có dẫn đến newFolder (nếu có)
            string targetFolder = Path.Combine(_environment.WebRootPath, newFolder?? "");
            //nếu đường dẫn ko tồn tại thì tạo ra
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }
            //cập nhật lại đường dẫn tới fileName
            targetFolder += $"\\{fileName.Trim()}";
            return targetFolder;
        }
       
    }
}
