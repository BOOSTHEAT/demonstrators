using ImpliciX.Language.Model;

namespace Gimlet.Model.Tree
{
    public class metrics : ModelNode
    {
        public MetricUrn pasteurize { get; }
        public MetricUrn forcemode { get; }
        public MetricUrn runmotor { get; }
        public MetricUrn motorstate { get; }
        public MetricUrn failmotor { get; }
        public MetricUrn notfailmotor { get; }
        public MetricUrn numberfailmotor { get; }
        public MetricUrn heaterstate { get; }
        public MetricUrn failheater { get; }
        public MetricUrn notfailheater { get; }
        public MetricUrn numberfailheater { get; }
        public MetricUrn fillingstate { get; }
        public MetricUrn failfilling { get; }
        public MetricUrn notfailfilling { get; }
        public MetricUrn numberfailfilling { get; }
        public MetricUrn emptyingstate { get; }
        public MetricUrn failemptying { get; }
        public MetricUrn notfailemptying { get; }
        public MetricUrn numberfailemptying { get; }
        public MetricUrn temperature_variation { get; }
        public MetricUrn mttr_motor { get; }
        public MetricUrn mtbf_motor { get; }
        public MetricUrn mttr_heater { get; }
        public MetricUrn mtbf_heater { get; }
        public MetricUrn mttr_filling { get; }
        public MetricUrn mtbf_filling { get; }
        public MetricUrn mttr_emptying { get; }
        public MetricUrn mtbf_emptying { get; }
        
        public metrics(string urnToken, ModelNode parent) : base(urnToken, parent)
        {
            pasteurize =  MetricUrn.Build(Urn,nameof(pasteurize));
            forcemode =  MetricUrn.Build(Urn,nameof(forcemode));
            runmotor =  MetricUrn.Build(Urn,nameof(runmotor));
            motorstate =  MetricUrn.Build(Urn,nameof(motorstate));
            failmotor =  MetricUrn.Build(Urn,nameof(failmotor));
            notfailmotor =  MetricUrn.Build(Urn,nameof(notfailmotor));
            numberfailmotor =  MetricUrn.Build(Urn,nameof(numberfailmotor));
            heaterstate =  MetricUrn.Build(Urn,nameof(heaterstate));
            failheater =  MetricUrn.Build(Urn,nameof(failheater));
            notfailheater =  MetricUrn.Build(Urn,nameof(notfailheater));
            numberfailheater =  MetricUrn.Build(Urn,nameof(numberfailheater));
            fillingstate =  MetricUrn.Build(Urn,nameof(fillingstate));
            failfilling =  MetricUrn.Build(Urn,nameof(failfilling));
            notfailfilling =  MetricUrn.Build(Urn,nameof(notfailfilling));
            numberfailfilling =  MetricUrn.Build(Urn,nameof(numberfailfilling));
            emptyingstate =  MetricUrn.Build(Urn,nameof(emptyingstate));
            failemptying =  MetricUrn.Build(Urn,nameof(failemptying));
            notfailemptying =  MetricUrn.Build(Urn,nameof(notfailemptying));
            numberfailemptying =  MetricUrn.Build(Urn,nameof(numberfailemptying));
            temperature_variation =  MetricUrn.Build(Urn,nameof(temperature_variation));
            mttr_motor =  MetricUrn.Build(Urn,nameof(mttr_motor));
            mtbf_motor =  MetricUrn.Build(Urn,nameof(mtbf_motor));
            mttr_heater =  MetricUrn.Build(Urn,nameof(mttr_heater));
            mtbf_heater =  MetricUrn.Build(Urn,nameof(mtbf_heater));
            mttr_filling =  MetricUrn.Build(Urn,nameof(mttr_filling));
            mtbf_filling =  MetricUrn.Build(Urn,nameof(mtbf_filling));
            mttr_emptying =  MetricUrn.Build(Urn,nameof(mttr_emptying));
            mtbf_emptying =  MetricUrn.Build(Urn,nameof(mtbf_emptying));
        }
    }
}