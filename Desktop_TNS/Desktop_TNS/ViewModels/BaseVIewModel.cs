using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Desktop_TNS.ViewModels
{
    public class BaseVIewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
