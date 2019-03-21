using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Класс станок
/// </summary>
/// <param name="materialsList">Список возможных материалов</param>
/// <param name="workTimeList">Список времени обработки материала соответственно</param>
namespace Schedule
{
    class Machine
    {
        List<string> MaterialsList { get; set; }
        List<string> WorkTimeList { get; set; }
        private int WorkingTime { get; set; } //Хранит оставшееся количество единиц времени работы

        public Machine(List<string> materialsList, List<string> workTimeList)
        {
            MaterialsList = materialsList;
            WorkTimeList = workTimeList;
        }

        /// <summary>
        /// Загрузить материал в станок
        /// </summary>
        /// <param name="material">Материал</param>
        /// <returns>Время работы станка, 0 - материал не подходит</returns>
        public int SetMachine (string material)
        {
            int canMake = 0; //1 - есть возможность обработки; 0 - материал недоступен
            int materialKind = -1; //хранит данные о усешно проверенном матерале
            foreach (string element in MaterialsList)
            {
                for (int i = 0; i < MaterialsList.Count; i++)
                    if (MaterialsList.ElementAt(i) == element)
                    {
                        canMake = 1;
                        materialKind = i;
                    } 
            }
            if (canMake == 1)
            {
               this.WorkingTime = Convert.ToInt32(WorkTimeList.ElementAt(materialKind));
            }
            return this.WorkingTime;
        }

        /// <summary>
        /// Сделать шаг работы с текущим матералом
        /// </summary>
        /// <returns>Оставшееся время работы</returns>
        public int Step ()
        {
            if (WorkingTime != 0)
                WorkingTime--;
            return WorkingTime;

        }
    } 
}
