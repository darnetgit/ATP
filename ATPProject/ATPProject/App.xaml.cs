using ATPProject.Model1;
using ATPProject.Presenter1;
using ATPProject.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ATPProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IModel model = new Model();
            IView view = new MainWindow();
            IView view2 = new PlayWindow();
            //Presenter presenter = new Presenter(model, view);
            Presenter presenter2 = new Presenter(model, view2);
            //view.Start();
            view2.Start();
        }
    }
}
