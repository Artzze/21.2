﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace эээ
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
        int[,] mas;
        int N, M;
        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            if (Range1.Text != "" && Range2.Text != "")
            {
                int randMin, randMax;
                if (int.TryParse(Range1.Text, out randMin) && int.TryParse(Range2.Text, out randMax))
                {
                    if (randMin <= randMax)
                    {
                        if(randMin == 0 && (randMax == 0 || randMax == 1))
                        {
                            MessageBox.Show("Диапазон должен быть больше или меньше нуля", "Ошибка");
                        }
                        else
                        {
                            Rez.Clear();
                            N = Convert.ToInt32(Column_Count1.Text);
                            M = Convert.ToInt32(Column_Count2.Text);
                            mas = new int[N, M];
                            Random rnd = new Random();
                            for (int i = 0; i < N; i++)
                            {
                                for (int j = 0; j < M; j++)
                                {
                                    do
                                    {
                                        mas[i, j] = rnd.Next(randMin, randMax);
                                    } while (mas[i, j] == 0);
                                }
                            }
                            DataGrid.ItemsSource = VisualArray.ToDataTable(mas).DefaultView;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Минимальное значение не может быть больше максимального", "Ошибка");
                    }
                }
                else
                {
                    MessageBox.Show("Диапазон значений задается числами", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Задайте диапазон значений", "Ошибка");
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (Column_Count1.Text != "" && Column_Count2.Text != "")
            {
                if (int.TryParse(Column_Count1.Text, out N) && int.TryParse(Column_Count2.Text, out M))
                {
                    if(N > 0 && M > 0)
                    {
                        mas = new int[N, M];
                        DataGrid.ItemsSource = VisualArray.ToDataTable(mas).DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("Размеры таблицы должны быть больше 0", "Ошибка");
                    }
                }
                else
                {
                    MessageBox.Show("Размеры таблицы задаются числами", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Задайте размеры таблицы", "Ошибка");
            }
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            int avr = 0, otv = -100000000, stroka =0, stolbec =0;
            if (DataGrid.ItemsSource != null)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        avr += mas[i, j];
                    }
                }
                avr = avr / (M + N);
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        if ((mas[i, j] < avr && mas[i, j] > otv) || (mas[i, j] > avr && mas[i, j] < otv))
                        {
                            otv = mas[i, j];
                            stroka = i+1; stolbec = j+1;
                        }
                    }
                }
                Rez.Text = "(" + stroka.ToString() + ";" + stolbec.ToString() + ")";
            }
            else
            {
                MessageBox.Show("Таблица пустая", "Ошибка");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = null;
            mas = null;
            Rez.Clear();
            Range1.Clear();
            Range2.Clear();
            Column_Count1.Clear();
            Column_Count2.Clear();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Создатель: Таут\nВариант: 7\nЗадача:  Дана матрица размера M * N. Найти номера строки и столбца для элемента матрицы, наиболее близкого к среднему значению всех ее элементов.", "Справка");
        }
    }
}