using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CherryPicking
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SearchWord cherryPick;

        private void Form1_Load(object sender, EventArgs e)
        {
            cherryPick = new SearchWord(textBox1.Text, "cherry");
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = 0;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            var random = new Random();
            (int Start, int Length) result = (textBox1.SelectionStart, textBox1.SelectionLength);

            int next = random.Next(1, 8);

            switch (next)
            {
                case 1:
                    result = cherryPick.FindByForEach(result.Start + result.Length);
                    break;
                case 2:
                    result = cherryPick.FindByFor(result.Start + result.Length);
                    break;
                case 3:
                    result = cherryPick.FindByRegularExpression(result.Start + result.Length);
                    break;
                case 4:
                    result = cherryPick.FindByIndexOf(result.Start + result.Length);
                    break;
                case 5:
                    result = cherryPick.FindByZip(result.Start + result.Length);
                    break;
                case 6:
                    var fSharplist = cherryPick.FindByFSharp(result.Start + result.Length);
                    result = (fSharplist.Start, fSharplist.Length);
                    break;
                case 7:
                    result = cherryPick.FindBySplit(result.Start + result.Length);
                    break;
            }
            
            if (result.Start <= 0)
            {
                result = (0, 0);
            }

            label1.Text = "Method " + next.ToString() + ". Result " + result.Start.ToString() + ", " + result.Length.ToString();

            textBox1.SelectionStart = result.Start;
            textBox1.SelectionLength = result.Length;
            textBox1.Focus();
        }
    }
}
