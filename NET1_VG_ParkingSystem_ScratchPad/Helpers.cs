using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET1_VG_ParkingSystem_ScratchPad
{
    internal class Helpers
    {
        public static Garage BuildGarage(Garage garage)
        {
            for (int i = 0; i < garage.NumberOfLots; i++)
            {
                garage.AllLots[i] = new ParkingLot(i);
            }
            return garage;
        }
        public static string MakeRndRegNo()
        {
            string regNo = "";
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                int x = rnd.Next(0, 26);
                char a = (char)('A' + x);
                regNo += a;
            }
            for (int i = 0; i < 3; ++i)
            {
                int x = rnd.Next(0,10);
                regNo += x;
            }
            return regNo;
        }
        public static string MakeRndVclCol()
        {
            string[] allColors =
            {
                "Porche Red",
                "Black",
                "Cherry Red",
                "Apple Green",
                "Epic Purple",
                "Pearl White",
                "Deep-Sky Blue",
                "Ocean Green",
                "Tangerine",
                "Sunburst Orange",
                "Rusty",
                "Electric Blue",
                "Hot Pink",
                "Silver",
                "Gold",
                "Chrome",
                "Copper",
                "Holographic Metallic",
                "Lambo Yellow"
            };
            Random rnd = new Random();
            int i = rnd.Next(allColors.Length);
            string vclCol = allColors[i];
            return vclCol;
        }
        public static string MakeMcBrand()
        {
            string[] mcBrands =
            {
                "Harley",
                "Suzuki",
                "Yamaha",
                "Triumph",
                "Enfield",
                "Buell",
                "Kawasaki",
                "Norton",
                "Honda"
            };
            Random rnd = new Random();
            int i = rnd.Next(mcBrands.Length);
            string brand = mcBrands[i];
            return brand;
        }
        public static bool MakeCarEv()
        {
            Random rnd = new Random();
            bool ev = false;
            int i = rnd.Next(0,101);
            if (i <= 30)
            {
                ev = true;
            }
            else
            {
                ev = false;
            }
            return ev;
        }
        public static int MakeBusSeats()
        {
            Random rnd = new Random();
            int seats = 0;
            seats = rnd.Next(7, 20);
            return seats;
        }
        public static List<Vehicle> MakeRndVehicle(List<Vehicle> tmpList)
        {
            Random rnd = new Random();
            int typeVcl = rnd.Next(1, 101);
            if (typeVcl <= 30) 
            {
                tmpList.Add(new MC(Helpers.MakeRndRegNo(), Helpers.MakeRndVclCol(), Helpers.MakeMcBrand()));
            }// 1-2 = MC
            else if (typeVcl <= 80) 
            {
                tmpList.Add(new Car(Helpers.MakeRndRegNo(), Helpers.MakeRndVclCol(), Helpers.MakeCarEv()));
            }// 3-5 = Car
            else 
            {
                 tmpList.Add(new Bus(Helpers.MakeRndRegNo(), Helpers.MakeRndVclCol(), Helpers.MakeBusSeats()));
            }// 6 = Bus
            return tmpList;
        }
    }
}
