using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.TileTools.Layers;

public partial class TileLayerDetails : PropertyCollection {
	private readonly TileLayerProxy _proxy = new();

	public TileMapLayerInfo? Layer {
		get => _proxy.Proxy;
		set {
			_proxy.Proxy = value;
			if (value == null) {
				Disable();
			} else {
				Enable();
			}
		}
	}

	public TileLayerDetails() {
		Name = nameof(TileLayerDetails);
		Vertical = false;

		AddInteger(string.Empty, _proxy, p => p.ZIndex, new IntegerPropertyOptions { AllowNegative = true });
		var name = AddString(string.Empty, _proxy, p => p.Name);
		name.SizeFlagsHorizontal = SizeFlags.ExpandFill;
	}
}