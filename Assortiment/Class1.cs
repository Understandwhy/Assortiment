using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assortiment
{
    public class Polzovatel
    {
        public static string role { get; set; }
    }
    partial class USER
    {
        public override string ToString()
        {
            return Id_user + "/" + name_user + " " + surname_user + "/" + role;
        }
    }
    partial class ASSORTIMENT
    {
        public override string ToString()
        {
            return "Название:" + name_assorti + "\n" + " Описание :" + opisaniye_assorti
                + "\n" + " Цена :" + price;
        }
    }
    partial class USER_HISTIRY
    {
        public override string ToString()
        {
            return Id_histiry + "." + time_VHOD + ":" + time_VIHOD;
        }
    }
    partial class CATEGORY
    {
        public override string ToString()
        {
            return name_categ;
        }
    }
    partial class OTCHETI
    {
        public override string ToString()
        {
            return "Номер отчета :" + Id_otchet + "/" + "Остаток на складе: :"
            + ostatok_na_saite + "/" + "Закупить: " + zakupit;
        }
    }
}