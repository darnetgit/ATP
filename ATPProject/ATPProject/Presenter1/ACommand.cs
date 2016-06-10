using ATPProject.Model1;
using ATPProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPProject.Presenter1
{
    abstract class ACommand:ICommand
    {
        protected IModel m_model;
        protected IView m_view;

        public ACommand(IModel model, IView view)
        {
            m_model = model;
            m_view = view;
        }

        public abstract void DoCommand(params string[] parameters);
        public abstract string GetName();
    }
}
