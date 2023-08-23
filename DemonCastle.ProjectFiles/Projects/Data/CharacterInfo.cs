using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data {
	public class CharacterInfo : FileInfo<CharacterFile>, IListableInfo {
		public AnimationInfoCollection Animations { get; }

		public CharacterInfo(FileNavigator<CharacterFile> file) : base(file) {
			Animations = new AnimationInfoCollection(file, Resource.Animations);
		}
		
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

		public float Width {
			get => Resource.Width;
			set { Resource.Width = value; Save(); }
		}

		public float Height {
			get => Resource.Height;
			set { Resource.Height = value; Save(); }
		}

		public string IdleAnimation {
			get => Resource.IdleAnimation;
			set { Resource.IdleAnimation = value; Save(); }
		}

		public string WalkAnimation {
			get => Resource.WalkAnimation;
			set { Resource.WalkAnimation = value; Save(); }
		}

		public string JumpAnimation {
			get => Resource.JumpAnimation;
			set { Resource.JumpAnimation = value; Save(); }
		}

		public Vector2 Size => new(Resource.Width, Resource.Height);
	}
}