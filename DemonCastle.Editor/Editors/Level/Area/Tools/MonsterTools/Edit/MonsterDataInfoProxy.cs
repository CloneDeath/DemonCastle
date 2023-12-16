using System;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools.Edit;

public class MonsterDataInfoProxy : InfoProxy<MonsterDataInfo> {
	protected override void NotifyProxyChanged() {

	}

	public Guid MonsterId {
		get => Proxy?.MonsterId ?? Guid.Empty;
		set {
			if (Proxy != null) Proxy.MonsterId = value;
		}
	}

	public Vector2 Position {
		get => Proxy?.Position ?? Vector2.Zero;
		set {
			if (Proxy != null) Proxy.Position = value;
		}
	}
}