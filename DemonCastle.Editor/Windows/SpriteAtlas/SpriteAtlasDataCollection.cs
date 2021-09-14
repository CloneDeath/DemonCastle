using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Windows.SpriteAtlas {
	public class SpriteAtlasDataCollection : ScrollContainer {
		protected VBoxContainer SpriteCollection { get; }
		public SpriteAtlasDataCollection(IEnumerable<SpriteAtlasDataInfo> spriteData) {
			AddChild(SpriteCollection = new VBoxContainer());
			foreach (var data in spriteData) {
				SpriteCollection.AddChild(new SpriteAtlasDataPanel(data));
			}
		}
	}
}