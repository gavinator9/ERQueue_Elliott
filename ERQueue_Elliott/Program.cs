// Gavin Elliott
// IT113
// NOTES: Online information about deep cloning was sparse, in a way. I had luck implementing it though when I
// stepped back from reading examples directly (there weren't any easy ones) and tried to manually put an abstract concept together piece by piece. kinda fun
// BEHAVIORS NOT IMPLEMENTED AND WHY: Couldn't find out how to deep clone while using sort algorithm in a timely or efficient manner,
// seems to be feature complete besides this one issue.

using System.Collections.Generic;
using System.Collections;
using System.Xml.Linq;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.IO.Pipes;
using System.Linq;

namespace ERQueue_Elliott
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            PriorityQueue<Patient, int> ERQueue = new PriorityQueue<Patient, int>();



            // CSV init data into queue block below. Self wrote but with reference and help from stack overflow
            // note this assumes the CSV file is travelling with the project as I submitted it, in the same directory as the csharp files
            string filename = "Patients-1.csv";
            string parent=Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string filepath = Path.Combine(parent, filename);
            StreamReader sr= new StreamReader(filepath);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string[] values = sr.ReadLine().Split(',');
                string firstname = values[0];
                string lastname= values[1];
                string birthday= values[2];
                int priority = int.Parse(values[3]);
                Patient patient= new Patient(firstname, lastname, birthday, priority);
                ERQueue.Enqueue(patient, patient.pubpriority);
            }

            int queuelength=ERQueue.Count;

            //^^CSV init block


            bool mainmenu = true;
            while (mainmenu == true)
            {
                Console.WriteLine("(A)dd Patient  (P)rocess Current Patient  (L)ist All in Queue  (Q)uit");
                Console.WriteLine("The Queue contains "+ queuelength.ToString() + " Patients currently\n");
                ConsoleKeyInfo input= Console.ReadKey();
                if (input.Key==ConsoleKey.A)
                {
                    //Add/Enqueue patient then display number of patients after addition
                    Console.WriteLine("\n Input patient first name \n");
                    string line=Console.ReadLine();
                    string firstname = line;
                    Console.WriteLine("\n Input patient last name \n");
                    line=Console.ReadLine();
                    string lastname = line;
                    Console.WriteLine("\n Input patient birthday in MM/DD/YYYY Format \n");
                    line=Console.ReadLine();
                    string birthday= line;
                    Console.WriteLine("\n Input patient priority as a number, lower number being higher priority. Minimum 1 \n");
                    line=Console.ReadLine();
                    int priority = int.Parse(line);
                    Console.WriteLine("\n Adding patient.. \n");
                    Patient patient = new Patient(firstname, lastname, birthday, priority);
                    Console.WriteLine(" Done. \n");
                    ERQueue.Enqueue(patient, patient.pubpriority);
                    queuelength++;
                    Console.WriteLine(queuelength.ToString() +" Patients in queue \n");
                }
                if (input.Key==ConsoleKey.P) 
                {
                    Console.WriteLine(ERQueue.Dequeue());
                    queuelength--;
                }
                if (input.Key== ConsoleKey.L)
                {
                    PriorityQueue<Patient, int> list= Clonemachine(ERQueue);
                    for (int i = 0; i<queuelength; i++)
                    {
                        Console.WriteLine(list.Dequeue());
                    }
                }
                if (input.Key== ConsoleKey.Q)
                {
                    mainmenu = false;
                }
            }


            // Deep cloning principal is applied here with each object manually recreated attribute by attribute
             PriorityQueue<Patient, int> Clonemachine(PriorityQueue<Patient, int> original)
            {
                int placeholder = original.Count;
                PriorityQueue<Patient, int> copy= new PriorityQueue<Patient, int>();
                Patient[] holder= new Patient[original.Count];
                Patient[] newholder= new Patient[original.Count];

                for (int i=0; i<placeholder; i++)
                {
                    holder[i]=original.Dequeue();
                }
                int counter = 0;
                foreach (Patient p in holder)
                {
                    Patient patient = new Patient(p.pubfirstname, p.publastname, p.pubbirthday, p.pubpriority);
                    newholder[counter] = patient;
                    counter++;
                }
                foreach (Patient p in newholder)
                {
                    copy.Enqueue(p, p.pubpriority);
                }
                foreach (Patient p in holder)
                {
                    ERQueue.Enqueue(p, p.pubpriority);
                }
                return copy;
            }

            

        }
    }
}