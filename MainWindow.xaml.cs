using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Backup_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string pathToBackup;
        private static string destPath = @"E:\BackupManager\Backup\";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseButton(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog openDialog = new CommonOpenFileDialog();
            openDialog.IsFolderPicker = true;

            CommonFileDialogResult res = openDialog.ShowDialog();

            if(res != CommonFileDialogResult.Ok)
            {
                return;
            }

            pathToBackup = openDialog.FileName;

        }

        private void BackupButton(object sender, RoutedEventArgs e)
        {
            Copy(pathToBackup, destPath + pathToBackup.Substring(pathToBackup.LastIndexOf(@"\")));
        }

        private void Copy(string src, string dest)
        {
            var DiSource = new DirectoryInfo(src);
            var DiDest = new DirectoryInfo(dest);

            CopyAll(DiSource, DiDest);
        }

        private void CopyAll(DirectoryInfo DiSource, DirectoryInfo DiDest)
        {
            Directory.CreateDirectory(DiDest.FullName);

            //Copy each file into destination directory
            foreach (FileInfo f in DiSource.GetFiles())
            {
                f.CopyTo(System.IO.Path.Combine(DiDest.FullName, f.Name), true);
            }

            //Copy Sub-directories
            foreach (DirectoryInfo d in DiSource.GetDirectories())
            {
                DirectoryInfo next = DiDest.CreateSubdirectory(d.Name);

            }
        }
    }
}
