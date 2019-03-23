using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Schedule
{
    /// <summary>
    /// Класс станок
    /// </summary>
    /// <param name="materialsList">Список возможных материалов</param>
    /// <param name="workTimeList">Список времени обработки материала соответственно</param>
    class Machine
    {
        List<string> MaterialsList { get; set; }
        List<string> WorkTimeList { get; set; }
        private int WorkingTime { get; set; } //Хранит оставшееся количество единиц времени работы
        string Name { get; set; }//название станка
        private bool busy = false; //1 - занят, 0 - свободен

        public Machine(string name, List<string> materialsList, List<string> workTimeList)
        {
            Name = name;
            MaterialsList = materialsList;
            WorkTimeList = workTimeList;
        }

        /// <summary>
        /// Загрузить материал в станок. Устанавливает время работы станка в случае удачи. 
        /// return: 0 - материал не подходит, -1 - станок занят, время работы станка - удача
        /// </summary>
        /// <param name="material">Материал</param>
        /// <returns>Время работы станка, 0 - материал не подходит, -1 - станок занят</returns>
        public int SetMachine(string material)
        {
            if (busy == false)
            {
                int canMake = 0; //1 - есть возможность обработки; 0 - материал недоступен
                int materialKind = -1; //хранит данные о усешно проверенном матерале
                foreach (string element in MaterialsList)
                {
                    if (element == material)
                    {
                        canMake = 1;
                        materialKind = MaterialsList.IndexOf(element);
                        busy = true;
                    }
                }
                if (canMake == 1)
                {
                    this.WorkingTime = Convert.ToInt32(WorkTimeList.ElementAt(materialKind));
                }
                return this.WorkingTime;
            }
            else
                return -1;
        }

        /// <summary>
        /// Сделать шаг работы с текущим матералом
        /// </summary>
        /// <returns>Оставшееся время работы</returns>
        public int Step ()
        {
            if (WorkingTime != 1)
                WorkingTime--;
            else
            {
                busy = false;
                WorkingTime--;
            }
            return WorkingTime;

        }

        /// <summary>
        /// Возвращет значение неотработанного времени
        /// </summary>
        /// <returns></returns>
        public int GetWorkingTime()
        {
            return WorkingTime;
        }
    } 
}
