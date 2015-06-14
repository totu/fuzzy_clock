using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace fuzzy_kellotin
{
  public partial class launcher : Window
  {
    private List<FileInfo> executables = new List<FileInfo>();
    private FileInfo found = null;
    public launcher()
    {
      InitializeComponent();
      checkColors();
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

    private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      FileInfo f = executables.Find(x => Contains(x.Name, searchBox.Text));
      if (f != null)
      {
        test.Text = f.Name.Replace(".lnk", "");
        found = f;
      }
      else
      {
        test.Text = "";
        found = null;
      }
    }

    private void searchBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (Keyboard.IsKeyDown(Key.Enter) && found != null)
      {
        Process proc = new Process();
        proc.StartInfo.FileName = found.FullName;
        proc.Start();
        test.Text = "";
        found = null;
        this.Close();
      }
      if (Keyboard.IsKeyDown(Key.Escape))
        this.Close();
    }

    private void test_GotFocus(object sender, RoutedEventArgs e)
    {
      searchBox.Focus();
    }

    private void checkColors()
    {

      DropShadowEffect dropShadow = new DropShadowEffect
      {
        Color = Colors.Black,
        Direction = 320,
        ShadowDepth = 0,
        Opacity = 1
      };

      if (MainWindow.black == true)
      {
        test.Foreground = new SolidColorBrush(Colors.Black);
        dropShadow.Color = Colors.White;
      }
      else
      {
        test.Foreground = new SolidColorBrush(Colors.White);
      }
      test.Effect = dropShadow;
      searchBox.Effect = test.Effect;
      searchBox.Foreground = test.Foreground;
      searchBox.BorderBrush = searchBox.CaretBrush = searchBox.Foreground;
    }
  }
}
