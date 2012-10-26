using System;
using System.Linq;
using CryEngine.Extensions;
using CryEngine.Initialization;

namespace CryEngine.Compilers.NET.Handlers
{
    internal class FlowNodeScriptRegistrationHandler : IScriptRegistrationParamsHandler
    {
        public IScriptRegistrationParams GetScriptRegistrationParams(Type type)
        {
            if (!type.GetMembers().Any(member => member.ContainsAttribute<PortAttribute>()))
                return null;

            var nodeRegistrationParams = new FlowNodeRegistrationParams();

            FlowNodeAttribute nodeInfo;
            if (type.TryGetAttribute(out nodeInfo))
            {
                if (!string.IsNullOrEmpty(nodeInfo.UICategory))
                    nodeRegistrationParams.Category = nodeInfo.UICategory;

                if (!string.IsNullOrEmpty(nodeInfo.Name))
                    nodeRegistrationParams.Name = nodeInfo.Name;
            }

            return nodeRegistrationParams;
        }
    }
}
