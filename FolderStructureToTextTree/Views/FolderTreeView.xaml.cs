using Microsoft.Win32;

using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FolderStructureToTextTree.Views;
/// <summary>
/// FolderTreeView.xaml에 대한 상호 작용 논리
/// </summary>
public partial class FolderTreeView : UserControl
{
    public FolderTreeView()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 폴더 선택 버튼 클릭 시 폴더 탐색 대화상자를 열어 선택한 폴더 경로를 TextBox에 설정합니다.
    /// </summary>
    private void BrowseButton_Click(object sender, RoutedEventArgs e)
    {
        string initialDir = RootPathTextBox.Text;
        // 초기 경로가 유효한 디렉토리인지 확인합니다.
        if (!Directory.Exists(initialDir))
        {
            // 유효하지 않은 경우 기본 경로(예: 내 문서 폴더)로 설정합니다.
            initialDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        var dialog = new OpenFolderDialog
        {
            Title = "폴더를 선택하세요.",
            InitialDirectory = initialDir
        };

        if (dialog.ShowDialog() == true)
        {
            RootPathTextBox.Text = dialog.FolderName;
        }
    }
}
