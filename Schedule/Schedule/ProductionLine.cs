using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    /// <summary>
    /// Линия станков
    /// </summary>
    class ProductionLine
    {
        List<Machine> machines = new List<Machine>();//Список станков линии
        public ProductionLine()
        {
        }

        /// <summary>
        /// Метод добавляет станок в линию и возвращает ссылку на список станков
        /// return: Линия станков List<Machine>
        /// </summary>
        /// <param name="listForMachine">Список параметров добавляемого станка</param>
        /// <returns>Линия станков</returns>
        public List<Machine> AddMachine(List<List<string>> listForMachine)
        {
            //Создаем новый станок
            Machine machine = new Machine(listForMachine.ElementAt(0).ElementAt(0),
                listForMachine.ElementAt(1), listForMachine.ElementAt(2));
            machines.Add(machine);//Добавляем станок в линию
            return machines;
        }

        /// <summary>
        /// Загружает материал в линию, в случае удачи - номер станка, неудача = -1
        /// return: Номер станка, в который загружен материал, -1 - неудача
        /// </summary>
        /// <param name="material">обрабатываемый материал</param>
        /// <returns>Номер станка, в который загружен материал. -1 - неудача</returns>
        /// 
        public int insertMaterial (string material)
        {
            int machineId = -1;
            int workingTime = -1;
            for (int i = 0; i < machines.Count; i++)
            {
                workingTime = machines.ElementAt(i).SetMachine(material);
                if (workingTime > 0)
                {
                    machineId = i;
                    break;
                }
            }
            return machineId;
        }
    }
}
