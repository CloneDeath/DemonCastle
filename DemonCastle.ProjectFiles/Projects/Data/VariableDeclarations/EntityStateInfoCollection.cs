using System.Collections.Generic;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;

public class VariableDeclarationInfoCollection : ObservableCollectionInfo<VariableDeclarationInfo, VariableDeclarationData> {
	private readonly IFileNavigator _file;
	public VariableDeclarationInfoCollection(IFileNavigator file, List<VariableDeclarationData> data) : base(new VariableDeclarationInfoFactory(file), data) {
		_file = file;
	}

	protected override void Save() => _file.Save();
}

public class VariableDeclarationInfoFactory : IInfoFactory<VariableDeclarationInfo, VariableDeclarationData> {
	private readonly IFileNavigator _file;

	public VariableDeclarationInfoFactory(IFileNavigator file) {
		_file = file;
	}

	public VariableDeclarationInfo CreateInfo(VariableDeclarationData data) => new(_file, data);
	public VariableDeclarationData CreateData() => new();
}