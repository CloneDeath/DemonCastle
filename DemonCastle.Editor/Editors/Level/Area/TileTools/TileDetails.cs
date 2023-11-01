using System.ComponentModel;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.TileTools;

public partial class TileDetails : PropertyCollection {
	private readonly TileProxy TileProxy = new();

	protected SpriteReferenceProperty SpriteIdProperty { get; }

	public TileInfo? Proxy {
		get => TileProxy.Proxy;
		set {
			TileProxy.Proxy = value;
			if (value == null) {
				DisableProperties();
			} else {
				EnableProperties();
			}
		}
	}

	public TileDetails(string levelDirectory) {
		Name = nameof(TileDetails);
		CustomMinimumSize = new Vector2I(160, 100);

		AddString("Name", TileProxy, x => x.Name);
		AddFile("Source", TileProxy, levelDirectory,  t => t.SourceFile, FileType.SpriteSources);
		SpriteIdProperty = AddSpriteReference("Sprite", TileProxy, x => x.SpriteId, TileProxy.SpriteOptions);
		DisableProperties();

		TileProxy.PropertyChanged += TileProxy_OnPropertyChanged;
	}

	private void TileProxy_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName is nameof(TileProxy.SpriteId) or nameof(TileProxy.SourceFile)) {
			SpriteIdProperty.LoadOptions(TileProxy.SpriteOptions);
		}
	}
}