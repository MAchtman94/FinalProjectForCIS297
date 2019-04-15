using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class StageEditPage : Page
    {
        BuildGame buildGame;

        public StageEditPage()
        {
            this.InitializeComponent();

            buildGame = new BuildGame();
        }

        private void X_CoordinateBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //This will only allow numeric values to be entered
            //Source for code: https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
            X_CoordinateBox.Text = Regex.Replace(X_CoordinateBox.Text, "[^0-9]+", "");
        }

        private void Y_CoordinateBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Y_CoordinateBox.Text = Regex.Replace(Y_CoordinateBox.Text, "[^0-9]+", "");
        }

        private void HeightBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            HeightBox.Text = Regex.Replace(HeightBox.Text, "[^0-9]+", "");
        }

        private void WidthBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            WidthBox.Text = Regex.Replace(WidthBox.Text, "[^0-9]+", "");
        }
    }
}
