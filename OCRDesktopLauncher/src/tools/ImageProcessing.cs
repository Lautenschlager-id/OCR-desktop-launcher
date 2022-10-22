using System;
using System.IO;
using System.Threading.Tasks;

static class ImageProcessing
{
	public static async Task<string> EncodeImageToBase64(string fileName)
	{
		byte[] image = await File.ReadAllBytesAsync(fileName);
		return Convert.ToBase64String(image);
	}
}