using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character {
	public class AnimationList : ItemList {
		protected CharacterInfo CharacterInfo { get; }

		public AnimationList(CharacterInfo characterInfo) {
			CharacterInfo = characterInfo;
			ReloadAnimations();
		}

		protected void ReloadAnimations() {
			Clear();
			foreach (var animation in CharacterInfo.Animations) {
				AddItem(animation.Name);
			}
		}
	}
}