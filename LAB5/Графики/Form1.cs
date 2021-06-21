using System; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ЛР2._2
{
    public partial class Form1 : Form
    {
        List<forLibraries> info = new List<forLibraries>();
        public Form1()
        {
            InitializeComponent();
        }

        //Выгружаем в лист методы для построения графиков
        private void Form1_Shown(object sender, EventArgs e)
        {
            //В строковый массив помещаем пути ко всем библиотекам из папки Plugins
            string[] getDLL = Directory.GetFiles(@"C:\Users\user1\Downloads\ТМП\ЛР5 +\Библиотека +\Графики\", "*.dll", SearchOption.TopDirectoryOnly);

            //Если библиотеку удалось загрузить и в ней нашлись функции, то добавляем в лист, иначе пропускаем
            foreach (var path in getDLL)
            {
                try
                {
                    info.Add(new forLibraries(path));
                }
                catch { }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x = 0;

            try
            {
                chart1.Series[0].Points.Clear();
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.Series[0].LegendText = "2sin(x) + 3";

                double min, max = 10;
                
                for (min = 0; x < max; x += 0.01)
                {
                    chart1.Series[0].Points.AddXY(x, 2 * Math.Sin(x) + 3);
                }

                MessageBox.Show("Время сигнала: " + info[0].time(min, max) + ", амплитуда: " + info[0].amplitude(chart1.Series[0].LegendText) + ".");
            } 
            catch
            {
                MessageBox.Show("Этот метод не сработал!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double x = 0;

            try
            {
                chart1.Series[0].Points.Clear();
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.Series[0].LegendText = "5sin(3 * x - 2) + 6";

                double min, max = 10;

                for (min = 0; x < max; x += 0.01)
                {
                    chart1.Series[0].Points.AddXY(x, 5 * Math.Sin(3 * x - 2) + 6);
                }

                MessageBox.Show("Время сигнала: " + info[0].time(min, max) + ", амплитуда: " + info[0].amplitude(chart1.Series[0].LegendText) + ".");
            } 
            catch
            {
                MessageBox.Show("Этот метод не сработал!");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double x = 0;

            try
            {
                chart1.Series[0].Points.Clear();
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.Series[0].LegendText = "sin(2 * x + 1)";

                double min, max = 10;

                for (min = 0; x < max; x += 0.01)
                {
                    chart1.Series[0].Points.AddXY(x, Math.Sin(2 * x + 1));
                }

                MessageBox.Show("Время сигнала: " + info[0].time(min, max) + ", амплитуда: " + info[0].amplitude(chart1.Series[0].LegendText) + ".");
            } 
            catch
            {
                MessageBox.Show("Этот метод не сработал!");
            }  
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double x = 0;

            try
            {
                chart1.Series[0].Points.Clear();
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.Series[0].LegendText = "0.25sin(x) + 2";

                double min, max = 10;

                for (min = 0; x < max; x += 0.01)
                {
                    chart1.Series[0].Points.AddXY(x, 0.25 * Math.Sin(x) + 2);
                }

                MessageBox.Show("Время сигнала: " + info[0].time(min, max) + ", амплитуда: " + info[0].amplitude(chart1.Series[0].LegendText) + ".");
            }
            catch
            {
                MessageBox.Show("Этот метод не сработал!");
            }
        }
    }
}
