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

namespace Schedule
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;//событие загрузки
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            Excel scheduleSheet = new Excel(@"C:\Users\Константин\source\repos\Schedule\Schedule\Schedule\schedule1.xlsx", 1);
            Excel timesSheet = new Excel(@"C:\Users\Константин\source\repos\Schedule\Schedule\Schedule\times.xlsx", 1);
            Excel machineToolsSheet = new Excel(@"C:\Users\Константин\source\repos\Schedule\Schedule\Schedule\machine_tools.xlsx", 1);
            Excel partiesSheet = new Excel(@"C:\Users\Константин\source\repos\Schedule\Schedule\Schedule\parties.xlsx", 1);
            Excel nomenclaturesSheet = new Excel(@"C:\Users\Константин\source\repos\Schedule\Schedule\Schedule\nomenclatures.xlsx", 1);


            List<List<string>> resTable = scheduleSheet.GetTable();
            //resList.ItemsSource = resTable.ElementAt(0);
            
            List<TimesItem> timesList = timesSheet.GetTimesList();
            List<MachineToolsItem> machineToolsList = machineToolsSheet.GetMachineToolsList();
            List<PartiesItem> partiesList = partiesSheet.GetPartiesList();
            List<NomenclaturesItem> nomenclaturesList = nomenclaturesSheet.GetNomenclaturesList(); 

            timesView.ItemsSource = timesList;
            machineToolsView.ItemsSource = machineToolsList;
            partiesView.ItemsSource = partiesList;
            nomenclaturesView.ItemsSource = nomenclaturesList;

            ScheduleMaker scheduleMaker = new ScheduleMaker(timesList, machineToolsList, partiesList, nomenclaturesList);
            List<ScheduleItem> schedule = scheduleMaker.MakeSchedule();
            partiesList = partiesSheet.GetPartiesList();

        }
    }
}
