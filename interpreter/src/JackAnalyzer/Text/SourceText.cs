namespace JackAnalyzer.Text;

/// <summary>
/// Represents a source text that provides functionality for text traversal and inspection.
/// </summary>
public class SourceText
{
    private readonly string _text;
    private int _tokenStart;
    private int _position;

    /// <summary>
    /// Initializes a new instance of the <see cref="SourceText"/> class with the specified text.
    /// </summary>
    /// <param name="text">The text to be analyzed.</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided text is null or empty.</exception>
    public SourceText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text));
        }
        _text = text;
        _tokenStart = 0;
        _position = 0;
    }

    /// <summary>
    /// Gets the current cursor position within the text.
    /// </summary>
    public int Position
    {
        get { return _position; }
    }

    /// <summary>
    /// Marks the start of the current token at the current cursor position.
    /// </summary>
    public void MarkTokenStart()
    {
        _tokenStart = _position;
    }

    /// <summary>
    /// Advances the cursor by one character and returns the character at the new cursor position.
    /// </summary>
    /// <returns>The character at the new cursor position.</returns>
    public char Advance() => _text[_position++];

    /// <summary>
    /// Advances the cursor by a specified number of characters.
    /// </summary>
    /// <param name="n">The number of characters to advance.</param>
    public void AdvanceBy(int n) => _position += n;

    /// <summary>
    /// Peeks at the character at the current cursor position without advancing the cursor.
    /// </summary>
    /// <returns>The character at the current cursor position, or '\0' if at the end of the text.</returns>
    public char Peek() => IsAtEnd() ? '\0' : _text[_position];

    /// <summary>
    /// Peeks at the character at a specified offset from the current cursor position without advancing the cursor.
    /// </summary>
    /// <param name="delta">The offset from the current cursor position.</param>
    /// <returns>The character at the specified offset, or '\0' if at the end of the text.</returns>
    public char PeekAt(int offset)
    {
        int index = _position + offset;
        return index >= _text.Length ? '\0' : _text[index];
    }

    /// <summary>
    /// Advances the cursor if the current character matches with the provided one.
    /// </summary>
    /// <param name="expected">The character to match.</param>
    /// <returns><c>true</c> if the characters match; otherwise, <c>false</c>.</returns>
    public bool Match(char expected)
    {
        if (IsAtEnd() || _text[_position] != expected)
            return false;

        _position++;
        return true;
    }

    /// <summary>
    /// Determines whether the cursor is at the end of the text.
    /// </summary>
    /// <param name="position">An optional offset from the current cursor position.</param>
    /// <returns><c>true</c> if the cursor is at the end; otherwise, <c>false</c>.</returns>
    public bool IsAtEnd(int offset = 0) => _position + offset >= _text.Length;

    /// <summary>
    /// Returns the substring from the last marked token start to the current position.
    /// </summary>
    public string GetSubstringFromMark() => _text.Substring(_tokenStart, _position - _tokenStart);

    public string GetSubstringFromMarkWithOffset(int startOffset, int endOffset)
    {
        return _text.Substring(_tokenStart + startOffset, _position - endOffset - _tokenStart);
    }
}