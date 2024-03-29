using System;
using System.Linq;
using DemonCastle.Files;
using DemonCastle.Files.Common;
using DemonCastle.Files.Elements;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

public class SpriteElementInfo : BaseInfo<SpriteElementData>, IElementInfo {
	public SpriteElementInfo(IFileNavigator file, SpriteElementData data) : base(file, data) {

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

	public string SpriteFile {
		get => Data.SpriteFile;
		set {
			Data.SpriteFile = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Guid SpriteId {
		get => Data.SpriteId;
		set {
			Data.SpriteId = value;
			Save();
			OnPropertyChanged();
		}
	}

	protected ISpriteSource SpriteSource => File.FileExists(Data.SpriteFile)
												? File.GetSprite(Data.SpriteFile)
												: new NullSpriteSource();
	public IEnumerableInfo<ISpriteDefinition> SpriteDefinitions => SpriteSource.Sprites;
	public ISpriteDefinition SpriteDefinition => SpriteSource.Sprites.FirstOrDefault(s => s.Id == Data.SpriteId)
												 ?? new NullSpriteDefinition();
}