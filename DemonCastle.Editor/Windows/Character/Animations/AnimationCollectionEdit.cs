using System.Linq;
using DemonCastle.ProjectFiles;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public partial class AnimationCollectionEdit : VBoxContainer {
		protected void OnAddPressed() {
			var animationInfo = Animations.Add(new AnimationData {
				Name = "animation" + Animations.Count()
			});
			AnimationItems.AddAnimation(animationInfo);
		}
	}
}