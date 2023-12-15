using System.ComponentModel;
using DemonCastle.Editor.Editors.Level.Area.Tools.TileTools.Collision;
using DemonCastle.Editor.Editors.Level.Area.Tools.TileTools.Stairs;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class TileDetails : PropertyCollection {
	private readonly TileProxy TileProxy = new();

	protected SpriteReferenceProperty SpriteIdProperty { get; }

	public TileInfo? Proxy {
		get => TileProxy.Proxy;
		set {
			TileProxy.Proxy = value;
			if (value == null) {
				Disable();
			} else {
				Enable();
			}
		}
	}

	public TileDetails(string levelDirectory) {
		Name = nameof(TileDetails);
		CustomMinimumSize = new Vector2I(160, 100);

		AddString("Name", TileProxy, x => x.Name);
		AddFile("Source", TileProxy, levelDirectory,  t => t.SourceFile, FileType.SpriteSources);
		SpriteIdProperty = AddSpriteReference("Sprite", TileProxy, x => x.SpriteId, TileProxy.SpriteOptions);
		SpriteIdProperty.ItemSelected += SpriteIdProperty_OnSpriteSelected;
		AddVector2I("Span", TileProxy, x => x.Span);
		AddChild(new TileCollisionView(TileProxy));
		AddChild(new TileStairView(TileProxy));

		Disable();

		TileProxy.PropertyChanged += TileProxy_OnPropertyChanged;
	}

	private void SpriteIdProperty_OnSpriteSelected(ISpriteDefinition obj) {
		if (string.IsNullOrEmpty(TileProxy.Name)) {
			TileProxy.Name = obj.Name;
		}
	}

	private void TileProxy_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName is not (nameof(TileProxy.SpriteId) or nameof(TileProxy.SourceFile))) return;
		SpriteIdProperty.LoadOptions(TileProxy.SpriteOptions);
		SpriteIdProperty.PropertyValue = TileProxy.SpriteId;
	}
}