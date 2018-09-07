using ILeaf.Repository;
using ILeaf.Service;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FormatWeeks(new byte[] { 1, 2, 3, 4, 5, 6, 8, 9, 10, 23, 56, 9 }));
            Console.Read();
        }
        public static string FormatWeeks(byte[] weeks)
        {
            StringBuilder sb = new StringBuilder();
            int currentRangeStart = 0;
            int currentRangeEnd = 0;
            int lastNumber = 0;
            int currentNumber = 0;
            for (int i = 0; i != weeks.Length; i++)
            {
                currentNumber = weeks[i];
                if (currentNumber == 0)
                    break;
                if (lastNumber != 0)
                {
                    if (currentNumber == lastNumber + 1)
                        currentRangeEnd = currentNumber;
                    else
                    {
                        if (currentRangeEnd == currentRangeStart)
                            sb.Append($"{currentRangeStart}, ");
                        else
                            sb.Append($"{currentRangeStart}~{currentRangeEnd}, ");
                        currentRangeEnd = currentNumber;
                        currentRangeStart = currentNumber;
                    }
                }
                else
                {
                    currentRangeStart = currentNumber;
                    currentRangeEnd = currentNumber;
                }

                lastNumber = currentNumber;
            }
            if (currentNumber == lastNumber + 1)
                sb.Append($"{currentRangeStart}~{currentNumber}");
            else
                sb.Append($"{currentNumber}");

            return sb.ToString();
        }
    }
}
