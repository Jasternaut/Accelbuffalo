using Microsoft.Office.Interop.Word;
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
using System.Windows.Xps.Packaging;

namespace MyDocs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // передвижение окна по экрану мышкой
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        // закрытие приложения
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        // свернуть окно
        private void Minimize_Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            Document_Viewer.Text = "";

            // открытие диалога для выбора документа
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Текстовые файлы (.txt)|*.txt";

            // появление диалога и ожидание выбора
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                if (dlg.FileName.Length > 0)
                {
                    Document_Name.Content = dlg.FileName;
                    CurrentDocument.Content = dlg.FileName;
                    
                    string[] line = File.ReadAllLines(dlg.FileName);

                    foreach (string line2 in line)
                    {
                        Document_Viewer.Text += line2 + "\n";
                    }                                                                        
                }
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {       
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "Текстовый файл (*.txt)|*.txt";

            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                System.IO.File.WriteAllText(filename, Document_Viewer.Text);
            }
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            Document_Viewer.Text = "";

            // открытие диалога для выбора документа
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Текстовые файлы (.txt)|*.txt";

            // появление диалога и ожидание выбора
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                if (dlg.FileName.Length > 0)
                {
                    Document_Name.Content = dlg.FileName;
                    CurrentDocument.Content = dlg.FileName;

                    string[] line = File.ReadAllLines(dlg.FileName);

                    foreach (string line2 in line)
                    {
                        // загрузка в редактор
                        Document_Viewer.Text += line2 + "\n";
                    }
                }
            }
        }
    }
}
