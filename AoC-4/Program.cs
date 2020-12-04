using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var passportBlocks = GetPassportBlocks(lines);

            var valid = passportBlocks.Where(p => p.IsValid());
            var invalid = passportBlocks.Where(p => !p.IsValid());


            var a = "";
            
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

            return passports;
        }

        private class Passport
        {
            public string Ecl { get; set; }
            public string Pid { get; set; }
            public int Eyr { get; set; }
            public string Hcl { get; set; }
            public int Byr { get; set; }
            public int Iyr { get; set; }
            public string Cid { get; set; }
            public Measurement Hgt { get; set; }

            public bool IsValid()
            {
                return
                    EclIsValid() &&
                    PidIsValid() &&
                    HclIsValid() && 
                    ByrIsValid() &&
                    IyrIsValid() &&
                    EyrIsValid() &&
                    HgtIsValid();
            }

            private bool ByrIsValid()
            {
                return (Byr >= 1920 && Byr <= 2002);
            }

            private bool IyrIsValid()
            {
                return (Iyr >= 2010 && Iyr <= 2020);
            }

            private bool EyrIsValid()
            {
                return (Eyr >= 2020 && Eyr <= 2030);
            }

            private bool HgtIsValid()
            {
                if (Hgt == null) return false;
                
                if (Hgt.Unit.ToLower() == "cm")
                {
                    return (Hgt.Size >= 150 && Hgt.Size <= 193);
                }

                if (Hgt.Unit.ToLower() == "in")
                {
                    return (Hgt.Size >= 59 && Hgt.Size <= 76);
                }

                return false;
            }

            private bool HclIsValid()
            {
                if (string.IsNullOrEmpty(Hcl)) return false;
                return Regex.Match(Hcl, @"^#(?:[0-9a-fA-F]{3}){1,2}$").Success;   
            }

            private bool EclIsValid()
            {
                if (string.IsNullOrEmpty(Ecl)) return false;
                return
                    Ecl == "amb" ||
                    Ecl == "blu" ||
                    Ecl == "brn" ||
                    Ecl == "gry" ||
                    Ecl == "grn" ||
                    Ecl == "hzl" ||
                    Ecl == "oth";
            }

            private bool PidIsValid()
            {
                if (string.IsNullOrEmpty(Pid)) return false;
                return Pid.Length == 9 && Regex.Match(Pid, @"[0-9]{9}").Success;
            }
            
            public void AssignValue(string key, string value)
            {
                switch (key.ToLower())
                {
                    case "ecl": Ecl = value;
                        break;
                    case "pid": Pid = value;
                        break;
                    case "eyr": Eyr = Convert.ToInt32(value);
                        break;
                    case "hcl": Hcl = value;
                        break;
                    case "byr": Byr = Convert.ToInt32(value);
                        break;
                    case "iyr": Iyr = Convert.ToInt32(value);
                        break;
                    case "cid": Cid = value;
                        break;
                    case "hgt": Hgt = ConvertHeight(value);
                        break;
                }
            }

            private Measurement ConvertHeight(string input)
            {
                var size = Convert.ToInt32(Regex.Match(input, @"\d+").Value);
                var unit = input.Substring(input.Length-2, 2);

                return new Measurement
                {
                    Size = size,
                    Unit = unit
                };
            }
        }

        public class Measurement
        {
            public int Size { get; set; }
            public string Unit { get; set; }
        }
    }
}