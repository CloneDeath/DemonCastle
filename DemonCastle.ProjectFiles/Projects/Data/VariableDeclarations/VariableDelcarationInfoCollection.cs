using System.Collections.Generic;
using System.Linq;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;

public class VariableDeclarationInfoCollection : ObservableCollectionInfo<VariableDeclarationInfo, VariableDeclarationData>,
												 IEnumerableInfoByEnum<VariableDeclarationInfo, VariableType>{
	private readonly IFileNavigator _file;
	public VariableDeclarationInfoCollection(IFileNavigator file, List<VariableDeclarationData> data) : base(new VariableDeclarationInfoFactory(file), data) {
		_file = file;
	}

	protected override void Save() => _file.Save();

	public VariableDeclarationInfo AppendNew(VariableType type, string name) {
		var variable = InfoFactory.CreateData(type);
		variable.Name = name;
		return Add(variable);
	}

	public IEnumerable<ItemVariableDeclarationInfo> Items => this.OfType<ItemVariableDeclarationInfo>();
	public IEnumerable<MonsterVariableDeclarationInfo> Monsters => this.OfType<MonsterVariableDeclarationInfo>();
	public IEnumerable<Vector2IVariableDeclarationInfo> Vector2I => this.OfType<Vector2IVariableDeclarationInfo>();
}

public class VariableDeclarationInfoFactory : IInfoFactory<VariableDeclarationInfo, VariableDeclarationData> {
	private readonly IFileNavigator _file;

	public VariableDeclarationInfoFactory(IFileNavigator file) {
		_file = file;
	}

	public VariableDeclarationInfo CreateInfo(VariableDeclarationData data) => InfoFactory.CreateInfo(_file, data);
	public VariableDeclarationData CreateData() => new();
}