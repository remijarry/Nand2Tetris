namespace JackAnalyzer.Text;

/// <summary>
/// Represents a source text that provides functionality for text traversal and inspection.
/// </summary>
public class SourceText
{
    private readonly string _text;
    private int _start;
    private int _current;

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
        _start = 0;
        _current = 0;
    }

    /// <summary>
    /// Gets the current cursor position within the text.
    /// </summary>
    public int Position
    {
        get { return _current; }
    }

    /// <summary>
    /// Sets the start position to the current position.
    /// </summary>
    public void Begin()
    {
        _start = _current;
    }

    /// <summary>
    /// Advances the cursor by one character and returns the character at the new cursor position.
    /// </summary>
    /// <returns>The character at the new cursor position.</returns>
    public char Advance()
    {
        return _text[_current++];
    }

    /// <summary>
    /// Advances the cursor by a specified number of characters.
    /// </summary>
    /// <param name="n">The number of characters to advance.</param>
    public void AdvanceBy(int n)
    {
        _current += n;
    }

    /// <summary>
    /// Peeks at the character at the current cursor position without advancing the cursor.
    /// </summary>
    /// <returns>The character at the current cursor position, or '\0' if at the end of the text.</returns>
    public char Peek()
    {
        if (IsAtEnd())
        {
            return '\0';
        }

        return _text[_current];
    }

    /// <summary>
    /// Peeks at the character at a specified offset from the current cursor position without advancing the cursor.
    /// </summary>
    /// <param name="delta">The offset from the current cursor position.</param>
    /// <returns>The character at the specified offset, or '\0' if at the end of the text.</returns>
    public char PeekAt(int delta)
    {
        var offset = _current + delta;
        if (IsAtEnd())
        {
            return '\0';
        }

        return _text[offset];
    }

    /// <summary>
    /// Advances the cursor if the current character matches with the provided one.
    /// </summary>
    /// <param name="c">The character to match.</param>
    /// <returns><c>true</c> if the characters match; otherwise, <c>false</c>.</returns>
    public bool Match(char c)
    {
        if (IsAtEnd())
        {
            return false;
        }

        if (_text[_current] != c)
        {
            return false;
        }

        _current++;
        return true;
    }

    /// <summary>
    /// Determines whether the cursor is at the end of the text.
    /// </summary>
    /// <param name="position">An optional offset from the current cursor position.</param>
    /// <returns><c>true</c> if the cursor is at the end; otherwise, <c>false</c>.</returns>
    public bool IsAtEnd(int position = 0)
    {
        return _current + position >= _text.Length;
    }

    /// <summary>
    /// Gets the text in the current window starting from the start position.
    /// </summary>
    /// <returns>A substring of the text from the start position to the current position.</returns>
    public string GetCurrentWindow()
    {
        return _text.Substring(_start, _current - _start);
    }

    public string GetSpecificWindow(int startOffset, int currentPositionOffset)
    {
        return _text.Substring(_start + startOffset, _current - currentPositionOffset - _start);
    }
}