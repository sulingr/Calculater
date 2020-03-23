using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculater
{
    public partial class Form1 : Form
    {
        bool isdot = false;
        double originnum = 0;
        double currentnum = double.NaN;
        bool ischange = false;
        bool iswait = false;
        char currentopr = ' ';
        string orignumexpress = "";
        string curnumexpress = "";
        
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Text = "";
        }

        private void NumButton_Click(object sender,EventArgs e)
        {
            Button b = sender as Button;
            if (label3.Text == "0"||!ischange) label3.Text = "";
            label3.Text += b.Text;
            ischange = true;
            iswait = false;
        }
        private void ZeroButton_Click(object sender,EventArgs e)
        {
            if (label3.Text == "0")
            {
                return;
            }
            else
            {
                label3.Text += '0';
            }
        }
       
        private void DoubleOprButton_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (iswait)
            {
                currentopr = b.Text[0];
                orignumexpress = orignumexpress.Substring(0, orignumexpress.Length - 1);
                orignumexpress += currentopr;
                label2.Text = orignumexpress;
                return;
            }
            if (ischange)
            {
                currentnum = double.Parse(label3.Text);
                orignumexpress += currentnum;
                ischange = false;
            }
            else
            {
                orignumexpress = label2.Text;
            }
            if (currentopr!=' ')
            {
                originnum = DoubleOprCal(originnum, currentnum, currentopr);
                currentopr = ' ';
                label3.Text = originnum.ToString();
            }
            else
            {
                originnum = currentnum;
            }
            currentopr = b.Text[0];
            orignumexpress+= currentopr.ToString();
            iswait = true;
            label2.Text = orignumexpress;
            isdot = false;
        }
        private double DoubleOprCal(double x,double y,char oper)
        {

                switch (oper)
                {
                    case '+':
                        return x + y;
                    case '-':
                        return x - y;
                    case '×':
                        return x * y;
                    case '÷':
                        if (y == 0.0) throw new DivideByZeroException();
                        return x / y;
                }
            return 0;

        }
        private bool SignEnd()
        {
            if (label2.Text != "")
            {
                string temp = label2.Text[label2.Text.Length - 1].ToString();
                if (temp == "=") return true;
                if (temp == ")") return false;
                return !int.TryParse(temp, out int r);
            }
            return false;
        }
        private double SingleOprCal(double x,char oper)
        {
            switch (oper)
            {
                case '%': return x / 100;
                case '2': return x * x;
                case 't': return Math.Sqrt(x);
                case '-': return -x;
                case 'x':
                    if (x == 0.0) throw new DivideByZeroException();
                    return 1 / x;
                case 'n': return Math.Sin(x);
            }

            return 0;

        }
        

        private void EqualBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void SingleOpr_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            double objnum = double.Parse(label3.Text);
            double result = SingleOprCal(objnum, b.Text[b.Text.Length - 1]);
            currentnum = result;
            switch (b.Text[b.Text.Length-1])
            {
                case '%':
                    curnumexpress = objnum.ToString() + "%";
                    break;
                case 'n':
                    curnumexpress = "sin(" + objnum.ToString() + ")";
                    break;
                case 't':
                    curnumexpress = "sqrt(" + objnum.ToString() + ")";
                    break;
                case '2':
                    curnumexpress = "sqr(" + objnum.ToString() + ")";
                    break;
                case '1':
                    curnumexpress = "1/(" + objnum.ToString() + ")";
                    break;
            }
            label3.Text = result.ToString();
            label2.Text = orignumexpress+curnumexpress;
            ischange = false;
            iswait = false;
            isdot = false;
        }

        private void Dot_Click(object sender, EventArgs e)
        {
            if (!isdot) label3.Text += ".";
            isdot = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!ischange || iswait)
            {
                return;
            }
            else if (label3.Text.Count()<=1)
            {
                label3.Text = "0";
            }
            else
            {
                if (label3.Text[label3.Text.Length - 1] == '.') isdot = false;
                label3.Text = label3.Text.Substring(0, label3.Text.Length - 1);
            }
        }

        private void TravesOpr_Click(object sender, EventArgs e)
        {
            double resum = double.Parse(label3.Text);
            resum = -resum;
            label3.Text = resum.ToString();
            ischange = true;
            iswait = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            orignumexpress = "";
            originnum = 0;
            currentnum = double.NaN;
            curnumexpress = "";
            label2.Text = "";
            label3.Text = "0";
            iswait = false;
            ischange = false;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                if (control.GetType() == typeof(Button))
                {
                    Button b;
                    b = (Button)control;
                    if (b.Text == "X^2") continue;
                    if (b.Text == "+/-") continue;
                    if (b.Text[0] == e.KeyChar)
                    {
                        if (b.Text == "sqrt") break;
                        if (b.Text == "Sin") break;
                        if (b.Text == "C") break;
                        b.PerformClick();
                        b.Focus();
                    }
                }
            }
        }
        
    }
}
