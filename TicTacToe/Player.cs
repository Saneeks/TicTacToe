using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public string Sign { get; set; }
        public void Create(string name, string sign) // Запись или обновление данных об игроке
        {
            Name = name;
            Sign = sign;
            Score = 0;
        }
        public void IncreaseScore() { Score++; } // Увеличение счёта игрока
        public void SwitchSign() { if (Sign == "X") Sign = "O"; else Sign = "X"; } // Смена знака
    }
}
