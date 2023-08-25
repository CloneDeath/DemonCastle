using System;
using System.Linq;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public partial class AnimationCollectionEdit : VBoxContainer {
		public event Action<AnimationInfo> AnimationSelected; 
		
		public override void _Process(double delta) {
			base._Process(delta);

			RemoveButton.Disabled = !AnimationItems.IsAnythingSelected();
		}

		protected void OnAnimationSelected(long index) {
			var animationInfo = Animations[(int)index];
			AnimationSelected?.Invoke(animationInfo);
		}

		protected void OnAddPressed() {
			var animationInfo = Animations.Add(new AnimationData {
				Name = "animation" + Animations.Count()
			});
			AnimationItems.AddAnimationLibrary(animationInfo);
		}

		protected void OnRemovePressed() {
			var animationIndex = AnimationItems.GetSelectedItems()[0];
			Animations.RemoveAt(animationIndex);
			AnimationItems.RemoveItem(animationIndex);
		}
	}
}