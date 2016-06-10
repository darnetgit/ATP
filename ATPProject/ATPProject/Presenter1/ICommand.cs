using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPProject.Presenter1
{
    interface ICommand
    {
        void DoCommand(params string[] parameters);
        string GetName();

    }
}
