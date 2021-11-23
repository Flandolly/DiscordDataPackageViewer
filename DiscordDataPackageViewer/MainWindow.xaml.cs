using Microsoft.Win32;
using System;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace DiscordDataPackageViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonUploadFile_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog openFolder = new CommonOpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                IsFolderPicker = true
            };

            if (openFolder.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string folderPath = openFolder.FileName;
                string missing = "";

                List<string> dirs = new List<string>(Directory.EnumerateDirectories(folderPath));
                List<string> dirList = new List<string>();

                string[] desiredDirs = { "account", "activity", "messages", "programs", "servers" };

                foreach (string directory in dirs)
                {
                    string separatedPath = directory.Substring(directory.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
                    if (desiredDirs.Contains(separatedPath)) 
                    {
                        //Console.WriteLine(separatedPath);
                        folderList.Items.Add(separatedPath);
                        folderPathExact.Content = directory;
                    }
                    dirList.Add(separatedPath);
                }
                //Console.WriteLine($"{dirs.Count} directories found.");
                foreach (string folder in desiredDirs)
                {
                    if (!dirList.Contains(folder))
                    {
                        missing += string.Format("{0}, ", folder);
                    }
                }

                if (!string.IsNullOrEmpty(missing))
                {
                    errorText.Content = string.Format("Missing directories: {0}", missing.Remove(missing.Length - 2));
                }
            }
        }
    }
}
