using Inventory.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ConsoleUI
{
    public class Option
    {
        public int Id { get; set; }
        public string Printed { get; set; }
        public object Data { get; set; }

        public static List<Option> OptionsFromOwners(List<Owner> oList)
        {
            List<Option> options = oList.Select(o => new Option
                {
                    Id = o.Id,
                    Printed = o.ToString(),
                    Data = o
                })
                .ToList();
            return options;
        } 

        public static List<Option> OptionsFromPossessions(List<Possession> sList)
        {
            List<Option> options = sList.Select(o => new Option
                {
                    Id = o.Id,
                    Printed = o.ToString(),
                    Data = o
                })
                .ToList();
            return options;
        }
    }
}
