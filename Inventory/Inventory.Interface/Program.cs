using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Classes;
using Inventory.DataModel;
using Inventory.Classes.Enums;
using Inventory.Interface.FunctionExtensions;

namespace Inventory.Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to your inventory! 'quit' or 'q' to exit");
            if (Repository.IsTableEmpty())
                DbSetup.SetUpDB();

            string input = "";
            while (input.ToLower() != "quit" && input.ToLower() != "q")
            {
                PrintOptions();
                input = ReadAndCheckForQuit();
                if (input.ToLower() == "q" || input.ToLower() == "quit")
                    continue;
                int selection;
                bool goodIn = int.TryParse(input, out selection);
                if (!goodIn || !(selection < 8 && selection > 0))
                {
                    Console.WriteLine("Please enter a number 1-7");
                    continue;
                }
                HandleSelection(selection);
            }

        }

        static string ReadAndCheckForQuit()
        {
            string input = Console.ReadLine();
            if (input == "q" || input == "quit")
                Environment.Exit(0);
            return input;
        }

        private static void HandleSelection(int selection)
        {
            switch (selection)
            {
                case 1: SeePeople();    break;
                case 2: SeeCats();      break;
                case 3: AddOwner(OwnerTypes.Human);    break;
                case 4: AddOwner(OwnerTypes.Cat);       break;
                case 5: UpdateOwner(OwnerTypes.Human); break;
                case 6: UpdateOwner(OwnerTypes.Cat);    break;
                case 7: DeleteOwner();  break;
                default: throw new ArgumentException();
            }
            Console.WriteLine("============================");

        }

        private static void DeleteOwner()
        {
            int id = -1;
            string input;
            List<Owner> owners = Repository.GetAllOwners().ToList();
            Console.WriteLine(owners.ToString(new object()));
            while (id < 1)
            {
                Console.Write($"Who is to be deleted? : ");
                input = ReadAndCheckForQuit();
                if (!int.TryParse(input, out id))
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }

            Owner toBeKilled = Repository.GetOwnerById(id);
            bool success = Repository.DestroyOwner(toBeKilled);

            string msg = success ? "Update successful" : "Update failed";
            Console.WriteLine(msg);
        }

        private static void UpdateOwner(OwnerTypes type)
        {
            int id = -1;
            string input;
            List<Owner> owners = Repository.GetOwnersByType(type).ToList();
            Console.WriteLine(owners.ToString(new object()));
            while (id < 1)
            {
                Console.Write($"Which {type.ToString()} would you like? : ");
                input = ReadAndCheckForQuit();
                if (!int.TryParse(input, out id))
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }

            Owner owner = null;
            try
            {
                owner = Repository.GetOwnerById(id);
                Console.WriteLine($"You selected : {owner.ToString()}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Couldn't find that... please enter valid id");
                UpdateOwner(type);
            }

            id = -1;
            while (id < 1)
            {
                Console.Write($"Which field to modify? 1 = First Name, 2 = Last Name etc... : ");
                input = ReadAndCheckForQuit();
                if (!int.TryParse(input, out id))
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }

            Console.Write("Enter new field value : ");
            input = Console.ReadLine();

            bool success = InsertNewValue(id, input, owner);

            success = Repository.UpdateOwner(owner);
            string msg = success ? "Update successful" : "Update failed";
            Console.WriteLine(msg);

        }

        private static bool InsertNewValue(int field, string value, Owner oldOwner)
        {
            switch (field)
            {
                case 1: oldOwner.FirstName = value; break;
                case 2: oldOwner.LastName = value; break;
                case 3:
                    DateTime dob = DateTime.MinValue;
                    oldOwner.Birthday = DateTime.TryParse(value, out dob)? dob : oldOwner.Birthday;
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

        private static void AddOwner(OwnerTypes type)
        {
            Console.Write("First Name : ");
            string fn = ReadAndCheckForQuit();
            Console.Write("Last Name : ");
            string ln = ReadAndCheckForQuit();
            DateTime dob = DateTime.MinValue;
            while (dob == DateTime.MinValue)
            {
                Console.Write("Birthday yyyy-mm-dd : ");
                string input = ReadAndCheckForQuit();

                bool success = DateTime.TryParse(input, out dob);
            }
            Console.Write("Gender (m/f) : ");
            string rawGender = ReadAndCheckForQuit();
            Gender gender = rawGender == "m" ? Gender.Male : Gender.Female;
            Owner newGuy = new Owner
            {
                FirstName = fn,
                LastName = ln,
                Birthday = dob,
                Gender = gender,
                Type = type,
            };

            Repository.CreateOwner(new Owner[] { newGuy });
        }

        private static void SeeCats()
        {
            List<Owner> owners = Repository.GetOwnersByType(OwnerTypes.Cat).ToList();
            Console.WriteLine(owners.ToString(new object()));
        }

        private static void SeePeople()
        {
            List<Owner> owners = Repository.GetOwnersByType(OwnerTypes.Human).ToList();
            Console.WriteLine(owners.ToString(new object()));
        }

        private static void PrintOptions()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) See all people");
            Console.WriteLine("2) See all cats");
            Console.WriteLine("3) Add person");
            Console.WriteLine("4) Add cat");
            Console.WriteLine("5) Update person");
            Console.WriteLine("6) Update cat");
            Console.WriteLine("7) Delete person/cat");
            Console.Write(":>");
        }

  
    }
}
