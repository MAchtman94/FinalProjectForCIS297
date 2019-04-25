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
    public sealed partial class GameTypeOne : Page
    {
        BuildGame buildGameOne;
        MediaElement mySong;

        public GameTypeOne()
        {
            this.InitializeComponent();

            buildGameOne = new BuildGame();

            buildGameOne.gameTypeToBuild = 1;

            mySong = new MediaElement();
            playBackground();
        }

        private void Canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {
            buildGameOne.DrawGame(args.DrawingSession);
        }

        private void Canvas_Update(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedUpdateEventArgs args)
        {
            buildGameOne.Update();
        }
        private void KeyDown_UIThread(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            char pressedLetter = GetPressedLetter(args);

            if (pressedLetter == 0)
            {
                return;
            }

            args.Handled = true;

            var action = canvas.RunOnGameLoopThreadAsync(() => buildGameOne.KeyDown(pressedLetter));
        }

        private void control_Loaded(object sender, RoutedEventArgs e)
        {
            // Register for keyboard events
            Window.Current.CoreWindow.KeyDown += KeyDown_UIThread;
            Window.Current.CoreWindow.KeyUp += KeyUp_UIThread;
        }

        private void control_Unloaded(object sender, RoutedEventArgs e)
        {
            // Unregister keyboard events
            Window.Current.CoreWindow.KeyDown -= KeyDown_UIThread;
            Window.Current.CoreWindow.KeyUp -= KeyUp_UIThread;

            // Explicitly remove references to allow the Win2D controls to get garbage collected
            canvas.RemoveFromVisualTree();
            canvas = null;
        }
        private void KeyUp_UIThread(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            char releasedLetter = GetPressedLetter(args);

            if (releasedLetter == 0)
            {
                return;
            }

            args.Handled = true;

            var action = canvas.RunOnGameLoopThreadAsync(() => buildGameOne.KeyUp(releasedLetter));
        }

        public async void playBackground()
        {
            //Main Song
            mySong.IsLooping = true;
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            Windows.Storage.StorageFile file;
            file = await folder.GetFileAsync("Background.mp3");
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            mySong.SetSource(stream, file.ContentType);
            mySong.Play();
        }
        private static char GetPressedLetter(Windows.UI.Core.KeyEventArgs args)
        {
            var key = args.VirtualKey;
            char pressed = (char)0;

            switch (key)
            {
                case Windows.System.VirtualKey.A: pressed = 'A'; break;
                case Windows.System.VirtualKey.B: pressed = 'B'; break;
                case Windows.System.VirtualKey.C: pressed = 'C'; break;
                case Windows.System.VirtualKey.D: pressed = 'D'; break;
                case Windows.System.VirtualKey.E: pressed = 'E'; break;
                case Windows.System.VirtualKey.F: pressed = 'F'; break;
                case Windows.System.VirtualKey.G: pressed = 'G'; break;
                case Windows.System.VirtualKey.H: pressed = 'H'; break;
                case Windows.System.VirtualKey.I: pressed = 'I'; break;
                case Windows.System.VirtualKey.J: pressed = 'J'; break;
                case Windows.System.VirtualKey.K: pressed = 'K'; break;
                case Windows.System.VirtualKey.L: pressed = 'L'; break;
                case Windows.System.VirtualKey.M: pressed = 'M'; break;
                case Windows.System.VirtualKey.N: pressed = 'N'; break;
                case Windows.System.VirtualKey.O: pressed = 'O'; break;
                case Windows.System.VirtualKey.P: pressed = 'P'; break;
                case Windows.System.VirtualKey.Q: pressed = 'Q'; break;
                case Windows.System.VirtualKey.R: pressed = 'R'; break;
                case Windows.System.VirtualKey.S: pressed = 'S'; break;
                case Windows.System.VirtualKey.T: pressed = 'T'; break;
                case Windows.System.VirtualKey.U: pressed = 'U'; break;
                case Windows.System.VirtualKey.V: pressed = 'V'; break;
                case Windows.System.VirtualKey.W: pressed = 'W'; break;
                case Windows.System.VirtualKey.X: pressed = 'X'; break;
                case Windows.System.VirtualKey.Y: pressed = 'Y'; break;
                case Windows.System.VirtualKey.Z: pressed = 'Z'; break;
            }
            return pressed;
        }
    }
}
