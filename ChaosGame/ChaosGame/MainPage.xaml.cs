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
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ChaosGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        int iterations;

        Chaos chaos;
        byte dotDiameter;
        Color dotColor;


        public MainPage()
        {
            this.InitializeComponent();
            Initialise();

            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(10_000_000);
        }

        void Initialise()
        {
            canvas.Width = 1500;
            canvas.Height = 1080;
            chaos = new Chaos(
                attractors: new Point[] {
                    new Point(750, 200),
                    new Point(300, 800),
                    new Point(1200, 800) },
                start: new Point(1, 1));
            dotDiameter = 5;
            dotColor = Colors.LightGreen;
            DrawAttractors();
        }

        private void Timer_Tick(object sender, object e)
        {
            DrawNextDot(new Random());
            iCounter.Text = iterations.ToString();
        }

        void DrawAttractors()
        {
            Ellipse[] dots = new Ellipse[chaos.attractors.Length];
            for (int i = 0; i < chaos.attractors.Length; i++)
            {
                dots[i] = new Ellipse();
                dots[i].Translation = new Vector3((float)chaos.attractors[i].X, (float)chaos.attractors[i].Y, 0);
                dots[i].Fill = new SolidColorBrush(Colors.Red);
                dots[i].Width = 10;
                dots[i].Height = 10;
                canvas.Children.Add(dots[i]);
            }
        }

        void DrawNextDot(Random r)
        {
            Point nextPoint = chaos.NextPoint(r);
            var dot = new Ellipse();
            dot.Translation = new Vector3((float)nextPoint.X, (float)nextPoint.Y, 0);
            dot.Fill = new SolidColorBrush(dotColor);
            dot.Width = dotDiameter;
            dot.Height = dotDiameter;
            canvas.Children.Add(dot);
            iterations++;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void Speed0_Click(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(long.MaxValue);
        }

        private void Speed1_Click(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(10_000_000);
        }

        private void Speed2_Click(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(1_000_000);
        }

        private void Speed3_Click(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan();
        }

        private async void BtnRestart_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            if (!byte.TryParse(tbDotDiameter.Text, out dotDiameter) || dotDiameter == 0)
            {
                ContentDialog errorMsg = new ContentDialog()
                {
                    Title = "VIRUS!!!",
                    Content = "Please, enter a correct dot diameter value (1-255)",
                    CloseButtonText = "Ok"
                };
                await errorMsg.ShowAsync();
            }
            canvas.Children.Clear();
            DrawAttractors();
            iterations = 0;
            iCounter.Text = iterations.ToString();
        }

        private void ColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {
            dotColor = colorPicker.Color;
        }
    }
}
