using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DemonCastle.ProjectFiles.Files.Elements.Types;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.Game.Scenes;

public class TextFinalizer {
	private readonly IGameState _gameState;
	private readonly TextTransform _transform;

	public TextFinalizer(IGameState gameState, TextTransform transform) {
		_gameState = gameState;
		_transform = transform;
	}

	public string Finalize(string text) {
		var regex = new Regex(@"(?<!\\)\{(?<variable>.*?)(:(?<format>.*?))?\}", RegexOptions.IgnoreCase);
		var replaced = regex.Replace(text, VariableMatchEvaluator);
		return GetTransformedText(replaced);
	}

	private string VariableMatchEvaluator(Match match) {
		var variable = match.Groups["variable"].Value.Trim();
		var format = match.Groups["format"].Value;
		return GetVariable(variable, format);
	}

	public static readonly IReadOnlyDictionary<string, Func<IGameState, int>> IntegerValues = new Dictionary<string, Func<IGameState, int>> {
		{ "Player.HP", state => state.Player.HP },
		{ "Player.MP", state => state.Player.MP },
		{ "Player.Lives", state => state.Player.Lives },
		{ "Player.Score", state => state.Player.Score }
	};

	private string GetVariable(string variable, string format) {
		if (IntegerValues.TryGetValue(variable, out var function)) {
			var value = function(_gameState);
			return value.ToString(format);
		}

		return variable;
	}

	private string GetTransformedText(string replaced) {
		return _transform switch {
			TextTransform.None => replaced,
			TextTransform.Lowercase => replaced.ToLower(),
			TextTransform.Uppercase => replaced.ToUpper(),
			_ => throw new NotSupportedException()
		};
	}
}