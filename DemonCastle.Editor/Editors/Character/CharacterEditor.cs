using DemonCastle.Editor.Editors.Character.Animations;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Character;

public partial class CharacterEditor : BaseEditor {
	public override Texture2D TabIcon => IconTextures.CharacterIcon;
	public override string TabText { get; }

	protected HSplitContainer SplitContainer { get; }

	public CharacterEditor(CharacterInfo characterInfo) {
		Name = nameof(CharacterEditor);
		TabText = characterInfo.FileName;
		CustomMinimumSize = new Vector2I(600, 300);

		AddChild(SplitContainer = new HSplitContainer());
		SplitContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		SplitContainer.AddChild(new CharacterDetails(characterInfo));
		SplitContainer.AddChild(new CharacterAnimations(characterInfo));
	}
}