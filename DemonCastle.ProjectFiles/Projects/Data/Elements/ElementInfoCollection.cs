using System.Collections.Generic;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Elements;

public class ElementInfoCollection : ObservableCollectionInfo<IElementInfo, ElementData>, IEnumerableInfoByEnum<IElementInfo, ElementType> {
	private readonly IFileNavigator _file;
	public ElementInfoCollection(IFileNavigator file, List<ElementData> data) : base(new ElementInfoDataFactory(file), data) {
		_file = file;
	}

	protected override void Save() => _file.Save();

	public IElementInfo AppendNew(ElementType type) {
		var elementData = InfoFactory.CreateElementTypeData(type);
		return Add(elementData);
	}
}

public class ElementInfoDataFactory : IInfoFactory<IElementInfo, ElementData> {
	private readonly IFileNavigator _file;

	public ElementInfoDataFactory(IFileNavigator file) {
		_file = file;
	}

	public IElementInfo CreateInfo(ElementData data) => InfoFactory.CreateInfo(_file, data);
	public ElementData CreateData() => new();
}