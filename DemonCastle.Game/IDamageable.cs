using System;

namespace DemonCastle.Game;

public interface IDamageable {
	Guid Id { get; }
	void TakeDamage(int amount);
}