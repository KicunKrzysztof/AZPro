using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LogoIntersectionFinder.Helpers
{
    public class FileGenerator
    {
        public static void GenerateFile(string filename, int fileLen)
        {
            File.WriteAllText(filename, RandomGenerator.Generate(fileLen));
        }
    }
}
