using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BinaryTree2App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        void displayTree(bool fromZero)
    {
        int times = (int)numericUpDown2.Value;
        int start = fromZero ? 0 : times;
        for (int i = start; i < times + 1; i++)
        {
            string[] tree = BinaryTree2.createBinaryTreeTo(i, "|").Split(new Char[] { ',' });
            String txt = "";
            foreach (string branch in tree)
            {
                txt += branch;
                txt += Environment.NewLine;
            }
            Output.AppendText(txt);
        }
    }         

    class BinaryTree2
{
    static string tree = "";
    static void create(string str, int times)
    {
        for (int i = 0; i < times; i++)
        {
            tree += str;
        }
    }

    static void createNode(string node, int nMiddleSpaces, int nUnderlines, int strLen)
    {
        create("_", nUnderlines); // Create the underlines before the node
        create(node, 1); // Create the node
        create("_", nUnderlines); // Create the underlines after the node
        create(" ", nMiddleSpaces - 2 * nUnderlines); // Create spaces between nodes and underlines
    }

    static void createEachNode(int nStartSpaces, int nMiddleSpaces, int nNodes, int nUnderlines, int strLen, string node)
    {
        create(" ", nStartSpaces - nUnderlines); // Create spaces before the first node
        for (int i = 0; i < nNodes; i++)
        {
            createNode(node, nMiddleSpaces, nUnderlines, strLen); // Create the level
        }
    }

    static void createNodes(double oppExp, int nNodes, string node, int strLen) // Create branches at a certain level in the tree
    {
        int nMiddleSpaces = (((int)Math.Pow(2, oppExp + 1) - 1) * strLen); // Number of spaces between nodes of the level
        int nStartSpaces = (nMiddleSpaces - strLen) / 2; // Number of spaces at the beginning of the level
        nStartSpaces = nStartSpaces > 0 ? nStartSpaces : 0; // Ensure no negative number as a result from the calculation

        int nUnderlines = (nStartSpaces - strLen) / 2; // Number of underlines to connect sub nodes
        nUnderlines = nUnderlines > 0 ? nUnderlines : 0; // Ensure no negative number as a result from the calculation

        createEachNode(nStartSpaces, nMiddleSpaces, nNodes, nUnderlines, strLen, node); // Create each node on each level
    }

    public static string createBinaryTreeTo(int exp, string node)
    {
        BinaryTree2.tree = "";
        for (int i = 0; i < exp + 1; i++) // Iterate through each level of the binary tree
        {
            /* oppExp
             * If current exponent i1 is at is 0, the opposite exponent is 3. If i1 is 1, opposite to that is 2.
             * The idea is that levels of the lowest exponent are the ones with largest spaces.
             * The number of spaces is calculated using two to the power of the opposite level. 
             * to the current level in the loop. */
            double oppExp = exp - i;
            int nNodes = (int)Math.Pow(2, i); // Number of nodes in the current level om the loop
            createNodes(oppExp, nNodes, node, node.Length); // Create the nodes in the tree
            create(",", 1); // Create new line
        }
        return BinaryTree2.tree;
    }
}
     

        private void checkBox1_Clicked(object sender, RoutedEventArgs e)
        {
            Output.Clear();
            if (checkBox1.IsChecked == true)
            {
                displayTree(true);
            }
            else
            {
                displayTree(false);
            }
        }

        private void numericUpDown2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Output.Clear();
            if (checkBox1.IsChecked == true)
            {
                displayTree(true);
            }
            else
            {
                displayTree(false);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Output.FontSize = (int)numericUpDown1.Value;
            Output.Clear();
            if (checkBox1.IsChecked == true)
            {
                displayTree(true);
            }
            else
            {
                displayTree(false);
            }
        }
    }
}
