using System.Windows;

namespace FolderStructureToTextTree.Views;
/// <summary>
/// TextTreeDialog.xaml에 대한 상호 작용 논리
/// </summary>
public partial class TextTreeDialog : Window
{
    public TextTreeDialog(string textTree)
    {
        InitializeComponent();
        TextTreeTextBox.Text = textTree;
    }

    /// <summary>
    /// "복사하기" 버튼 클릭 시 텍스트를 클립보드에 복사합니다.
    /// </summary>
    private void CopyButton_Click(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(TextTreeTextBox.Text);
        MessageBox.Show("텍스트 트리가 클립보드에 복사되었습니다.", "복사 완료");
    }

    /// <summary>
    /// "닫기" 버튼 클릭 시 대화 상자를 닫습니다.
    /// </summary>
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
