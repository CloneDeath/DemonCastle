using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data {
	public class CharacterInfo : FileInfo<CharacterFile>, IListableInfo {
		public CharacterInfo(FileNavigator<CharacterFile> file) : base(file) { }
		
		public string Name {
			get => Resource.Name;
			set { Resource.Name = value; Save(); }
		}

		public float WalkSpeed {
			get => Resource.WalkSpeed;
			set { Resource.WalkSpeed = value; Save(); }
		}

		public float JumpHeight {
			get => Resource.JumpHeight;
			set { Resource.JumpHeight = value; Save(); }
		}

		public float Gravity {
			get => Resource.Gravity;
			set { Resource.Gravity = value; Save(); }
		}

		public IEnumerable<AnimationInfo> Animations => Resource.Animations.Select(data => new AnimationInfo(File, data));
		public string IdleAnimation => Resource.IdleAnimation;
		public string WalkAnimation => Resource.WalkAnimation;
		public Vector2 Size => new Vector2(Resource.Width, Resource.Height);
	}
}