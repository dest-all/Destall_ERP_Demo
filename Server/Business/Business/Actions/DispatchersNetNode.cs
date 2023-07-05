using Business.ActionPoints;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Actions
{
    public abstract class BusinessNetNode
    {
        protected IBusinessActionsNet _accessor;

        protected BusinessNetNode(IBusinessActionsNet accessor)
        {
            _accessor = accessor;
        }
    }

    public abstract class BusinessNetNodeSingleton
    {
        protected IBusinessActionsNetSingleton _accessor;

        protected BusinessNetNodeSingleton(IBusinessActionsNetSingleton accessor)
        {
            _accessor = accessor;
        }
    }
}
