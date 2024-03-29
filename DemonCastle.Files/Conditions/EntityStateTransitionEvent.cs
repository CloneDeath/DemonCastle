using DemonCastle.Files.Actions.Values;
using DemonCastle.Files.Conditions.Events;

namespace DemonCastle.Files.Conditions;

public class EntityStateTransitionEvent {
	public SelfEvent? Self;
	public AnimationEvent? Animation;
	public RandomTimerExpires? RandomTimerExpires { get; set; }

	public BooleanConditionData? Condition;
}

public class BooleanConditionData {
	public BooleanConditionData? Not { get; set; }
	public BooleanValueData? Value {get; set; }
	// public BooleanConditionData[]? And { get; set; }
	// public BooleanConditionData[]? Or { get; set; }
	// public ComparisonData? Comparison { get; set; }
}

// public class ComparisonData {
// 	public DataSource? Left { get; set; }
// 	public DataSource? Right { get; set; }
// 	public Operator? Operator { get; set; }
// }
//
// public class DataSource {
// 	public Guid? Variable { get; set; }
// 	public int? IntegerValue { get; set; }
// 	public int? FloatValue { get; set; }
// }
//
// [JsonConverter(typeof(StringEnumConverter))]
// public enum Operator {
// 	Equal,
// 	NotEqual,
// 	LessThan,
// 	LessThanOrEqual,
// 	GreaterThan,
// 	GreaterThanOrEqual,
// }