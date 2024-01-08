using DemonCastle.ProjectFiles.Locations;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.Game.GameStates;

public class CurrentAreaState : ICurrentArea {
	private readonly GameArea _area;

	public CurrentAreaState(GameArea area) {
		_area = area;
	}

	public AreaPosition Position => _area.AreaPosition;
}