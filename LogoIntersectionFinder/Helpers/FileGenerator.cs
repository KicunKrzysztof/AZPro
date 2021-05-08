using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LogoIntersectionFinder.Helpers
{
    public class FileGenerator
    {
        private static List<string> commandsList = new List<string>() { "pu", "pd", "fd", "bk", "lt", "rt" };
        private static Random r = new Random();
        private static int maxDist = 1000;
        private static int maxAngle = 360;
        public static void GenerateFile(string filename, int fileLen)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i< fileLen; i++)
            {
                switch (commandsList[r.Next(commandsList.Count)])
                {
                    case "pu":
                        sb.Append("pu ");
                        break;
                    case "pd":
                        sb.Append("pd ");
                        break;
                    case "fd":
                        sb.Append($"fd {r.Next(maxDist)} ");
                        break;
                    case "bk":
                        sb.Append($"bk {r.Next(maxDist)} ");
                        break;
                    case "lt":
                        sb.Append($"lt {r.Next(maxAngle)} ");
                        break;
                    case "rt":
                        sb.Append($"rt {r.Next(maxAngle)} ");
                        break;
                }
            }
            File.WriteAllText(filename, sb.ToString());
        }
    }
}
