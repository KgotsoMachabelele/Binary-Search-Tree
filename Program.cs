using System;

namespace Binary_Search_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create tree
            BST<int> tree = new BST<int>(19);
            string s = "";
            
            /*
            tree.Insert(5); s += "5, ";
            tree.Insert(8); s += "8, ";
            tree.Insert(6); s += "6, ";
            tree.Insert(9); s += "9, ";
            tree.Insert(7); s += "7, ";
            tree.Insert(6); s += "6, "; //Second time
            tree.Insert(1); s += "1, ";
            tree.Insert(3); s += "3, ";
            tree.Insert(4); s += "4, ";
            tree.Insert(2); s += "2, ";
            */
            
            tree.Add(19); s += "19, ";
            tree.Add(11); s += "11, ";
            tree.Add(35); s += "35, ";
            tree.Add(23); s += "23, ";
            tree.Add(7); s += "7, ";
            tree.Add(16); s += "16, ";
            tree.Add(17); s += "17, ";
            tree.Add(13); s += "13, ";

            //Print node in order of insertion
            Console.WriteLine("Order of insertion:");
            Console.WriteLine(s + "\n");

            //Visualise the tree
            Console.WriteLine("Visualisation of tree:");
            tree.PrintVisual();
            Console.WriteLine();

            //Traverse and print the tree in in-order manner
            Console.Write("In order (left, root, right) : " + string.Join(", ", tree.VisitInOrder()));
            Console.WriteLine();

            //Find
            int searchFor = 9;
            if (tree.Contains(searchFor))
                Console.WriteLine("The tree contains " + searchFor + ".");
            else
                Console.WriteLine("The tree does not contain " + searchFor + ".");
            Console.WriteLine();

            //Remove leaf
            tree.Remove(13);
            Console.WriteLine("Tree after removal of 13 (leaf):");
            tree.PrintVisual();
            Console.Write("\nIn order: " + string.Join(", ", tree.VisitInOrder()));
            Console.WriteLine();

            //Remove somewhere in the middle
            tree.Remove(11); //Nakov p 708
            Console.WriteLine("Tree after removal of 11:");
            tree.PrintVisual();
            Console.Write("\nIn order: " + string.Join(", ", tree.VisitInOrder()));
            Console.WriteLine();

            //Remove root
            tree.Remove(19);
            Console.WriteLine("Tree after removal of 19 (root):");
            tree.PrintVisual();
            Console.Write("\nIn order: " + string.Join(", ", tree.VisitInOrder()));
            Console.WriteLine();

            //Opportunity to read output
            Console.Write("\nPress any key to exit ... ");
            Console.ReadKey();
        } //Main
    } //class Program
} //namespace
