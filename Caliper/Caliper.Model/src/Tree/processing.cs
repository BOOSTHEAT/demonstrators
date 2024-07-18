using ImpliciX.Language.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliper.Model.Tree
{
    public class processing : SubSystemNode
    {
        public processing(string name, ModelNode parent) : base(name, parent)
        {
            addition = VersionSettingUrn<FunctionDefinition>.Build(Urn, nameof(addition));
            presence = VersionSettingUrn<Presence>.Build(Urn, nameof(presence));
        }

        public VersionSettingUrn<FunctionDefinition> addition { get; }
        public VersionSettingUrn<Presence> presence { get; }
    }
}
