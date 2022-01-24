using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorTestWpf.Model
{
    #region Сalculation

    static class MathCalculateArithmetics
    {
        public static double Calculate(string expression)
        {
            double result;

            while (true)
            {
                if (double.TryParse(expression, out result))
                {
                    return result;
                }
                string subExpression = GetFirstExpression(expression);
                string subResult = CalculateSubExpression(subExpression);

                string replaced = Regex.Replace(subExpression, @"[()×÷+-]", @"\$0");
                expression = new Regex(replaced).Replace(expression, subResult, 1);
            }
        }

        private static string CalculateSubExpression(string subExpression)
        {
            if (Regex.Match(subExpression, @"(\d+[\+|\-|\×|\÷]\d+)").Value == "")
            {
                throw new ArgumentException("Incorrect expression", subExpression);
            }

            subExpression = Regex.Replace(subExpression, @"[()]", "");

            while (Regex.Match(subExpression, @"(\d+[\+|\-|\×|\÷]\d+)").Value != "")
            {

                string firstSubExpression = GetFirstExpression(subExpression);

                string operatorExpression = Regex.Match(firstSubExpression, @"[\+|\-|\×|\÷]").Value;
                double firstNumber = Convert.ToDouble(Regex.Matches(firstSubExpression, @"\d+")[0].Value);
                double secondNumber = Convert.ToDouble(Regex.Matches(firstSubExpression, @"\d+")[1].Value);
                double result = 0;

                if (operatorExpression != "")
                {
                    switch (operatorExpression)
                    {
                        case "+":
                            result = firstNumber + secondNumber;
                            break;
                        case "-":
                            result = firstNumber - secondNumber;
                            break;
                        case "×":
                            result = firstNumber * secondNumber;
                            break;
                        case "÷":
                            try
                            {
                                if (secondNumber != 0)
                                {
                                    result = Math.Round((firstNumber / secondNumber), 6);
                                    break;
                                }
                            }
                            catch (DivideByZeroException ex)
                            {
                                result.ToString(ex.Message);
                                break;
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                string replaced = Regex.Replace(firstSubExpression, @"[()×÷+-]", @"\$0");
                subExpression = new Regex(replaced).Replace(subExpression, Convert.ToString(result), 1);
            }

            return subExpression;
        }

        private static string GetFirstExpression(string expression)
        {
            string subExpression;

            string patternBrackets = @"\([^(|)]+\)";
            subExpression = Regex.Match(expression, patternBrackets).Value;
            if (subExpression != "")
            {
                return subExpression;
            }

            string patternMultiply = @"\d+[\×|÷]\d+";
            subExpression = Regex.Match(expression, patternMultiply).Value;
            if (subExpression != "")
            {
                return subExpression;
            }

            string patternAdd = @"\d+[\+|-]\d+";
            subExpression = Regex.Match(expression, patternAdd).Value;
            if (subExpression != "")
            {
                return subExpression;
            }

            return expression;
        }
        #endregion
    }
}
