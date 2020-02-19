using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            // Первый запуск // 
            InitializeComponent();
            GameField.SetButtons(this); // Размещение кнопок в текущей форме.
            GameField.GiveLabels(label1, label2, label3, label4, label5, label6);
            GameField.RefreshButtons();
            // Повторяющийся запуск //
        }

    }
    //~~~~~~~~~~~~~~~~~~~//
    static class GameField
    {
        static public Player player1 = new Player();
        static public Player player2 = new Player();
        static public Button[,] buttons = new Button[3, 3];
        static public string turnOf = "X";
        static public Label label1, label2, label3, label4, label5, label6;
        static GameField() // Конструктор
        {
            for (int x = 0; x < buttons.GetLength(0); x++) // Создание кнопок
            {
                for (int y = 0; y < buttons.GetLength(1); y++)
                {
                    buttons[x, y] = new Button();
                    buttons[x, y].Size = new Size(100, 100);
                    buttons[x, y].Location = new Point(300 + 106 * x, 12 + 106 * y);
                    buttons[x, y].Font = new Font(new FontFamily("Microsoft Sans Serif"), 60);
                    buttons[x, y].TextAlign = ContentAlignment.MiddleCenter;
                    buttons[x, y].Click += new EventHandler(button_Click);
                    CreatePlayers();
                }
            }
        }
        static public void GiveLabels(Label _label1, Label _label2, Label _label3, Label _label4, Label _label5, Label _label6)
        {
            label1 = _label1;
            label2 = _label2;
            label3 = _label3;
            label4 = _label4;
            label5 = _label5;
            label6 = _label6;
        }
        static public void SetButtons(Form form) // Размещение кнопок в окне
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    form.Controls.Add(GameField.buttons[x, y]);
                }
            }
        }
        static private void button_Click(object sender, System.EventArgs e) // Обработчик события клика по кнопке
        {
            if (sender is Button) 
            {
                Button button = sender as Button;
                if (button.Text == String.Empty)
                {
                    button.Text = turnOf; // Установка знака
                    CheckVictory();
                }
            }
        }
        static public void CheckVictory() // Проверка на выигрыш
        {
            if (CheckLine(0, 0, 0, 1, 0, 2) != "0" || CheckLine(1, 0, 1, 1, 1, 2) != "0" || CheckLine(2, 0, 2, 1, 2, 2) != "0" ||
                CheckLine(0, 0, 1, 0, 2, 0) != "0" || CheckLine(0, 1, 1, 1, 2, 1) != "0" || CheckLine(0, 2, 1, 2, 2, 2) != "0" ||
                CheckLine(0, 0, 1, 1, 2, 2) != "0" || CheckLine(2, 0, 1, 1, 0, 2) != "0")
            {
                if (turnOf == player1.Sign)
                {
                    player1.IncreaseScore();
                    MessageBox.Show(player1.Name + " победил!");
                }
                else
                {
                    player2.IncreaseScore();
                    MessageBox.Show(player2.Name + " победил!");
                }
                RefreshButtons();
            }
            else if (CheckFull())
            {
                MessageBox.Show("Ничья");
                RefreshButtons();
            }
            else
                SwitchTurn();

        }
        static public string CheckLine(int a1, int a2, int b1, int b2, int c1, int c2) // Проверка равнозначности символов в линии
        {
            if (buttons[a1,a2].Text != "" && buttons[a1,a2].Text == buttons[b1,b2].Text && buttons[b1,b2].Text == buttons[c1,c2].Text)
                return buttons[a1,a2].Text;
            else
                return "0";
        }
        static public bool CheckFull()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (buttons[x, y].Text == "")
                        return false;
                }
            }
            return true;
        }
        static public void SwitchTurn()
        {
            if (turnOf == "X")
                turnOf = "O";
            else
                turnOf = "X";
        }
        static public void CreatePlayers() // Запись информации об игроках
        {
            Random rnd = new Random();
            if (rnd.Next(2) == 0)
            {
                player1.Sign = "X";
                player2.Sign = "O";
            }
            else
            {
                player1.Sign = "O";
                player2.Sign = "X";
            }
            player1.Name = "Player 1";
            player2.Name = "Player 2";
        }
        static public void RefreshButtons ()
        {
            player1.SwitchSign();
            player2.SwitchSign();
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    buttons[x, y].Text = "";
                }
            }
            label1.Text = player1.Name;
            label2.Text = "Знак: " + player1.Sign;
            label3.Text = "Счёт: " + player1.Score;
            label4.Text = player2.Name;
            label5.Text = "Знак: " + player2.Sign;
            label6.Text = "Счёт: " + player2.Score;
            turnOf = "X";
        }
    }
}
