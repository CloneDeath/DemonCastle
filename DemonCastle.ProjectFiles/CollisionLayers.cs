namespace DemonCastle.ProjectFiles;

public enum CollisionLayers : uint {
	Player = 1 << 0,
	World = 1 << 1,
	HitBox = 1 << 2,
	HurtBox = 1 << 3
}