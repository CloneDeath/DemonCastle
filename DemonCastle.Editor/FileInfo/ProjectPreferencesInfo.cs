using DemonCastle.Editor.Files;
using DemonCastle.Navigation;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.Editor.FileInfo;

public class ProjectPreferencesInfo : FileInfo<ProjectPreferencesFile> {
	public ProjectPreferencesInfo(FileNavigator<ProjectPreferencesFile> file) : base(file) {}

	public static ProjectPreferencesInfo Load(FileNavigator preferencesFile, ProjectResources resources) {
		return new ProjectPreferencesInfo(new FileNavigator<ProjectPreferencesFile>(preferencesFile, resources));
	}

	public int ExplorerPanelWidth {
		get => Resource.ExplorerPanelWidth;
		set => SaveField(ref Resource.ExplorerPanelWidth, value);
	}
}