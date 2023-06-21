using System;
using System.Text;

namespace ParkingSystem
{
    class Program
    {
        static int totalLots;
        static Dictionary<int, Vehicle> parkingLots;

        static void Main(string[] args)
        {
            parkingLots = new Dictionary<int, Vehicle>();
            bool exitProgram = false;

            Console.WriteLine(
                "============================= Parking System ===============================\n"
            );
            Console.WriteLine("Commands:");
            Console.WriteLine("  create_parking_lot\t\t Create a parking lot with <n> slots");
            Console.WriteLine("  park\t\t\t\t Create a new vehicle parking entry");
            Console.WriteLine("  leave\t\t\t\t Remove a vehicle from the parking lot");
            Console.WriteLine("  status\t\t\t Display the current status of the parking lot");
            Console.WriteLine("  exit\t\t\t\t Close the program");
            Console.WriteLine("  full command on doc.txt\n");

            while (!exitProgram)
            {
                Console.WriteLine("Enter a command: ");
                string command = Console.ReadLine();
                string[] commandParts = command.Split(' ');
                switch (commandParts[0])
                {
                    case "create_parking_lot":
                        if (commandParts.Length > 1)
                        {
                            if (commandParts[1] == "0" || string.IsNullOrEmpty(commandParts[1]))
                            {
                                Console.WriteLine("Failed to create a parking lot\n");
                            }
                            else if (int.TryParse(commandParts[1], out totalLots) && totalLots > 0)
                            {
                                Console.WriteLine(
                                    $"Created a parking lot with {totalLots} slots\n"
                                );
                            }
                            else
                            {
                                Console.WriteLine("Invalid number of parking lots\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed to create a parking lot\n");
                        }
                        break;

                    case "park":
                        if (commandParts.Length > 3)
                        {
                            string registrationNumber = commandParts[1];
                            string color = commandParts[2];
                            string type = commandParts[3];

                            if (
                                !string.IsNullOrEmpty(registrationNumber)
                                && !string.IsNullOrEmpty(color)
                                && !string.IsNullOrEmpty(type)
                            )
                            {
                                if (type != "Mobil" && type != "Motor")
                                {
                                    Console.WriteLine("Type Vehicles must 'Mobil' Or 'Motor'\n");
                                }
                                else
                                {
                                    if (parkingLots.Count < totalLots)
                                    {
                                        int slotNumberInput = GetNextAvailableSlot();
                                        Vehicle vehicle = new Vehicle(
                                            registrationNumber,
                                            color,
                                            type
                                        );
                                        bool found = false;

                                        foreach (KeyValuePair<int, Vehicle> entry in parkingLots)
                                        {
                                            if (
                                                entry.Value.RegistrationNumber == registrationNumber
                                            )
                                            {
                                                found = true;
                                                break;
                                            }
                                        }
                                        if (found)
                                        {
                                            Console.WriteLine(
                                                "The registration number has been entered\n"
                                            );
                                        }
                                        else
                                        {
                                            parkingLots.Add(slotNumberInput, vehicle);
                                            Console.WriteLine(
                                                $"Allocated slot number: {slotNumberInput}\n"
                                            );
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Sorry, parking lot is full\n");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Failed to create a park\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed to create a park\n");
                        }

                        break;

                    case "leave":
                        if (commandParts.Length > 1)
                        {
                            int slotToFree;
                            if (int.TryParse(commandParts[1], out slotToFree))
                            {
                                if (parkingLots.ContainsKey(slotToFree))
                                {
                                    parkingLots.Remove(slotToFree);
                                    Console.WriteLine($"Slot number {slotToFree} is free\n");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid slot number\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid command\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Command\n");
                        }

                        break;

                    case "status":
                        if (commandParts.Length > 1)
                        {
                            Console.WriteLine("Invalid Command\n");
                        }
                        else
                        {
                            PrintParkingStatus();
                        }

                        break;

                    case "type_of_vehicles":
                        if (commandParts.Length > 1)
                        {
                            string requestedType = commandParts[1];
                            int totalCount = CountVehicleByType(requestedType);
                            bool found = false;

                            foreach (KeyValuePair<int, Vehicle> entry in parkingLots)
                            {
                                if (entry.Value.Type == requestedType)
                                {
                                    found = true;
                                    break;
                                }
                            }

                            if (found)
                            {
                                Console.WriteLine($"Total {requestedType}s: {totalCount}\n");
                            }
                            else
                            {
                                Console.WriteLine("Not found\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Command\n");
                        }

                        break;

                    case "registration_numbers_for_vehicles_with_odd_plate":
                        if (commandParts.Length > 1)
                        {
                            Console.WriteLine("Invalid Command\n");
                        }
                        else
                        {
                            string oddPlateNumbers =
                                GetRegistrationNumbersForVehiclesWithOddPlate();

                            if (!string.IsNullOrEmpty(oddPlateNumbers))
                            {
                                Console.WriteLine(oddPlateNumbers + Environment.NewLine);
                            }
                            else
                            {
                                Console.WriteLine("Not found" + Environment.NewLine);
                            }
                        }
                        break;

                    case "registration_numbers_for_vehicles_with_even_plate":
                        if (commandParts.Length > 1)
                        {
                            Console.WriteLine("Invalid Command\n");
                        }
                        else
                        {
                            string evenPlateNumbers =
                                GetRegistrationNumbersForVehiclesWithEvenPlate();

                            if (!string.IsNullOrEmpty(evenPlateNumbers))
                            {
                                Console.WriteLine(evenPlateNumbers + Environment.NewLine);
                            }
                            else
                            {
                                Console.WriteLine("Not found" + Environment.NewLine);
                            }
                        }

                        break;

                    case "registration_numbers_for_vehicles_with_color":
                        if (commandParts.Length > 1)
                        {
                            string colorInput = commandParts[1];
                            string registrationNumbers = GetRegistrationNumbersForVehiclesWithColor(
                                colorInput
                            );

                            if (!string.IsNullOrEmpty(registrationNumbers))
                            {
                                Console.WriteLine(registrationNumbers + Environment.NewLine);
                            }
                            else
                            {
                                Console.WriteLine("Not found" + Environment.NewLine);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Command, need color" + Environment.NewLine);
                        }

                        break;

                    case "slot_numbers_for_vehicles_with_color":
                        if (commandParts.Length > 1)
                        {
                            string colorInput2 = commandParts[1];
                            bool found = false;
                            StringBuilder slotNumbers = new StringBuilder();

                            foreach (KeyValuePair<int, Vehicle> entry in parkingLots)
                            {
                                if (entry.Value.Color == colorInput2)
                                {
                                    found = true;
                                    slotNumbers.Append(entry.Key).Append(", ");
                                }
                            }

                            if (found)
                            {
                                Console.WriteLine(
                                    slotNumbers.ToString().TrimEnd(',', ' ') + Environment.NewLine
                                );
                            }
                            else
                            {
                                Console.WriteLine("Not found" + Environment.NewLine);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Command, need color\n");
                        }
                        break;

                    case "slot_number_for_registration_number":
                        if (commandParts.Length > 1)
                        {
                            string targetRegistrationNumber = commandParts[1];
                            bool found = false;

                            foreach (KeyValuePair<int, Vehicle> entry in parkingLots)
                            {
                                if (entry.Value.RegistrationNumber == targetRegistrationNumber)
                                {
                                    found = true;
                                    break;
                                }
                            }

                            if (found)
                            {
                                int slotNumber = GetSlotNumberForRegistrationNumber(
                                    targetRegistrationNumber
                                );

                                if (slotNumber != -1)
                                {
                                    Console.WriteLine(slotNumber + Environment.NewLine);
                                }
                                else
                                {
                                    Console.WriteLine("Not found" + Environment.NewLine);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Not found" + Environment.NewLine);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Command, need registration number\n");
                        }

                        break;

                    case "exit":
                        if (commandParts.Length > 1)
                        {
                            Console.WriteLine("Invalid Command\n");
                        }
                        else
                        {
                            Console.WriteLine(
                                Environment.NewLine
                                    + "Are you sure you want to exit the application? (y/n)"
                            );
                            string confirm = Console.ReadLine();
                            if (confirm.ToLower() == "y")
                            {
                                Console.WriteLine(
                                    Environment.NewLine + "Thank You, See You Next Time!"
                                );
                                Thread.Sleep(1500);
                                exitProgram = true;
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid command\n");
                        break;
                }
            }
        }

        static int GetNextAvailableSlot()
        {
            for (int i = 1; i <= totalLots; i++)
            {
                if (!parkingLots.ContainsKey(i))
                {
                    return i;
                }
            }
            return -1;
        }

        static void PrintParkingStatus()
        {
            Console.WriteLine("Slot\t No.Registration\t Type\t Color");
            foreach (var kvp in parkingLots)
            {
                Console.WriteLine(
                    $"{kvp.Key}\t {kvp.Value.RegistrationNumber}\t\t {kvp.Value.Type}\t {kvp.Value.Color}"
                );
            }
            Console.WriteLine(Environment.NewLine);
        }

        static int CountVehicleByType(string type)
        {
            int count = 0;
            foreach (var vehicle in parkingLots.Values)
            {
                if (vehicle.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                }
            }
            return count;
        }

        static string GetRegistrationNumbersForVehiclesWithOddPlate()
        {
            StringBuilder oddPlateNumbers = new StringBuilder();
            foreach (var vehicle in parkingLots.Values)
            {
                string registrationNumber = vehicle.RegistrationNumber;
                string numberPart = registrationNumber.Split('-')[1];
                int lastDigit;
                if (int.TryParse(numberPart, out lastDigit))
                {
                    if (lastDigit % 2 != 0)
                    {
                        oddPlateNumbers.Append(registrationNumber).Append(", ");
                    }
                }
            }
            return oddPlateNumbers.ToString().TrimEnd(',', ' ');
        }

        static string GetRegistrationNumbersForVehiclesWithEvenPlate()
        {
            StringBuilder evenPlateNumbers = new StringBuilder();
            foreach (var vehicle in parkingLots.Values)
            {
                string registrationNumber = vehicle.RegistrationNumber;
                string numberPart = registrationNumber.Split('-')[1];
                int lastDigit;
                if (int.TryParse(numberPart, out lastDigit))
                {
                    if (lastDigit % 2 == 0)
                    {
                        evenPlateNumbers.Append(registrationNumber).Append(", ");
                    }
                }
            }
            return evenPlateNumbers.ToString().TrimEnd(',', ' ');
        }

        static string GetRegistrationNumbersForVehiclesWithColor(string color)
        {
            StringBuilder registrationNumbers = new StringBuilder();
            foreach (var vehicle in parkingLots.Values)
            {
                if (vehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                {
                    registrationNumbers.Append(vehicle.RegistrationNumber).Append(", ");
                }
            }
            return registrationNumbers.ToString().TrimEnd(',', ' ');
        }

        static int GetSlotNumberForRegistrationNumber(string registrationNumber)
        {
            foreach (var slot in parkingLots)
            {
                if (
                    slot.Value.RegistrationNumber.Equals(
                        registrationNumber,
                        StringComparison.OrdinalIgnoreCase
                    )
                )
                {
                    return slot.Key;
                }
            }
            return -1;
        }

        static string GetSlotNumbersByColor(string color)
        {
            StringBuilder slotNumbers = new StringBuilder();
            foreach (var slot in parkingLots)
            {
                if (slot.Value.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                {
                    slotNumbers.Append(slot.Key).Append(", ");
                }
            }
            return slotNumbers.ToString().TrimEnd(',', ' ');
        }
    }

    class Vehicle
    {
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }

        public Vehicle(string registrationNumber, string color, string type)
        {
            RegistrationNumber = registrationNumber;
            Color = color;
            Type = type;
        }
    }
}
