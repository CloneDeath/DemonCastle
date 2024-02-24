using DemonCastle.Editor.Editors.Character.Animations;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Character;

public partial class CharacterEditor : Control {
	protected HSplitContainer SplitContainer { get; }

	public CharacterEditor(CharacterInfo character) {
		Name = nameof(CharacterEditor);
		CustomMinimumSize = new Vector2I(600, 300);

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		SplitContainer.AddChild(new CharacterDetails(character));
		SplitContainer.AddChild(new CharacterAnimations(character));
	}
}