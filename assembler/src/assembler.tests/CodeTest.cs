namespace assembler.tests;

public class CodeTests
{
  [Fact]
  public void AInstructionShouldThrowArgumentExceptionWhenInstructionIsNullOrEmpty()
  {
    var code = new Code();
    void Act() => code.GetAInstruction(string.Empty);

    Assert.Throws<ArgumentException>(Act);
  }
}