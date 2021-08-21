using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data {
	public class CharacterInfo : IListableInfo {
		protected FileNavigator<CharacterFile> File { get; }
		protected CharacterFile Character => File.Resource;
		public CharacterInfo(FileNavigator<CharacterFile> file) {
			File = file;
		}
		
		public string Name => Character.Name;
		public float WalkSpeed => Character.WalkSpeed;
		public float JumpHeight => Character.JumpHeight;
		public float Gravity => Character.Gravity;
		public IEnumerable<AnimationInfo> Animations => Character.Animations.Select(data => new AnimationInfo(File, data));
		public string IdleAnimation => Character.IdleAnimation;
		public string WalkAnimation => Character.WalkAnimation;
		public Vector2 Size => new Vector2(Character.Width, Character.Height);
	}
}