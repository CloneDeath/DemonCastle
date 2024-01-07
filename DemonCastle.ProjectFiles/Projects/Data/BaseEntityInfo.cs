using System;
using System.Linq;
using DemonCastle.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data;

public interface IBaseEntityInfo : IListableInfo {
	public Guid Id { get; }
	public AnimationInfoCollection Animations { get; }
	public EntityStateInfoCollection States { get; }
	public VariableDeclarationInfoCollection Variables { get; }
}

public abstract class BaseEntityInfo<TFile> : FileInfo<TFile>, IBaseEntityInfo, IEntityStateInfoRetriever
	where TFile : BaseEntityFile {

	protected BaseEntityInfo(FileNavigator<TFile> file) : base(file) {
		Animations = new AnimationInfoCollection(file, Resource.Animations);
		States = new EntityStateInfoCollection(file, this, Resource.States);
		Variables = new VariableDeclarationInfoCollection(file, Resource.Variables);
	}

	public Guid Id => Resource.Id;
	public string ListLabel => Name;

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(ListLabel));
		}
	}


	public Guid InitialState {
		get => Resource.InitialState;
		set {
			Resource.InitialState = value;
			Save();
			OnPropertyChanged();
		}
	}

	public AnimationInfoCollection Animations { get; }
	public EntityStateInfoCollection States { get; }
	public VariableDeclarationInfoCollection Variables { get; }

	public ISpriteDefinition PreviewSpriteDefinition => PreviewFrame?.SpriteDefinition ?? new NullSpriteDefinition();

	public Vector2 PreviewOrigin => PreviewFrame?.Origin ?? Vector2.Zero;

	private IFrameInfo? PreviewFrame {
		get {
			var state = States.FirstOrDefault(s => s.Id == InitialState);
			var animation = Animations.FirstOrDefault(a => a.Id == state?.Animation);
			return animation?.Frames.FirstOrDefault();
		}
	}

	public Texture2D PreviewTexture => new AtlasTexture
		{ Atlas = PreviewSpriteDefinition.Texture, Region = PreviewSpriteDefinition.Region };

	public EntityStateInfo? RetrieveEntityStateInfo(Guid stateId) => States.FirstOrDefault(s => s.Id == stateId);
}