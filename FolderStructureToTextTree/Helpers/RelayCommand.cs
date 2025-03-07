using System.Windows.Input;

namespace FolderStructureToTextTree.Helpers;
/// <summary>
/// 기본적인 RelayCommand 구현체입니다.
/// ICommand 인터페이스를 구현하여 명령 실행 로직과 실행 가능 여부를 지정할 수 있습니다.
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Predicate<object> _canExecute;

    /// <summary>
    /// RelayCommand 생성자.
    /// </summary>
    /// <param name="execute">명령 실행 시 호출할 액션입니다.</param>
    /// <param name="canExecute">명령 실행 가능 여부를 판단하는 함수입니다. (옵션)</param>
    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    /// <summary>
    /// 명령 실행 가능 여부가 변경되었음을 알리는 이벤트입니다.
    /// </summary>
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    /// <summary>
    /// 현재 명령을 실행할 수 있는지 여부를 반환합니다.
    /// </summary>
    /// <param name="parameter">명령에 전달되는 인수입니다.</param>
    /// <returns>실행 가능하면 true, 아니면 false</returns>
    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    /// <summary>
    /// 명령을 실행합니다.
    /// </summary>
    /// <param name="parameter">명령에 전달되는 인수입니다.</param>
    public void Execute(object parameter)
    {
        _execute(parameter);
    }
}
