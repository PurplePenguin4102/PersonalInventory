﻿using Inventory.Classes;
using Inventory.Classes.Enums;
using Inventory.DataModel.Repositories;
using Inventory.ConsoleUI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ConsoleUI
{
    public static class OwnerConsoleUI
    {
        public static void DeleteOwner()
        {
            Owners owners = OwnerRepository.GetAllOwners().ToList();
            Console.WriteLine(owners.ToString());
            List<Option> UIList = Option.OptionsFromOwners(owners);
            Owner toBeKilled = TextParser.SelectItemFromList("Who is to be deleted? : ", UIList).Data as Owner;
            bool success = OwnerRepository.DestroyOwner(toBeKilled);
            string msg = success ? "Update successful" : "Update failed";
            Console.WriteLine(msg);
            ModifyIList(new List<object>());
        }

        private static IList<object> ModifyIList(IList<object> l)
        {
            return null;
        }

        public static void UpdateOwner(OwnerTypes type)
        {
            int id = -1;
            string input;
            Owners owners = OwnerRepository.GetOwnersByType(type).ToList();
            Console.WriteLine(owners.ToString());
            while (id < 1)
            {
                Console.Write($"Which {type.ToString()} would you like? : ");
                input = Utility.ReadAndCheckForQuit();
                if (!int.TryParse(input, out id))
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }

            Owner owner = null;
            try
            {
                owner = OwnerRepository.GetOwnerById(id);
                Console.WriteLine($"You selected : {owner.ToString()}");
            }
            catch
            {
                Console.WriteLine($"Couldn't find that... please enter valid id");
                UpdateOwner(type);
            }

            id = -1;
            while (id < 1)
            {
                Console.Write($"Which field to modify? 1 = First Name, 2 = Last Name etc... : ");
                input = Utility.ReadAndCheckForQuit();
                if (!int.TryParse(input, out id))
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }

            Console.Write("Enter new field value : ");
            input = Console.ReadLine();

            bool success = InsertNewValue(id, input, owner);

            success = OwnerRepository.UpdateOwner(owner);
            string msg = success ? "Update successful" : "Update failed";
            Console.WriteLine(msg);

        }

        public static bool InsertNewValue(int field, string value, Owner oldOwner)
        {
            switch (field)
            {
                case 1: oldOwner.FirstName = value; break;
                case 2: oldOwner.LastName = value; break;
                case 3:
                    DateTime dob = DateTime.MinValue;
                    oldOwner.Birthday = DateTime.TryParse(value, out dob) ? dob : oldOwner.Birthday;
                    break;
                case 4:
                    Gender parsedGender = value == "m" ? Gender.Male : Gender.Female;
                    oldOwner.Gender = parsedGender;
                    break;
                case 5:
                    OwnerTypes parsedType;
                    bool success = Enum.TryParse(value, out parsedType);
                    oldOwner.Type = success ? parsedType : oldOwner.Type;
                    break;
            }
            return false;
        }

        public static void AddOwner(OwnerTypes type)
        {
            Console.Write("First Name : ");
            string fn = Utility.ReadAndCheckForQuit();
            Console.Write("Last Name : ");
            string ln = Utility.ReadAndCheckForQuit();
            DateTime dob = DateTime.MinValue;
            while (dob == DateTime.MinValue)
            {
                Console.Write("Birthday yyyy-mm-dd : ");
                string input = Utility.ReadAndCheckForQuit();

                bool success = DateTime.TryParse(input, out dob);
            }
            Console.Write("Gender (m/f) : ");
            string rawGender = Utility.ReadAndCheckForQuit();
            Gender gender = rawGender == "m" ? Gender.Male : Gender.Female;
            Owner newGuy = new Owner
            {
                FirstName = fn,
                LastName = ln,
                Birthday = dob,
                Gender = gender,
                Type = type,
            };

            OwnerRepository.CreateOwner(new Owner[] { newGuy });
        }

        public static void SeeCats()
        {
            Owners owners = OwnerRepository.GetOwnersByType(OwnerTypes.Cat).ToList();
            Console.WriteLine(owners.ToString());
        }

        public static void SeePeople()
        {
            Owners owners = OwnerRepository.GetOwnersByType(OwnerTypes.Human).ToList();
            Console.WriteLine(owners.ToString());
        }
    }
}
