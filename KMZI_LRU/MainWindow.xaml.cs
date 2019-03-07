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

namespace KMZI_LRU
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<int> ai = new List<int>();
        public static List<int> Pi = new List<int>();
        public static int c = 0;
        public static int p;
        public static int max;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                p = int.Parse(tbMod.Text.Trim());
                getVector(tbVector.Text.Trim().Split(), tbPi.Text.Trim().Split());
                c = 0;
                max = (int)Math.Pow(p, Pi.Count) - 1;
                LRP(ai);
                tblResult.Text ="Период равен "+ Period(Pi.Count).ToString();
                tblResult.Text += "\nЛРП: "+Output(ai);
            } catch(Exception ex) { MessageBox.Show(ex.Message,"Ошибка"); }
        }

        public static int Period(int k)
        {
            int t = 0;
            for(int i = k-1; i < max; i++)
            {
                int b = 0;
                while(b!=k && ai[i+b]==ai[b])
                {
                    b++;
                }
                if (b < k) t++;
                else break;
            }
            return t - 1 + k;
        }
        public static List<int> LRP(List<int> vector)
        {
            if (c > max)
                return newElement(vector);
            c++;
            vector = LRP(vector);
            return newElement(vector);
        }
        static List<int> newElement(List<int> ls)
        {
            int buf = 0;
            for(int i = 0; i < Pi.Count; i++)
            {
                buf += ls[ls.Count - i - 1] * Pi[Pi.Count - i - 1] ;
            }
            ls.Add(buf%p);
            return ls;
        }
        public static void getVector(string[] str_ai, string[] str_Pi)
        {
            ai.Clear();
            Pi.Clear();
            for (int i = 0; i < str_ai.Length; i++)
            {
                ai.Add(int.Parse(str_ai[i]));
                Pi.Add(int.Parse(str_Pi[i]));
            }
        }
        public static string Output(List<int> vector)
        {
            string s = "";
            foreach (int x in vector) s += x + " ";
            return s;
        }
    }
}
