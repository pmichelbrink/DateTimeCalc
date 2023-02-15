using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace DateTimeCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            txtStartTime.Focus();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (TimeSpan.TryParse(txtStartTime.Text, out TimeSpan start) && TimeSpan.TryParse(txtEndTime.Text, out TimeSpan end))
            {
                TimeSpan ellapsed = end - start;
                txtEllapsedTime.Text = new DateTime(ellapsed.Ticks).ToString("HH:mm:ss.f");

                Clipboard.SetText(txtEllapsedTime.Text);
            }
            else if (DateTime.TryParse(txtStartTime.Text, out DateTime startDate) && DateTime.TryParse(txtEndTime.Text, out DateTime endDate))
            {
                txtEllapsedTime.Text = new DateTime((endDate - startDate).Ticks).ToString("HH:mm:ss.f");
                //txtEllapsedTime.Text = ellapsedDate.ToString("HH:mm:ss.f");

                Clipboard.SetText(txtEllapsedTime.Text);
            }
            else
            {
                MessageBox.Show("Could not convert the input to a DateTime");
            }
        }

        private void btnCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtEllapsedTime.Text);
            btnCopyToClipboard.Dispatcher.Invoke(new Action(() =>
            {
                btnCopyToClipboard.Content = "Copied";
                Task.Run(() =>
                {
                    Thread.Sleep(2000);
                    btnCopyToClipboard.Dispatcher.Invoke(new Action(() => btnCopyToClipboard.Content = "Copy To Clipboard"));
                });
            }));

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtEllapsedTime.Text = string.Empty;
            txtEndTime.Text = string.Empty;
            txtStartTime.Text = string.Empty;

            txtStartTime.Focus();
        }
    }
}
