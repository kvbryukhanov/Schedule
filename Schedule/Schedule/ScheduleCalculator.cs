using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Калькулятор. Проводит необходимые рассчеты для составления расписания
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

        /// <summary>
        /// Метод готовит данные для работы станка, вызов необходим перед началом работы станка
        /// </summary>
        /// <param name="machineToolsItem">Строка таблицы machine tools с параметрами станка</param>
        /// <returns>название станка, список доступных материалов, список времени обработки соответственно</returns>
        private List<List<string>> SetDataForMachine(MachineToolsItem machineToolsItem)
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
                            materialsList.Add(nomenclaturesItem.nomenclature);
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

        /// <summary>
        /// Создает линию станков. Передавая информацио о каждом объекту ProductionLine.
        /// return: 1 - успешно, 0 - неудача
        /// </summary>
        /// <returns></returns>
        public ProductionLine MakeProductionLine()
        {
            ProductionLine productionLine = new ProductionLine();
            foreach (MachineToolsItem machineToolsItem in MachineToolsList)
            {
                if (productionLine.AddMachine(SetDataForMachine(machineToolsItem)) == null)
                    return null;
            }
            return productionLine;
        }
    }
}
