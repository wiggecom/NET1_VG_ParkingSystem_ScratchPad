using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;

namespace NET1_VG_ParkingSystem_ScratchPad

{
    internal class Program
    {
        public static double cash = 0;
        static void Main(string[] args)
        {
            Console.SetWindowSize(140, 48);
            Console.SetBufferSize(140, 48);
            Console.CursorVisible = false;
            List<Vehicle> tmpList = new List<Vehicle>();
            List<Vehicle> fullList = new List<Vehicle>();

            
            double pricePerSecond = 1.25;
            int garageSize = 15;  // Recommended value: 2 - 18
            int garageGfxLeft = 10;
            int garageGfxTop = 6;
            int menuSizeSwitch = 0;
            // -------------------------- //
            Garage garage = new Garage(garageSize);
            string removeReg = "";
            Console.Clear();
            Gfx.DrawFrame();
            Gfx.DrawTinyGarage(garage, garageGfxLeft, garageGfxTop);
            Gfx.DrawMenu(garageGfxLeft, garageGfxTop - 12, cash);
            DisplayList(garage, fullList, garageGfxTop, garageGfxLeft);
            while (true)
            {
                #region Read Keys
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.F1:
                            Console.Clear();
                            if (menuSizeSwitch == 0)
                            {
                                Gfx.DrawFrame();
                                Gfx.DrawGarage(garage, garageGfxLeft, garageGfxTop);
                                Gfx.DrawMenu(garageGfxLeft, garageGfxTop - 1, cash);
                                DisplayList(garage, fullList, garageGfxTop, garageGfxLeft);
                                menuSizeSwitch = 1;
                            }
                            else
                            {
                                Gfx.DrawFrame();
                                Gfx.DrawTinyGarage(garage, garageGfxLeft, garageGfxTop);
                                Gfx.DrawMenu(garageGfxLeft, garageGfxTop - 12, cash);
                                DisplayList(garage, fullList, garageGfxTop, garageGfxLeft);
                                menuSizeSwitch = 0;
                            }
                            break; // Toggle Mapsize
                        case ConsoleKey.F2:
                            tmpList = Helpers.MakeRndVehicle(tmpList);  // Make random vehicle
                            Vehicle v = tmpList[tmpList.Count - 1];
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.SetCursorPosition(10, 4);
                            Console.WriteLine("                                                                                           ");
                            Console.SetCursorPosition(10, 4);
                            Console.WriteLine("Here comes a " + v.VehicleType + ", it's " + v.Color + "!  Can it park here?");
                            Console.ReadKey();
                            FindParking(garage, v);
                            if (v.ParkedLocation < 50)
                            {
                                v.DriveIn();
                                fullList.Add(v);
                            }
                            tmpList.Clear();

                            PrintMenu(garage, fullList, garageGfxTop, garageGfxLeft, menuSizeSwitch, cash);
                            Gfx.BlinkGreen();
                            tmpList.Clear(); // remove vehicle from tmplist
                            break; // Generate Vehicle
                        case ConsoleKey.F3:
                            removeReg = CheckOutVehicle(garage, fullList, garageGfxTop, garageGfxLeft, menuSizeSwitch, pricePerSecond);
                            if (removeReg != "")
                            {
                                garage = ClearParkinglot(garage, fullList, removeReg);
                                foreach (Vehicle vRem in fullList)
                                {
                                    if (vRem.Registration == removeReg)
                                    {
                                        fullList.Remove(vRem);
                                        break;
                                    }
                                }
                            }
                            PrintMenu(garage, fullList, garageGfxTop, garageGfxLeft, menuSizeSwitch, cash);
                            
                            Gfx.BlinkRed();
                            break; // Checkout Vehicle
                        default:
                            break;
                    }
                }
                #endregion
            }
        }
        public static Garage ClearParkinglot(Garage garage, List<Vehicle> fullList, string removeRegistration)
        {
            foreach (Vehicle v in fullList)
            {
                if (v.Registration == removeRegistration)
                {
                    int lot = v.ParkedLocation;
                    if (v is MC)
                    {
                        garage.AllLots[lot].Space += 0.5;
                    }
                    else if (v is Car)
                    {
                        garage.AllLots[lot].Space += 1;
                    }
                    else if (v is Bus) 
                    {
                        garage.AllLots[lot].Space += 1;
                        garage.AllLots[lot-1].Space += 1;
                    }
                }
            }
            return garage;
        }
        public static string CheckOutVehicle(Garage garage, List<Vehicle> fullList, int garageGfxTop, int garageGfxLeft, int menuSizeSwitch, double pricePerSecond)
        {
            if (menuSizeSwitch == 0)
            {
                garageGfxTop -= 11;
            }
            else
            {

            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(garageGfxLeft + 25, garageGfxTop + 15);
            Console.Write("╔══════════════════════╗");
            Console.SetCursorPosition(garageGfxLeft + 25, garageGfxTop + 16);
            Console.Write("║ Enter registration   ║");
            Console.SetCursorPosition(garageGfxLeft + 25, garageGfxTop + 17);
            Console.Write("╠══════════════════════╣");
            Console.SetCursorPosition(garageGfxLeft + 25, garageGfxTop + 18);
            Console.Write("║                      ║");
            Console.SetCursorPosition(garageGfxLeft + 25, garageGfxTop + 19);
            Console.Write("╠══════════════════════╣");
            Console.SetCursorPosition(garageGfxLeft + 25, garageGfxTop + 20);
            Console.Write("║ 1 buck / second plx! ║");
            Console.SetCursorPosition(garageGfxLeft + 25, garageGfxTop + 21);
            Console.Write("╚══════════════════════╝");
            Console.CursorVisible = true;
            Console.SetCursorPosition(garageGfxLeft + 27, garageGfxTop + 18);
            string removeReg = "";
            string regNoChk = Console.ReadLine().ToUpper();
            bool foundVehicle = false;
            Console.CursorVisible = false;

            foreach (Vehicle v in fullList)
            {
                if (v.Registration == regNoChk)
                {

                    foundVehicle = true;
                    TimeSpan timeToPay = DateTime.Now - v.ParkingStarted;
                    double secondsToPay = timeToPay.Seconds*pricePerSecond;
                    cash += secondsToPay;
                    string payUp = secondsToPay.ToString() + "B                ";
                    payUp = payUp.Remove(8);
                    Console.SetCursorPosition(garageGfxLeft + 25, garageGfxTop + 20);
                    Console.Write("║ Sum to pay: " + payUp + " ║");
                    // remove vehicle //
                    removeReg = v.Registration;
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
            }
            if (foundVehicle == false)
            {
                Console.SetCursorPosition(garageGfxLeft + 25, garageGfxTop + 20);
                Console.Write("║ Vehicle not found    ║");
                Console.ReadKey();
                Console.Clear();
            }
            return removeReg;
        }
        public static void PrintMenu(Garage garage, List<Vehicle> fullList, int garageGfxTop, int garageGfxLeft, int menuSizeSwitch, double cash)
        {
            if (menuSizeSwitch == 1)
            {
                for (int i = 0; i < 17; i++)
                {
                    Console.CursorTop = i; Console.WriteLine();
                }
                Gfx.DrawFrame();
                Gfx.DrawGarage(garage, garageGfxLeft, garageGfxTop);
                Gfx.DrawMenu(garageGfxLeft, garageGfxTop - 1, cash);
                DisplayList(garage, fullList, garageGfxTop, garageGfxLeft);
            }
            else
            {
                Gfx.DrawFrame();
                Gfx.DrawTinyGarage(garage, garageGfxLeft, garageGfxTop);
                Gfx.DrawMenu(garageGfxLeft, garageGfxTop - 12, cash);
                DisplayList(garage, fullList, garageGfxTop, garageGfxLeft);
            }
            
        }
        public static void DisplayList(Garage garage, List<Vehicle> fullList, int garageGfxTop, int garageGfxLeft)
        {
            int listLength = fullList.Count;
            garageGfxLeft = 2;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(garageGfxLeft + 15 + (garage.NumberOfLots * 4), garageGfxTop);
            Console.Write("▓▓▓▓▓▓▓▓▓▓▓▓▓▓ Current guests ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
            for (int i = 0; i < listLength; i++)
            {
                Console.SetCursorPosition(garageGfxLeft + 10, garageGfxTop + 2);
                fullList.Sort((x, y) => (x.Registration.CompareTo(y.Registration)));
                foreach (Vehicle vehicle in fullList)
                {
                    Console.CursorLeft = garageGfxLeft + 15 + (garage.NumberOfLots * 4);
                    string printMe = vehicle.Registration + " - " + vehicle.VehicleType + ", " + vehicle.Color + "                           ";
                    printMe = printMe.Remove(45);
                    Console.WriteLine(printMe);

                }
            }
        }
        public static void FindParking(Garage garage, Vehicle vehicle)
        {
            foreach (ParkingLot p in garage.AllLots)
            {
                if (p.Index % 2 != 0)
                {
                    int idx = p.Index;
                    if (p.Space == 1 && garage.AllLots[idx - 1].Space == 1)
                    {
                        p.DoubleSpace = true;
                        p.CarOk = false;
                        p.McSlot = false;
                        garage.AllLots[idx - 1].DoubleSpace = true;
                        garage.AllLots[idx - 1].CarOk = false;
                        garage.AllLots[idx - 1].McSlot = false;
                    }
                    else if (p.Space == 0.5)
                    {
                        p.DoubleSpace = false;
                        p.CarOk = false;
                        p.McSlot = true;
                    }
                    else if (p.Space == 1 && p.DoubleSpace == false)
                    {
                        p.CarOk = true;
                        p.McSlot = false;
                    }
                    else
                    {
                        p.DoubleSpace = false;
                        p.CarOk = false;
                        p.McSlot = false;
                    }

                }
            } // Flag odd parkings with suitability (because even may have an extra slot if uneven)
            foreach (ParkingLot p in garage.AllLots)
            {
                if (p.Index % 2 == 0)
                {
                    int idx = p.Index;
                    if (p.Space == 0.5)
                    {
                        p.DoubleSpace = false;
                        p.CarOk = false;
                        p.McSlot = true;
                    }
                    else if (p.Space == 1 && p.DoubleSpace == false)
                    {
                        p.CarOk = true;
                    }
                    else
                    {
                        p.DoubleSpace = false;
                        p.CarOk = false;
                        p.McSlot = false;
                    }
                }
            } // Flag even parkings with suitability

            bool parkingFound = false;

            //first loop tries to find mcslot, unique for only mc's
            if (vehicle is MC)
            {
                foreach (ParkingLot p in garage.AllLots)
                {
                    if (p.McSlot == true)
                    {
                        vehicle.ParkedLocation = p.Index;
                        //                        p.McSlot = false;
                        p.Space -= 0.5;
                        parkingFound = true;
                        vehicle.ParkingStarted = DateTime.Now;
                        break;
                    }
                }
            }
            //second loop tries to find carok, shared for car and mc (that didn't find an mcslot)
            if ((vehicle is MC && parkingFound == false) || vehicle is Car)
            {
                //first check if there is an odd lot available, the odd will not fit a bus and needs to be checked first
                if (garage.AllLots.Length % 2 != 0 && garage.AllLots[garage.AllLots.Length - 1].CarOk == true && parkingFound == false)
                {
                    vehicle.ParkedLocation = garage.AllLots[garage.AllLots.Length - 1].Index;
                    vehicle.ParkingStarted = DateTime.Now;
                    parkingFound = true;
                    if (vehicle is MC) { garage.AllLots[garage.AllLots.Length - 1].Space -= 0.5; }  //p.McSlot = true;
                    else if (vehicle is Car) { garage.AllLots[garage.AllLots.Length - 1].Space -= 1; } //p.CarOk = false;
                }

                if (parkingFound == false)
                {
                    foreach (ParkingLot p in garage.AllLots)
                    {
                        if (p.CarOk == true)
                        {
                            vehicle.ParkedLocation = garage.AllLots[garage.AllLots.Length - 1].Index;
                            vehicle.ParkingStarted = DateTime.Now;
                            parkingFound = true;
                            if (vehicle is MC) { p.Space -= 0.5; }  //p.McSlot = true;
                            else if (vehicle is Car) { p.Space -= 1; }
                            //p.CarOk = false;
                        }
                    }

                }
            }
            //third loop tries to find a space that is dobulespace, and fits any vehicle if paired
            if (vehicle is Bus || vehicle is MC || vehicle is Car)
            {

                foreach (ParkingLot p in garage.AllLots)
                {
                    if (p.DoubleSpace == true && parkingFound == false)
                    {
                        vehicle.ParkedLocation = p.Index;
                        vehicle.ParkingStarted = DateTime.Now;
                        parkingFound = true;
                        if (vehicle is MC) { p.Space -= 0.5; } // p.McSlot = true; 
                        else if (vehicle is Car) { p.Space -= 1; }
                        // p.CarOk = false;
                        //else if (vehicle is Bus && (p.Index % 2 == 0))
                        //{
                        //    //p.Space -= 1;
                        //   // garage.AllLots[p.Index + 1].Space -= 1;
                        //   // vehicle.ParkedLocationNxt = garage.AllLots[p.Index + 1].Index;
                        //}  // Bus enters at top row
                        else if (vehicle is Bus && (p.Index % 2 != 0))
                        {
                            p.Space -= 1;
                            garage.AllLots[p.Index - 1].Space -= 1;
                            vehicle.ParkedLocationNxt = garage.AllLots[p.Index - 1].Index;
                        }  // Bus enters at low row
                    }
                }
            }

            if (parkingFound == false)
            {
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("                                                                                           ");
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("Unfortunately there was no available parking for the " + vehicle.VehicleType);
            }
            else
            {
                Console.SetCursorPosition(4, 4);
                Console.WriteLine("                                                                                           ");
            }
        }
    }
}

