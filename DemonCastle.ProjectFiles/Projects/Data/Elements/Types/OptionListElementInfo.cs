using System;
using DemonCastle.Files;
using DemonCastle.Files.Common;
using DemonCastle.Files.Elements;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

public class OptionListElementInfo : BaseInfo<OptionListElementData>, IElementInfo {
	public OptionListElementInfo(IFileNavigator file, OptionListElementData data) : base(file, data) {
		Options = new InfoList<OptionInfo, OptionData>(file, data.Options, d => new OptionInfo(file, d));
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
			OnPropertyChanged(nameof(ListLabel));
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

	public IEnumerableInfo<OptionInfo> Options { get; }
}

public class OptionInfo : BaseInfo<OptionData>, IListableInfo {
	public OptionInfo(IFileNavigator file, OptionData data) : base(file, data) {
		OnSelect = new SceneEventActionInfoCollection(file, data.OnSelect);
	}

	public string ListLabel => Text;

	public string Text {
		get => Data.Text;
		set {
			if (SaveField(ref Data.Text, value)) {
				OnPropertyChanged(nameof(ListLabel));
			}
		}
	}

	public SceneEventActionInfoCollection OnSelect { get; }
}