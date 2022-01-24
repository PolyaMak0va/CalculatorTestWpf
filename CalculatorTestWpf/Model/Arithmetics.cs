using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTestWpf.Model
{
    class Arithmetics
    {
        #region Initialization of fields for display

        // Установлены поля для ввода значений и вывода результата в TextBlock
        private string content;
        private double result;

        // Установлены свойства для ввода значений и вывода результата
        public string Content { get => content; }
        public double Result { get => result; }

        #endregion

        #region Constructors
        // Конструктор класса
        public Arithmetics() { }

        public Arithmetics(string expression)
        {
            content = expression;
        }

        #endregion

        #region Methods

        // Метод для добавления знака/числа в поле для ввода
        public void AddChar(object obj)
        {
            char symbol = Convert.ToChar(obj);
            content += symbol;
        }

        // Метод для вычисления выражения
        public void CalculateResult()
        {
            result = MathCalculateArithmetics.Calculate(content);
        }

        // Метод для очистки поля
        public void Clear()
        {
            content = " ";
            result = 0;
        }

        // Метод для удаления последнего знака/числа (backspace)
        public void Delete()
        {
            if (content.Length > 0)
            {
                content = content.Remove(content.Length - 1);
            }
            else
            {
                result = 0;
            }
        }

        #endregion
    }
}
