using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Bestiary {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Weakness { get; set; }
        public string Strength { get; set; }
        public string Source { get; set; }
        public bool Enabled { get; set; }

        FileManager manager = new FileManager();
        public Bestiary() {

        }
        public Bestiary(string Name, string Description, string Weakness, string Strength, string Source, bool Enabled) {
            this.Name = Name;
            this.Description = Description;
            this.Weakness = Weakness;
            this.Strength = Strength;
            this.Source = Source;
            this.Enabled = Enabled;
        }
        public void CreateBestiary() {
            List<Bestiary> bestiary = new List<Bestiary>();

            bestiary.Add(new Bestiary("Ghůl", "Tvrdí se, že ghúlové byli kdysi lidmi, kteří byli přinuceni jíst mršiny a po letech strávených v temných kryptách podlehli monstrózní změně. Věčný hlad ghúlů dokáže utišit pouze lidské maso, proto stvoření zabíjejí lidi a nesnědené pozůstatky nechávají ve výklencích svých krypt..", "Citlivý na stříbro", "Silný protivník, který je silnější pokud má hráč málo vitality", "img/Monsters/Ghoul.png", true));
            bestiary.Add(new Bestiary("Barghest", "Barghesti jsou prý zjevení, která mají podobu přízračných psů a pronásledují živé. Podle některých podání jsou tyto stvůry zvědové Divokého honu. Jiné legendy tvrdí, že se přeludy objeví jako boží trest a představují vtělení pomsty. Všechna vyprávění se ale shodují na jednom - barghesti jsou vůči živým naprosto nemilosrdní", "Citlivý na stříbro", "Rychlý protivník, který má vysokou šanci na úhyb", "img/Monsters/Barghest.png", false));
            bestiary.Add(new Bestiary("Utopenec", "Utopenci jsou zločinci, kteří svůj život skončili ve vodě. Utopení či po smrti hození do vody, mění se v mstivá stvoření, číhající na obyvatele pobřežních osad..", "Citlivý na stříbro", "Rychlý protivník, který má vysokou šanci na úhyb", "img/Monsters/Drowner.jpg", false));
            manager.SaveBestiary(bestiary);
        }
    }
}
