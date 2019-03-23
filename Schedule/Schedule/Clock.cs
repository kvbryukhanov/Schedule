using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    /// <summary>
    /// Класс часы считает время
    /// </summary>
    class Clock
    {
        private int value = 0;
        
        public Clock()
        {
        }

        /// <summary>
        /// Прибавить одну единицу времени и возвращает текущее время
        /// </summary>
        /// <returns></returns>
        public int Step()
        {
            value++;
            return value;
        }

        /// <summary>
        /// Возвращает текущее время, не увеличивая его
        /// </summary>
        /// <returns>Текущее время</returns>
        public int GetValue()
        {
            return value;
        }

    }

    

}
