using Microsoft.Win32;
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

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Encoding CurrentEncoding { get; protected set; } = Encoding.UTF8;
        public Encoding PrevEncoding { get; protected set; } = Encoding.UTF8;

        public List<Encoding> AllowedEncodings { get; protected set; } = new List<Encoding>()
        {
            Encoding.GetEncoding(1251),
            Encoding.GetEncoding(10017),
            Encoding.ASCII,
            Encoding.UTF7,
            Encoding.UTF8,
            Encoding.UTF32,
            Encoding.BigEndianUnicode,
            Encoding.Unicode
        };

        public FileView CurrentFileView { 
            get
            {
                return files.Items[files.SelectedIndex] as FileView;
            } 
            set
            {
                files.SelectedIndex = files.Items.IndexOf(value);
            }
        }

        private MenuItem _prevCheckedEncodingItem;

        public MainWindow()
        {
            InitializeComponent();

            foreach (var item in AllowedEncodings)
            {
                MenuItem encodingMenuItem = new MenuItem
                {
                    Header = item.EncodingName
                };
                encodingMenuItem.Click += ChangeEncoding;
                encodingMenuItem.IsCheckable = true;
                encodingMenuItem.Tag = item;

                if (item == CurrentEncoding)
                {
                    encodingMenuItem.IsChecked = true;
                    _prevCheckedEncodingItem = encodingMenuItem;
                }

                EncodingItems.Items.Add(encodingMenuItem);
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                InitialDirectory = Environment.CurrentDirectory
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                FileView newFile = new FileView();
                newFile.CloseFileEvent += CloseFile;
                newFile.ReadFile(dlg.FileName, CurrentEncoding);

                files.Items.Add(newFile);

                CurrentFileView = newFile;
            }
        }

        private void ChangeEncoding(object sender, RoutedEventArgs e)
        {
            _prevCheckedEncodingItem.IsChecked = false;
            _prevCheckedEncodingItem = sender as MenuItem;

            PrevEncoding = CurrentEncoding;

            CurrentEncoding = (sender as MenuItem).Tag as Encoding;

            ReReadFiles();
        }

        private void ReReadFiles()
        {
            foreach (FileView item in files.Items)
            {
                item.ReadFile(item.FileName, CurrentEncoding);
            }
        }

        private void CloseFile(FileView file)
        {
            files.Items.Remove(file);
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(CurrentFileView.FileName, CurrentFileView.Text);
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, CurrentFileView.Text);
            }
        }
    }
}
