using System;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Files.Common;
using DemonCastle.ProjectFiles.Files.Elements.Types;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

public class LevelViewElementInfo : BaseInfo<LevelViewElementData>, IElementInfo {
	public LevelViewElementInfo(IFileNavigator file, LevelViewElementData data) : base(file, data) {

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
}