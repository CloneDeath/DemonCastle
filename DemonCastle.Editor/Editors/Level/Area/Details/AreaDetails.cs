using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels;

namespace DemonCastle.Editor.Editors.Level.Area.Details;

public partial class AreaDetails : PropertyCollection {
	private readonly AreaProxy AreaProxy = new();

	public AreaInfo? Proxy {
		get => AreaProxy.Proxy;
		set => AreaProxy.Proxy = value;
	}

	public AreaDetails() {
		Name = nameof(AreaDetails);

		AddString("Name", AreaProxy, x => x.Name);
		AddVector2I("Position", AreaProxy, x => x.AreaPosition);
		AddVector2I("Size", AreaProxy, x => x.Size);
	}
}