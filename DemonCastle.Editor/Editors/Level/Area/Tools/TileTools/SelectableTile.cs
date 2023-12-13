using System.ComponentModel;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Levels;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools;

public partial class SelectableTile : SelectableControl {
	protected SpriteDefinitionView SpriteDefinitionView { get; }
	protected Outline Outline { get; }

	public TileInfo Tile { get; }

	public SelectableTile(TileInfo tile) {
		Name = nameof(SelectableTile);
		Tile = tile;

		AddChild(SpriteDefinitionView = new SpriteDefinitionView(tile.Sprite));
		AddChild(Outline = new Outline {
			Visible = false
		});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect);
	}

	public override void _EnterTree() {
		base._EnterTree();
		Tile.PropertyChanged += Tile_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Tile.PropertyChanged -= Tile_OnPropertyChanged;
	}

	private void Tile_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName == nameof(Tile.Sprite)) {
			SpriteDefinitionView.Load(Tile.Sprite);
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);
		Outline.Visible = IsSelected;
		CustomMinimumSize = Tile.Region.Size;
	}

	protected override void OnSelected() {
		base.OnSelected();
		DeselectSiblings();
	}
}