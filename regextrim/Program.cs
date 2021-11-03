using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace regextrim
{
    class Program
    {       
        static void Main(string[] args)
        {
            string file = string.Empty;
            string regexString = string.Empty;
            bool ignoreCase = false;

            var parser = new CommandLine();
            parser.Parse(args);

            if (parser.Arguments.Count > 0)
            {
                if (parser.Arguments.ContainsKey("file"))
                {
                    file = parser.Arguments["file"][0];
                }

                if (parser.Arguments.ContainsKey("regex"))
                {
                    regexString = parser.Arguments["regex"][0];
                }

                if (parser.Arguments.ContainsKey("ignorecase"))
                {
                    ignoreCase = true;
                }
            }
            else
            {
                Usage();
            }

            if (string.IsNullOrEmpty(file) || string.IsNullOrEmpty(regexString))
            {
                if (string.IsNullOrEmpty(file))
                {
                    throw new ArgumentException("File parameter not set.");
                }
                if (string.IsNullOrEmpty(regexString))
                {
                    throw new ArgumentException("Regex parameter not set.");
                }
            }
            RegexOptions regexOptions = RegexOptions.Compiled;
            if (ignoreCase)
            {
                regexOptions = regexOptions | RegexOptions.IgnoreCase;
            }

            Process(file, new Regex(regexString, regexOptions));
        }
        static void Usage()
        {
            Console.WriteLine("regextrim -regex regex -file file [-ignorecase]");
        }

        static void Process(string file, Regex regex)
        {
            var updatedFile = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + "-updated.txt");
            var fileStream = new FileStream(@file, FileMode.Open, FileAccess.Read);
            var outputFileStream = new FileStream(updatedFile, FileMode.Create, FileAccess.ReadWrite);
            using (var streamWriter = new StreamWriter(outputFileStream, Encoding.UTF8))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {                        
                        Match match = regex.Match(line);
                        if (match.Length > 0)
                        {
                            string newString = regex.Replace(line, "");
                            streamWriter.WriteLine(newString);
                        }
                        else
                        {
                            System.Console.WriteLine("Skipping - " + line);
                        }
                    }
                }
            }
        }
    }

}
