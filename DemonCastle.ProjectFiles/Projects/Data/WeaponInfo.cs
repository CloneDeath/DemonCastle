using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class WeaponInfo : FileInfo<WeaponFile>, IListableInfo {
	public WeaponInfo(FileNavigator<WeaponFile> file) : base(file) {
		Animations = new AnimationInfoCollection(file, Resource.Animations);
	}

	public string ListLabel => Name;

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(ListLabel);
		}
	}

	public AnimationInfoCollection Animations { get; }
}