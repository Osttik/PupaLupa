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

namespace TestWPF.FileExplorer
{
    /// <summary>
    /// Interaction logic for ExplorerItem.xaml
    /// </summary>
    public partial class ExplorerItem : TreeViewItem
    { 
        public string Path { get; set; }
        public ExplorerItemType Type { get; set; }
        public delegate void OneStringFunc(string path);
        public event OneStringFunc OnExecute;

        private static readonly UIElement _tempElement = new UIElement();

        public ExplorerItem()
        {
            InitializeComponent();
        }

        public ExplorerItem(ExplorerItemType type)
        {
            InitializeComponent();
            DataContext = this;
            Type = type;
            if (Type != ExplorerItemType.File)
                ExplorerElement.Items.Add(_tempElement);
        }

        protected override void OnExpanded(RoutedEventArgs e)
        {
            if (Type != ExplorerItemType.File)
            {
                ExplorerElement.Items.Clear();

                foreach (var item in Directory.GetDirectories(Path))
                {
                    ExplorerItem newItem = new ExplorerItem(ExplorerItemType.Folder)
                    {
                        Path = item,
                        Header = item.Split('\\')[item.Split('\\').Length - 1],
                    };
                    newItem.OnExecute += OnExecute;
                    ExplorerElement.Items.Add(newItem);
                }

                foreach (string item in Directory.GetFiles(Path))
                {
                    ExplorerItem newItem = new ExplorerItem(ExplorerItemType.File)
                    {
                        Path = item,
                        Header = item.Split('\\')[item.Split('\\').Length - 1],
                    };
                    newItem.OnExecute += OnExecute;
                    ExplorerElement.Items.Add(newItem);
                }
            }
        }

        protected override void OnCollapsed(RoutedEventArgs e)
        {
            ExplorerElement.Items.Clear();
            if (Type != ExplorerItemType.File)
                ExplorerElement.Items.Add(_tempElement);
        }

        protected override void OnSelected(RoutedEventArgs e)
        {
            if (Type == ExplorerItemType.File)
            {
                OnExecute.Invoke(Path);
            }
        }
    }
}
