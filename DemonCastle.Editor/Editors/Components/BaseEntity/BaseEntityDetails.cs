using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Monster;
using DemonCastle.Files.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Components.BaseEntity;

public abstract partial class BaseEntityDetails<TInfo, TFile> : PropertyCollection
	where TInfo : BaseEntityInfo<TFile>
	where TFile : BaseEntityFile {

	protected PropertyCollection CustomProperties { get; }

	protected BaseEntityDetails(TInfo entity) {
		Name = nameof(MonsterDetails);

		AddString("Name", entity, m => m.Name);

		AddChild(CustomProperties = new PropertyCollection());

		AddStateReference("Initial State", entity, m => m.InitialState, entity.States);
	}
}