using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class ItemInfo : BaseEntityInfo<ItemFile>, IFileInfo {
	public ItemInfo(FileNavigator<ItemFile> file) : base(file, file.Resource) {
		OnPickup = new ItemActionInfoCollection(file, Data.OnPickup);
	}

	public float MoveSpeed {
		get => Data.MoveSpeed;
		set => SaveField(ref Data.MoveSpeed, value);
	}

	public float Gravity {
		get => Data.Gravity;
		set => SaveField(ref Data.Gravity, value);
	}

	public ItemActionInfoCollection OnPickup { get; }

	public string FileName => File.FileName;
	public string Directory => File.Directory;
	void IFileInfo.Save() => base.Save();
}