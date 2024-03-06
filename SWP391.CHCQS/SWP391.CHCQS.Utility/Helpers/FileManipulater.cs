using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.Model;
using Microsoft.AspNetCore.Hosting;

namespace SWP391.CHCQS.Utility.Helpers
{
	public class FileManipulater<T> where T : class
	{
		private static readonly IWebHostEnvironment _environment;
		//HƯỚNG DẪN SỬ DỤNG - SẢN PHẨM KHÔNG PHẢI LÀ THUỐC - ĐỌC KỸ TRƯỚC KHI SỬ DỤNG
		//lƯU jSON VÀO FILE: lưu 
		public static void SaveJsonToFile(string filePath, T jsonIn, bool append = true)
		{
			//tuần tự hóa theo Json
			var option = new JsonSerializerOptions();
			option.WriteIndented = true;
			string json = JsonSerializer.Serialize<T>( jsonIn, option);
			// Mở tệp tin để ghi
			using (StreamWriter sw = new StreamWriter(filePath, append))
			{
				// Ghi chuỗi JSON vào tệp tin, cùng với dấu xuống dòng để phân biệt giữa các đối tượng
				sw.WriteLine(json);
				sw.Write("\n");
			}
		}
		public static List<T> LoadJsonFromFile(string filePath)
		{
			List<T> list = new List<T>();

			// Kiểm tra xem tệp tin có tồn tại không
			if (!File.Exists(filePath))
			{
				Console.WriteLine("File does not exist.");
				return list;
			}

			// Đọc dữ liệu từ tệp tin
			using (StreamReader reader = new StreamReader(filePath))
			{
				string line = "";
					
				while ((line = reader.ReadLine()) != null)
				{
                    if (line == "")
                        break;
                    string json = "";
                    do
					{
						json += line;
						
						line = reader.ReadLine();
					}
					while (line != "");
                    T obj = JsonSerializer.Deserialize<T>(json);
					list.Add(obj);
					
				}
			}
			return list;
		}
	}
}
