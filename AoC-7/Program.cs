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

            foreach (var line in lines)
            {
                var components = line.Split("contain");
                var bagColourRaw = components[0];
                var bagColour = bagColourRaw.Substring(0, bagColourRaw.Length - 6); // -4 because "bags", 2 for spaces

                var contentColours = components[1];

                var baseBag = baseColours.First(b => b.Colour == bagColour);
                if (contentColours.Trim() == "no other bags.")
                {
                    // end of the line for that bag, still needs adding though
                   
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
            
            // Tree is fully populate with nodes that reference bags properly 
            var numberOfBagsContainingAtLeastOne = 0;
            foreach (var bag in listOfBags)
            {
                // each node with contain lots of bags, each with their own content, 
                if (bag.Colour == "shiny gold")
                {
                    // skip? 
                }
                else
                {

                    var amountInThisBag = CountContent("shiny gold", bag.Contents);
                    if (amountInThisBag > 0)
                        numberOfBagsContainingAtLeastOne++;
                    
                    //Console.WriteLine(amountInThisBag);
                }

            }
            
            
            Console.WriteLine(numberOfBagsContainingAtLeastOne);
            
        }

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