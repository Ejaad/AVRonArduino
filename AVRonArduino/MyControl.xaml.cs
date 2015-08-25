using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO.Ports;


namespace EjaadTech.AVRonArduino
{
    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
    public partial class MyControl : UserControl
    {
        public MyControl()
        {
            InitializeComponent();
        }

        private SerialPort comport = new SerialPort();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(string.Format(System.Globalization.CultureInfo.CurrentUICulture, "We are inside {0}.button1_Click() for UNO UNO UNO", this.ToString()),
                            "AVR on Arduino");

        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(string.Format(System.Globalization.CultureInfo.CurrentUICulture, "We are inside {0}.button1_Click() for NANO", this.ToString()),
                            "AVR on Arduino");

        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(string.Format(System.Globalization.CultureInfo.CurrentUICulture, "We are inside {0}.button1_Click() for MEGA", this.ToString()),
                            "AVR on Arduino");
        }

        private void bt_scan_Click(object sender, RoutedEventArgs e)
        {
            RefreshComPortList();
        }

        private void RefreshComPortList()
        {
            cbox_portList.Items.Clear();
            string[] portList = OrderedPortNames();
            foreach (string portName in portList)
            {
                cbox_portList.Items.Add(portName);
                cbox_portList.SelectedIndex = cbox_portList.Items.Count - 1;    // select the last item
            }
        }

        private string[] OrderedPortNames()
        {
            // Just a placeholder for a successful parsing of a string to an integer
            int num;

            // Order the serial port names in numberic order (if possible)
            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray();
        }
    }

}