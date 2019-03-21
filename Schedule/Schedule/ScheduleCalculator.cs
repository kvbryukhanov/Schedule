using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Рассчитывает расписание
/// </summary>
/// <param name="timesList">Список элементов таблицы "times"</param>
/// <param name="machineToolsList">Список элементов таблицы "machine tools"</param>
/// <param name="partiesList">Список элементов таблицы "parties"</param>
/// <param name="nomenclaturesList">Список элементов таблицы "nomenclatures"</param>

namespace Schedule
{
    class ScheduleCalculator
    {
        public List<TimesItem> TimesList { get; set; }
        public List<MachineToolsItem> MachineToolsList { get; set; }
        public List<PartiesItem> PartiesList { get; set; } 
        public List<NomenclaturesItem> NomenclaturesList { get; set; }
        public List<Machine> Machines { get; set; }

        public ScheduleCalculator(
            List<TimesItem> timesList, 
            List<MachineToolsItem> machineToolsList, 
            List<PartiesItem> partiesList, 
            List<NomenclaturesItem> nomenclaturesList
            )
        {
            TimesList = timesList;
            MachineToolsList = machineToolsList;
            PartiesList = partiesList;
            NomenclaturesList = nomenclaturesList;
        }

        public List<List<string>> SetDataForMachine(MachineToolsItem machineToolsItem)
        {
            List<string> name = new List<string>();//название станка
            List<string> materialsList = new List<string>(); //список доступных материалов
            List<string> workTimesList = new List<string>(); //список времени обработки соответственно
            foreach (TimesItem timesItem in TimesList)
            {
                if (machineToolsItem.id == timesItem.machineToolId)
                {
                    workTimesList.Add(timesItem.operationTime);
                    foreach (NomenclaturesItem nomenclaturesItem in NomenclaturesList)
                    {
                        if (nomenclaturesItem.id == timesItem.nomenclatureId)
                            workTimesList.Add(nomenclaturesItem.nomenclature);
                    }
                }
            }
            name.Add(machineToolsItem.name);

            List<List<string>> listForMachine = new List<List<string>>();
            listForMachine.Add(name);
            listForMachine.Add(materialsList);
            listForMachine.Add(workTimesList);
            return listForMachine;
        }
        
    }
}
