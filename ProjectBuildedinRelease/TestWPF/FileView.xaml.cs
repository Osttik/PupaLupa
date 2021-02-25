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
    /// Interaction logic for FileView.xaml
    /// </summary>
    public partial class FileView : TabItem
    {
        public string FileName { get; protected set; } = string.Empty;
        public string Text { get => TextBoxText.Text; protected set => TextBoxText.Text = value; }
        public delegate void FileViewAction(FileView file);
        public event FileViewAction CloseFileEvent;

        public FileView()
        {
            InitializeComponent();
        }

        public void ReadFile(string fileName, Encoding encoding)
        {
            try
            {
                Text = File.ReadAllText(fileName, encoding);

                FileName = fileName;

                FileNameText.Text = FileName.Split('\\').Last();
            }
            catch(Exception)
            {

            }
        }

        private void CloseFile(object sender, RoutedEventArgs e)
        {
            CloseFileEvent?.Invoke(this);
        }
    }
}
