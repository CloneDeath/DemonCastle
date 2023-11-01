using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas.Details; 

public partial class SpriteAtlasDataCollection : ScrollContainer {
	private readonly IEnumerable<SpriteAtlasDataInfo> _spriteData;
	protected VBoxContainer SpriteCollection { get; }
	
	public SpriteAtlasDataCollection(IEnumerable<SpriteAtlasDataInfo> spriteData) {
		_spriteData = spriteData;
		AddChild(SpriteCollection = new VBoxContainer {
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		Reload();
	}

	public void Reload() {
		foreach (var child in SpriteCollection.GetChildren()) {
			child.QueueFree();
		}
		foreach (var data in _spriteData) {
			SpriteCollection.AddChild(new SpriteAtlasDataPanel(data));
		}
	}
}