using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Win32;
using Windows.System.Display;
using Windows.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Combat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HowToPlayPage : Page
    {
        
        public HowToPlayPage()
        {
            this.InitializeComponent();

            howToPlayText();
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        public void howToPlayText()
        {
            TextHowTo.Text = "Welcome to the game of Combat! Where you and one other person will go one on one in combat!" + System.Environment.NewLine +
                "Use the left analog stick to move your tank around the board" + System.Environment.NewLine + "Use the B Button to fire your bullets" + System.Environment.NewLine +
                "First person to get hit 5 times loses!" + System.Environment.NewLine + "Player one's health bar is on the left and player two will be on the right" + System.Environment.NewLine +
                "Good luck!" + System.Environment.NewLine + "On keyboard: Player One, use WASD and V to fire" + System.Environment.NewLine + "Player Two, use IKJL and B to fire";
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }
    }
}
