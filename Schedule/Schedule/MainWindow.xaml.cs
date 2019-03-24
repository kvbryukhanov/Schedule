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
using Microsoft.Win32;

namespace Schedule
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Excel scheduleSheet;
        //Excel scheduleSheet = new Excel(@"C:\Users\Константин\source\repos\Schedule\Schedule\Schedule\schedule1.xlsx", 1);
        Excel timesSheet;
        //Excel timesSheet = new Excel(@"C:\Users\Константин\source\repos\Schedule\Schedule\Schedule\times.xlsx", 1);
        Excel machineToolsSheet;
        //Excel machineToolsSheet = new Excel(@"C:\Users\Константин\source\repos\Schedule\Schedule\Schedule\machine_tools.xlsx", 1);
        Excel partiesSheet;
        //Excel partiesSheet = new Excel(@"C:\Users\Константин\source\repos\Schedule\Schedule\Schedule\parties.xlsx", 1);
        Excel nomenclaturesSheet;
        //Excel nomenclaturesSheet = new Excel(@"C:\Users\Константин\source\repos\Schedule\Schedule\Schedule\nomenclatures.xlsx", 1);

        List<TimesItem> timesList;
        List<MachineToolsItem> machineToolsList;
        List<PartiesItem> partiesList;
        List<NomenclaturesItem> nomenclaturesList;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;//событие загрузки

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            //resList.ItemsSource = resTable.ElementAt(0);

        }

        private void ButtonClickTimes(object sender, RoutedEventArgs e)
        {
            var FilePath = "";//Путь к файлу
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                timesSheet = new Excel(FilePath, 1);
                timesList = timesSheet.GetTimesList();
                timesView.ItemsSource = timesList;
            }               
        }
        private void ButtonClickMachineTools(object sender, RoutedEventArgs e)
        {
            var FilePath = "";//Путь к файлу
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                machineToolsSheet = new Excel(FilePath, 1);
                machineToolsList = machineToolsSheet.GetMachineToolsList();
                machineToolsView.ItemsSource = machineToolsList;
            }
        }
        private void ButtonClickNomenclatures(object sender, RoutedEventArgs e)
        {
            var FilePath = "";//Путь к файлу
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                nomenclaturesSheet = new Excel(FilePath, 1);
                nomenclaturesList = nomenclaturesSheet.GetNomenclaturesList();
                nomenclaturesView.ItemsSource = nomenclaturesList;
            }
        }
        private void ButtonClickParties(object sender, RoutedEventArgs e)
        {
            var FilePath = "";//Путь к файлу
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                partiesSheet = new Excel(FilePath, 1);
                partiesList = partiesSheet.GetPartiesList();
                partiesView.ItemsSource = partiesList;
            }
        }
        

        private void ButtonClick(object sender, RoutedEventArgs e)
        {

            ScheduleMaker scheduleMaker = new ScheduleMaker(timesList, machineToolsList, partiesList, nomenclaturesList);
            List<ScheduleItem> schedule = scheduleMaker.MakeSchedule();
            partiesList = partiesSheet.GetPartiesList();
            scheduleView.ItemsSource = schedule;
            ExcelCreator excelCreator = new ExcelCreator();
            excelCreator.createFile();
            excelCreator.WriteSchedule(schedule);
            //scheduleSheet = new Excel
            //List<List<string>> resTable = scheduleSheet.GetTable();

        }
    }
}
