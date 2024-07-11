namespace VMTranslator
{

  // this interface is very similar to ICommand, can we do something about it?
  public interface IFunction
  {
    public string FunctionName { get; }
    public string GetAssemblyCode();
  }
}
