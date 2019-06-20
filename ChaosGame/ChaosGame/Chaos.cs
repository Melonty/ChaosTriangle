using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace ChaosGame
{
    public class Chaos
    {
        public readonly Point[] attractors;
        Point previous;

        public Chaos(Point[] attractors, Point start)
        {
            this.attractors = attractors;
            previous = start;
        }

        public Point NextPoint(Random r)
        {
            Point attractor = attractors[r.Next(attractors.Length)];
            previous = new Point(
                (attractor.X + previous.X) / 2,
                (attractor.Y + previous.Y) / 2);
            return previous;
        }
    }
}
