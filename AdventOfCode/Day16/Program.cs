using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day16
{
    public class Program
    {
        public void Select()
        {
            Console.WriteLine("Choose which version to run");
            Console.WriteLine("1 - Part 1");
            Console.WriteLine("2 - Part 2");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine(Part1());
                    break;
                case "2":
                    Console.WriteLine(Part2());
                    break;

                default:
                    Console.WriteLine("Default Case");
                    break;
            }
        }

        private string Part1()
        {
            var inputs = GetInput();
            var ticketRules = inputs.TakeWhile(s => s != "");
            var myTicket = inputs.ElementAt(22).Split(',').Select(s => int.Parse(s));
            var otherTickets = inputs.Skip(25).Select(s => s.Split(','));

            var invalidNumbers = new List<int>();
            for(int i = 0; i<1000; i++)
            {
                invalidNumbers.Add(i);
            }

            foreach(string rule in ticketRules)
            {
                var parsedRule = rule.Split(' ');
                var firstPair = parsedRule[parsedRule.Length - 3].Split('-').Select(s => int.Parse(s));
                var lastPair = parsedRule[parsedRule.Length - 1].Split('-').Select(s => int.Parse(s));
                for (int i = firstPair.First(); i < firstPair.Last(); i++)
                {
                    if (invalidNumbers.Contains(i))
                        invalidNumbers.Remove(i);
                }
                for (int i = lastPair.First(); i < lastPair.Last(); i++)
                {
                    if (invalidNumbers.Contains(i))
                        invalidNumbers.Remove(i);
                }
            }

            var errorRate = 0;
            foreach(string[] ticket in otherTickets)
            {
                var parsedTicket = ticket.Select(s => int.Parse(s));
                foreach(int value in parsedTicket)
                {
                    if(invalidNumbers.Contains(value))
                    {
                        errorRate += value;
                    }
                }
            }
            return errorRate.ToString();
        }

        private string Part2()
        {
            var inputs = GetInput();
            var ticketRules = inputs.TakeWhile(s => s != "");
            var myTicket = inputs.ElementAt(22).Split(',').Select(s => int.Parse(s));
            var otherTickets = inputs.Skip(25);


            // Get Valid Tickets
            var invalidNumbers = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                invalidNumbers.Add(i);
            }

            foreach (string rule in ticketRules)
            {
                var parsedRule = rule.Split(' ');
                var firstPair = parsedRule[parsedRule.Length - 3].Split('-').Select(s => int.Parse(s));
                var lastPair = parsedRule[parsedRule.Length - 1].Split('-').Select(s => int.Parse(s));
                for (int i = firstPair.First(); i < firstPair.Last(); i++)
                {
                    if (invalidNumbers.Contains(i))
                        invalidNumbers.Remove(i);
                }
                for (int i = lastPair.First(); i < lastPair.Last(); i++)
                {
                    if (invalidNumbers.Contains(i))
                        invalidNumbers.Remove(i);
                }
            }

            var ticketsToRemove = new List<string>();
            foreach (string ticket in otherTickets)
            {
                var parsedTicket = ticket.Split(',').Select(s => int.Parse(s));
                foreach (int value in parsedTicket)
                {
                    if (invalidNumbers.Contains(value))
                    {
                        ticketsToRemove.Add(ticket);
                    }
                }
            }
            var validTickets = otherTickets.Except(ticketsToRemove);


            // Find what entry is what rule
            var ruleDict = new Dictionary<string, TicketRange>();
            foreach(string rule in ticketRules)
            {
                var ruleName = rule.Split(':').First();
                var parsedRule = rule.Split(' ');
                var firstPair = parsedRule[parsedRule.Length - 3].Split('-').Select(s => int.Parse(s));
                var lastPair = parsedRule[parsedRule.Length - 1].Split('-').Select(s => int.Parse(s));
                ruleDict.Add(ruleName, new TicketRange(firstPair.First(), firstPair.Last(), lastPair.First(), lastPair.Last()));
            }

            //Setup Potential matches
            var potentialMatches = new List<List<string>>();
            for(int i=0; i< ruleDict.Count(); i++)
            {
                var listToAdd = new List<string>();
                foreach(var rule in ruleDict)
                {
                    listToAdd.Add(rule.Key);
                }
                potentialMatches.Add(listToAdd);
            }

            //Remove invalid matches
            for (int i = 0; i < ruleDict.Count(); i++)
            {
                foreach (var ticket in validTickets)
                {
                    var parsedTicket = ticket.Split(',').Select(s => int.Parse(s));
                    foreach(var rule in ruleDict)
                    {
                        if(!rule.Value.ContainsValue(parsedTicket.ElementAt(i)))
                        {
                            potentialMatches.ElementAt(i).Remove(rule.Key);
                        }
                    }
                }
            }

            var foundPostitions = new List<string>();
            for(int i=0; i<potentialMatches.Count(); i++)
            {
                var matchesToAdd = potentialMatches.FindAll(m => m.Count() == 1 && !foundPostitions.Contains(m.First())).Select(e => e.First());
                foundPostitions.AddRange(matchesToAdd);
                for(int j=0; j<potentialMatches.Count(); j++)
                {
                    if(potentialMatches.ElementAt(j).Count() > 1)
                    {
                        potentialMatches.ElementAt(j).RemoveAll(e => foundPostitions.Contains(e));
                    }
                }
            }

            //Calculate Answer from positions
            long answerValue = 1;
            for(int i=0; i<potentialMatches.Count(); i++)
            {
                if(potentialMatches.ElementAt(i).First().Contains("departure"))
                {
                    answerValue = answerValue * myTicket.ElementAt(i);
                }
            }

            return answerValue.ToString();
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day16\Input.txt";
            List<string> values = new List<string>();

            using (StreamReader sr = File.OpenText(filePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    values.Add(s);
                }
            }
            return values;
        }

        private class TicketRange
        {
            int lowerMin;
            int lowerMax;
            int upperMin;
            int upperMax;

            public TicketRange(int _lowerMin, int _lowerMax, int _upperMin, int _upperMax)
            {
                lowerMin = _lowerMin;
                lowerMax = _lowerMax;
                upperMin = _upperMin;
                upperMax = _upperMax;
            }

            public bool ContainsValue(int value)
            {
                if ((value >= lowerMin && value <= lowerMax) || (value >= upperMin && value <= upperMax))
                    return true;
                else
                    return false;
            }
        }
    }
}
