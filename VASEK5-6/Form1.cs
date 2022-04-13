using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace VASEK5_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double Number = 0;
            retry:
            try
            {
                Number = Convert.ToDouble(Interaction.InputBox("Vvedi chislo", "CHISLO", "1", -1, -1));
            }
            catch(Exception)
            {
                textBox1.Text = "Неверный формат данных";
               DialogResult result = MessageBox.Show("Что делать дальше", "ERROR", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                switch (result)
                {
                    case DialogResult.Abort:
                        break;
                    case DialogResult.Retry:
                        goto retry;
                        break;
                    case DialogResult.Ignore:
                    Number = 1;
                        break;

                
                }
            }

            MessageBox.Show("Ваше чичсло " + Number);
        }

        private void kolvoN_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(textBoxNx.Text);
                if(Convert.ToInt32(textBoxNx.Text) < 0)
                {
                    throw new ArgumentNullException("");
                }
                textBoxNx.ReadOnly = true;
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Шаг не может быть отрицательным");
            }
            catch (Exception)
            {
                MessageBox.Show("Вы ничего не ввели или ввели строку");
            }
        }

        private void buttonX_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(textBoxX0.Text);
                Convert.ToDouble(textBoxHx.Text);
                textBoxX0.ReadOnly = true;
                textBoxHx.ReadOnly = true;
            }           
            catch (Exception)
            {
                MessageBox.Show("Вы ничего не ввели или ввели строку");
            }

        }

        private void buttonY_Click(object sender, EventArgs e)
        {
            try
            {
                double a = double.Parse(textBoxY.Text);
                comboBoxY.Items.Add(textBoxY.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Вы ввели строку или ничего");
            }

        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            try
            {
                int n;
                if (textBoxNx.Text == null || textBoxX0.Text == null || textBoxHx.Text == null)
                {
                    throw new ArgumentNullException("");
                }
                n = Convert.ToInt32(textBoxNx.Text);
                    for (int i = 1; i <= n; i++)
                {

                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("вы не ввели какой-то аргумент");
            }
        }
    }

}
