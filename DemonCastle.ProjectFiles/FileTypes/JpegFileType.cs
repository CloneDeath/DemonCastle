namespace DemonCastle.ProjectFiles.FileTypes;

public class JpegFileType : IFileType {
	public string Name => "JPEG Image";
	public string Extension => ".jpeg";
	public string Filter => "*.jpeg; JPEG Image";
}