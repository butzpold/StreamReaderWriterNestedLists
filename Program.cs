using System.IO.Pipes;

namespace StreamReaderWriterNestedLists
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<String[]> mainlist = new ();
            string[] array1 = new string[3] { "Luke", "28", "12 00" };
            string[] array2 = new string[3] { "Leia", "26", "16 00" };
            string[] array3 = new string[3] { "Han", "29", "18 00" };

            mainlist.Add(array1);
            mainlist.Add(array2);
            mainlist.Add(array3);

            Console.WriteLine(mainlist.Count);
            Console.WriteLine(mainlist[0].GetValue(0));
            Console.WriteLine(mainlist[0]);           
            // difference between System.String[] and System.String
            string name = "LukeSingleStringTest";
            Console.WriteLine(name);
            Console.WriteLine(name.GetType());

            // excersise how to get an array in an .csv-lookalike format
            Console.WriteLine(String.Join(";", array1));
            Console.WriteLine(array1[0] + ";" + array1[1] + ";" + array1[2]);
            // excersise with foreach-loop on an array
            foreach (string s in array1)
            {
                Console.WriteLine(s);
            }       
            // Task: Getting lists back in a format to save them in a csv
            // simple and clean way for less strings
            foreach (Array i in mainlist)
            {
                string mainlistarraytostring = i.GetValue(0) + ";" + i.GetValue(1) + ";" + i.GetValue(2);
                Console.WriteLine(mainlistarraytostring);
            }
            // using string.Join for collections of strings 
            foreach (var i in mainlist)
            {
                string mainlistarraytostring = String.Join(";", i);
                Console.WriteLine(mainlistarraytostring);
            }
            // example for a nested foreach-loop
            foreach (Array i in mainlist)
            {                
                foreach (string s in i)
                {
                    Console.WriteLine(s);
                }                
            }
            // create a .csv and write in the data of the nested list, if the .csv already exists it overrides the .csv 
            using (var writer = new StreamWriter("NestedList.csv", append: false))
            {
                foreach (var i in mainlist)
                {
                string mainlistarraytostring = String.Join(";", i);
                writer.WriteLine(mainlistarraytostring);
                }
            }
            // read out the .csv linewise, transform the data and append it to the .csv
            List<string[]> rows = new ();
            using (var reader = new StreamReader("NestedList.csv"))
            {
                while(!reader.EndOfStream)
                {
                    var row = reader.ReadLine().Split(";");
                    rows.Add(row);
                }
            }
            // print list<row>
            Console.WriteLine("Liste:");
            foreach (var r in rows)
            {                
                Console.WriteLine(String.Join(";",r));
            }
            // transform the data by adding 5 to the second column; means second item of each inner list
            Console.WriteLine("Liste(update1):");            
            foreach (var r in rows)
            {                
                r[1] = (int.Parse(r[1]) + 5).ToString();
                Console.WriteLine(String.Join(";", r));
            }
            // transform the data from rowwise to columnwise and then adding 5 to the second column           
            var columns = new List<List<string>>();
            int columnCount = rows[0].Count();
           
            for (int col = 0; col < columnCount; col++)
            {
                var column = new List<string>();
                foreach (var r in rows)
                {
                    column.Add(r[col]);
                }
                columns.Add(column);
            }
            // print list<columns>
            Console.WriteLine("Spalten:");
            foreach (var c in columns)
            {
                Console.WriteLine(String.Join(";", c));
            }
            // column[1] + 5
            Console.WriteLine(columns[1][0]);
            for (int i = 0; i < columns[1].Count; i++)
            {
                columns[1][i] = (int.Parse(columns[1][i]) + 5).ToString();
            }
            // print column[1]
            Console.WriteLine("Spalte2:");
            Console.WriteLine(String.Join(";", columns[1]));
            // append the updated list to the .csv
            using (var writer = new StreamWriter("NestedList.csv", append: true))
            {
                foreach (var r in rows)
                {
                    var line = String.Join(";", r);
                    writer.WriteLine(line);
                }
            }
        }
    }
}
