using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace fuzzy_kellotin
{
  public partial class launcher : Window
  {
    private bool black = true;
    private List<FileInfo> executables = new List<FileInfo>();
    public FileInfo found = null;
    public launcher()
    {
      InitializeComponent();
      string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) + @"\Programs\";
      searchFolder(path);
      path = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + @"\Programs\";
      searchFolder(path);
      searchBox.Focus();
    }

    private void searchFolder(string path)
    {
      DirectoryInfo dirInfo = new DirectoryInfo(path);
      FileInfo[] info = dirInfo.GetFiles("*.lnk");
      executables.AddRange(info);
      DirectoryInfo[] subDirectories = dirInfo.GetDirectories();
      foreach (DirectoryInfo directory in subDirectories)
      {
        searchFolder(directory.FullName);
      }
    }

    private bool Contains(string source, string toCheck)
    {
      return source.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    public static ImageSource GetIcon(string fileName)
    {
      Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(fileName);
      return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(icon.Handle, new Int32Rect(0, 0, icon.Width, icon.Height), BitmapSizeOptions.FromEmptyOptions());
    }

    private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      FileInfo f = executables.Find(x => Contains(x.Name, searchBox.Text));
      if (f != null && searchBox.Text.Length > 0)
      {
        appName.Text = f.Name.Replace(".lnk", "");
        IWshRuntimeLibrary.IWshShell wsh = new IWshRuntimeLibrary.WshShellClass();
        IWshRuntimeLibrary.IWshShortcut sc = (IWshRuntimeLibrary.IWshShortcut)wsh.CreateShortcut(f.FullName);
        if (sc.TargetPath != null && sc.TargetPath != "")
        {
          image.Source = GetIcon(sc.TargetPath);
        } else
        {
          image.Source = null;
        }
        found = f;
      }
      else
      {
        appName.Text = "";
        found = null;
        image.Source = null;
      }
    }


    private void searchBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (Keyboard.IsKeyDown(Key.Enter) && found != null)
      {
        Process proc = new Process();
        proc.StartInfo.FileName = found.FullName;
        appName.Text = "";
        searchBox.Text = String.Empty;
        found = null;
        image.Source = null;
        this.Hide();
        proc.Start();
      }
      if (Keyboard.IsKeyDown(Key.Escape))
      {
        appName.Text = "";
        searchBox.Text = String.Empty;
        found = null;
        image.Source = null;
        this.Hide();
      }
    }

    private void appName_GotFocus(object sender, RoutedEventArgs e)
    {
      searchBox.Focus();
    }

    public void checkColors()
    {
      black = MainWindow.black;
      DropShadowEffect dropShadow = new DropShadowEffect
      {
        Color = Colors.Black,
        Direction = 320,
        ShadowDepth = 0,
        Opacity = 1
      };

      if (black == true)
      {
        appName.Foreground = new SolidColorBrush(Colors.Black);
        dropShadow.Color = Colors.White;
      }
      else
      {
        appName.Foreground = new SolidColorBrush(Colors.White);
      }
      appName.Effect = dropShadow;
      searchBox.Effect = appName.Effect;
      searchBox.Foreground = appName.Foreground;
      searchBox.BorderBrush = searchBox.CaretBrush = searchBox.Foreground;
    }
  }
}
