using DemonCastle.Files.Conditions.Events;

namespace DemonCastle.Files.Conditions;

public class EntityStateTransitionEvent {
	public SelfEvent? Self { get; set; }
	public AnimationEvent? Animation { get; set; }
	public RandomTimerExpires? RandomTimerExpires { get; set; }
}