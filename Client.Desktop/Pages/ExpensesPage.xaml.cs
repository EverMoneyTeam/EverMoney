using System.Windows.Controls;
using Client.DataAccess.Repository;
using System.ComponentModel;
using System.Threading;

namespace Client.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class ExpensesPage : Page
    {
        private BackgroundWorker backgroundWorker;
        public ExpensesPage()
        {
            InitializeComponent();
            
            //backgroundWorker = new BackgroundWorker();
            //backgroundWorker.DoWork += BackgroundWorker_DoWork;
            //backgroundWorker.RunWorkerAsync();

            Dispatcher.BeginInvoke(new ThreadStart(delegate { LoadData(); }));
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            UserRepository userRepository = new UserRepository();
            dataGrid.ItemsSource = userRepository.GetAllUsers();
        }
    }
}
