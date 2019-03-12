using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WitcherWPF {
    class Quest {
        public int QuestID { get; set; }
        public string QuestType { get; set; }
        public string QuestName { get; set; }
        public string QuestDescription { get; set; }
        public string QuestGoal { get; set; }
        public int QuestLevel { get; set; }
        public bool QuestActive { get; set; }
        public string QuestSeries { get; set; }
        public string DialogueActivate { get; set; }
        public int experience { get; set; }
        public int reward { get; set; }

        FileManager manager = new FileManager();
        public Quest() {

        }
        public Quest(int QuestID, string QuestType, string QuestName, string QuestDescription, string QuestGoal, int QuestLevel, bool QuestActive, string QuestSeries, string DialogueActivate, int experience, int reward) {
            this.QuestID = QuestID;
            this.QuestType = QuestType;
            this.QuestName = QuestName;
            this.QuestDescription = QuestDescription;
            this.QuestGoal = QuestGoal;
            this.QuestLevel = QuestLevel;
            this.QuestActive = QuestActive;
            this.QuestSeries = QuestSeries;
            this.DialogueActivate = DialogueActivate;
            this.experience = experience;
            this.reward = reward;

        }
        public void CreateQuests() {
            List<Quest> qq = new List<Quest>();
            qq.Add(new Quest(1, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krála zachránil.", "Zajdi za Foltestem", 1, true, "Něco končí, něco začíná", null, 0, 0));
            qq.Add(new Quest(2, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss", "Zajdi za Triss a zjisti něco o vrahovi", 1, true, "Něco končí, něco začíná", "Zjistila jsi něco nového o tom vrahovi?", 0, 0));
            qq.Add(new Quest(3, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss. Triss Geraltovi sdělila, že na to, aby zjistila, kdo byl vrah, tak potřebuje více času", "Počkej až se Triss dozví více o vrahovi", 1, true, "Něco končí, něco začíná", null, 0, 0));
            qq.Add(new Quest(4, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss. Triss Geraltovi sdělila, že na to, aby zjistila, kdo byl vrah, tak potřebuje více času", "Vydej se za Triss a zjisti jestli už něco ví", 1, true, "Něco končí, něco začíná", "Získala jsi už nějaké informace o vrahovi?", 0, 0));
            qq.Add(new Quest(5, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss. Triss Geraltovi sdělila, že na to, aby zjistila, kdo byl vrah, tak potřebuje více času. Triss Geraltovi sdělila, že vrah před smrtí mluvil s Lambertem přes megaskop(telekomunikační zařízení). Tím pádem se Geralt musí vydat na cestu, aby Lamberta našel", "Zajdi za Foltestem a řekni mu, že vyrážíš na cestu", 1, true, "Něco končí, něco začíná", "Mám informace o vrahovi", 0, 0));
            qq.Add(new Quest(6, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss. Triss Geraltovi sdělila, že na to, aby zjistila, kdo byl vrah, tak potřebuje více času. Triss Geraltovi sdělila, že vrah před smrtí mluvil s Lambertem přes megaskop(telekomunikační zařízení). Tím pádem se Geralt musí vydat na cestu, aby Lamberta našel", "Pokud chceš vyrazit na cestu, tak zajdi za Triss", 1, true, "Něco končí, něco začíná", "Vyrazit na cestu", 0, 0));
            qq.Add(new Quest(7, "Primary", "Něco končí, něco začíná", "Foltest si předvolal Geralta hned druhý den potom co krále zachránil. Foltest Geraltovi oznámil, že by měl něco zjistit o vrahovi s pomocí Triss. Triss Geraltovi sdělila, že na to, aby zjistila, kdo byl vrah, tak potřebuje více času. Triss Geraltovi sdělila, že vrah před smrtí mluvil s Lambertem přes megaskop(telekomunikační zařízení). Tím pádem se Geralt musí vydat na cestu, aby Lamberta našel", "Úkol Dokončen", 1, true, "Něco končí, něco začíná", null, 500, 0));
            qq.Add(new Quest(1, "Primary", "Kovářova zrůda", "Triss řekla Geraltovi, aby pomohl svému starému známému Yavenovi Briggsovi", "Zajdi za kovářem do staré wyzimy a zjisti jaký má problém", 1, true, "Kovářova zrůda", "Problém s příšerou", 0, 0));
            qq.Add(new Quest(2, "Primary", "Kovářova zrůda", "Geralt se za kovářem vydal a kovář mu slibíl, že dostane 50 orénů za to, že nestvůru zabije.", "Jdi do sklepa zabít nestvůru", 1, true, "Kovářova zrůda", null, 0, 0));
            qq.Add(new Quest(3, "Primary", "Kovářova zrůda", "Triss řekla Geraltovi, aby pomohl svému starému známému Yavenovi Briggsovi. Geralt se za kovářem vydal a kovář mu slibíl, že dostane 50 orénů za to, že nestvůru zabije. Geralt po drsném souboji Ghůla zabil a může si dojít pro odměnu", "Vyzvedni si u Yavena odměnu za zabití Ghůla", 1, true, "Kovářova zrůda", "Odměna za Ghůla", 0, 0));
            qq.Add(new Quest(4, "Primary", "Kovářova zrůda", "Triss řekla Geraltovi, aby pomohl svému starému známému Yavenovi Briggsovi. Geralt se za kovářem vydal a kovář mu slibíl, že dostane 50 orénů za to, že nestvůru zabije. Geralt po drsném souboji Ghůla zabil a může si dojít pro odměnu", "Úkol dokončen", 1, true, "Kovářova zrůda", "Odměna za Ghůla", 500, 50));

            //STRAŠIDELNÝ DŮM
            qq.Add(new Quest(1, "Secondary", "Strašidelný dům", "Jeden z převižší ve staré wyzimě požádal Geralta o pomoc. Potřeboval získat z domu jeho prsten", "Jdi do přeživšího domu a najdi prsten", 1, true, "Strašidelný dům", null, 0, 0));
            qq.Add(new Quest(2, "Secondary", "Strašidelný dům", "Jeden z převižší ve staré wyzimě požádal Geralta o pomoc. Potřeboval získat z domu jeho prsten. Geralt prsten získal a může se vrátit za přeživším", "Zabij ghůla", 1, true, "Strašidelný dům", null, 0, 0));
            qq.Add(new Quest(3, "Secondary", "Strašidelný dům", "Jeden z převižší ve staré wyzimě požádal Geralta o pomoc. Potřeboval získat z domu jeho prsten. Geralt prsten získal a může se vrátit za přeživším. Když už byl Geralt v domě, tak se také postaral o příšeru", "Vrať se k přeživšímu pro odměnu", 1, true, "Strašidelný dům", "Našel jsem prsten", 0, 0));
            qq.Add(new Quest(4, "Secondary", "Strašidelný dům", "Jeden z převižší ve staré wyzimě požádal Geralta o pomoc. Potřeboval získat z domu jeho prsten. Geralt prsten získal a může se vrátit za přeživším. Když už byl Geralt v domě, tak se také postaral o příšeru. Geralt si vyzvednul odměnu od přeživšího", "Úkol Dokončen", 1, true, "Strašidelný dům", null, 200, 60));


            //----------------------------------------CHAPTER I------------------------------------
            //Záhadná Vesnice
            qq.Add(new Quest(1, "Primary", "Záhadná vesnice", "Po cestě do Novigradu, Geralt narazil na Barghesta ve zvláštní vesnici. Geralta vesničani ve vesnici nechtěli, ale dozvěděl se, že ve vesnici byl i jiný zaklínač. Geralt se rozhodl, že se ve vesnici porozhlídne", "Prohledej vesnici a zeptej se lidí na Lamberta", 1, true, "Záhadná vesnice", null, 0, 0));

            manager.SaveQuests(qq);
        }
        


    }
}
