using FolderStructureToTextTree.Helpers;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FolderStructureToTextTree.Models;
/// <summary>
/// 폴더 또는 파일 정보를 나타내는 모델 클래스입니다.
/// </summary>
public class FolderItem : INotifyPropertyChanged
{
    private bool _isExcluded;
    /// <summary>
    /// 이 항목을 변환에서 제외할지 여부입니다.
    /// </summary>
    public bool IsExcluded
    {
        get => _isExcluded;
        set
        {
            if (_isExcluded != value)
            {
                _isExcluded = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// 항목의 이름입니다.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 항목의 전체 경로입니다.
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 이 항목이 폴더인지 여부입니다.
    /// </summary>
    public bool IsFolder { get; set; }

    public FolderItem()
    {
    }

    /// <summary>
    /// 하위 항목들을 나타내는 컬렉션입니다.
    /// 폴더인 경우에만 하위 항목이 존재합니다.
    /// </summary>
    public ObservableCollection<FolderItem> Children { get; set; } = new ObservableCollection<FolderItem>();

    #region INotifyPropertyChanged 구현
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}
