using System;
using System.Collections.Generic;
using VMtranslator.Commands;
using VMTranslator.Commands.Arithmetic;
using VMTranslator.Commands.Function;
using VMTranslator.Commands.Logical;
using VMTranslator.Commands.Memory;
using VMTranslator.Commands.ProgramFlow;
using VMTranslator.Commands.Relational;
using VMTranslator.Commands.Stack;
using VMTranslator.Constants;

namespace VMTranslator.Commands
{
  public class CommandFactory : ICommandFactory
  {
    //todo: refactor these dictionaries, it's a mess.
    private readonly Dictionary<string, Func<ICommand>> _commandMap = new Dictionary<string, Func<ICommand>>()
    {
        { CommandName.ADD, () => new Add() },
        { CommandName.SUB, () => new Sub() },
        { CommandName.NEG, () => new Neg() },
        { CommandName.AND, () => new And() },
        { CommandName.NOT, () => new Not() },
        { CommandName.OR, () => new Or() },
        { CommandName.EQ, () => new Eq() },
        { CommandName.LT, () => new Lt() },
        { CommandName.GT, () => new Gt() },
        { CommandName.RETURN, () => new Return() }
    };

    private readonly Dictionary<string, Func<string, string, ICommand>> _memorySegmentMap = new Dictionary<string, Func<string, string, ICommand>>()
        {
            { MemorySegment.CONSTANT, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation constant) ? new Constant(constant, index) : null },
            { MemorySegment.TEMP, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation temp) ? new Temp(temp, index) : null },
            { MemorySegment.POINTER, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation pointer) ? new Pointer(pointer, index) : null },
            { MemorySegment.ARGUMENT, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation argument) ? new Argument(argument, index) : null },
            { MemorySegment.LOCAL, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation local) ? new Local(local, index) : null },
            { MemorySegment.STATIC, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation statc) ? new Static(statc, index) : null },
            { MemorySegment.THIS, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation dis) ? new This(dis, index) : null },
            { MemorySegment.THAT, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation dat) ? new That(dat, index) : null },
            // todo: this does not belong here, all these dicitonaries might need refactoring.
            {CommandName.FUNCTION, (function, nArgs) => new Function.Function(function, int.Parse(nArgs))},
            {CommandName.CALL, (label, nArgs) => new Function.Call(new GoTo(label), int.Parse(nArgs))}
        };

    private readonly Dictionary<string, Func<string, ICommand>> _functionMap = new Dictionary<string, Func<string, ICommand>>()
    {
      { CommandName.LABEL, (s) => new Label(s) },
      { CommandName.GOTO, (s) => new GoTo(s) },
      { CommandName.IF_GO_TO, (s) => new IfGoTo(s) },
    };

    public ICommand CreateCommand(string line)
    {
      var tokens = line.Split(' ');

      if (_commandMap.TryGetValue(tokens[0], out var createCommand))
      {
        return createCommand();
      }

      if (tokens[0] == CommandName.FUNCTION)
      {
        return new Function.Function(tokens[1], int.Parse(tokens[2]));
      }

      if (tokens[0] == CommandName.CALL)
      {
        return new Call(new GoTo(tokens[1]), int.Parse(tokens[2]));
      }

      if (_memorySegmentMap.TryGetValue(tokens[1], out var createMemoryCommand))
      {
        return createMemoryCommand(tokens[0], tokens[2]);
      }

      if (_functionMap.TryGetValue(tokens[0], out var createFunctionCommand))
      {
        return createFunctionCommand(tokens[1]);
      }

      throw new Exception();
    }
  }
}