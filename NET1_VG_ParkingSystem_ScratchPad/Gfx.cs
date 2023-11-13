using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET1_VG_ParkingSystem_ScratchPad
{
    internal class Gfx
    {
        public static void DrawTinyGarage(Garage garage, int garageGfxLeft, int garageGfxTop)
        {
            int miniTop = 0;
            int miniBot = 0;
            foreach(ParkingLot lot in garage.AllLots)
            {
                if (lot.Index % 2 == 0)
                {
                    Console.SetCursorPosition(garageGfxLeft + miniTop, garageGfxTop);
                    Console.ForegroundColor = (ConsoleColor)ColorStatus(lot.Space);
                    Console.Write(lot.MiniLotGfx);
                    miniTop++;
                }
                else
                {
                    Console.SetCursorPosition(garageGfxLeft + miniBot, garageGfxTop+1);
                    Console.ForegroundColor = (ConsoleColor)ColorStatus(lot.Space);
                    Console.Write(lot.MiniLotGfx);
                    miniBot++;
                }
            }

        }
        public static void DrawGarage(Garage garage, int garageGfxLeft, int garageGfxTop)
        {
            List<ParkingLot> oddLots = new List<ParkingLot>();
            List<ParkingLot> evenLots = new List<ParkingLot>();

            #region Split odd and even lots
            foreach (ParkingLot parkingLot in garage.AllLots)
            {
                if (parkingLot.Index % 2 == 0)
                {
                    evenLots.Add(parkingLot);
                }
                else
                {
                    oddLots.Add(parkingLot);
                }
            }
            #endregion

            Console.SetCursorPosition(garageGfxLeft, garageGfxTop);

            int allLots = garage.NumberOfLots;
            int parkingUpper = evenLots.Count;
            int parkingLower = oddLots.Count;
            int x = 0;


            #region Even lots aka upper row
            foreach (ParkingLot lotRow0 in evenLots)
            {
                double availableSpace = lotRow0.Space;
                for (int i = 0; i < 6; i++)
                {
                    Console.SetCursorPosition(garageGfxLeft + x, garageGfxTop + i);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("|");
                    Console.ForegroundColor = (ConsoleColor)ColorStatus(availableSpace);
                    Console.Write(lotRow0.LotGfx[i]);
                }
                x += 8;
            }

            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(garageGfxLeft + x, garageGfxTop + i);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("|");
            }

            #endregion

            #region Mid Divider


            for (int i = 0; i < parkingUpper; i++)
            {
                Console.SetCursorPosition(garageGfxLeft + (i * 8), garageGfxTop + 6);
                Console.Write("┼───────┼");
            }
            Console.SetCursorPosition(garageGfxLeft, garageGfxTop + 6);
            Console.Write("├");

            Console.SetCursorPosition(garageGfxLeft + (parkingUpper * 8), garageGfxTop + 6);

            if (allLots % 2 == 0)
            {
                Console.Write("┤");
            }
            else
            {
                Console.Write("┘");
            }
            #endregion

            x = 0;

            #region Odd lots aka lower row
            foreach (ParkingLot lotRow1 in oddLots)
            {
                double availableSpace = lotRow1.Space;
                for (int i = 0; i < 6; i++)
                {
                    Console.SetCursorPosition(garageGfxLeft + x, garageGfxTop + 7 + i);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("|");
                    Console.ForegroundColor = (ConsoleColor)ColorStatus(availableSpace);
                    Console.Write(lotRow1.LotGfx[i]);
                }
                x += 8;
            }

            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(garageGfxLeft + x, garageGfxTop + 7 + i);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("|");
            }
            #endregion

        }
        public static int ColorStatus(double availableSpace)
        {
            if (availableSpace == 1)
            {
                return 2; // 2 - DarkGreen
            }
            else if (availableSpace == 0.5)
            {
                return 6; // 6 - DarkYellow
            }
            else
            {
                return 4; // 4 - DarkRed
            }
        }
        public static void DrawMenu(int garageGfxLeft, int garageGfxTop, double cash)
        {
            int distanceLeft = 0;
            int distanceTop = 16;
            string cashStr = cash.ToString() + "                   ";
            cashStr = cashStr.Remove(15);
            string[] menuGfx = 
                {
                "╔══════════════════════╗",
                "║ F1: Toggle Map       ║",
                "╠══════════════════════╣",
                "║ F2: Next Vehicle     ║",
                "╠══════════════════════╣",
                "║ F3: Checkout Vehicle ║",
                "╠══════════════════════╣",
                "║ Cash: " + cashStr + "║",
                "╚══════════════════════╝",
                };
            // ╔═╦═╗
            // ╠═╬═╣
            // ║ ║ ║
            // ╚═╩═╝
            // ☺ ☻ ♥ ♦ ♣ ♠ • ◘ ○ ◙ ♂ ♀ ♪ ♫ ☼ ► ◄ ↕ ‼ ¶ § ▬ ↨ ↑ ↓
            // → ← ∟ ↔ ▲ ▼ ^ ` ⌂ Ü ü º¿¡ ⌐ ¬ ª ® © « » ░ ▒ ▓ █ ▄ ▌▐ ▀

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < menuGfx.Length; i++)
            {
                Console.SetCursorPosition(garageGfxLeft, garageGfxTop + distanceTop + i);
                Console.Write(menuGfx[i]);
            }
            Console.WriteLine();



        }
        public static void DrawFrame()
        {
            Console.ForegroundColor= ConsoleColor.DarkGray;
            for (int i = 0; i < 140;  i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("▒");
                Console.SetCursorPosition(i, 1);
                Console.Write("░");
            }
            for (int i = 0; i < 140; i++)
            {
                Console.SetCursorPosition(i, 32);
                Console.Write("░");
                Console.SetCursorPosition(i, 33);
                Console.Write("▒");
            }
            for (int i = 1; i < 33; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("▒▒░░");
            }
            for (int i = 1; i < 33; i++)
            {
                Console.SetCursorPosition(136, i);
                Console.Write("░░▒▒");
            }
            Console.SetCursorPosition(0, 34);
            Console.WriteLine("                                 ░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░                                 ");
            Console.WriteLine(" ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ ");
            Console.WriteLine("░▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░");
            Console.WriteLine("▒▓▒█████████▒▒███████▒▒▒▒██▒▒▒▒▒▒▒▒▒▒██▒▒▓▓▓██▓▓▓██▓▓▓██▓▓▓███▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓█████████████████████████████████████████████████▓▓▓▓▓▒");
            Console.WriteLine("▒▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓█▓▓█▓█▓▓█▓█▓▓█▓█▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▒");
            Console.WriteLine("▒▓▒▒▒▒████▒▒▒▒██▒▒▒▒▒██▒▒████▒▒▒▒▒▒████▒▒▓▓▓██▓▓█▓▓█▓▓██▓▓███▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▓███▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░░▓░░▓▓▓▓██▓▓▓▓▓▒");
            Console.WriteLine("▒▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓█▓▓█▓█▓▓█▓█▓▓█▓█▓▓█▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▓▒▒██▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▒");
            Console.WriteLine("▒▓▒▒▒▒████▒▒▒▒██▒▒▒▒▒██▒▒██████▒▒██████▒▒▓▓▓██▓▓▓██▓▓▓██▓▓▓██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▒░░░░░██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▓▓██▓▓▓▓▓▒");
            Console.WriteLine("▒▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▓▒▒██▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▒");
            Console.WriteLine("▒▓▒▒████████▒▒███████▒▒▒▒████▒▒██▒▒████▒▒▓▓███████████████████▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▓▓▓█▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▒");
            Console.WriteLine("▒▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓█████████████████████████████████████████████████▓▓▓▓▓▒");
            Console.WriteLine("░▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓░");
            Console.WriteLine(" ░░▒▒▒▒▒▒▒▒▒▒░░░                                                                                                          ░░░▒▒▒▒▒▒▒▒▒▒▒░░ ");
        }
        public static void BlinkGreen()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(122, 39);
            Console.Write("▒▒");
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(122, 39);
            Console.Write("▒▒");

        }
        public static void BlinkRed()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(125, 39);
            Console.Write("▒▒");
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(125, 39);
            Console.Write("▒▒");
        }












    }
}

