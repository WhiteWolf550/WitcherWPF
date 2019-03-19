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
            //----------------------------------------PROLOGUE------------------------------------
            //Něco končí, něco začíná
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
            qq.Add(new Quest(1, "Primary", "Záhadná vesnice", "Po cestě do Novigradu, Geralt narazil na Barghesta ve zvláštní vesnici. Geralta vesničani ve vesnici nechtěli, ale dozvěděl se, že ve vesnici byl i jiný zaklínač. Geralt se rozhodl, že se ve vesnici porozhlídne", "Prohledej vesnici a zeptej se lidí na Lamberta", 1, true, "Záhadná vesnice", "Sháním informace", 0, 0));
            qq.Add(new Quest(2, "Primary", "Záhadná vesnice", "Po cestě do Novigradu, Geralt narazil na Barghesta ve zvláštní vesnici. Geralta vesničani ve vesnici nechtěli, ale dozvěděl se, že ve vesnici byl i jiný zaklínač. Geralt se rozhodl, že se ve vesnici porozhlídne. Geralt v krčmě narazil na hospodského, který mu řekl, že Lambert měl rozhovor se starostou.", "Najdi starostu a zeptej se ho na Lamberta", 1, true, "Záhadná vesnice", "Zaklínač", 0, 0));
            qq.Add(new Quest(3, "Primary", "Záhadná vesnice", "Po cestě do Novigradu, Geralt narazil na Barghesta ve zvláštní vesnici. Geralta vesničani ve vesnici nechtěli, ale dozvěděl se, že ve vesnici byl i jiný zaklínač. Geralt se rozhodl, že se ve vesnici porozhlídne. Geralt v krčmě narazil na hospodského, který mu řekl, že Lambert měl rozhovor se starostou. Starosta Geraltovi odmítl pomoci", "Najdi Lamberta", 1, true, "Záhadná vesnice", null, 0, 0));
            qq.Add(new Quest(4, "Primary", "Záhadná vesnice", "Po cestě do Novigradu, Geralt narazil na Barghesta ve zvláštní vesnici. Geralta vesničani ve vesnici nechtěli, ale dozvěděl se, že ve vesnici byl i jiný zaklínač. Geralt se rozhodl, že se ve vesnici porozhlídne. Geralt v krčmě narazil na hospodského, který mu řekl, že Lambert měl rozhovor se starostou. Starosta Geraltovi odmítl pomoci. Starosta už zašel moc daleko a je na čase, aby to někdo ukončil", "Konfrontuj starostu", 1, true, "Záhadná vesnice", "Co si myslíš, že děláš", 0, 0));
            qq.Add(new Quest(5, "Primary", "Záhadná vesnice", "Po cestě do Novigradu, Geralt narazil na Barghesta ve zvláštní vesnici. Geralta vesničani ve vesnici nechtěli, ale dozvěděl se, že ve vesnici byl i jiný zaklínač. Geralt se rozhodl, že se ve vesnici porozhlídne. Geralt v krčmě narazil na hospodského, který mu řekl, že Lambert měl rozhovor se starostou. Starosta Geraltovi odmítl pomoci. Starosta už zašel moc daleko a je na čase, aby to někdo ukončil. Starosta na Geralta poslal stráže.", "Zabij stráž", 1, true, "Záhadná vesnice", null, 0, 0));
            qq.Add(new Quest(6, "Primary", "Záhadná vesnice", "Po cestě do Novigradu, Geralt narazil na Barghesta ve zvláštní vesnici. Geralta vesničani ve vesnici nechtěli, ale dozvěděl se, že ve vesnici byl i jiný zaklínač. Geralt se rozhodl, že se ve vesnici porozhlídne. Geralt v krčmě narazil na hospodského, který mu řekl, že Lambert měl rozhovor se starostou. Starosta Geraltovi odmítl pomoci. Starosta už zašel moc daleko a je na čase, aby to někdo ukončil. Starosta na Geralta poslal stráže.Teď je na čase rozhodnout o starostově osudu...", "Rozhodni o starostově osudu", 1, true, "Záhadná vesnice", null, 0, 0));
            qq.Add(new Quest(7, "Primary", "Záhadná vesnice", "Po cestě do Novigradu, Geralt narazil na Barghesta ve zvláštní vesnici. Geralta vesničani ve vesnici nechtěli, ale dozvěděl se, že ve vesnici byl i jiný zaklínač. Geralt se rozhodl, že se ve vesnici porozhlídne. Geralt v krčmě narazil na hospodského, který mu řekl, že Lambert měl rozhovor se starostou. Starosta Geraltovi odmítl pomoci. Starosta už zašel moc daleko a je na čase, aby to někdo ukončil. Starosta na Geralta poslal stráže. Teď je na čase rozhodnout o starostově osudu... Geralt se od starosty nedozvěděl, kam zavedli Zoltana a proto musí najít jiný způsob, jak to zjistit", "Zeptej se šílence, jestli něco neví", 1, true, "Záhadná vesnice", "Zoltan", 0, 0));
            qq.Add(new Quest(8, "Primary", "Záhadná vesnice", "Po cestě do Novigradu, Geralt narazil na Barghesta ve zvláštní vesnici. Geralta vesničani ve vesnici nechtěli, ale dozvěděl se, že ve vesnici byl i jiný zaklínač. Geralt se rozhodl, že se ve vesnici porozhlídne. Geralt v krčmě narazil na hospodského, který mu řekl, že Lambert měl rozhovor se starostou. Starosta Geraltovi odmítl pomoci. Starosta už zašel moc daleko a je na čase, aby to někdo ukončil. Starosta na Geralta poslal stráže. Teď je na čase rozhodnout o starostově osudu... Geralt se od starosty nedozvěděl, kam zavedli Zoltana a proto musí najít jiný způsob, jak to zjistit. Šílenec Geraltovi řekl, že viděl, jak Zoltana vedli do staré části vesnice", "Najdi Zoltana ve staré části vesnice", 1, true, "Záhadná vesnice", "Jsi v pořádku?", 0, 0));
            qq.Add(new Quest(9, "Primary", "Záhadná vesnice", "Po cestě do Novigradu, Geralt narazil na Barghesta ve zvláštní vesnici. Geralta vesničani ve vesnici nechtěli, ale dozvěděl se, že ve vesnici byl i jiný zaklínač. Geralt se rozhodl, že se ve vesnici porozhlídne. Geralt v krčmě narazil na hospodského, který mu řekl, že Lambert měl rozhovor se starostou. Starosta Geraltovi odmítl pomoci. Starosta už zašel moc daleko a je na čase, aby to někdo ukončil. Starosta na Geralta poslal stráže. Teď je na čase rozhodnout o starostově osudu... Geralt se od starosty nedozvěděl, kam zavedli Zoltana a proto musí najít jiný způsob, jak to zjistit. Šílenec Geraltovi řekl, že viděl, jak Zoltana vedli do staré části vesnice. Geralt Zoltana našel živého a v pořádku, ale společně s ním tam také byla neznámá žena. Rozhodli se jí pomoci", "Odejdi ze staré části vesnice", 1, true, "Záhadná vesnice", "Postaráš se o ní?", 0, 0));
            qq.Add(new Quest(10, "Primary", "Záhadná vesnice", "Po cestě do Novigradu, Geralt narazil na Barghesta ve zvláštní vesnici. Geralta vesničani ve vesnici nechtěli, ale dozvěděl se, že ve vesnici byl i jiný zaklínač. Geralt se rozhodl, že se ve vesnici porozhlídne. Geralt v krčmě narazil na hospodského, který mu řekl, že Lambert měl rozhovor se starostou. Starosta Geraltovi odmítl pomoci. Starosta už zašel moc daleko a je na čase, aby to někdo ukončil. Starosta na Geralta poslal stráže. Teď je na čase rozhodnout o starostově osudu... Geralt se od starosty nedozvěděl, kam zavedli Zoltana a proto musí najít jiný způsob, jak to zjistit. Šílenec Geraltovi řekl, že viděl, jak Zoltana vedli do staré části vesnice. Geralt Zoltana našel živého a v pořádku, ale společně s ním tam také byla neznámá žena. Rozhodli se jí pomoci. Geralt Zoltana poprosil, jestli by se mohl prozatím postarat o neznámou ženu, mezitím co on najde Lamberta.", "Úkol Dokončen", 1, true, "Záhadná vesnice", null, 1000, 0));



            //Na stopě zaklínači
            qq.Add(new Quest(1, "Primary", "Na stopě Zaklínači", "Starosta byl Geraltovým projevem lehce zastrašen a jsem si skoro jist, že to byl i Geraltův plán, ale jelikož mu starosta odmítl pomoci, tak Geralt musí najít jiný způsob jak Lamberta najít", "Zjisti informace o Lambertovi", 1, true, "Na stopě zaklínači", "Lambert", 0, 0));
            qq.Add(new Quest(2, "Primary", "Na stopě Zaklínači", "Starosta byl Geraltovým projevem lehce zastrašen a jsem si skoro jist, že to byl i Geraltův plán, ale jelikož mu starosta odmítl pomoci, tak Geralt musí najít jiný způsob jak Lamberta najít. Zoltan Geraltovi řekl o šílenci ve vesnici, který by mohl mít nějaké informace.", "Najdi vesnického šílence", 1, true, "Na stopě zaklínači", "Informace o Lambertovi", 0, 0));
            qq.Add(new Quest(3, "Primary", "Na stopě Zaklínači", "Starosta byl Geraltovým projevem lehce zastrašen a jsem si skoro jist, že to byl i Geraltův plán, ale jelikož mu starosta odmítl pomoci, tak Geralt musí najít jiný způsob jak Lamberta najít. Zoltan Geraltovi řekl o šílenci ve vesnici, který by mohl mít nějaké informace. Šílenec Geraltovi prozradil, že se Lambert často setkával s mistrem lovčím", "Zeptej se lovčího na Lamberta", 1, true, "Na stopě zaklínači", "Co víš o tom zaklínači", 0, 0));
            qq.Add(new Quest(4, "Primary", "Na stopě Zaklínači", "Starosta byl Geraltovým projevem lehce zastrašen a jsem si skoro jist, že to byl i Geraltův plán, ale jelikož mu starosta odmítl pomoci, tak Geralt musí najít jiný způsob jak Lamberta najít. Zoltan Geraltovi řekl o šílenci ve vesnici, který by mohl mít nějaké informace. Šílenec Geraltovi prozradil, že se Lambert často setkával s mistrem lovčím. Lovčí řekl, že Geraltovi pomůže pokud se postará o ghúli ve vesnici", "Vrať se za lovčím až zabiješ ghúli", 1, true, "Na stopě zaklínači", null, 0, 0));
            qq.Add(new Quest(5, "Primary", "Na stopě Zaklínači", "Starosta byl Geraltovým projevem lehce zastrašen a jsem si skoro jist, že to byl i Geraltův plán, ale jelikož mu starosta odmítl pomoci, tak Geralt musí najít jiný způsob jak Lamberta najít. Zoltan Geraltovi řekl o šílenci ve vesnici, který by mohl mít nějaké informace. Šílenec Geraltovi prozradil, že se Lambert často setkával s mistrem lovčím. Lovčí řekl, že Geraltovi pomůže pokud se postará o ghúli ve vesnici. Geralt nemohl jít za lovčím, protože se objevil problém, který nelze odložit", "Vrať se za lovčím až se postaráš o problém ve vesnici", 1, true, "Na stopě zaklínači", null, 0, 0));
            qq.Add(new Quest(6, "Primary", "Na stopě Zaklínači", "Starosta byl Geraltovým projevem lehce zastrašen a jsem si skoro jist, že to byl i Geraltův plán, ale jelikož mu starosta odmítl pomoci, tak Geralt musí najít jiný způsob jak Lamberta najít. Zoltan Geraltovi řekl o šílenci ve vesnici, který by mohl mít nějaké informace. Šílenec Geraltovi prozradil, že se Lambert často setkával s mistrem lovčím. Lovčí řekl, že Geraltovi pomůže pokud se postará o ghúli ve vesnici. Geralt nemohl jít za lovčím, protože se objevil problém, který nelze odložit. Geralt se konečně mohl vrátit za lovčím a najít Lamberta", "Vrať se za lovčím", 1, true, "Na stopě zaklínači", "Kde je Lambert?", 0, 0));
            qq.Add(new Quest(7, "Primary", "Na stopě Zaklínači", "Starosta byl Geraltovým projevem lehce zastrašen a jsem si skoro jist, že to byl i Geraltův plán, ale jelikož mu starosta odmítl pomoci, tak Geralt musí najít jiný způsob jak Lamberta najít. Zoltan Geraltovi řekl o šílenci ve vesnici, který by mohl mít nějaké informace. Šílenec Geraltovi prozradil, že se Lambert často setkával s mistrem lovčím. Lovčí řekl, že Geraltovi pomůže pokud se postará o ghúli ve vesnici. Geralt nemohl jít za lovčím, protože se objevil problém, který nelze odložit. Geralt se konečně mohl vrátit za lovčím a najít Lamberta. Lovčí Geraltovi pověděl, že Lamberta najde na sever od vesnice.", "Najdi Lamberta v opuštěném domě", 1, true, "Na stopě zaklínači", "Dlouho jsme se neviděli", 0, 0));
            qq.Add(new Quest(8, "Primary", "Na stopě Zaklínači", "Starosta byl Geraltovým projevem lehce zastrašen a jsem si skoro jist, že to byl i Geraltův plán, ale jelikož mu starosta odmítl pomoci, tak Geralt musí najít jiný způsob jak Lamberta najít. Zoltan Geraltovi řekl o šílenci ve vesnici, který by mohl mít nějaké informace. Šílenec Geraltovi prozradil, že se Lambert často setkával s mistrem lovčím. Lovčí řekl, že Geraltovi pomůže pokud se postará o ghúli ve vesnici. Geralt nemohl jít za lovčím, protože se objevil problém, který nelze odložit. Geralt se konečně mohl vrátit za lovčím a najít Lamberta. Lovčí Geraltovi pověděl, že Lamberta najde na sever od vesnice. Geralt Lamberta našel a Lambert mu sdělil, že je pronásledován zvláštní skupinkou hrlořezů, kteří se schovávají v Novigradu", "Úkol dokončen", 1, true, "Na stopě zaklínači", "Dlouho jsme se neviděli", 1000, 0));
            //Cesta do Novigradu
            qq.Add(new Quest(1, "Primary", "Cesta do Novigradu", "Geralt našel Lamberta a mohl se vydat do Novigradu, ale ještě předtím musí zajít za Zoltanem", "Vrať se za Zoltanem", 1, true, "Cesta do Novigradu", "Jak jí je?", 0, 0));
            qq.Add(new Quest(2, "Primary", "Cesta do Novigradu", "Geralt našel Lamberta a mohl se vydat do Novigradu, ale ještě předtím musí zajít za Zoltanem", "Zkontroluj neznámou ženu", 1, true, "Cesta do Novigradu", "Jak ti je?", 0, 0));
            qq.Add(new Quest(3, "Primary", "Cesta do Novigradu", "Geralt našel Lamberta a mohl se vydat do Novigradu, ale ještě předtím musí zajít za Zoltanem", "Úkol dokončen", 1, true, "Cesta do Novigradu", null, 500, 0));
            //Problém s Ghúly
            qq.Add(new Quest(1, "Primary", "Problém s ghúly", "Lovčí pozádal Geralte ať se postará o ghúli ve vesnici", "Zabij 3 ghúly ve vesnici", 1, true, "Problém s ghúly", null, 0, 0));
            qq.Add(new Quest(2, "Primary", "Problém s ghúly", "Lovčí pozádal Geralte ať se postará o ghúli ve vesnici", "Zabij 2 ghúly ve vesnici", 1, true, "Problém s ghúly", null, 0, 0));
            qq.Add(new Quest(3, "Primary", "Problém s ghúly", "Lovčí pozádal Geralte ať se postará o ghúli ve vesnici", "Zabij posledního ghúla", 1, true, "Problém s ghúly", null, 0, 0));
            qq.Add(new Quest(4, "Primary", "Problém s ghúly", "Lovčí pozádal Geralte ať se postará o ghúli ve vesnici", "Úkol dokončen", 1, true, "Problém s ghúly", "Co se děje?", 1000, 0));
            manager.SaveQuests(qq);
        }



    }
}
