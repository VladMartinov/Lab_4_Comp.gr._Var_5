//==========================================
// Лабораторная работа №4, Вариант №5
// 
// Задание:
// Получить кривую дракона 7-го порядка. Кривая изображается
// прерывистой линией.Голова дракона рисуется комбинированной линией.
//==========================================

// Подключение всех используемых библиоиек
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            // Инициализация всех компонентов на Form1
            InitializeComponent();

            // Задания цвета для заднего фона pictureBox1
            pictureBox1.BackColor =  Color.FromKnownColor(KnownColor.Azure);
        }

        // Метод создания порядка для кривой дракона
        private string CreateDragonLine()
        {
            
            // Задаем порядок кривой дракона
            int orderCurve = 7;
            
            // Задаем кривую дракона первого порядка
            string str = "1";
           
            for (int i = 1; i < orderCurve; i++)
            { 
                // Создаем на базе строки предыдущего порядка изменяемую
                // Строку символов
                StringBuilder sb = new StringBuilder(str);
                
                // Находим индекс центральной цифры
                int seredina = (int)Math.Floor((double)sb.Length / 2);
               
                // Меняем символ с этим индексом на 0
                sb[seredina] = '0';

                // Формируем строку нового порядка кривой дракона
                str = str + "1" + sb;
            }

            // Возврат кривой дракона
            return str;
        }

        // Метод вызываемый при нажатии на кнопку для запуска отрисовки кривой дракона
        private void ButtonDrawDragon(object sender, EventArgs e)
        {

            Graphics g = pictureBox1.CreateGraphics();
            
            // Задаем кисть для тела дракона
            Pen pDash = new Pen(Color.Red)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
            };

            // Задаем кисть для головы дракона
            Pen pCombine = new Pen(Color.Black, 20)
            {
                CompoundArray = new float[] { 0.0f, 0.1f, 0.15f, 0.30f, 0.60f, 1.0f }
            };

            int dx = 20;

            // Формируем строку кривой дракона
            string str = CreateDragonLine();
            
            // Начальная точка первой линии
            int x1 = pictureBox1.Size.Width / 2;
            int y1 = pictureBox1.Size.Height / 2;
            
            // Конечная точка первой линии
            int x2 = pictureBox1.Size.Width / 2;
            int y2 = pictureBox1.Size.Height / 2 - dx;
            
            // Сохраняем координаты конечной точки
            int x3 = x2; int y3 = y2;
            
            // Рисуем линию из начальной в конечную точку
            g.DrawLine(pCombine, x1, y1, x3, y3);
            
            // Цикл по всем цифрам кривой дракона
            for (int i = 0; i < str.Length; i++)
            {

                if (y2 - y1 < 0)
                { 
                    // Рисовали вверх на предыдущем шаге
                    if (str[i] == '1') x3 = x2 - dx;    // Поворот налево
                    else x3 = x2 + dx;                  // Поворот направо
                   
                    y3 = y2;
                }
                if (x2 - x1 < 0)
                { 
                    // Писовали влево на предыдущем шаге
                    if (str[i] == '1') y3 = y2 - dx;    // Поворот налево
                    else y3 = y2 + dx;                  // Поворот направо
                    
                    x3 = x2;
                }
                if (x2 - x1 > 0)
                { 
                    // Рисовали вправо на предыдущем шаге
                    if (str[i] == '1') y3 = y2 + dx;    // Поворот налево
                    else y3 = y2 - dx;                  // Поворот направо
                    x3 = x2;
                }
                if (y2 - y1 > 0)
                { 
                    // Рисовали вниз на предыдущем шаге
                    if (str[i] == '1') x3 = x2 + dx;    // Поворот налево
                    else x3 = x2 - dx;                  // Поворот направо
                    
                    y3 = y2;
                }
                if (i == str.Length - 1)
                {
                    // Цвет и стиль последней линии
                    g.DrawLine(pCombine, x2, y2, x3, y3);
                }
                else
                { // Цвет и стиль остальных линий ( голова дракона )
                    g.DrawLine(pDash, x2, y2, x3, y3);
                }

                // Переприсваивание для следующего шага
                x1 = x2; y1 = y2;
                x2 = x3; y2 = y3;

            }   // Конец цикла по цифрам кривой дракона
        }       // Конец метода обработки события клика на кнопку

        // Метод очистки pictureBox1
        private void ButtonClear_Click(object sender, EventArgs e)
        {
            // Выделение памяти для объекта класса Graphics
            Graphics g = pictureBox1.CreateGraphics();

            // Вызов метода Clear для заполнения ( заливки ) pictureBox1 цветом заднего фона
            g.Clear(pictureBox1.BackColor);
        }

    } // конец формы
}