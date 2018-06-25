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
using System.Windows.Forms;

namespace Final_Project
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {

        Timer Timer;
        int TimeSec ,TimeMin;
        int ClickCount = 0;
        double MinValue;
        double SecValue;
        string pauseMin, pauseSec;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TitleBar_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaxBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal; //设置窗口还原
            }
            else
            {
                this.WindowState = WindowState.Maximized; //设置窗口最大化
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //倒數計時
        void timer_Tick(object sender, EventArgs e)
        {
            if (TimeSec > -1)
            {
                MinCount.Text = TimeMin.ToString();
                SecCount.Text = TimeSec.ToString();

                if (TimeSec > -1)
                {
                    TimeSec--;
                    if (TimeSec == 0)
                    {
                        TimeMin--;
                        TimeSec = 60;
                    }
                }
                
                
            }
            else if (SecCount.Text == "0")
            {
                Timer.Stop();
                System.Windows.Forms.MessageBox.Show("Stop!");
                ClickCount = 0;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if(ClickCount == 1)
            {
                StartButton.IsEnabled = false;
            }
            if(ClickCount == 0)
            {
                StartButton.IsEnabled = true;
                TimeMin = int.Parse(MinValue.ToString());
                TimeSec = int.Parse(SecValue.ToString());
                Timer = new Timer();

                Timer.Interval = 1000;
                Timer.Tick += new EventHandler(timer_Tick);
                Timer.Start();

                ClickCount = 1;
            }
            if(ClickCount == 2)
            {
                Timer.Start();
                TimeMin = int.Parse(pauseMin);
                TimeSec = int.Parse(pauseSec);

            }


        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
            pauseMin = MinCount.Text;
            pauseSec = SecCount.Text;
            ClickCount = 2;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
            SecSlider.Value = 0;
            MinSlider.Value = 0;
            MinCount.Text = "00";
            SecCount.Text = "00";

            ClickCount = 0;
        }

        private void MinSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            // number
                MinValue = Math.Round(MinSlider.Value, 0);
                MinNum.Text = MinValue.ToString();
            MinCount.Text = MinValue.ToString();

            // position
            double v = (MinValue / 60) * 310;
            Canvas.SetLeft(Min, v);

        }


        private void SecSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // number
            SecValue = Math.Round(SecSlider.Value, 0);
            SecNum.Text = SecValue.ToString();
            SecCount.Text = SecValue.ToString();

            // position
            double v = (SecValue / 60) * 310;
            Canvas.SetLeft(Sec , v);
        }

    }
}
