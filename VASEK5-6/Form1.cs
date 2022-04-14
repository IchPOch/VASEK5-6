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
using System.IO;
namespace VASEK5_6
{
    public partial class Form1 : Form
    {
        private List<double> lX0 = new List<double>();
        private List<List<double>> lY = new List<List<double>>();
        private List<double> lHx = new List<double>();
        private List<int> lNx = new List<int>();

        private List<List<double>> vivodX = new List<List<double>>();
        private List<List<double>> vivodY = new List<List<double>>();
        private List<List<double>> vivodG = new List<List<double>>();
        private List<string> pathDat = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }
        double F(double x, double y)
        {
            return y * Math.Pow(2, Math.Log(x - 1));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double Number = 0;
        retry:
            try
            {
                Number = Convert.ToDouble(Interaction.InputBox("Vvedi chislo", "CHISLO", "1", -1, -1));
            }
            catch (Exception)
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
                if (Convert.ToInt32(textBoxNx.Text) < 0)
                {
                    throw new ArgumentNullException("");
                }
                lNx.Add(Convert.ToInt32(textBoxNx.Text));
                if (Convert.ToInt32(textBoxN.Text) <= lNx.Count)
                {
                    textBoxNx.ReadOnly = true;
                }
                else
                {
                    textBoxNx.Text = "Вводите следующее число";
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Колво точек не может быть отрицательным");
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
                double.Parse(textBoxX0.Text);
                double.Parse(textBoxHx.Text);
                lX0.Add(double.Parse(textBoxX0.Text));
                lHx.Add(double.Parse(textBoxHx.Text));
                if (Convert.ToInt32(textBoxN.Text) <= lX0.Count)
                {
                    textBoxX0.ReadOnly = true;
                    textBoxHx.ReadOnly = true;
                }
                else
                {
                    textBoxX0.Text = "Вводите следующее число";
                    textBoxHx.Text = "Вводите следующее число";
                }
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
                textBoxY.Text = "";
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

                if (textBoxNx.Text == null || textBoxX0.Text == null || textBoxHx.Text == null)
                {
                    throw new ArgumentNullException("");
                }

                if (comboBoxY.Items.Count == 0)
                {
                    throw new ArgumentNullException("");
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("вы не ввели какой-то аргумент");
            }
            int Nx, N;
            double Hx, X0;
            DateTime now_Date = DateTime.Now;
            string nowdatetime = now_Date.ToString();
            string path1 = @"myProgram.log";
            string name = "VASEK5-6, Вариант 28";
            string func = "y*2^ln(x-1)";
            using (StreamWriter file1 = new StreamWriter(path1))
            {
                file1.WriteLine("Название программы и номер варианта: " + name);
                file1.WriteLine("Дата и время начала выполнения расчёта: " + nowdatetime);
                file1.WriteLine("Функция: " + func);


                string path2 = @"myErrors.log";
                StreamWriter file_Errors = new StreamWriter(path2);
                N = Convert.ToInt32(textBoxN.Text);

                string FileName = "";
                int data = 1;
                for (int j = 0; j < lY.Count; j++)
                {
                    for (int i = 0; i < N; i++)
                    {

                        Hx = lHx[i];
                        X0 = lX0[i];

                        FileName = Path.Combine(Environment.CurrentDirectory, "G" + string.Format("{0:d4}", data) + ".dat");
                        double n = 0;

                        try
                        {
                            using (StreamWriter fileResult = new StreamWriter(FileName))
                            {

                                fileResult.WriteLine(func);
                                string xadd = "y/x".PadRight(10);
                                string yadd = "";
                                double y = 0, x = 0;
                                int count = 0;
                                Nx = lNx[i];
                                for (int j2 = 0; j2 < lY[j].Count; j2++)
                                {
                                    y = lY[j][j2];
                                    yadd = (y).ToString("##0.0##").PadRight(10);
                                    for (int i2 = 0; i2 < Nx; i2++)
                                    {
                                       
                                        string funcc = "";
                                        x = X0 + i2 * Hx;
                                        double Res = F(x, y);
                                        xadd += (x).ToString("##0.0##").PadRight(10);
                                        if (double.IsNaN(Res) || x <= 1)
                                        {
                                            funcc = ("NaN").PadRight(10);
                                            file_Errors.WriteLine(FileName);
                                            file_Errors.WriteLine(func);
                                            file_Errors.WriteLine(string.Format("x={0}; y={1}", x, y));
                                            file_Errors.WriteLine("FormatException");
                                        }
                                        else
                                        {
                                            funcc = Res.ToString("#0.0").PadRight(9);
                                        }
                                        listBox1.Items.Add("G" + count + " (" + x + ";" + y + ")" + " = " + Res);
                                        yadd += funcc + " ";
                                        count++;
                                    }
                                    if (j2 == 0)
                                    {
                                        fileResult.WriteLine(xadd);
                                    }
                                    fileResult.WriteLine(yadd);
                                }
                                string countInfo = string.Format("Всего точек: {0}", count);
                                fileResult.WriteLine(countInfo);
                                fileResult.Close();
                                file1.WriteLine(FileName);
                                data++;

                            }
                        }
                        catch(FormatException)
                        {
;
                        }

                    }

                }
                file_Errors.Close();
                file1.Close();
            }
        }

        private void buttonN_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(textBoxN.Text);
                if (Convert.ToInt32(textBoxN.Text) <= 0)
                {
                    throw new ArgumentNullException("");
                }
                textBoxN.ReadOnly = true;
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Количество данных не может быит меньше или равно нулю");
            }
            catch (Exception)
            {
                MessageBox.Show("Вы ничего не ввели или ввели строку");
            }
        }

        private void buttonYSave_Click(object sender, EventArgs e)
        {

            lY.Add(new List<double>());
            int a = lY.Count - 1;
            for (int i = 0; i < comboBoxY.Items.Count; i++)
            {
                comboBoxY.SelectedIndex = i;
                lY[a].Add(double.Parse(comboBoxY.SelectedItem.ToString()));
            }
            if (Convert.ToInt32(textBoxN.Text) <= lY.Count)
            {
                textBoxY.ReadOnly = true;
                buttonY.Enabled = false;
                buttonYSave.Enabled = false;
                comboBoxY.Enabled = false;
            }
            else
            {
                comboBoxY.Items.Clear();
                comboBoxY.Text = "";
            }
        }

        private void VivodF_Click(object sender, EventArgs e)
        {
            string path1 = @"myProgram.log";
            using (StreamReader reader = new StreamReader(path1))
            {
                string line;
                int k = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    k++;
                    if(k > 3)
                    {
                        pathDat.Add(line);
                    }
                    
                }
            }
            for(int i = 0; i < pathDat.Count; i++)
            {
                using (StreamReader read = new StreamReader(pathDat[i]))
                {
                    string line;
                    int s = 0;
                    for(int n = 0; n < pathDat.Count; n++)
                    {
                        vivodX.Add(new List<double>());
                        vivodY.Add(new List<double>());
                        vivodG.Add(new List<double>());
                    }
                    while ((line = read.ReadLine()) != null)
                    {
                        try
                        {
                            s++;
                            if (s == 2)
                            {
                                string[] dat = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int j = 1; j < dat.Length; j++)
                                {
                                    vivodX[i].Add(double.Parse(dat[j]));
                                }
                            }
                            if (s > 2)
                            {
                                string[] dat = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                vivodY[i].Add(double.Parse(dat[0]));
                                for (int j = 1; j < dat.Length; j++)
                                {
                                    vivodG[i].Add(double.Parse(dat[j]));
                                }
                            }
                        }
                        catch(FormatException)
                        {

                        }
                    }
                }
            }
            for(int i = 0; i < pathDat.Count; i++)
            {
                Vivod.Text += Environment.NewLine;
                Vivod.Text += (i + 1) + " Набор y: ";
                for(int y = 0; y < vivodY[i].Count; y++)
                {
                    Vivod.Text += vivodY[i][y] + " "; 
                }
                Vivod.Text += Environment.NewLine;
                Vivod.Text += (i + 1) + " Набор x: ";
                for (int x = 0; x < vivodX[i].Count; x++)
                {
                    Vivod.Text += vivodX[i][x] + " ";
                }
                Vivod.Text += Environment.NewLine;
            }
        }
    }
}


