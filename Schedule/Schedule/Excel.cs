using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Schedule
{
    class Excel
    {
        string path = ""; //Путь до файла
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="sheet">Номер листа с данными</param>
        public Excel(string path, int sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = excel.Worksheets[sheet];
        }


        /// <summary>
        /// Возвращает элемент ячейки листа Excel
        /// </summary>
        /// <param name="i">Номер строки</param>
        /// <param name="j">Номер столбца</param>
        public string ReadCell(int i, int j) //возвращает значение ячейки с индексом [i,j]
        {
            i++;
            j++;
            string tempElement = "";
            if (ws.Cells[i, j].Value2 != null)
                tempElement = ws.Cells[i, j].Value2.ToString();
                return tempElement;
        }

        /// <summary>
        /// Возвращает строку листа Excel
        /// </summary>
        /// <param name="rowNumb">Номер возвращаемой строки</param>
        public List<string> GetRow(int rowNumb)
        {
            List<string> row = new List<string>();
            string element = "";
            int count = 1;
            do
            {
                if (ws.Cells[rowNumb, count].Value2 != null)
                {
                    element = ws.Cells[rowNumb, count].Value2.ToString();
                    row.Add(element);
                }
                else
                {
                    element = null;//сигнал завершения
                    break;
                }
                count++;
            } while (element != null);
            return row;
        }

        /// <summary>
        /// Возвращает таблицу из непустых ячеек листа Excel от (0,0)
        /// </summary>
        public List<List<string>> GetTable()
        {
            int count = 1;
            List<string> tempList;
            List<List<string>> table = new List<List<string>>();
            do
            {
                tempList = GetRow(count);
                table.Add(tempList);
                count++;
            } while (tempList.Count != 0);
            table.RemoveAt(table.Count - 1);//очищаем пустой элемент
            return table;
        }

        /// <summary>
        /// Возвращает список объектов таблицы "times"
        /// </summary>
        public List<TimesItem> GetTimesList()
        {
            int i = 0;
            List<TimesItem> timesList = new List<TimesItem>();
            do
            {
                timesList.Add(new TimesItem
                {
                    machineToolId = ReadCell(1+i, 0).ToString(),
                    nomenclatureId = ReadCell(1+i, 1).ToString(),
                    operationTime = ReadCell(1+i, 2).ToString()
                });
                i++;
            } while (timesList.ElementAt(i-1).machineToolId != "");
            timesList.RemoveAt(i - 1);
            return timesList;
        }

        /// <summary>
        /// Возвращает список объектов таблицы "machine tools"
        /// </summary>
        public List<MachineToolsItem> GetMachineToolsList()
        {
            int i = 0;
            List<MachineToolsItem> machineToolsList = new List<MachineToolsItem>();
            do
            {
                machineToolsList.Add(new MachineToolsItem
                {
                    id = ReadCell(1 + i, 0).ToString(),
                    name = ReadCell(1 + i, 1).ToString()
                });
                i++;
            } while (machineToolsList.ElementAt(i - 1).id != "");
            machineToolsList.RemoveAt(i - 1);
            return machineToolsList;
        }

        /// <summary>
        /// Возвращает список объектов таблицы "nomenclatures"
        /// </summary>
        public List<NomenclaturesItem> GetNomenclaturesList()
        {
            int i = 0;
            List<NomenclaturesItem> nomenclaturesList = new List<NomenclaturesItem>();
            do
            {
                nomenclaturesList.Add(new NomenclaturesItem
                {
                    id = ReadCell(1 + i, 0).ToString(),
                    nomenclature = ReadCell(1 + i, 1).ToString()
                });
                i++;
            } while (nomenclaturesList.ElementAt(i - 1).id != "");
            nomenclaturesList.RemoveAt(i - 1);
            return nomenclaturesList;
        }

        /// <summary>
        /// Возвращает список объектов таблицы "parties"
        /// </summary>
        public List<PartiesItem> GetPartiesList()
        {
            int i = 0;
            List<PartiesItem> partiesList = new List<PartiesItem>();
            do
            {
                partiesList.Add(new PartiesItem
                {
                    id = ReadCell(1 + i, 0).ToString(),
                    nomenclatureId = ReadCell(1 + i, 1).ToString()
                });
                i++;
            } while (partiesList.ElementAt(i - 1).id != "");
            partiesList.RemoveAt(i - 1);
            return partiesList;
        }

        /// <summary>
        /// Возвращает список объектов таблицы "schedule"
        /// </summary>
        public List<ScheduleItem> GetScheduleList()
        {
            int i = 0;
            List<ScheduleItem> scheduleList = new List<ScheduleItem>();
            do
            {
                scheduleList.Add(new ScheduleItem
                {
                    parties = ReadCell(1 + i, 0).ToString(),
                    machineTools = ReadCell(1 + i, 1).ToString(),
                    startTime = ReadCell(1 + i, 2).ToString(),
                    endTime = ReadCell(1 + i, 3).ToString()
                });
                i++;
            } while (scheduleList.ElementAt(i - 1).parties != "");
            scheduleList.RemoveAt(i - 1);
            return scheduleList;
        }

    }
}
