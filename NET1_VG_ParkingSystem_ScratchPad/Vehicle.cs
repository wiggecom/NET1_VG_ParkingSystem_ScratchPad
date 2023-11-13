using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NET1_VG_ParkingSystem_ScratchPad
{
    public class Vehicle
    {
        public virtual string VehicleType { get; set; }
        public virtual double Size { get; set; } = 1;
        public string Registration { get; set; }
        public virtual int ParkedLocation { get; set; } = 99;
        public virtual int ParkedLocationNxt { get; set; } = 99;
        public virtual DateTime ParkingStarted { get; set; }
        public string Color { get; set; }
        public Vehicle(string registration, string color)
        {
            ParkingStarted = DateTime.MinValue;
            Registration = registration;
            Color = color;

        }
        internal virtual void DriveIn()
        {
            //Console.WriteLine("The vehicle drives in to lot " + parkedLocationX + " on row " + parkedLocationY);
            this.ParkingStarted = DateTime.Now;
        }
    }
    class MC : Vehicle
    {
        public override string VehicleType { get; set; } = "Motorcycle";
        public override double Size { get; set; } = 0.5;
        public override int ParkedLocation { get; set; } = 99;
        public override int ParkedLocationNxt { get; set; } = 99;
        public string Brand { get; set; }
        public override DateTime ParkingStarted { get; set; }

        public MC(string registration, string color, string brand) : base(registration, color)
        {
            ParkingStarted = DateTime.MinValue;
            Brand = brand;
        }
        internal override void DriveIn()
        {
            Console.SetCursorPosition(10,4);
            Console.WriteLine("                                                                                           ");
            Console.SetCursorPosition(10, 4);
            Console.WriteLine("The " + this.Color + " " + this.Brand + " motorcycle drives in to park");

        }
    }
    class Car : Vehicle
    {
        public override string VehicleType { get; set; } = "Car";
        public override int ParkedLocation { get; set; } = 99;
        public override int ParkedLocationNxt { get; set; } = 99;
        public override double Size { get; set; } = 1;
        public bool Ev { get; set; }
        public override DateTime ParkingStarted { get; set; }

        public Car(string registration, string color, bool ev) : base(registration, color)
        {
            ParkingStarted = DateTime.MinValue;
            Ev = ev;
        }
        internal override void DriveIn()
        {
            //// ParkVehicle(); // Scan for the most suitable spot and mark location as occupied by size
            if (this.Ev)
            {
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("                                                                                           ");
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("The " + this.Color + " electric car silently rolls in to park");
            }
            else
            {
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("                                                                                           ");
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("The " + this.Color + " regular car rolls in to park");
            }
        }
    }
    class Bus : Vehicle
    {
        public override string VehicleType { get; set; } = "Bus";
        public override double Size { get; set; } = 2;
        public override int ParkedLocation { get; set; } = 99;
        public override int ParkedLocationNxt { get; set; } = 99;
        public int Seats { get; set; }
        public override DateTime ParkingStarted { get; set; }

        public Bus(string registration, string color, int seats) : base(registration, color)
        {
            ParkingStarted = DateTime.MinValue;
            Seats = seats;
        }
        internal override void DriveIn()
        {
            if (this.Seats <= 10)
            {
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("                                                                                           ");
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("The small " + this.Color + " bus with " + this.Seats + " seats drives to park");
            }
            else if (this.Seats <= 15)
            {
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("                                                                                           ");
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("The medium sized " + this.Color + " bus with " + this.Seats + " seats gently drives to park");
            }
            else
            {
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("                                                                                           ");
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("The big " + this.Color + " bus with " + this.Seats + " seats squeezes in to park");
            }
        }
    }
}
