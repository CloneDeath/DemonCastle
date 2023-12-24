namespace DemonCastle.ProjectFiles.FileTypes;

public class WavFileType : IFileType {
	public string Name => "WAV Audio";
	public string Extension => ".wav";
	public string Filter => "*.wav; Microsoft WAV Audio";
}