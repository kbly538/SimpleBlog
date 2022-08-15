using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using PhotoSauce.MagicScaler;

namespace SimpleBlog.Data.FileManager
{
	public class FileManager : IFileManager
	{
		private string _imagePath;
		public FileManager(IConfiguration config)
		{
			_imagePath = config["Path:Images"];
		}
		
		public FileStream ImageStream(string image)
		{
			try
			{
				return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);

			} catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			return null;
		}

		public bool RemoveImage(string image)
		{

			try {
				var file = Path.Combine(_imagePath, image);
				if (File.Exists(file))
				{
					File.Delete(file);
				}

				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}

		}

		public async Task<string> SaveImage(IFormFile image)
		{
			try
			{
				var savingPath = Path.Combine(_imagePath);
				if (!Directory.Exists(savingPath))
				{
					Directory.CreateDirectory(savingPath);
				}

				var mime = image.FileName.Substring(image.FileName.LastIndexOf("."));
				var fileName = $"img_{DateTime.Now:dd-MM-yyyy-HH-mm-ss}{mime}";

				using (var fileStream = new FileStream(Path.Combine(savingPath, fileName), FileMode.Create))
				{
					await Task.Run(()=> MagicImageProcessor.ProcessImage(image.OpenReadStream(), fileStream, ImageOptions()));

				}


				return fileName;
			} catch (Exception e)
			{
				Console.Write(e.Message);
				return "Error loading the image.";
			}
		}

		private ProcessImageSettings ImageOptions()
		{
			ProcessImageSettings processImageSettings = new ProcessImageSettings
			{
				Width = 801,
				Height = 500,
				ResizeMode = CropScaleMode.Crop,
				EncoderOptions = new JpegEncoderOptions(100, ChromaSubsampleMode.Subsample420, false)


			};

			processImageSettings.TrySetEncoderFormat(ImageMimeTypes.Jpeg);

			return processImageSettings;

		}

			
	}
}
