using System.Collections.Generic;

namespace DemonCastle.Editor.Files;

public class ProjectPreferencesFile {
	public int ExplorerPanelWidth;
	public List<string> ExpandedDirectories { get; set; } = new();
}