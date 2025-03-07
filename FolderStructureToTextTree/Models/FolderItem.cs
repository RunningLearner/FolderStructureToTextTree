using System.Collections.ObjectModel;

namespace FolderStructureToTextTree.Models;
/// <summary>
/// 폴더 또는 파일 정보를 나타내는 모델 클래스입니다.
/// </summary>
public class FolderItem
{
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

    /// <summary>
    /// 하위 항목들을 나타내는 컬렉션입니다.
    /// 폴더인 경우에만 하위 항목이 존재합니다.
    /// </summary>
    public ObservableCollection<FolderItem> Children { get; set; } = new ObservableCollection<FolderItem>();
}
