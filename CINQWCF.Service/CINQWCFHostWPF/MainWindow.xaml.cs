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
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using CINQWCF.Service;
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;

namespace CINQWCFHostWPF
{
    [CallbackBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class MainWindow : Window
    {
        BackgroundWorker myWorker = new BackgroundWorker();
        ConsumeWCF iWcf = new ConsumeWCF();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Start();
            myWorker.DoWork += myWorker_DoWork;
            myWorker.ProgressChanged += myWorker_ProgressChanged;
            myWorker.RunWorkerCompleted += myWorker_RunWorkerCompleted;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Ltime.Content = DateTime.Now.ToString("HH:mm:ss");
        }
        private void Bsoap_Click(object sender, RoutedEventArgs e)
        {
            buttonDefaultTasks();
            iWcf.endPoint = "esoap";
        }
        private void Btcp_Click(object sender, RoutedEventArgs e)
        {
            buttonDefaultTasks();
            iWcf.endPoint = "etcp";
        }
        private void Bmeta_Click(object sender, RoutedEventArgs e)
        {
            buttonDefaultTasks();
            iWcf.endPoint = "emeta";
        }
        private void buttonDefaultTasks()
        {
            FetchDataProgressBar.Value = 0;
            LVmain.ItemsSource = null;
            L1.Content = string.Empty;
            if (myWorker.IsBusy != true)
            {
                L1.Foreground = Brushes.White;
                L1.Content = "Call to WCF requested, please wait...";
                myWorker.WorkerReportsProgress = true;
                myWorker.WorkerSupportsCancellation = true;
                myWorker.RunWorkerAsync();
            }
            else
            {
                L1.Content = "Please, wait...";
            }
        }
        private void myWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                iWcf.GetDataList(iWcf.endPoint);
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    L1.Content = "The proxy " + iWcf.endPoint + " was opened! Loading request...";
                    L1.Foreground = Brushes.Red;
                });
                Thread.Sleep(2000);
                myWorker.ReportProgress(5000);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                myWorker.CancelAsync();
            }
        }
        void myWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            FetchDataProgressBar.Value = e.ProgressPercentage;
        }

        private void myWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LVmain.ItemsSource = iWcf.GetDataList(iWcf.endPoint);
            L1.Content = "Done. The proxy " + iWcf.endPoint + " has been closed now!";
            L1.Foreground = Brushes.Green;
        }
    }
}
