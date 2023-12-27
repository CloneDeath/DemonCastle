using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class SceneInfo : FileInfo<SceneFile> {
	public SceneInfo(FileNavigator<SceneFile> file) : base(file) {
		Elements = new ElementInfoCollection(file, Resource.Elements);
	}

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public ElementInfoCollection Elements { get; }
}