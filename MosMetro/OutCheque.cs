using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosMetro
{
    internal class OutCheque
    {
        public static void Outing(int id, string product, int cost)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string text = "\t\t Метро\n" +
                $"\tКассовый чек№{id}\n" +
                $"{product} - {cost}\n" +
                $"Итого к оплате - {cost}\n" +
                $"Сдача - 0";
            string FileName = $"чек номер №{id}";
            File.WriteAllText(desktop + "//" + FileName +".txt", text); ;
            Process.Start(desktop + "//" + FileName + ".txt");
        }
    }
}
