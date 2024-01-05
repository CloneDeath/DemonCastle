using System;
using DemonCastle.Files;
using DemonCastle.Files.Common;
using DemonCastle.Files.Elements;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

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

	public Rect2I Region {
		get => Data.Region.ToRect2I();
		set {
			Data.Region = value.ToRegion2I();
			Save();
			OnPropertyChanged();
		}
	}

	public string Text {
		get => Data.Text;
		set {
			Data.Text = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string? FontFile {
		get => Data.FontFile;
		set {
			Data.FontFile = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Font? Font => FontFile != null && File.FileExists(FontFile) ? File.GetFont(FontFile) : null;

	public int FontSize {
		get => Data.FontSize;
		set {
			Data.FontSize = value;
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

	public TextTransform TextTransform {
		get => Data.TextTransform;
		set {
			Data.TextTransform = value;
			Save();
			OnPropertyChanged();
		}
	}
}