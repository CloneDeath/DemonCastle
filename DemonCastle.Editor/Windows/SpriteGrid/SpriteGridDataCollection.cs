using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using Godot;

namespace DemonCastle.Editor.Windows.SpriteGrid {
	public partial class SpriteGridDataCollection : ScrollContainer {
		protected SpriteGridInfo SpriteGrid { get; }

		protected void ReloadSpriteData() {
			foreach (var child in SpriteCollection.GetChildren().Cast<Node>()) {
				child.QueueFree();
			}
			foreach (var data in SpriteGrid.SpriteData) {
				SpriteCollection.AddChild(new SpriteGridDataPanel(data));
			}
		}
		
		protected void OnAddSpriteDataButtonPressed() {
			SpriteGrid.AddNewSpriteData();
			ReloadSpriteData();
		}
	}
}