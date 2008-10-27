using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.Kernel
{
    public class BindingController : DomainController
    {
        public BindingController(ITransactionContext context)
            : base(context)
        {
        }

        public List<Binding> GetByBindablePointId(int bindablePointId)
        {
            using (BindingFactory bindingFactory = BindingFactory.GetInstance())
            {
                return bindingFactory.LoadByBindablePointId(bindablePointId);
            }
        }
    }
}