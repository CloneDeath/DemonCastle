using System.Collections.Generic;
using DemonCastle.Files.Variables;

namespace DemonCastle.Files;

public class ProjectFile : IGameFile {
	public int FileVersion => 1;
	public string Name { get; set; } = string.Empty;
	public string Version { get; set; } = string.Empty;
	public string StartScene { get; set; } = string.Empty;

	public List<VariableDeclarationData> Variables { get; set; } = new();
}