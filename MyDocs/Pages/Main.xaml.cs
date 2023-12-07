using Accelbuffalo.Core;
using Microsoft.Data.SqlClient;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
using Color = System.Windows.Media.Color;

namespace Accelbuffalo.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();          
            
            Calendar.SelectedDate = DateTime.Now;
            int selected_day = Calendar.SelectedDate.Value.Day;
            int check_day = DateTime.DaysInMonth(Calendar.SelectedDate.Value.Year, Calendar.SelectedDate.Value.Month);
            if (check_day - selected_day <= 25)
            {
                AddToast("Accelbuffalo", "Не забудьте отправить отчёт до конца месяца!");

                ListBoxItem item = new ListBoxItem();
                item.Height = 40;
                item.Width = 205;
                item.Background = new SolidColorBrush(Color.FromArgb(255,255,255,255));
                item.Content = "Не забудьте отправить отчёт до конца месяца!";
                notify_list.Items.Add(item);
            }
        }

        // уведомленпие
        void AddToast(string Header, string Description)
        {
            new ToastContentBuilder()
                .AddText(Header)
                .AddText(Description)
                .Show();
        }

        // закрытие приложения
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        // свернуть окно
        private void Minimize_Button_Click(object sender, RoutedEventArgs e)
        {
            // this.WindowState = WindowState.Minimized;
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

        private async void UsernameLabel_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseCore core = new DatabaseCore();
            while (true)
            {              
                UsernameLabel.Content = core.GetName();
                await Task.Delay(2000);
            }      
        }

        private async void OrganisationLabel_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseCore core = new DatabaseCore();
            while (true)
            {
                OrganisationLabel.Content = "OOO «" + core.GetOrganisation() + "»";
                await Task.Delay(2000);
            }
        }

        private void send_to_email_MouseDown(object sender, MouseButtonEventArgs e)
        {
            /*
            string to = "email to";
            string from = "email from";
            string subject = "text";
            string body = @"test";
            MailMessage message = new MailMessage(from, to, subject, body);
            SmtpClient client = new SmtpClient();
            client.Timeout = 100;

            client.Credentials = CredentialCache.DefaultNetworkCredentials;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                
            }
            */
        }
    }
}
