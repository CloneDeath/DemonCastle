using System.Collections.Generic;
using DemonCastle.ProjectFiles;
using Godot;

namespace DemonCastle.Editor.Windows.SpriteAtlas {
	public class SpriteAtlasDataCollection : ScrollContainer {
		protected VBoxContainer SpriteCollection { get; }
		public SpriteAtlasDataCollection(IEnumerable<SpriteAtlasData> spriteData) {
			AddChild(SpriteCollection = new VBoxContainer());
			foreach (var data in spriteData) {
				SpriteCollection.AddChild(new SpriteAtlasDataPanel(data));
			}
		}
	}
}