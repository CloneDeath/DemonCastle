using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;

namespace DemonCastle.Editor.Editors.SpriteGrid.Details;

public partial class SpriteDetails : PropertyCollection {
	private readonly SpriteGridDataInfoProxy _proxy = new();

	public SpriteGridDataInfo? Proxy {
		get => _proxy.Proxy;
		set {
			_proxy.Proxy = value;
			if (value == null) Disable();
			else Enable();
		}
	}

	public SpriteDetails() {
		Name = nameof(SpriteDetails);

		AddString("Name", _proxy, x => x.Name);
		AddInteger("X", _proxy, x => x.X);
		AddInteger("Y", _proxy, x => x.Y);
		AddBoolean("Flip Horizontal", _proxy, x => x.FlipHorizontal);

		AddChild(new SpriteDefinitionView(_proxy));
	}
}