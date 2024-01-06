using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.Editor.Editors.Components.Properties;

namespace DemonCastle.Editor.Editors.Level.Area.Details;

public partial class AreaDetails : PropertyCollection {
	private readonly AreaProxy AreaProxy = new();

	public AreaInfo? Area {
		get => AreaProxy.Proxy;
		set {
			AreaProxy.Proxy = value;
			if (value == null) {
				Disable();
			} else {
				Enable();
			}
		}
	}

	public AreaDetails() {
		Name = nameof(AreaDetails);

		AddString("Name", AreaProxy, x => x.Name);
		AddVector2I("Position", AreaProxy, x => x.AreaPosition);
		AddVector2I("Size", AreaProxy, x => x.Size);

		Disable();
	}
}