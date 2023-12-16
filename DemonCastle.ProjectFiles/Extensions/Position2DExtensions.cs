using DemonCastle.ProjectFiles.Files;
using Godot;

namespace DemonCastle.ProjectFiles.Extensions;

public static class Position2DExtensions {
	public static Vector2 ToVector2(this Position2D position) => new(position.X, position.Y);
	public static Position2D ToPosition2D(this Vector2 vector) => new() { X = vector.X, Y = vector.Y };
}