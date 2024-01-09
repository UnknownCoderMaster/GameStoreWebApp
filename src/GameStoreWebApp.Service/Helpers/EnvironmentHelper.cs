using System.IO;

namespace GameStoreWebApp.Service.Helpers;

public class EnvironmentHelper
{
	public static string WebRootPath { get; set; }
	public static string AttachmentPath => Path.Combine(WebRootPath, "images");
	public static string GameFilePath => Path.Combine(WebRootPath, "games");
    public static string FilePath => "images";
}
