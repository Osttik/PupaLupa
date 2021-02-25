using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FileView.FileView CurrentFileView
        {
            get
            {
                return files.Items[files.SelectedIndex] as FileView.FileView;
            }
            set
            {
                files.SelectedIndex = files.Items.IndexOf(value);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            foreach (var item in DriveInfo.GetDrives())
            {
                var newItem = new FileExplorer.ExplorerItem(FileExplorer.ExplorerItemType.Disk)
                {
                    Path = item.RootDirectory.FullName,
                    Header = item.Name
                };
                newItem.OnExecute += ExecuteFile;

                FileSystemView.Items.Add(newItem);
            }
        }

        public void ExecuteFile(string path)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                FileView.FileView newFile = new FileView.FileView();
                newFile.CloseFileEvent += CloseFile;
                newFile.ReadFile(path);

                files.Items.Add(newFile);

                CurrentFileView = newFile;
            }
            catch(Exception)
            {

            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void CloseFile(FileView.FileView file)
        {
            files.Items.Remove(file);
        }
    }
}
