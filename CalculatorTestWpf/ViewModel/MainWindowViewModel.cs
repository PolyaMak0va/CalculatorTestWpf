using CalculatorTestWpf.Commands;
using CalculatorTestWpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculatorTestWpf.ViewModel
{
    class MainWindowViewModel : ViewModel
    {
        #region Properties

        public ObservableCollection<Arithmetics> ExpressionHistory { get; set; }

        public Arithmetics CurrentExpression { get; set; }

        #endregion

        #region Commands

        public ICommand ButtonPressCommand { get; }
        private bool CanButtonPressCommandExecute(object p) => true;
        private void OnButtonPressCommandExecute(object p)
        {
            CurrentExpression.AddChar(p);
            OnPropertyChanged("CurrentExpression");
        }

        public ICommand EqualPressCommand { get; }
        private bool CanEqualPressCommandExecute(object p) => true;
        private void OnEqualPressCommandExecute(object p)
        {
            if (CurrentExpression.Content != "")
            {
                CurrentExpression.CalculateResult();
                ExpressionHistory.Add(CurrentExpression);
                OnPropertyChanged("CurrentExpression");

                CurrentExpression = new Arithmetics();
            }
        }

        public ICommand ClearPressCommand { get; }
        private bool CanClearPressCommandExecute(object p) => true;
        private void OnClearPressCommandExecute(object p)
        {
            CurrentExpression.Clear();
            OnPropertyChanged("CurrentExpression");
        }

        public ICommand DeletePressCommand { get; }
        private bool CanDeletePressCommandExecute(object p) => true;
        private void OnDeletePressCommandExecute(object p)
        {
            CurrentExpression.Delete();
            OnPropertyChanged("CurrentExpression");
        }

        #endregion

        public MainWindowViewModel()
        {
            ButtonPressCommand = new RelayCommand(OnButtonPressCommandExecute, CanButtonPressCommandExecute);
            EqualPressCommand = new RelayCommand(OnEqualPressCommandExecute, CanEqualPressCommandExecute);
            ClearPressCommand = new RelayCommand(OnClearPressCommandExecute, CanClearPressCommandExecute);
            DeletePressCommand = new RelayCommand(OnDeletePressCommandExecute, CanDeletePressCommandExecute);

            ExpressionHistory = new ObservableCollection<Arithmetics>();
            CurrentExpression = new Arithmetics();
        }
    }
}
