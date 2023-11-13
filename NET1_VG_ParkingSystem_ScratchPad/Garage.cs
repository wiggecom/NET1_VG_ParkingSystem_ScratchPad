using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace NET1_VG_ParkingSystem_ScratchPad
{
    public class Garage
    {
        public int NumberOfLots { get; set; }
        public ParkingLot[] AllLots { get; set; }
        public Garage(int numberOfLots)
        {
            NumberOfLots = numberOfLots;
            ParkingLot[] allLots = new ParkingLot[numberOfLots];
            AllLots = allLots;
            Helpers.BuildGarage(this);
        }
    }
}
