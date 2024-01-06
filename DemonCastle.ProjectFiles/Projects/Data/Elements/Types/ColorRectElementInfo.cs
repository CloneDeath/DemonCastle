using System;
using DemonCastle.Files;
using DemonCastle.Files.Common;
using DemonCastle.Files.Elements;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

public class ColorRectElementInfo : BaseInfo<ColorRectElementData>, IElementInfo {
	public ColorRectElementInfo(IFileNavigator file, ColorRectElementData data) : base(file, data) {

	}

	public Guid Id => Data.Id;
	public ElementType Type => Data.Type;
	public string ListLabel => Name;

	public string Name {
		get => Data.Name;
		set {
			Data.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Rect2I Region {
		get => Data.Region.ToRect2I();
		set {
			Data.Region = value.ToRegion2I();
			Save();
			OnPropertyChanged();
		}
	}

	public Color Color {
		get => Data.Color.ToColor();
		set {
			Data.Color = value.ToColorData();
			Save();
			OnPropertyChanged();
		}
	}
}