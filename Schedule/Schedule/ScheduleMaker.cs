using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    class ScheduleMaker
    {
        public List<TimesItem> TimesList { get; set; }
        public List<MachineToolsItem> MachineToolsList { get; set; }
        public List<PartiesItem> PartiesList { get; set; }
        public List<NomenclaturesItem> NomenclaturesList { get; set; }
        public List<Machine> Machines { get; set; }

        public ScheduleMaker(
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

        public void MakeSchedule()
        {
            ScheduleCalculator calculator = new ScheduleCalculator(TimesList, MachineToolsList, PartiesList, NomenclaturesList);
            ProductionLine productionLine = new ProductionLine();
            productionLine = calculator.MakeProductionLine();
            

        }

    }
}
