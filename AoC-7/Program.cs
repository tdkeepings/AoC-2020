using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_7
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var listOfBags = new List<Bag>();
            var baseColours = new List<Bag>();
            
            // Grab all of the distinct bag names to give us a place to start
            foreach (var line in lines)
            {
                var components = line.Split("contain");
                var bagColourRaw = components[0];
                var bagColour = bagColourRaw.Substring(0, bagColourRaw.Length - 6); // -4 because "bags", 2 for spaces

                if (!baseColours.Any(b => b.Colour == bagColour))
                {
                    baseColours.Add(new Bag(){ Colour = bagColour});
                }
            }
            
            // baseColours now has a list of Bags that have all the top level colours
            
            // now need to iterate again to fill up each Bag with the correct contents, using existing Bag refs as
            // the content for the bags 

            // Step through it all again but this time add bags to the list using references saved from above
            // Might be able to do this in one loop, but making the assumption that the contents of 
            // the input file is NOT in order. If it is in order, can do this in one loop
            foreach (var line in lines)
            {
                var components = line.Split("contain");
                var bagColourRaw = components[0];
                var bagColour = bagColourRaw.Substring(0, bagColourRaw.Length - 6); // -4 because "bags", 2 for spaces

                var contentColours = components[1];

                var baseBag = baseColours.First(b => b.Colour == bagColour);
                if (contentColours.Trim() == "no other bags.")
                {
                    listOfBags.Add(baseBag);
                    continue;
                }
                
                var contentBagsRaw = contentColours.Split(',');

                foreach (var content in contentBagsRaw)
                {
                    var cnt = content.Trim();
                    var pieces = cnt.Split(' ');
                    var quantity = pieces[0];
                    var colour = pieces[1] + " " + pieces[2];

                    var bag = baseColours.First(v => v.Colour == colour);

                    var bagContent = new BagContent()
                    {
                        Bag = bag,
                        Quantity = Convert.ToInt32(quantity)
                    };
                    
                    baseBag.Contents.Add(bagContent);
                    
                }
                listOfBags.Add(baseBag);
                
            }
            
            // By here the list is fully populate with nodes that reference bags properly 
            
            var numberOfBagsContainingAtLeastOne = 0;
            foreach (var bag in listOfBags)
            {
                // Solution for Part 2
                if (bag.Colour == "shiny gold")
                {
                    var amountInThisBag = CountBags(bag.Contents);
                    Console.WriteLine("Shiny gold:" + amountInThisBag);
                }
                // Solution for Part 1
                else
                {
                    var amountInThisBag = CountContent("shiny gold", bag.Contents);
                    if (amountInThisBag > 0)
                        numberOfBagsContainingAtLeastOne++;
                }
            }
            
            Console.WriteLine(numberOfBagsContainingAtLeastOne);
        }

        // Recursively dig through the content of each bag
        private static int CountContent(string bagColour, List<BagContent> contents)
        {

            var total = 0;
            foreach (var content in contents)
            {
                var bag = content.Bag;

                if (bag.Colour == bagColour)
                {
                    total += content.Quantity;
                }
                else
                {
                    total += content.Quantity * CountContent(bagColour, bag.Contents);
                }
            }

            return total;
        }

        // Recursively count the number of bags inside a given set of content
        private static int CountBags(List<BagContent> contents)
        {
            var total = 0;
            foreach (var content in contents)
            {
                var bag = content.Bag;
                total += content.Quantity + (content.Quantity * CountBags(bag.Contents));
                // Quantity + (Quantity * Content)
            }

            return total;
        }
        
        
        public class Bag
        {
            public Bag()
            {
                Contents = new List<BagContent>();
            }
            public string Colour { get; set; }
            public List<BagContent> Contents { get; set; }
        }

        public class BagContent
        {
            public int Quantity { get; set; }
            public Bag Bag { get; set; }
        }
    }
}