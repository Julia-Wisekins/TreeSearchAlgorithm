using System.Collections.Generic;
using System.Diagnostics;
namespace Tree
{
    internal class Program
    {
        /// <summary>
        /// A data point in a larger network
        /// </summary>
        public class Node
        {
            /// <summary>
            /// An identifing labed for the user
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Flag used to determine whether a node has already been counted during the search
            /// </summary>
            public bool Searched { get; set; }
            /// <summary>
            /// Stack of <see cref="Node"/>
            /// </summary>
            public Stack<Node> Children { get; set; } = new Stack<Node>();

            public Node(string name)
            {
                Name = name;
            }
            /// <summary>
            /// Gets the Total desendants of a given node using  recursion
            /// </summary>
            /// <returns>Total Desendants</returns>
            public int CountDesendantsRecursion()
            {
                try
                {
                    return Desendants();
                }
                finally
                {
                    Searched = false;
                    ResetSearched();
                }
            }
            public int CountDesendantsNoRecursion()
            {
                try
                {
                    return DesendantsNoRecursion();
                }
                finally
                {
                    Searched = false;
                    ResetSearched();
                }
            }
            /// <summary>
            /// Gets the Total desendants of a given node without the use of recursion
            /// </summary>
            /// <returns>Total Desendants</returns>
            private int DesendantsNoRecursion()
            {
                int count = 0;
                Queue<Node> queue = new Queue<Node>();
                queue.Enqueue(this);
                while(queue.Count != 0)
                {
                    Node node = queue.Dequeue();
                    foreach(Node child in node.Children)
                    {
                        if (!child.Searched)
                        {
                            count++;
                            child.Searched = true;
                            queue.Enqueue(child);
                        }
                    }
                }
                return count;
            }

            /// <summary>
            /// Resets the <see cref="Node.Searched"/> property of the <see cref="Children"/>
            /// </summary>
            private void ResetSearched()
            {
                foreach (var child in Children)
                {
                    if (child.Searched)
                    {
                        child.ResetSearched();
                    }
                }
                Searched = false;
            }
            /// <summary>
            /// Gets the count of the unsearched Decendants and sets them to searched. 
            /// </summary>
            /// <returns>the count of the unsearched</returns>
            private int Desendants()
            {
                if (!Searched)
                {
                    Searched = true;
                    int count = 0;
                    foreach (var node in Children)
                    {
                        count += node.Desendants();
                    }
                    return count + Children.Count;
                }
                return 0;
            }

            public override string ToString()
            {
                return Name;
            }
        }
        /// <summary>
        ///  First function that is executed on launch
        /// </summary>
        static void Main()
        {
            Node A = new Node("A");
            Node B = new Node("B");
            Node C = new Node("C");
            Node D = new Node("D");
            Node E = new Node("E");
            Node F = new Node("F");

            A.Children.Push(B);
            B.Children.Push(D);
            B.Children.Push(E);
            A.Children.Push(C);
            C.Children.Push(F);
            F.Children.Push(A);

            // Assuming a tree and A is the only one we have
            var total = A.CountDesendantsRecursion();
            var noRecursionTotal = A.CountDesendantsNoRecursion();

            Debug.Assert(total == 6);
            Debug.Assert(noRecursionTotal == 6);
        }
    }
}
