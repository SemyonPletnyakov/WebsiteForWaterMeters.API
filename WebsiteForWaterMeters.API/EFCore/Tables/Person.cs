using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteForWaterMeters.API.EFCore.Tables
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Gorod { get; set; }
        public string Ulica { get; set; }
        public string Dom { get; set; }
        public string Kvartira { get; set; }
        public string NameСounter1 { get; set; }
        public double Сounter1 { get; set; }
        public string NameСounter2 { get; set; }
        public double Сounter2 { get; set; }
        public string NameСounter3 { get; set; }
        public double Сounter3 { get; set; }
        public string NameСounter4 { get; set; }
        public double Сounter4 { get; set; }
        public string NameСounter5 { get; set; }
        public double Сounter5 { get; set; }
        public string NameСounter6 { get; set; }
        public double Сounter6 { get; set; }
        public string NameСounter7 { get; set; }
        public double Сounter7 { get; set; }
        public string NameСounter8 { get; set; }
        public double Сounter8 { get; set; }
        public string NameСounter9 { get; set; }
        public double Сounter9 { get; set; }
    }
}
