using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FolderStructureToTextTree.ViewModels;
public class MainWindowViewModel : INotifyPropertyChanged
{
    // 예: MainWindow에서 여러 ViewModel을 관리할 수 있음.
    // public FolderTreeViewModel FolderTreeVM { get; set; } = new FolderTreeViewModel();

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}