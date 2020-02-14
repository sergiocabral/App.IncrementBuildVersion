using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        foreach (var file in new DirectoryInfo(Environment.CurrentDirectory).GetFiles())
        {
            if (!string.Equals(file.Extension, ".csproj", StringComparison.InvariantCultureIgnoreCase) &&
                !string.Equals(file.Name, "AssemblyInfo.cs", StringComparison.InvariantCultureIgnoreCase)) 
                continue;
            
            var content = File.ReadAllText(file.FullName);

            var matches = Regex.Matches(content, @"((?<=<\w+>\d+\.\d+\.\d+\.)\d+(?=</\w+>)|(?<=Version\([@$]*""\d+\.\d+\.\d+\.)\d+(?=""\)))",
                RegexOptions.Singleline | RegexOptions.IgnoreCase);

            for (var i = matches.Count - 1; i >= 0; i--)
            {
                var match = matches[i];
                
                var current = (int.Parse(match.Value) + 1).ToString();
                content = content.Substring(0, match.Index) + current + content.Substring(match.Index + match.Length);
            }

            try
            {
                File.WriteAllText(file.FullName, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}