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
        /// <summary>
        /// hàm tạo ra đường dẫn tới fileName được đưa vào, không có thì tạo ra rồi trả về đường dẫn tới file đó
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="newFolder"></param>
        /// <returns></returns>
        public string CreateFilePathInRoot(string fileName, string? newFolder = null)
        {
            //lấy đường dẫn staic file có dẫn đến newFolder (nếu có)
            string targetFolder = Path.Combine(_environment.WebRootPath, newFolder?? "");
            //cập nhật lại đường dẫn tới fileName
            //targetFolder += $"\\{fileName.Trim()}";
            //nếu đường dẫn ko tồn tại thì tạo ra, file cũng vậy
            if (!File.Exists(targetFolder + $"\\{fileName.Trim()}"))
            {
                //Directory.CreateDirectory(targetFolder);
                //File.Open(targetFolder, FileMode.Create, FileAccess.ReadWrite);
                using (File.Create(targetFolder + $"\\{fileName.Trim()}")) ;
            }
            return targetFolder + $"\\{fileName.Trim()}";
        }
       
    }
}
