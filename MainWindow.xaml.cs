using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

namespace PurposeApp
{
    public partial class MainWindow : Window
    {
        AppContext appContext;
        User userN;
        public MainWindow()
        {
            InitializeComponent();
            appContext = new ContextFactory().CreateDbContext(null);
            appContext.Users.Load();
            listUsers.ItemsSource = appContext.Users.Local.ToObservableCollection();
            listBoxPurpose.SelectionChanged += ListBoxPurpose_SelectionChanged;
            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
        }

        private void ListBoxPurpose_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Purpose purpose = (sender as ListBox).SelectedItem as Purpose;
            for(int i = 0; i < userN.Purposes.Count; i++)
            {
                if (purpose.Name == userN.Purposes[i].Name)
                {
                    listViewSubTask.ItemsSource = userN.Purposes[i].subtasks;
                    break;
                }
            }
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            try
            {
                appContext.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void AddPurposeButton_Click(object sender, RoutedEventArgs e)
        {
            addPurposeGrid.Visibility = Visibility.Visible;
        }

        private void UsersListButton_Click(object sender, RoutedEventArgs e)
        {
            PurposeGridBase2.Visibility = Visibility.Hidden;
            gridUsersList.Visibility = Visibility.Visible;
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        bool bold=false;
        private void ButtonBold_Click(object sender, RoutedEventArgs e)
        {
            if (!bold)
            {
                textBlock.FontWeight=FontWeights.UltraBold;
                bold = true;
                ButtonBold.Background = Brushes.AliceBlue;
            }
            else
            {
                textBlock.FontWeight=FontWeights.Normal;
                bold = false;
                ButtonBold.Background = Brushes.White;
            }
        }

        private void buttonAddPurpose_Click(object sender, RoutedEventArgs e)
        {
            if(textBoxNamePurpose.Text!=null)
            {
                if (appContext.Purposes.Where(x=>x.UserId==userN.Id).FirstOrDefault(x => x.Name == textBoxNamePurpose.Text) == null)
                {
                    appContext.Purposes.Add(new Purpose() { Name=textBoxNamePurpose.Text, UserP=userN, UserId=userN.Id, });
                    listBoxPurpose.ItemsSource = appContext.Purposes.Local.ToObservableCollection().Where(x => x.UserId == userN.Id);
                    listBoxPurpose.UpdateLayout();
                    addPurposeGrid.Visibility = Visibility.Hidden;
                }
            }
        }

        private void buttonCancelAddPurpose_Click(object sender, RoutedEventArgs e)
        {
            addPurposeGrid.Visibility = Visibility.Hidden;
        }

        private void buttonCancelRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxNameRegister.Text != null && textBoxEmailRegister.Text != null &&
                    textBoxPasswordRegister.Text != null && textBoxUserNameRegister.Text != null)
                {
                    User user = new User()
                    {
                        Name = textBoxNameRegister.Text,
                        UserName = textBoxNameRegister.Text,
                        Email = textBoxEmailRegister.Text,
                        Password = textBoxPasswordRegister.Text,
                    };
                    for(int i=0;i<appContext.Users.Count();i++)
                    {
                        if(user.UserName != appContext.Users.ToList()[i].UserName && user.Email!= appContext.Users.ToList()[i].Email)
                        {
                            appContext.Users.Add(user);
                            registerGrid.Visibility = Visibility.Hidden;
                            choiceEnterInProgramGrid.Visibility = Visibility.Hidden;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBoxRegisters_GotFocus(object sender, RoutedEventArgs e)
        {
            switch((sender as TextBox).Name)
            {
                case "textBoxNameRegister":
                    if (textBoxNameRegister.Text == "Имя")
                    {
                        textBoxNameRegister.Text = "";
                        textBoxNameRegister.Foreground = Brushes.Black;
                    }
                    break;
                case "textBoxEmailRegister":
                    if (textBoxEmailRegister.Text == "Почта")
                    {
                        textBoxEmailRegister.Text = "";
                        textBoxEmailRegister.Foreground = Brushes.Black;
                    }
                    break;
                case "textBoxPasswordRegister":
                    if (textBoxPasswordRegister.Text == "Пароль")
                    {
                        textBoxPasswordRegister.Text = "";
                        textBoxPasswordRegister.Foreground = Brushes.Black;
                    }
                    break;
                case "textBoxUserNameRegister":
                    if (textBoxUserNameRegister.Text == "Имя пользователя")
                    {
                        textBoxUserNameRegister.Text = "";
                        textBoxUserNameRegister.Foreground = Brushes.Black;
                    }
                    break;
            }
        }
        
        private void textBoxRegisters_LostFocus(object sender, RoutedEventArgs e)
        {
            switch ((sender as TextBox).Name)
            {
                case "textBoxNameRegister":
                    if (textBoxNameRegister.Text == "")
                    {
                        textBoxNameRegister.Text = "Имя";
                        textBoxNameRegister.Foreground = Brushes.LightGray;
                    }
                    break;
                case "textBoxEmailRegister":
                    if (textBoxEmailRegister.Text == "")
                    {
                        textBoxEmailRegister.Text = "Почта";
                        textBoxEmailRegister.Foreground = Brushes.LightGray;
                    }
                    break;
                case "textBoxPasswordRegister":
                    if (textBoxPasswordRegister.Text == "")
                    {
                        textBoxPasswordRegister.Text = "Пароль";
                        textBoxPasswordRegister.Foreground = Brushes.LightGray;
                    }
                    break;
                case "textBoxUserNameRegister":
                    if (textBoxUserNameRegister.Text == "")
                    {
                        textBoxUserNameRegister.Text = "Имя пользователя";
                        textBoxUserNameRegister.Foreground = Brushes.LightGray;
                    }
                    break;
            }
        }
        private void textBoxEnters_LostFocus(object sender, RoutedEventArgs args) 
        {
            switch ((sender as TextBox).Name)
            {
                case "textBoxPasswordEnter":
                    if (textBoxPasswordEnter.Text == "")
                    {
                        textBoxPasswordEnter.Text = "Пароль";
                        textBoxPasswordEnter.Foreground = Brushes.LightGray;
                    }
                    break;
                case "textBoxUserNameEnter":
                    if (textBoxUserNameEnter.Text == "")
                    {
                        textBoxUserNameEnter.Text = "Имя пользователя";
                        textBoxUserNameEnter.Foreground = Brushes.LightGray;
                    }
                    break;
            }
        }
        private void textBoxEnters_GotFocus(object sender, RoutedEventArgs args)
        {
            switch ((sender as TextBox).Name)
            {
                case "textBoxPasswordEnter":
                    if (textBoxPasswordEnter.Text == "Пароль")
                    {
                        textBoxPasswordEnter.Text = "";
                        textBoxPasswordEnter.Foreground = Brushes.Black;
                    }
                    break;
                case "textBoxUserNameEnter":
                    if (textBoxUserNameEnter.Text == "Имя пользователя")
                    {
                        textBoxUserNameEnter.Text = "";
                        textBoxUserNameEnter.Foreground = Brushes.Black;
                    }
                    break;
            }
        }
        private void buttonCancelEnter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxUserNameEnter.Text != null && textBoxPasswordEnter.Text != null )
                {
                    for (int i = 0; i < appContext.Users.Count(); i++)
                    {
                        if (textBoxPasswordEnter.Text== appContext.Users.ToList()[i].Password&& textBoxUserNameEnter.Text== appContext.Users.ToList()[i].UserName)
                        {
                            enterGrid.Visibility = Visibility.Hidden;
                            choiceEnterInProgramGrid.Visibility = Visibility.Hidden;
                            PurposeGridBase2.Visibility= Visibility.Visible;
                            userN = appContext.Users.ToList()[i];
                            appContext.Purposes.Load();
                            listBoxPurpose.ItemsSource = appContext.Purposes.Where(x => x.UserId == userN.Id).ToList();
                            appContext.Subtasks.Load();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void choiceRegister_Click(object sender, RoutedEventArgs e)
        {
            choiceEnterInProgramGrid.Visibility = Visibility.Hidden;
            registerGrid.Visibility = Visibility.Visible;
        }

        private void choiceEnter_Click(object sender, RoutedEventArgs e)
        {
            choiceEnterInProgramGrid.Visibility = Visibility.Hidden;
            enterGrid.Visibility = Visibility.Visible;
        }
    }
    
}
