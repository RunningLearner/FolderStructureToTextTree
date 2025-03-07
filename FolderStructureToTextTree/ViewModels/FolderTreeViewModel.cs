using FolderStructureToTextTree.Helpers;
using FolderStructureToTextTree.Models;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FolderStructureToTextTree.ViewModels;
public class FolderTreeViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// 트리 뷰에 바인딩할 루트 폴더 컬렉션입니다.
    /// </summary>
    public ObservableCollection<FolderItem> RootItems { get; set; } = new ObservableCollection<FolderItem>();

    public ICommand LoadTreeCommand { get; }

    public FolderTreeViewModel()
    {
        // LoadTreeCommand는 폴더 구조를 불러오는 명령입니다.
        LoadTreeCommand = new RelayCommand(LoadTree);
    }

    /// <summary>
    /// 루트 폴더 경로를 지정하여 트리 구조를 로드합니다.
    /// </summary>
    /// <param name="parameter">루트 폴더 경로 (string)로 캐스팅합니다.</param>
    public void LoadTree(object parameter)
    {
        if (parameter is string rootPath && Directory.Exists(rootPath))
        {
            RootItems.Clear();
            var rootItem = CreateFolderItem(rootPath);
            RootItems.Add(rootItem);
        }
    }

    /// <summary>
    /// 지정한 경로의 폴더 구조를 재귀적으로 생성합니다.
    /// </summary>
    /// <param name="path">폴더 경로</param>
    /// <returns>FolderItem 객체</returns>
    private FolderItem CreateFolderItem(string path)
    {
        var item = new FolderItem
        {
            Name = System.IO.Path.GetFileName(path),
            Path = path,
            IsFolder = true
        };

        try
        {
            // 하위 폴더 추가
            var directories = Directory.GetDirectories(path);
            foreach (var dir in directories)
            {
                item.Children.Add(CreateFolderItem(dir));
            }

            // 하위 파일 추가 (옵션: 파일도 표시하려면)
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                item.Children.Add(new FolderItem
                {
                    Name = System.IO.Path.GetFileName(file),
                    Path = file,
                    IsFolder = false
                });
            }
        }
        catch
        {
            // 접근 권한 등 예외가 발생하면 무시합니다.
        }
        return item;
    }

    #region INotifyPropertyChanged 구현
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}
