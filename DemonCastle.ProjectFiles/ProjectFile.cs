using System.Collections.Generic;

namespace DemonCastle.ProjectFiles; 

public class ProjectFile {
	public string Name { get; set; } = string.Empty;
	public string Version { get; set; } = string.Empty;
	public List<string> Levels { get; set; } = new();
}