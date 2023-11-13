using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NET1_VG_ParkingSystem_ScratchPad
{
    public class ParkingLot
    {
        public string[] LotGfx { get; set; }
        public string MiniLotGfx { get; set; }
        public double Space { get; set; }
        public bool DoubleSpace { get; set; }
        public bool McSlot { get; set; }
        public bool CarOk { get; set; }
        public int Index { get; set; }
        public Vehicle Vehicle { get; set; }
        public ParkingLot(int index)
        {
            Index = index;
            Space = 1;
            DoubleSpace = false;
            McSlot = false;
            CarOk = false;
            string idxStr = this.Index.ToString();
            if (idxStr.Length < 2 ) { idxStr += " "; }
            idxStr = "  " + idxStr + " ";
            string[] lotGfx = new string[6];
            MiniLotGfx = "░";
            LotGfx = lotGfx;

            LotGfx[0] = "░░░░░░░";
            LotGfx[1] = "░░░░░░░";
            LotGfx[2] = "░" + idxStr + "░";
            LotGfx[3] = "░░░░░░░";
            LotGfx[4] = "░░░░░░░";
            LotGfx[5] = "░░░░░░░";

        }
    }
}
