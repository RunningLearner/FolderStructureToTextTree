using FolderStructureToTextTree.Helpers;
using FolderStructureToTextTree.Models;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace FolderStructureToTextTree.ViewModels;
public class FolderTreeViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// 트리 뷰에 바인딩할 루트 폴더 컬렉션입니다.
    /// </summary>
    public ObservableCollection<FolderItem> RootItems { get; set; } = new ObservableCollection<FolderItem>();
    public ICollectionView RootItemsView { get; }

    public ICommand LoadTreeCommand { get; }
    public ICommand ConvertTreeCommand { get; }
    public ICommand ToggleExcludeCommand { get; }


    public FolderTreeViewModel()
    {
        LoadTreeCommand = new RelayCommand(LoadTree);
        ConvertTreeCommand = new RelayCommand(ConvertTree);
        ToggleExcludeCommand = new RelayCommand(ToggleExclude);

        // 컬렉션 뷰 생성 및 필터 설정
        RootItemsView = CollectionViewSource.GetDefaultView(RootItems);
    }

    private bool ExcludeFilter(object obj)
    {
        if (obj is FolderItem item)
        {
            return !item.IsExcluded;
        }
        return true;
    }

    private void ToggleExclude(object parameter)
    {
        if (parameter is FolderItem item)
        {
            item.IsExcluded = !item.IsExcluded;
            // 필터를 새로 고침하여 변경 반영
        }
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
    private FolderItem CreateFolderItem(string path, FolderItem parent = null)
    {
        var item = new FolderItem
        {
            Name = System.IO.Path.GetFileName(path),
            Path = path,
            IsFolder = true,
            Parent = parent
        };

        try
        {
            // 하위 폴더 추가
            var directories = Directory.GetDirectories(path);
            foreach (var dir in directories)
            {
                item.Children.Add(CreateFolderItem(dir, item));
            }

            // 하위 파일 추가 (옵션: 파일도 표시하려면)
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                item.Children.Add(new FolderItem
                {
                    Name = System.IO.Path.GetFileName(file),
                    Path = file,
                    IsFolder = false,
                    Parent = item
                });
            }
        }
        catch
        {
            // 접근 권한 등 예외가 발생하면 무시합니다.
        }
        return item;
    }

    /// <summary>
    /// 제외되지 않은 폴더/파일들을 기반으로 텍스트 트리 문자열을 생성합니다.
    /// </summary>
    private void ConvertTree(object parameter)
    {
        var sb = new StringBuilder();
        foreach (var item in RootItems)
        {
            AppendItem(sb, item, "");
        }
        string result = sb.ToString();

        // 사용자 정의 대화상자를 생성하고 표시합니다.
        var dialog = new FolderStructureToTextTree.Views.TextTreeDialog(result);
        dialog.Owner = System.Windows.Application.Current.MainWindow;
        dialog.ShowDialog();
    }

    private void AppendItem(StringBuilder sb, FolderItem item, string indent)
    {
        if (item.IsExcluded)
            return;

        sb.Append(indent);
        sb.Append(item.IsFolder ? "├── " : "└── ");
        sb.AppendLine(item.Name);

        if (item.IsFolder)
        {
            // 다음 들여쓰기, 마지막 항목 여부를 판단하여 수정할 수 있습니다.
            string childIndent = indent + "    ";
            foreach (var child in item.Children)
            {
                AppendItem(sb, child, childIndent);
            }
        }
    }


    #region INotifyPropertyChanged 구현
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}
