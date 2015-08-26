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
        private string[] boardList = { "Uno ATMega328p", "Nano ATMega328p", "Mega ATMega2560" };

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
            foreach (string boardName in boardList)
            {
                cbox_boardList.Items.Add(boardName);
            }
            cbox_boardList.SelectedIndex = 1;    // select the second item, Nano
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
        }
        void onBuildDoneEventHandler(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Build Event DONE");
        }

        private void cbox_boardList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // check if any solution is OPEN
            if (AVRonArduinoPackage.dte.Solution.Count > 0)
            {
                // check if any project is OPEN and GET it
                EnvDTE.Project project;
                if (AVRonArduinoPackage.dte.Solution.Projects.Count > 0)
                {
                    project = AVRonArduinoPackage.dte.Solution.Projects.Item(1);
                    // not sure why first project is at 1 and not at 0
                    if (project.Properties != null)
                    {
                        EnvDTE.Configuration configuration = project.ConfigurationManager.ActiveConfiguration;
                        string postBuild = "", avrdudeAddress = "here it is", hexFileName = "testHexFile.hex";

                        if (cbox_boardList.SelectedItem.ToString() == boardList[0])
                        {
                            // UNO 328
                            project.Properties.Item("DeviceName").Value = "atmega328p";
                            postBuild = avrdudeAddress + "avrdude -C avrdude.conf -v -p atmega328p -c arduino -P " + cbox_portList.SelectedItem.ToString() +
                                        " -b 115200 -D -U flash:w:" + hexFileName + ":i";
                        }
                        else if (cbox_boardList.SelectedItem.ToString() == boardList[1])
                        {
                            // Nano 328
                            project.Properties.Item("DeviceName").Value = "atmega328p";
                            postBuild = avrdudeAddress + "avrdude -C avrdude.conf -v -p atmega328p -c arduino -P " + cbox_portList.SelectedItem.ToString() +
                                        " -b 57600 -D -U flash:w:" + hexFileName + ":i";
                        }
                        else if (cbox_boardList.SelectedItem.ToString() == boardList[2])
                        {
                            // Mega 2560
                            project.Properties.Item("DeviceName").Value = "atmega2560";
                            postBuild = avrdudeAddress + "avrdude -C avrdude.conf -v -p atmega2560 -c wiring -P " + cbox_portList.SelectedItem.ToString() +
                                        " -b 115200 -D -U flash:w:" + hexFileName + ":i";
                        }
                        configuration.Properties.Item("PostBuildEventCommand").Value = postBuild;

                        // COMMANDS FROM ARDUINO
                        // ORG:  C:\Program Files (x86)\Arduino\hardware\tools\avr/bin/avrdude -CC:\Program Files (x86)\Arduino\hardware\tools\avr/etc/avrdude.conf -v -patmega328p -carduino -PCOM1 -b115200 -D -Uflash:w:C:\Users\zaid\AppData\Local\Temp\build7239693675212912702.tmp/sketch_aug26a.cpp.hex:i 
                        // UNO:  avrdude -C avrdude.conf -v -p atmega328p -c arduino -P COM1 -b 115200 -D -U flash:w:sketch_aug26a.cpp.hex:i 
                        // NANO: avrdude -C avrdude.conf -v -p atmega328p -c arduino -P COM1 -b 57600  -D -U flash:w:sketch_aug26a.cpp.hex:i 
                        // MEGA: avrdude -C avrdude.conf -v -p atmega2560 -c wiring  -P COM1 -b 115200 -D -U flash:w:sketch_aug26a.cpp.hex:i                        // LENO: ????

                        /* LINKS:
                         * https://msdn.microsoft.com/en-us/library/ms228959.aspx
                         * http://stackoverflow.com/questions/25020255/change-the-debug-properties-of-visual-studio-project-programmatically-by-envdte
                         * https://msdn.microsoft.com/en-us/library/aa984055(v=vs.100).aspx
                         * https://msdn.microsoft.com/en-us/library/aa983813(v=vs.100).aspx     but it actually is PostBuildEventCommand
                         */
                    }
                }
            }
        }
    }
}