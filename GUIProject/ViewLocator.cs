using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using GUIProject.ViewModels;

namespace GUIProject
{
    public class ViewLocator : IDataTemplate,IViewCreator
    {
        public bool SupportsRecycling => false;
        public IControl Build(object data, params object[] args)
        {
            var type = GetType(data);
            if (type != null)
            {
                return (Control)Activator.CreateInstance(type,args);
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + data.GetType().FullName };
            }
        }

        public IControl Build(object data)
        {
            var type = GetType(data);
            if (type != null)
            {
                return (Control)Activator.CreateInstance(type);
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + data.GetType().FullName };
            }
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
        private Type GetType(object data)
        {
            var dataNamespace = data.GetType().Namespace.Replace("ViewModel", "View");
            var dataName = data.GetType().Name.Replace("ViewModel", "");
            var name = String.Join('.', dataNamespace, dataName);
            return Type.GetType(name);
        }
    }
}