using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites;

public class SpriteGridInfo : FileInfo<SpriteGridFile>, ISpriteSource {
	public SpriteGridInfo(FileNavigator<SpriteGridFile> file) : base(file) {
		GridSprites = new SpriteDefinitionCollection<SpriteGridDataInfo, SpriteGridData>(file, new SpriteGridInfoFactory(this), Resource.Sprites);
	}

	public SpriteDefinitionCollection<SpriteGridDataInfo, SpriteGridData> GridSprites { get; }
	public IEnumerableInfo<ISpriteDefinition> Sprites => GridSprites;

	public Texture2D Texture => File.GetTexture(Resource.File);

	public Vector2I Offset => new(Resource.XOffset, Resource.YOffset);
	public Vector2I Span => new(Resource.Width + Resource.XSeparation, Resource.Height + Resource.YSeparation);
	public Vector2I Size => new(Resource.Width, Resource.Height);

	public string SpriteFile {
		get => Resource.File;
		set {
			Resource.File = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int Width {
		get => Resource.Width;
		set {
			Resource.Width = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int Height {
		get => Resource.Height;
		set {
			Resource.Height = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int XOffset {
		get => Resource.XOffset;
		set {
			Resource.XOffset = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int YOffset {
		get => Resource.YOffset;
		set {
			Resource.YOffset = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int XSeparation {
		get => Resource.XSeparation;
		set {
			Resource.XSeparation = value;
			Save();
			OnPropertyChanged();
		}
	}

	public int YSeparation {
		get => Resource.YSeparation;
		set {
			Resource.YSeparation = value;
			Save();
			OnPropertyChanged();
		}
	}
}