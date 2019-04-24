using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Combat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorPlayerOne : Page
    {
        BuildGame buildGame;

        public ColorPlayerOne()
        {
            this.InitializeComponent();

            buildGame = new BuildGame();

            pickAColor();
        }

        private void pickAColor()
        {
            ColorType.Text = "Select a color from below Player One!" + System.Environment.NewLine + "This will be your tank, bullet, and health bar colors";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            buildGame.getColorTypePlayerOne = 1;
            this.Frame.Navigate(typeof(ColorPlayerTwo));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            buildGame.getColorTypePlayerOne = 2;
            this.Frame.Navigate(typeof(ColorPlayerTwo));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            buildGame.getColorTypePlayerOne = 3;
            this.Frame.Navigate(typeof(ColorPlayerTwo));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            buildGame.getColorTypePlayerOne = 4;
            this.Frame.Navigate(typeof(ColorPlayerTwo));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
