using Accelbuffalo.Core;
using Microsoft.Data.SqlClient;
using MyDocs;
using System;
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

namespace Accelbuffalo.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        SqlCommand command;
        SqlConnection connection;
        SqlDataReader reader;
        public Registration()
        {
            InitializeComponent();

            connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Desktop\MyDocs\MyDocs\Core\Data.mdf;Integrated Security=True");
            connection.Open();
        }

        // registration
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text != String.Empty && password.Text != String.Empty && organisation.Text != String.Empty)
            {
                command = new SqlCommand("select * from LoginTable where username='" + username.Text + "'", connection);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();
                    // если имя пользователя подошло, то ...
                    command = new SqlCommand("select * from LoginTable where password='" + password.Text + "'", connection);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        reader.Close();

                        OrganisationList list = new OrganisationList();
                        foreach (string organ in list.GetOrganisations())
                        {
                            if (organisation.Text == organ)
                            {
                                MessageBox.Show("Аккаунт был найден! Теперь вы можете пользоваться приложением!");

                                NavigationService nav;
                                nav = NavigationService.GetNavigationService(this);
                                nav.Navigate(new System.Uri("Pages\\Main.xaml", UriKind.RelativeOrAbsolute));

                                DatabaseCore core = new DatabaseCore();
                                core.SetName(username.Text);
                                core.SetOrganisation(organisation.Text);
                            }
                            
                        }                                                                     
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль. Пожалуйста, попробуйте ещё раз.");
                    }
                }
                else
                {
                    // если пользователь  не зарегистрирован, то ...
                    reader.Close();

                    OrganisationList list = new OrganisationList();
                    foreach (string organ in list.GetOrganisations())
                    {
                        if (organisation.Text == organ)
                        {
                            command = new SqlCommand("INSERT INTO LoginTable (username, password, organisation) " +
                                "VALUES(@username, @password, @organisation)", connection);
                            command.Parameters.AddWithValue("@username", username.Text.ToString());
                            command.Parameters.AddWithValue("@password", password.Text.ToString());
                            command.Parameters.AddWithValue("@organisation", organisation.Text.ToString());
                            command.ExecuteNonQuery();

                            MessageBox.Show("Аккаунт был успешно создан. Теперь вы можете пользоваться приложением!");

                            NavigationService nav;
                            nav = NavigationService.GetNavigationService(this);
                            nav.Navigate(new System.Uri("Pages\\Main.xaml", UriKind.RelativeOrAbsolute));

                            DatabaseCore core = new DatabaseCore();
                            core.SetName(username.Text);
                            core.SetOrganisation(organisation.Text);
                        }
                        
                    }                                                              
                }
            }
            else
            {
                MessageBox.Show("Не все данные введены. Пожалуйста, заполните все поля.");
            }
        }
    }
}
