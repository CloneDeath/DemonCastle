using System;
using System.Linq;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public partial class AnimationCollectionEdit : VBoxContainer {
		public event Action<AnimationInfo> AnimationSelected; 
		
		public override void _Process(float delta) {
			base._Process(delta);

			RemoveButton.Disabled = !AnimationItems.IsAnythingSelected();
		}

		protected void OnAnimationSelected(int index) {
			var animationInfo = Animations[index];
			AnimationSelected?.Invoke(animationInfo);
		}

		protected void OnAddPressed() {
			var animationInfo = Animations.Add(new AnimationData {
				Name = "animation" + Animations.Count()
			});
			AnimationItems.AddAnimation(animationInfo);
		}

		protected void OnRemovePressed() {
			var animationIndex = AnimationItems.GetSelectedItems()[0];
			Animations.RemoveAt(animationIndex);
			AnimationItems.RemoveItem(animationIndex);
		}
	}
}