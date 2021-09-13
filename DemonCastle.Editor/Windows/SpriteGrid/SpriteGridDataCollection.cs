using System.Collections.Generic;
using DemonCastle.ProjectFiles;
using Godot;

namespace DemonCastle.Editor.Windows.SpriteGrid {
	public class SpriteGridDataCollection : ScrollContainer {
		protected VBoxContainer SpriteCollection { get; }
		public SpriteGridDataCollection(IEnumerable<SpriteGridData> spriteData) {
			AddChild(SpriteCollection = new VBoxContainer());
			foreach (var data in spriteData) {
				SpriteCollection.AddChild(new SpriteGridDataPanel(data));
			}
		}
	}
}