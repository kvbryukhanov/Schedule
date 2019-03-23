using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    /// <summary>
    /// Класс работает с расписанием
    /// </summary>
    class ScheduleMaker
    {
        public List<TimesItem> TimesList { get; set; }
        public List<MachineToolsItem> MachineToolsList { get; set; }
        public List<PartiesItem> PartiesList { get; set; }
        public List<NomenclaturesItem> NomenclaturesList { get; set; }
        public List<Machine> Machines { get; set; }
        private List<ScheduleItem> schedule = new List<ScheduleItem>();

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

        public List<ScheduleItem> MakeSchedule()
        {
            ScheduleCalculator calculator = new ScheduleCalculator(TimesList, MachineToolsList, PartiesList, NomenclaturesList);
            ProductionLine productionLine = new ProductionLine();
            productionLine = calculator.MakeProductionLine();
            Clock clock = new Clock();


            do//начинаем перебор значений списка партий
            {
                int currentMachineId = -1;//номер текущего станка

                do//перебираем, пока не будут заняты все станки
                {
                    int i = 0;//всегда берем только первую по очереди партию
                    string material = "";

                    //ищем название материала по id, чтобы загрузить в станок
                    for (int j = 0; j < NomenclaturesList.Count; j++)
                    {
                        //если материал в списке материалов найден
                        if (NomenclaturesList.ElementAt(j).id == PartiesList.ElementAt(i).nomenclatureId)
                        {
                            //присваиваем переменной строковое значение
                            material = NomenclaturesList.ElementAt(j).nomenclature;
                            currentMachineId = -1;//номер текущего станка
                            //пытаемся установить партию в линию
                            currentMachineId = productionLine.insertMaterial(material);
                            //установка прошла успешно
                            if (currentMachineId >= 0)
                            {
                                ScheduleItem scheduleItem = new ScheduleItem();
                                scheduleItem.partiesId = i.ToString();//Записываем id загруженной партии
                                scheduleItem.startTime = clock.GetValue().ToString();//Записываем время начала работы
                                scheduleItem.machineToolsId = currentMachineId.ToString(); //Записываем Id станка
                                scheduleItem.partiesId = PartiesList.ElementAt(i).id;//Записываем Id партии
                                //Записываем название станка
                                scheduleItem.machineTools = MachineToolsList.ElementAt(currentMachineId).name;
                                //Записываем название материала
                                scheduleItem.partiesName = NomenclaturesList.ElementAt(j).nomenclature;
                                //Записываем время окончания
                                scheduleItem.endTime = (Convert.ToInt32(clock.GetValue() +
                                    Convert.ToInt32(productionLine.GetWorkingTime().ElementAt(currentMachineId)))).ToString();
                                //i++;//переходим к следующей партии
                                PartiesList.RemoveAt(i);//удаляем элемент из списка, он ушел в работу
                                schedule.Add(scheduleItem);//Добавляем строку расписания
                                break;

                            }
                        }
                    } //if (material == "") return null;
                } while ((currentMachineId >= 0) && (PartiesList.Count > 0));
                productionLine.workStep();//Запускаем рабочую линию
                clock.Step();//Отсчитываем единицу времени
            } while (PartiesList.Count > 0);
            return schedule;
        }

    }
}
