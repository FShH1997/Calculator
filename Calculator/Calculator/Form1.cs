using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        double lastNumber = 0;              //记录上一个结果
        double number = 0;                  //记录当前数字
        bool haveLastOperator = false;      //是否有上一个运算操作
        String lastOperator = null;         //记录上一个运算操作
        bool justOperat = false;            //是否刚刚按下运算符键
        bool justResult = false;            //是否刚刚按下等号键

        public Form1()
        {
            InitializeComponent();
            textBoxMain.Text = "0";
        }

        //按下AC键
        private void buttonAC_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "0";
            lastNumber = 0;
            haveLastOperator = false;
            lastOperator = null;
            justOperat = false;
            justResult = false;
        }

        //按下数字键
        private void buttonNumber_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (textBoxMain.Text.Equals("0") || textBoxMain.Text.Equals("") || justOperat || justResult)
            {
                textBoxMain.Text = button.Text;
                if (justResult)
                {
                    haveLastOperator = false;
                }
            }
            else
            {
                textBoxMain.AppendText(button.Text);
            }
            justOperat = false;
            justResult = false;
        }

        //按下小数点键
        private void buttonDot_Click(object sender, EventArgs e)
        {
            if (justOperat || justResult)
            {
                textBoxMain.Text = "0";
                if (justResult)
                {
                    haveLastOperator = false;
                }
            }
            else
            {
                for (textBoxMain.SelectionStart = 0; textBoxMain.SelectionStart < textBoxMain.TextLength; textBoxMain.SelectionStart++)
                {
                    textBoxMain.SelectionLength = 1;
                    if (textBoxMain.SelectedText.Equals("."))
                    {
                        return;
                    }
                }
            }
            textBoxMain.AppendText(".");
            justOperat = false;
            justResult = false;
        }

        //按下DEL键
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (!justOperat && !justResult && !textBoxMain.Text.Equals("0") && !textBoxMain.Text.Equals(""))
            {
                if (textBoxMain.TextLength > 1)
                {
                    textBoxMain.SelectionStart = 0;
                    textBoxMain.SelectionLength = textBoxMain.TextLength - 1;
                    textBoxMain.Text = textBoxMain.SelectedText;
                }
                else
                {
                    textBoxMain.Text = "0";
                }
                justOperat = false;
                justResult = false;
            }
        }

        //按下运算符键
        private void buttonOperator_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (justOperat)
            {
                lastOperator = button.Name;
                return;
            }
            else if (!haveLastOperator || justResult)
            {
                lastNumber = Convert.ToDouble(textBoxMain.Text);
            }
            else
            {
                number = Convert.ToDouble(textBoxMain.Text);
                switch (lastOperator)
                {
                    case "buttonAdd":
                        {
                            lastNumber = lastNumber + number;
                            break;
                        }
                    case "buttonSubtract":
                        {
                            lastNumber = lastNumber - number;
                            break;
                        }
                    case "buttonMultiply":
                        {
                            lastNumber = lastNumber * number;
                            break;
                        }
                    case "buttonDivide":
                        {
                            if (number == 0)
                            {
                                textBoxMain.Text = "除数不能为零";
                            }
                            else
                            {
                                lastNumber = lastNumber / number;
                            }
                            break;
                        }
                }
            }
            textBoxMain.Text = lastNumber.ToString();
            haveLastOperator = true;
            lastOperator = button.Name;
            justOperat = true;
            justResult = false;
        }

        //按下等号键
        private void buttonResult_Click(object sender, EventArgs e)
        {
            if (justResult)
            {
                if (!haveLastOperator)
                {
                    textBoxMain.Text = Convert.ToDouble(textBoxMain.Text).ToString();
                }
                else
                {
                    switch (lastOperator)
                    {
                        case "buttonAdd":
                            {
                                lastNumber = lastNumber + number;
                                break;
                            }
                        case "buttonSubtract":
                            {
                                lastNumber = lastNumber - number;
                                break;
                            }
                        case "buttonMultiply":
                            {
                                lastNumber = lastNumber * number;
                                break;
                            }
                        case "buttonDivide":
                            {
                                if (Convert.ToDouble(textBoxMain.Text) == 0)
                                {
                                    textBoxMain.Text = "除数不能为零";
                                }
                                else
                                {
                                    lastNumber = lastNumber / number;
                                }
                                break;
                            }
                    }
                    textBoxMain.Text = lastNumber.ToString();
                }
            }
            else
            {
                number = Convert.ToDouble(textBoxMain.Text);
                if (!haveLastOperator)
                {
                    textBoxMain.Text = number.ToString();
                }
                else
                {
                    switch (lastOperator)
                    {
                        case "buttonAdd":
                            {
                                lastNumber = lastNumber + number;
                                break;
                            }
                        case "buttonSubtract":
                            {
                                lastNumber = lastNumber - number;
                                break;
                            }
                        case "buttonMultiply":
                            {
                                lastNumber = lastNumber * number;
                                break;
                            }
                        case "buttonDivide":
                            {
                                if (Convert.ToDouble(textBoxMain.Text) == 0)
                                {
                                    textBoxMain.Text = "除数不能为零";
                                }
                                else
                                {
                                    lastNumber = lastNumber / number;
                                }
                                break;
                            }
                    }
                    textBoxMain.Text = lastNumber.ToString();
                }
            }
            justResult = true;
            justOperat = false;
        }

        //按下键盘上的按键
        private void anywhereKeyPress(object sender, KeyPressEventArgs e)
        {
            Button button = new Button();
            EventArgs eventArgs = new EventArgs();
            if (e.KeyChar == '0')                           //按下数字键
            {
                textBoxMain.Focus();
                button.Text = "0";
                buttonNumber_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '1')
            {
                textBoxMain.Focus();
                button.Text = "1";
                buttonNumber_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '2')
            {
                textBoxMain.Focus();
                button.Text = "2";
                buttonNumber_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '3')
            {
                textBoxMain.Focus();
                button.Text = "3";
                buttonNumber_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '4')
            {
                textBoxMain.Focus();
                button.Text = "4";
                buttonNumber_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '5')
            {
                textBoxMain.Focus();
                button.Text = "5";
                buttonNumber_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '6')
            {
                textBoxMain.Focus();
                button.Text = "6";
                buttonNumber_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '7')
            {
                textBoxMain.Focus();
                button.Text = "7";
                buttonNumber_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '8')
            {
                textBoxMain.Focus();
                button.Text = "8";
                buttonNumber_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '9')
            {
                textBoxMain.Focus();
                button.Text = "9";
                buttonNumber_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '.')                      //按下小数点
            {
                textBoxMain.Focus();
                buttonDot_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '\b')                     //按下退格键
            {
                textBoxMain.Focus();
                buttonDelete_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '+')                      //按下运算符键
            {
                textBoxMain.Focus();
                button.Name = "buttonAdd";
                buttonOperator_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '-')
            {
                textBoxMain.Focus();
                button.Name = "buttonSubtract";
                buttonOperator_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '*')
            {
                textBoxMain.Focus();
                button.Name = "buttonMultiply";
                buttonOperator_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '/')
            {
                textBoxMain.Focus();
                button.Name = "buttonDivide";
                buttonOperator_Click(button, eventArgs);
                e.Handled = true;
            }
            else if (e.KeyChar == '=' || e.KeyChar == (char)13)     //按下等号键或回车键
            {
                textBoxMain.Focus();
                buttonResult_Click(button, eventArgs);
                e.Handled = true;
            }
        }
    }
}
