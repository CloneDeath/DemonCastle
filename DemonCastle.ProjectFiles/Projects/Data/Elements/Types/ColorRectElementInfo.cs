using System;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

public class ColorRectElementInfo : BaseInfo<ColorRectElementData>, IElementInfo {
	public ColorRectElementInfo(IFileNavigator file, ColorRectElementData data) : base(file, data) {

	}

	public Guid Id => Data.Id;

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
		}
	}
}