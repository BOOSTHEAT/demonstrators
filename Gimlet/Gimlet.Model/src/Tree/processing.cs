using ImpliciX.Language.Model;

namespace Gimlet.Model.Tree
{
    public class processing : SubSystemNode
    {
        public processing(string name, ModelNode parent) : base(name, parent)
        {
            division = VersionSettingUrn<FunctionDefinition>.Build(Urn, nameof(division));
            presence = VersionSettingUrn<Presence>.Build(Urn, nameof(presence));
        }

        public VersionSettingUrn<FunctionDefinition> division { get; }
        public VersionSettingUrn<Presence> presence { get; }
    }
}