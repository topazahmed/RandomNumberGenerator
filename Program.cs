using System;
using System.Linq;
using System.Collections.Generic;

namespace RandonNumberGenerator
{
    class Program
    {
        //the input variables 
        public const int minNumber = 1;             //the minimum number to start counting
        public const int maxNumber = 1000;          //the maximum number for random number generation
        public const int listSize = maxNumber;      //in this case the list size is the same as above

        static void Main(string[] args)
        {
            //using this array to output the list
            int[] allNumbers = new int[listSize];
            var rand = new Random();
    	
            //Method #1, not used cause of time taken
            //allNumbers = Method1(rand,allNumbers);

            //Second better method
            allNumbers = Method2(rand,allNumbers);

            //This will varify the results and check if there is any duplicates 
            TestDuplicateValues(allNumbers);

            //this will print the whole array
            PrintArray(allNumbers);

            Console.WriteLine("Done, Now get me the Job... Thanks.");
        }

        /* I was thinking of generating a new random number every time and add it to the array
        ** then do a while loop to check if the next number is already in the array or not
        ** keep finding new random number while no new ones are found
        ** if a unique one is found add it to the array and move on
        ** ** After finishing writing this found it to take longer time, so wrote faster method 2
        */
        private static void Method1(Random rand, int[] allNumbers){
        	for(int i = 0; i < listSize; i++)
	    	{
                int currentNumber = rand.Next(minNumber,maxNumber);
            
                if(i==0) //first one get added without checking for already existing
                {    
                    allNumbers[i]=currentNumber;
                }
                else
                {                
                    while(allNumbers.Contains(currentNumber))
                    {
                        currentNumber = rand.Next(minNumber,maxNumber);
                    }
                    allNumbers[i] = currentNumber;
                }
            }
        }

        // Method to print an array to the console 
        private static void PrintArray(int[] allNumbers){
            Console.WriteLine(String.Join(", ",allNumbers));
        }

        // varify results 
        // do a group by on the array and then see if anything was generated twice
        private static void TestDuplicateValues(int[] allNumbers){
            var groups = allNumbers.GroupBy(v => v);
            foreach(var group in groups){
                if(group.Count()>1)
                {
                    Console.WriteLine("Value {0} has {1} items", group.Key, group.Count());
                }
            }
            Console.WriteLine("No duplicates found");
        }

        // Faster method:
        // have all the numbers between min and max in a list
        // find a random location in the array and use that number from the above list
        // then remove that number so duplicates dont need to be checked
        // as the final result gets bigger the initial list gets smaller and it takes significatly shorter time 
        private static int[] Method2(Random rand, int[] allNumbers){
            List<int> numberList = new List<int>();
        	for(int i = minNumber; i <= maxNumber; i++)
            {
                numberList.Add(i);
            }

        	for(int i = 0; i < maxNumber; i++)
            {
                int currentNumber = rand.Next(0,numberList.Count);
                allNumbers[i] = numberList[currentNumber];
                numberList.RemoveAt(currentNumber);
            }

            return allNumbers;
        }

    }
}
