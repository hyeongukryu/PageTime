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

namespace PageTime
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            BeginTime = new DateTime(2013, 12, 10, 10, 0, 0);
            EndTime = new DateTime(2013, 12, 10, 21, 0, 0);

            StartPage = 256;
            EndPage = 1024;

            new Thread(UpdateLoop).Start();
        }

        private void Update()
        {
            if (DateTime.Now >= EndTime)
            {
                textBoxPage.Text = "종료";
            }
            else if (DateTime.Now < BeginTime)
            {
                textBoxPage.Text = "준비";
            }
            else
            {
                double total = (EndTime - BeginTime).TotalSeconds;
                double elapsed = (DateTime.Now - BeginTime).TotalSeconds;

                double t = elapsed / total;

                double page = StartPage + (EndPage - StartPage) * t;

                textBoxPage.Text = string.Format("{0:0.000}", page);
            }
        }

        private void UpdateLoop()
        {
            while (true)
            {
                Dispatcher.Invoke(Update);
                Thread.Sleep(8);
            }
        }

        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }

        public double StartPage { get; set; }
        public double EndPage { get; set; }
    }
}
