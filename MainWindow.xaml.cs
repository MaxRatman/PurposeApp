using Microsoft.EntityFrameworkCore;
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

namespace PurposeApp
{
    public partial class MainWindow : Window
    {
        AppContext appContext;
        public MainWindow()
        {
            InitializeComponent();
            appContext = new ContextFactory().CreateDbContext(null);
            appContext.Subtasks.Load();
            listViewSubTask.ItemsSource= appContext.Subtasks.Local.ToObservableCollection();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UsersListButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    
}
