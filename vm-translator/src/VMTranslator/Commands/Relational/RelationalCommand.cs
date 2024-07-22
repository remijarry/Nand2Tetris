using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Relational
{
  public abstract class RelationalCommand
  {
    // lt, gt and eq have to check whether or not the condition is true or false, for this we generate functions that need unique ids
    protected static int _index = 0;
  }
}