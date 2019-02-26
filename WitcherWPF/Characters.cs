using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherWPF {
    class Characters {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }

        FileManager manager = new FileManager();
        public Characters() {

        }
        public Characters(string Name, string Description, string Source) {
            this.Name = Name;
            this.Description = Description;
            this.Source = Source;
        }
        public void CreateCharacters() {
            List<Characters> characters = new List<Characters>();
            characters.Add(new Characters("Geralt z Rivie", "Geralt z Rivie, dryádami ve Starší mluvě také zvaný Gwynnbleid – Bílý vlk nebo mezi elfy Vatt'ghern, je bělovlasý zaklínač.Navzdory svému jménu Geralt ve skutečnosti nepochází z Rivie. Mladé zaklínače však mistr Vesemir vedl k tomu, aby si vybrali nějaké příjmení, protože jejich jméno pak vypadalo důvěryhodněji. Geralt si nejprve vybral jméno Geralt Roger Eric du Haute-Bellegarde, ale Vesemir mu ho zamítl s tím, že je hloupé a zní domýšlivě.", "gifs/Characters/Geralt.gif"));
            characters.Add(new Characters("Triss Ranuncul", "Triss Ranuncul je má přítelkyně. Viděla, jak umírám, a můj návrat do světa živých ji tedy velmi zaskočil. Je to čarodějka - jedna z nejtalentovanějších a nejvlivnějších. Má mnoho známých, zná také zaklínače z Kaer Morhen a je jednou z mála osob, které znají cestu k naší tvrzi. Mám pocit, že mě má Triss hodně ráda.Společně se mnou a zbývajícími zaklínači bránila Triss Kaer Morhen.Čarodějka se pustila do boje s tajemným mágem, jedním z velitelů útoku, a nakonec byla zraněna a ztratila vědomí. Ačkoli to zní paradoxně, je Triss alergická na magii a je nutné ji léčit pouze přirozenými elixíry.Po Leově pohřbu se čarodějka teleportovala do Wyzimy.Rozhodla se využít své rozsáhlé kontakty a získat informace o Salamandře. Triss slíbila, že mě vyhledá, jakmile se dostanu do Wyzimy.Triss mě vyhledala na blatech, kde jsem ležel v bezvědomí po střetnutí s Azarem Javedem.Čarodějka mě přenesla do svého domu v obchodní Wyzimě a starala se o mě, dokud jsem nenabyl vědomí. Po probuzení jsem vyslechl, jak Triss intrikuje s přítelkyněmi přes magický komunikátor, a už vím, že se záležitostí Salamandry zabývají i další čarodějky.", "gifs/Characters/Triss.gif"));

            manager.SaveCharacters(characters);
        }
    }
}
