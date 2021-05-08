using System;
using System.Collections.Generic;
using System.Linq;
using LogoIntersectionFinder.Helpers;

namespace LogoIntersectionFinder.LogoParser
{
    public class Turtle
    {
        private double x;
        private double y;
        private double angle;
        private bool penDown;
        private double stepDistance;
        private double stepAngle;
        private List<Segment> result;
        private int stepId;

        public List<Segment> Parse(string logoProgram)
        {
            ResetTurtle();
            var tokens= logoProgram.Split().ToList();
            tokens.RemoveAll(t => t.Equals(""));
            int i = 0;
            while (i < tokens.Count)
            {
                switch (tokens[i])
                {
                    case "pu":
                        penDown = false;
                        ++stepId;
                        break;
                    case "pd":
                        penDown = true;
                        break;
                    case "fd":
                        try
                        {
                            stepDistance = double.Parse(tokens[++i]);
                        }
                        catch 
                        {
                            return null;
                        }
                        GoForward();
                        ++stepId;
                        break;
                    case "bk":
                        try
                        {
                            stepDistance = double.Parse(tokens[++i]);
                        }
                        catch
                        {
                            return null;
                        }
                        GoBack();
                        ++stepId;
                        break;
                    case "lt":
                        try
                        {
                            stepAngle = double.Parse(tokens[++i]);
                        }
                        catch
                        {
                            return null;
                        }
                        Left();
                        break;
                    case "rt":
                        try
                        {
                            stepAngle = double.Parse(tokens[++i]);
                        }
                        catch
                        {
                            return null;
                        }
                        Right();
                        break;
                    default:
                        return null;
                }
                ++i;
            }
            return result;
        }

        private void Right()
        {
            angle += (stepAngle * Math.PI) / 180;
        }

        private void Left()
        {
            angle -= (stepAngle * Math.PI) / 180;
        }

        private void GoBack()
        {
            Point p1 = new Point((int)x, (int)y);
            x -= Math.Sin(angle) * stepDistance;
            y -= Math.Cos(angle) * stepDistance;
            if (penDown)
            {
                Point p2 = new Point((int)x, (int)y);
                result.Add(new Segment(p1, p2, stepId));
            }
        }

        private void GoForward()
        {
            Point p1 = new Point((int)x, (int)y);
            x += Math.Sin(angle) * stepDistance;
            y += Math.Cos(angle) * stepDistance;
            if (penDown)
            {
                Point p2 = new Point((int)x, (int)y);
                result.Add(new Segment(p1, p2, stepId));
            }
        }

        private void ResetTurtle()
        {
            x = 0;
            y = 0;
            angle = 0;
            penDown = true;
            stepId = 0;
            result = new List<Segment>();
        }
    }
}
