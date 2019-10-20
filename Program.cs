﻿using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        foreach (var file in new DirectoryInfo(Environment.CurrentDirectory).GetFiles())
        {
            if (!string.Equals(file.Extension, ".csproj", StringComparison.InvariantCultureIgnoreCase)) continue;
            
            var content = File.ReadAllText(file.FullName);

            var matches = Regex.Matches(content, @"(?<=<\w+>\d+\.\d+\.\d+\.)\d+(?=</\w+>)",
                RegexOptions.Singleline | RegexOptions.IgnoreCase);

            for (var i = matches.Count - 1; i >= 0; i--)
            {
                var match = matches[i];
                
                var current = (int.Parse(match.Value) + 1).ToString();
                content = content.Substring(0, match.Index) + current + content.Substring(match.Index + match.Length);
            }

            File.WriteAllText(file.FullName, content);
        }
    }
}