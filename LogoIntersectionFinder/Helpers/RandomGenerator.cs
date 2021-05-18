using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogoIntersectionFinder.Helpers
{
    public class RandomGenerator
    {
        private static List<string> commandsList = new List<string>() { "pu", "pd", "fd", "bk", "lt", "rt" };
        private static Random r = new Random();
        private static int maxDist = 1000;
        private static int maxAngle = 360;
        public static string Generate(int size)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < size; i++)
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
            return sb.ToString();
        }
    }
}
