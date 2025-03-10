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
                // EffectiveIsExcluded 속성 변경 알림 추가
                OnPropertyChanged(nameof(EffectiveIsExcluded));
                // 자식 아이템에게도 EffectiveIsExcluded 변경을 알림
                UpdateChildrenEffectiveExclusion();
            }
        }
    }

    /// <summary>
    /// 현재 아이템 또는 상위 아이템이 제외된 경우 true를 반환합니다.
    /// </summary>
    public bool EffectiveIsExcluded
    {
        get => IsExcluded || (Parent != null && Parent.EffectiveIsExcluded);
    }

    /// <summary>
    /// 상위 폴더를 참조합니다.
    /// </summary>
    public FolderItem Parent { get; set; }

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

    /// <summary>
    /// 자식 아이템의 EffectiveIsExcluded 속성 변경을 재귀적으로 알립니다.
    /// </summary>
    private void UpdateChildrenEffectiveExclusion()
    {
        foreach (var child in Children)
        {
            // 자식의 EffectiveIsExcluded 값이 변할 수 있으므로 변경 알림 전송
            child.OnPropertyChanged(nameof(EffectiveIsExcluded));
            child.UpdateChildrenEffectiveExclusion();
        }
    }

    #region INotifyPropertyChanged 구현
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}
