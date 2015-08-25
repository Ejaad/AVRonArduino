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
            InitializeBoardList();
        }

        // ALL SERIAL PORT RELATED CODE is from SERIALPORTTERMINAL APP of Noah Coad - http://noahcoad.com
        // http://coad.net/blog/resources/clickonce/SerialPortTerminal/publish.htm
        // http://noahcoad.com/projects

        private SerialPort comport = new SerialPort();

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
        private void InitializeBoardList()
        {
            string[] boardList = { "Uno ATMega328p", "Nano ATMega328p", "Mega ATMega2560", "Leonardo" };
            foreach (string boardName in boardList)
            {
                cbox_boardList.Items.Add(boardName);
            }
            cbox_boardList.SelectedIndex = 1;    // select the second item, Nano
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            if (AVRonArduinoPackage.dte.Solution.Count > 0)
            {
                EnvDTE.Project abc;
                if (AVRonArduinoPackage.dte.Solution.Projects.Count > 0)
                {
                    abc = AVRonArduinoPackage.dte.Solution.Projects.Item(1);
                    //comboBox1.Items.Add(abc.Name);
                    //comboBox1.Items.Add(abc.Properties.Count.ToString());
                    if (abc.Properties != null)
                    {
                        foreach (EnvDTE.Property prop in abc.Properties)
                        {
                            comboBox1.Items.Add(prop.Name);
                        }
                    }
                }
            }
        }
    }
}