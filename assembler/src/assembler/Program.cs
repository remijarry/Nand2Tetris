namespace assembler;

class Program
{
    static void Main(string[] args)
    {
        var directory = "programs";
        var fileName = "MaxL.asm";

        var filePath = Path.Combine(directory, fileName);

        if (File.Exists(filePath))
        {
            using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read);
            using StreamReader streamReader = new(fileStream);
            var parser = new Parser(streamReader);
            try
            {
                var result = parser.Parse();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }
        }
    }
}
