using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculatorTestWpf.Commands
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;


        // Событие, извещающее об измении состояния команды
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        // Конструктор команды
        // "execute" - выполняемый метод команды
        // "canExecute" - метод, разрешающий выполнение команды
        public RelayCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            canExecute = CanExecute;
        }

        // Вызов разрешающего метода команды
        // "parameter" - параметр команды
        /// true - если выполнение команды разрешено
        public bool CanExecute(object parameter) => canExecute?.Invoke(parameter) ?? true;

        // Вызов выполняющего метода команды
        // "parameter" - параметр команды
        public void Execute(object parameter) => execute(parameter);
    }
}
