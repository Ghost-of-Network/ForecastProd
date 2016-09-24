using System;
using System.Linq;

namespace ForecastProd
{
    public static class Сalc
    {
        public static double avg(double[] array)
        {
            return array.Average();
        }

        public static double[] avgProd(double[] actual)
        {
            double[] avgActual = new double[actual.Length];
            avgActual[0] = (actual[0] + actual[1]) / 2;
            for (int i = 1; i < avgActual.Length - 1; i++)
            {
                avgActual[i] = (actual[i - 1] + actual[i] + actual[i + 1]) / 3;
            }
            avgActual[avgActual.Length - 1] = (actual[avgActual.Length - 2] + actual[avgActual.Length - 1]) / 2;

            return avgActual;
        }

        public static double[] lnWor(double[] avgActual)
        {
            double[] lnWor = new double[avgActual.Length];
            for (int i = 0; i < avgActual.Length; i++)
            {
                lnWor[i] = Math.Log(avgActual[i] / (1 - avgActual[i]));
            }
            return lnWor;
        }

        /// <summary>
        /// Вычисляет добытые НИЗ
        /// </summary>
        /// <param name="cumulativeProd">Накопленная добыча</param>
        /// <param name="irr">Начальные извлекаемые запасы</param>
        /// <returns>Массив чисел</returns>
        public static double[] mined(double[] cumulativeProd, double irr)
        {
            double[] mined = new double[cumulativeProd.Length];

            mined[0] = cumulativeProd[0] / irr;
            for (int i = 1; i < cumulativeProd.Length - 1; i++)
            {
                mined[i] = cumulativeProd[i] / irr;
            }
            mined[mined.Length - 1] = cumulativeProd[cumulativeProd.Length - 1] / irr;

            return mined;
        }

        /// <summary>
        /// Возвращает наклон линии линейной регрессии для точек данных в аргументах
        /// </summary>
        /// <param name="arrayY">Известные значения Y</param>
        /// <param name="arrayX">Известные значения X</param>
        public static double incline(double[] arrayY, double[] arrayX)
        {
            double incline;

            double[] lnWorError = new double[arrayY.Length];
            double avgLnWor = arrayY.Average();
            for (int i = 0; i < lnWorError.Length; i++)
            {
                lnWorError[i] = arrayY[i] - avgLnWor;
            }

            double[] minedError = new double[arrayX.Length];
            double avgmined = arrayX.Average();
            for (int i = 0; i < minedError.Length; i++)
            {
                minedError[i] = arrayX[i] - avgmined;
            }


            double[] multNumerator = new double[lnWorError.Length];
            double[] multDenominator = new double[minedError.Length];
            for (int i = 0; i < lnWorError.Length; i++)
            {
                multNumerator[i] = lnWorError[i] * minedError[i];
                multDenominator[i] = minedError[i] * minedError[i];
            }

            incline = summ(multNumerator) / summ(multDenominator);

            return incline;
        }

        /// <summary>
        /// Вычисляет точку пересечения линии с осью y, используя значения аргументов 
        /// </summary>
        /// <param name="arrayY">Известные значения Y</param>
        /// <param name="arrayX">Известные значения X</param>
        public static double line(double[] arrayY, double[] arrayX, double incline)
        {
            double line;

            //   double[] lnWorError = new double[arrayY.Length];
            double avgLnWor = arrayY.Average();
            double[] lnWorError = uncertainty(arrayY, avgLnWor);

            //  double[] minedError = new double[arrayX.Length];
            double avgmined = arrayX.Average();
            double[] minedError = uncertainty(arrayX, avgLnWor);

            line = avgLnWor - incline * avgmined;
            return line;

        }

        /// <summary>
        /// Находит значения функции аппроксимирующей прямой
        /// </summary>
        public static double[] findApproximatedStraight(double[] yArray, double[] xArray)
        {
            double[] y = new double[yArray.Length];
            for (int i = 0; i < yArray.Length; i++)
            {
                y[i] = findAB(yArray, xArray)[1] + findAB(yArray, xArray)[0] * xArray[i];
            }
            return y;
        }

        /// <summary>
        /// Находит a и b для аппроксимирующей прямой
        /// </summary>
        private static double[] findAB(double[] yArray, double[] xArray)
        {
            double[] result = new double[2];

            double xy = 0;
            double sum_x = 0, sum_y = 0, sum_sqr_x = 0;

            for (int i = 0; i < yArray.GetLength(0); i++)
            {
                xy += xArray[i] * yArray[i];
                sum_x += xArray[i];
                sum_y += yArray[i];
                sum_sqr_x += Math.Pow(xArray[i], 2);
            }
            result[0] = (yArray.GetLength(0) * xy - sum_x * sum_y) / (yArray.GetLength(0) * sum_sqr_x - Math.Pow(sum_x, 2));
            result[1] = (sum_y - result[0] * sum_x) / yArray.GetLength(0);

            return result;
        }

        /// <summary>
        /// Находит погрешность известных значений
        /// </summary>
        private static double[] uncertainty(double[] array, double avgArray)
        {
            double[] uncertaintyArray = new double[array.Length];
            for (int i = 0; i < uncertaintyArray.Length; i++)
            {
                uncertaintyArray[i] = array[i] - avgArray;
            }
            return uncertaintyArray;
        }

        /// <summary>
        /// Находит сумму всех элементов массива
        /// </summary>>
        private static double summ(double[] array)
        {
            double summ = 0;
            foreach (double item in array)
            {
                summ += item;
            }
            return summ;
        }
    }
}
