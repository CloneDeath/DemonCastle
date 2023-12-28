using System;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Files.Elements.Types;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

public class LabelElementInfo : BaseInfo<LabelElementData>, IElementInfo {
	public LabelElementInfo(IFileNavigator file, LabelElementData data) : base(file, data) {

	}

	public Guid Id => Data.Id;
	public ElementType Type => Data.Type;

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
		}
	}
}