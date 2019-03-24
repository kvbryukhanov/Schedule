using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Schedule
{
    class ExcelCreator
    {
        private _Excel.Application appForSaving = null;
        private _Excel.Workbook workbookForSaving = null;
        private _Excel.Worksheet worksheetForSaving = null;
        private _Excel.Range workSheetRangeForSaving = null;

        public void createFile()
        {
            try
            {
                appForSaving = new _Excel.Application();
                //appForSaving.Visible = true;
                workbookForSaving = appForSaving.Workbooks.Add(1);
                worksheetForSaving = (_Excel.Worksheet)workbookForSaving.Sheets[1];
            }
            catch (Exception e)
            {
                Console.Write("Error");
            }
            finally
            {
            }
        }


        public void WriteSchedule(List<ScheduleItem> sheduleItems)
        {
            worksheetForSaving.Cells[1, 1] = "partiesId";
            worksheetForSaving.Cells[1, 2] = "partiesName";
            worksheetForSaving.Cells[1, 3] = "machineTools";
            worksheetForSaving.Cells[1, 4] = "machineToolsId";
            worksheetForSaving.Cells[1, 5] = "startTime";
            worksheetForSaving.Cells[1, 6] = "endTime";
            
            for (int i = 0; i < sheduleItems.Count; i++)
            {
                worksheetForSaving.Cells[2 + i, 1] = sheduleItems.ElementAt(i).partiesId;
                worksheetForSaving.Cells[2 + i, 2] = sheduleItems.ElementAt(i).partiesName;
                worksheetForSaving.Cells[2 + i, 3] = sheduleItems.ElementAt(i).machineTools;
                worksheetForSaving.Cells[2 + i, 4] = sheduleItems.ElementAt(i).machineToolsId;
                worksheetForSaving.Cells[2 + i, 5] = sheduleItems.ElementAt(i).startTime;
                worksheetForSaving.Cells[2 + i, 6] = sheduleItems.ElementAt(i).endTime;
            }
            appForSaving.Visible = true;
        }
    }
}
