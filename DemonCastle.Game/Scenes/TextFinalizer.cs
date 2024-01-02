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
		var regex = new Regex(@"(?<!\\)\$\{(?<variable>.*?)(:(?<format>.*))?\}", RegexOptions.IgnoreCase);
		var replaced = regex.Replace(text, VariableMatchEvaluator);
		return GetTransformedText(replaced);
	}

	private string VariableMatchEvaluator(Match match) {
		var variable = match.Groups["variable"].Value.Trim();
		var format = match.Groups["format"].Value;
		return GetVariable(variable, format);
	}

	private string GetVariable(string variable, string format) {
		var intValues = new Dictionary<string, int> {
			{ "Player.HP", _gameState.Player.HP },
			{ "Player.MP", _gameState.Player.MP },
			{ "Player.Lives", _gameState.Player.Lives },
			{ "Player.Score", _gameState.Player.Score }
		};
		if (intValues.TryGetValue(variable, out var value)) {
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