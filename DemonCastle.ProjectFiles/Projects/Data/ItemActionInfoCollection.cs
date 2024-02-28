using System.Collections.Generic;
using DemonCastle.Files.Actions;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class ItemActionInfoCollection : ObservableCollectionInfo<ItemActionInfo, ItemActionData> {
	private readonly IFileNavigator _file;

	public ItemActionInfoCollection(IFileNavigator file, List<ItemActionData> actions) : base(new ItemActionInfoFactory(file), actions) {
		_file = file;
	}

	protected override void Save() => _file.Save();

	public void Execute(IGameState game) {
		foreach (var action in this) {
			action.Execute(game);
		}
	}
}

public class ItemActionInfoFactory : IInfoFactory<ItemActionInfo, ItemActionData> {
	private readonly IFileNavigator _file;

	public ItemActionInfoFactory(IFileNavigator file) {
		_file = file;
	}

	public ItemActionInfo CreateInfo(ItemActionData data) => new(_file, data);

	public ItemActionData CreateData() => new();
}