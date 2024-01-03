namespace DemonCastle.ProjectFiles.Files.BaseEntity;

public class MonsterStateTransitionEvent {
	public SelfEvent? Self { get; set; }
	public AnimationEvent? Animation { get; set; }
	public RandomTimerExpires? RandomTimerExpires { get; set; }
}