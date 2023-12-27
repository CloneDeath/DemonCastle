namespace DemonCastle.ProjectFiles.Files.Common;

public class Size {
	public Size() : this(16, 16) {}

	public Size(int width, int height) {
		Width = width;
		Height = height;
	}

	public int Width { get; set; }
	public int Height { get; set; }
}