#region colortable
// Black           0 ;
// DarkGray        8 ;
// Gray            7 ;
// White           15;
// DarkBlue        1 ;
// Blue            9 ;
// DarkCyan        3 ;
// Cyan            11;
// DarkGreen       2 ;
// Green           10;
// DarkYellow      6 ;
// Yellow          14;
// DarkMagenta     5 ;
// Magenta         13;
// DarkRed         4 ;
// Red             12;

#endregion
// ☺ ☻ ♥ ♦ ♣ ♠ • ◘ ○ ◙ ♂ ♀ ♪ ♫ ☼ ► ◄ ↕ ‼ ¶ § ▬ ↨ ↑ ↓
// → ← ∟ ↔ ▲ ▼ ^ ` ⌂ Ü ü º¿¡ ⌐ ¬ ª ® © « » ░ ▒ ▓ █ ▄ ▌▐ ▀
// ┌─┬─┐
// ├─┼─┤
// │ │ │
// └─┴─┘
// - _ / | \
// ╔═╦═╗
// ╠═╬═╣
// ║ ║ ║
// ╚═╩═╝
// ┌─┬─┐
// ├─┼─┤
// │ │ │
// └─┴─┘
// ╓─╥─╖ 
// ╟─╫─╢      
// ║ ║ ║
// ╙─╨─╜
// ╒═╤═╕
// ╞═╪═╡
// │ │ │
// ╘═╧═╛
// 
