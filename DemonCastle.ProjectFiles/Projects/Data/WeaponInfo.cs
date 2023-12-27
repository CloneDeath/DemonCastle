using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class WeaponInfo : FileInfo<WeaponFile>, IListableInfo {
	public WeaponInfo(FileNavigator<WeaponFile> file) : base(file) {
		Animations = new AnimationInfoCollection(file, Resource.Animations);
	}

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public AnimationInfoCollection Animations { get; }
}