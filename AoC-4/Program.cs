using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var passportBlocks = GetPassportBlocks(lines);

            Console.WriteLine(passportBlocks.Count(p => p.IsValid()));
        }


        private static List<Passport> GetPassportBlocks(string[] lines)
        {
            var passport = new Passport();
            var passports = new List<Passport>();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    passports.Add(passport);
                    passport = new Passport();
                }
                else
                {
                    var lineComponents = line.Split(' ');
                    foreach (var lineComponent in lineComponents)
                    {
                        var kvp = lineComponent.Split(':');
                        
                        passport.AssignValue(kvp[0], kvp[1]);
                    }
                }
            }
            
            passports.Add(passport); 

            return passports;
        }

        private class Passport
        {
            public string Ecl { get; set; }
            public string Pid { get; set; }
            public string Eyr { get; set; }
            public string Hcl { get; set; }
            public string Byr { get; set; }
            public string Iyr { get; set; }
            public string Cid { get; set; }
            public string Hgt { get; set; }

            public bool IsValid()
            {
                return
                    !string.IsNullOrEmpty(Ecl) &&
                    !string.IsNullOrEmpty(Pid) &&
                    !string.IsNullOrEmpty(Eyr) &&
                    !string.IsNullOrEmpty(Hcl) &&
                    !string.IsNullOrEmpty(Byr) &&
                    !string.IsNullOrEmpty(Iyr) &&
                    //!string.IsNullOrEmpty(Cid) &&
                    !string.IsNullOrEmpty(Hgt);
            }

            public void AssignValue(string key, string value)
            {
                switch (key.ToLower())
                {
                    case "ecl": Ecl = value;
                        break;
                    case "pid": Pid = value;
                        break;
                    case "eyr": Eyr = value;
                        break;
                    case "hcl": Hcl = value;
                        break;
                    case "byr": Byr = value;
                        break;
                    case "iyr": Iyr = value;
                        break;
                    case "cid": Cid = value;
                        break;
                    case "hgt": Hgt = value;
                        break;
                }
            }
        }
    }
}