using QuanLib.Chemical.Extensions;
using QuanLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.TestConsole
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            int number = 1;
            while (true)
            {
                Element element = PeriodicTable.GetElement((ElementSymbol)number);
                ShowElementInfo(element);

                read:
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.UpArrow:
                        number--;
                        if (number < 1)
                            number = 118;
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.DownArrow:
                        number++;
                        if (number > 118)
                            number = 1;
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        Console.Write("--> ");
                        string? input = Console.ReadLine();
                        if (!string.IsNullOrEmpty(input))
                        {
                            if (int.TryParse(input, out number))
                            {
                                number = Math.Clamp(number, 1, 118);
                                break;
                            }
                            if (Enum.TryParse<ElementSymbol>(input, true, out var elementSymbol))
                            {
                                number = (int)elementSymbol;
                                break;
                            }
                        }
                        goto default;
                    case ConsoleKey.Escape:
                        goto exit;
                    default:
                        goto read;
                }
            }

            exit:;
        }

        private static void ShowElementInfo(Element element)
        {
            ArgumentNullException.ThrowIfNull(element, nameof(element));

            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(element.GetPropertiesInfo());

            Console.WriteLine("ElectronsPerShell: " + ObjectFormatter.Format(element.GetElectronsPerShell()));
            Console.WriteLine();

            Console.WriteLine("StableIsotop: " + element.GetStableIsotope());
            Isotope[] isotopes = PeriodicTable.GetIsotopes(element.Symbol);
            foreach (Isotope isotope in isotopes)
                Console.WriteLine(isotope);
        }
    }
}
