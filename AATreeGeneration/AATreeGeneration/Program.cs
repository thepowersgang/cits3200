using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generations
{    
    class Program
    {
        static void minitest()
        {
            for (int j = 0; j < 9; j++)
            {
                AATreeGeneration g = new AATreeGeneration();

                Random r = new Random(0);

                List<IndividualWithFitness> mylist = new List<IndividualWithFitness>();
                mylist.Add(new IndividualWithFitness("9", 9));
                mylist.Add(new IndividualWithFitness("8", 8));
                mylist.Add(new IndividualWithFitness("7", 7));
                mylist.Add(new IndividualWithFitness("6", 6));
                mylist.Add(new IndividualWithFitness("5", 5));
                mylist.Add(new IndividualWithFitness("4", 4));
                mylist.Add(new IndividualWithFitness("3", 3));
                mylist.Add(new IndividualWithFitness("2", 2));
                mylist.Add(new IndividualWithFitness("1", 1));

                while (mylist.Count > 0)
                {
                    int i = r.Next(mylist.Count);
                    g.Insert(mylist[i].Individual, mylist[i].Fitness);
                    mylist.RemoveAt(i);
                }

                g.Print();
                Console.WriteLine();
                g.Test();

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine(g.Remove(j).Individual);

                Console.WriteLine();
                Console.WriteLine();

                g.Print();
                Console.WriteLine();
                g.Test();

                Console.WriteLine();
                Console.WriteLine("----------");
            }

            Console.ReadLine();
        }

        static void maxitest()
        {
            Random r = new Random(0);

            AATreeGeneration g = new AATreeGeneration();

            Console.WriteLine("----------");

            for (int i = 0; i < 1000000; i++)
            {
                g.Insert(null, (uint)r.Next());
            }

            g.Test();

            Console.WriteLine("----------");

            for (int i = 0; i < 100000; i++)
            {
                g.Remove(r.Next(g.Count));
            }

            g.Test();

            Console.WriteLine("----------");

            for (int i = 0; i < 899995; i++)
            {
                g.Remove(r.Next(g.Count));
            }

            g.Test();

            Console.WriteLine("----------");

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            
        }
    }
}
