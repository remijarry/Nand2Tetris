using VMTranslator.Enums;

namespace VMTranslator.Commands
{
  public interface ICommand
  {
    public string CommandType { get; }

    public CommandName CommandName { get; set; }

    /// <summary>
    /// line at which the command appears on the VM file. This will be used at an anchor point when calling subroutines
    /// </summary>
    /// <value></value>
    public int LineIndex { get; }

    public string GetAssemblyCode();
  }
}