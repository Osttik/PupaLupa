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

namespace TestWPF.FileView
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

        public void ReadFile(string fileName)
        {
            try
            {
                Text = File.ReadAllText(fileName);

                FileName = fileName;

                FileNameText.Text = fileName;

                var info = new FileInfo(fileName);

                DateText.Text = "-->" + info.CreationTime.ToString();
                SizeText.Text = "-->" + info.Length.ToString();

                int answer = 0;
                foreach (var line in Text.Split('\n'))
                {
                    foreach (var number in line.Trim().Split(' '))
                    {
                        try
                        {
                            var value = Convert.ToInt32(number);
                            answer += value;
                        }
                        catch(Exception)
                        {

                        }
                    }
                }

                EvalText.Text = answer.ToString();
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
