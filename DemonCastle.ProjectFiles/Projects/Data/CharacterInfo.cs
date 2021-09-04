using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data {
	public class CharacterInfo : IListableInfo {
		protected FileNavigator<CharacterFile> File { get; }
		public string FileName => File.FileName;
		protected CharacterFile Character => File.Resource;
		public CharacterInfo(FileNavigator<CharacterFile> file) {
			File = file;
		}
		
		public string Name {
			get => Character.Name;
			set { Character.Name = value; Save(); }
		}

		public float WalkSpeed {
			get => Character.WalkSpeed;
			set { Character.WalkSpeed = value; Save(); }
		}

		public float JumpHeight {
			get => Character.JumpHeight;
			set { Character.JumpHeight = value; Save(); }
		}

		public float Gravity {
			get => Character.Gravity;
			set { Character.Gravity = value; Save(); }
		}

		public IEnumerable<AnimationInfo> Animations => Character.Animations.Select(data => new AnimationInfo(File, data));
		public string IdleAnimation => Character.IdleAnimation;
		public string WalkAnimation => Character.WalkAnimation;
		public Vector2 Size => new Vector2(Character.Width, Character.Height);
		
		protected void Save() => File.Save();
	}
}