using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Libs
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private Dictionary<string, object> Properties = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected T Get<T>([CallerMemberName] string name = "")
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return Properties.ContainsKey(name) ? (T)Properties[name] : default(T);
            }

            return default(T);
        }

        protected void Set(object newValue, [CallerMemberName] string name = "")
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (!Properties.ContainsKey(name))
                {
                    Properties.Add(name, newValue);
                }
                else
                {
                    Properties[name] = newValue;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
