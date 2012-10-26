using CryEngine;
namespace NETFramework.Tests.Samples
{
    [FlowNode(Description="MyFlowNode", Category = FlowNodeCategory.Advanced)]
    public class SampleFlowNode : FlowNode
    {
        [Port]
        public OutputPort SamplePort
        {
            get; set;
        }
    }
}
