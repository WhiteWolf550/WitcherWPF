using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
namespace WitcherWPF
{
    class Dialogues
    {
        public string CharName { get; set; }
        public string CharSay { get; set; }
        public int DialogueID { get; set; }
        public string Choice { get; set; }
        public string Type { get; set; }
        public string Dialogue { get; set; }
        public bool Enabled { get; set; }
        public string QuestActivate { get; set; }
        public string Decision { get; set; }
        public bool IsChoice { get; set; }

        public Dialogues() {

        }
        public Dialogues(string charName, string charSay, int dialogueID, string choice, string type, string dialogue, bool enabled, string QuestActivate, string Decision, bool IsChoice) {
            this.CharName = charName;
            this.CharSay = charSay;
            this.DialogueID = dialogueID;
            this.Choice = choice;
            this.Type = type;
            this.Dialogue = dialogue;
            this.Enabled = enabled;
            this.QuestActivate = QuestActivate;
            this.Decision = Decision;
            this.IsChoice = IsChoice;
        }
        private FileManager manager = new FileManager();
        
        
        //---------------------DIALOGUE START---------------------------
        public async void DialogueGreet(Label Name, TextBlock Text, string Character) {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string prolog = Globals.DialoguePath;
            string jsonFromFileinv = File.ReadAllText(prolog);
            List<Dialogues> dialog = JsonConvert.DeserializeObject<List<Dialogues>>(jsonFromFileinv, settings);
            var matches = dialog.Where(s => s.Dialogue == Character);
            var matches2 = matches.Where(s => s.Type == "Greet");
            
            foreach (var item in matches2) {
                Name.Content = item.CharName;
                Text.Text = item.CharSay;
                int leng = item.CharSay.Length;
                await Task.Delay(20000);
                
   
            }
        }
        //---------------------DIALOGUE EXIT---------------------------
        public async void DialogueLeave(Label Name, TextBlock Text, Frame parentFrame, string Character, Time time) {
            JsonSerializerSettings settings = new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.All
            };
            string prolog = Globals.DialoguePath;
            string jsonFromFileinv = File.ReadAllText(prolog);
            List<Dialogues> dialog = JsonConvert.DeserializeObject<List<Dialogues>>(jsonFromFileinv, settings);
            var matches = dialog.Where(s => s.Dialogue == Character);
            var matches2 = matches.Where(s => s.Choice == "Nashle");

            foreach (var item in matches2) {
                Name.Content = item.CharName;
                Text.Text = item.CharSay;
                int leng = item.CharSay.Length;
                await Task.Delay(2000);


            }
            parentFrame.Navigate(new Location(parentFrame, time));

        }
        
        //--
        //---------------------MAIN DIALOGUE FUNCTION (LOADS DIALOGUE JSON AND WRITES TO USER) ACTIVATES QUESTS BASED ON DIALOGUE---------------------------
        public async void MainDialogue(Label Name, TextBlock Text, Button button, Label QueName, TextBlock QueGoal, StackPanel Pop, StackPanel DialogueOptions, string Character, Frame parentFrame, Time time) {
            Text.Text = "";
            //---------------------VARIABLES---------------------------
            Brush c;
            string qname = "";
            string qdesc = "";
            string diadis = "";
            string Activate = "";
            string Activate2 = "";
            PlayerQuest que = new PlayerQuest();
            
            //---------------------PATHS---------------------------
            
            string prolog = Globals.DialoguePath;
            
            //---------------------FILE LOADING---------------------------
            List<Quest> Quests = manager.LoadQuests();
            List<PlayerQuest> PlayerQuests = manager.LoadPlayerQuests();
            List<Dialogues> dialog = manager.LoadDialogue(prolog);
            //---------------------FILTERS---------------------------
            var matches = dialog.Where(s => s.Dialogue == Character);
            var matches2 = matches.Where(s => s.Type == "Talk");
            var matches3 = matches2.Where(s => s.Enabled == true);
            var matches4 = matches2.Where(s => s.Choice == button.Content.ToString());
            //---------------------WRITING DIALOGUE LINES TO USER---------------------------
            int cd = matches4.Count();
            int ic = 0;
            int id2 = 0;
            foreach (var item in matches4) {
                Text.Text = "";
                string StringToDisplay = item.CharSay;
                char[] CharactersofString = StringToDisplay.ToCharArray();
                Name.Content = item.CharName;
                foreach (char f in CharactersofString) {
                    Text.Text += f;
                    await Task.Delay(40);
                }
                await Task.Delay(1000);
                
                
                diadis = item.Choice;
                
                if (ic == cd-2) {
                    Activate2 = item.QuestActivate;
                    
                    
                }
                Activate = item.QuestActivate;
                ic++;
            }
            //---------------------IF DIALOGUE HAS QUEST ACTIVATION---------------------------
            if (Activate != null) {
                
                var match = PlayerQuests.Where(s => s.Quest.QuestSeries == Activate);
                if (match.Count() == 0) {
                    
                    c = Brushes.Yellow;
                    var match2 = Quests.Where(s => s.QuestSeries == Activate);
                    
                    var match3 = match2.Where(s => s.QuestID == 1);
                    
                    foreach(var pitem in match3) {
                        PlayerQuests.Add(new PlayerQuest(pitem));
                        qname = pitem.QuestName;
                        qdesc = pitem.QuestGoal;
                        if(pitem.DialogueActivate != null) {
                            var mat = dialog.Where(s => s.Choice == pitem.DialogueActivate);
                            foreach (var di in mat) {
                                di.Enabled = true;
                            }
                        }
                    }
                    
                    
                } else {
                   
                    c = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB8A917"));
                    int id = 0;
                    
                    foreach (var item in match) {
                        id = item.Quest.QuestID;
                    }
                    var matches5 = Quests.Where(s => s.QuestSeries == Activate);
                    var matches6 = matches5.Where(s => s.QuestID == id + 1);
                    foreach (var it in match) {
                        foreach (var it2 in matches6) {
                            it.Quest.QuestID++;
                            it.Quest.QuestDescription = it2.QuestDescription;
                            it.Quest.QuestGoal = it2.QuestGoal;
                            it.Quest.DialogueActivate = it2.DialogueActivate;
                            qname = it2.QuestName;
                            qdesc = it2.QuestGoal;
                            if (it2.DialogueActivate != null) {
                                var mat = dialog.Where(s => s.Choice == it2.DialogueActivate);
                                foreach(var di in mat) {
                                    di.Enabled = true;
                                }
                            }
                        }
                    }
                    
                }
                if (Activate2 != Activate) {
                    var matchactiv2 = Quests.Where(s => s.QuestSeries == Activate2);
                    var matchactiv3 = PlayerQuests.Where(s => s.Quest.QuestSeries == Activate2);
                    foreach (var item in matchactiv3) {
                        id2 = item.Quest.QuestID;
                    }
                    var matchactiv4 = matchactiv2.Where(s => s.QuestID == id2 + 1);
                    foreach (var it in matchactiv3) {
                        foreach (var it2 in matchactiv4) {
                            it.Quest.QuestID++;
                            it.Quest.QuestDescription = it2.QuestDescription;
                            it.Quest.QuestGoal = it2.QuestGoal;
                            it.Quest.DialogueActivate = it2.DialogueActivate;
                        }
                    }

                }

                var diamatch = dialog.Where(s => s.Choice == diadis);
                foreach(Dialogues dit in diamatch) {
                    dit.Enabled = false;
                }
                QueName.Content = qname;
                QueGoal.Text = qdesc;

                que.QuestShow(Pop);
                await Task.Delay(10000);
                que.QuestHide(Pop);
                que.QuestSave(PlayerQuests);

                //saving

                manager.SavePlayerQuests(PlayerQuests);
                manager.SaveDialogues(dialog, prolog);
                //ScriptedEvents(diadis, parentFrame);

            }
            DialogueOptions.Visibility = Visibility.Visible;
            







        }
        public void ScriptedEvents(string DialogueChoice, Frame parentFrame, Time time) {
            if (DialogueChoice == "Problém s příšerou") {
                //parentFrame.Navigate(new Combat(parentFrame, time));
            }
        }
        public void CreateDialogues() {
            List<Dialogues> dialog = new List<Dialogues>();
            List<Dialogues> dialog1 = new List<Dialogues>();
            string path = @"../../dialogues/DialoguePrologue.json";
            string path1 = @"../../dialogues/DialogueChapter1.json";
            string path2 = @"../../dialogues/DialogueChapter2.json";
            //----------------------FOLTEST----------------
            //greet
            dialog.Add(new Dialogues("Foltest", "Vítej zpět Zaklínači", 1, "Pozdrav", "Greet", "Foltest", true, null, null, false));

            //1
            dialog.Add(new Dialogues("Foltest", "Geralte, zjistil si už něco o tom vrahovi?", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Ne králi, zatím ne", 1, "Co potřebujete králi?", "Talk", "Foltest", true, null, null, false));
            dialog.Add(new Dialogues("Foltest", "Tak to by sis měl pospíšit Geralte. Přeci jen jde o tvojí reputaci", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "To že se jeden Zaklínač pokusí o zabití krále, ihned neznamená, že takový jsou všichni zaklínači", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Takhle to ale u prostého lidu nefunguje. Lidem stačí, aby se jeden Zaklínač pokusil o vraždu a budou nenávidět všechny", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Potřebuji mít tuto záležitost co nejrychleji za sebou Zaklínači.", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Dobře králi", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));

            //2
            dialog.Add(new Dialogues("Geralt", "Jak se daří Wyzimě?", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true, null, null, false));
            dialog.Add(new Dialogues("Foltest", "To tě to opravdu zajímá Zaklínači?", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true, null, null, false));
            dialog.Add(new Dialogues("Geralt", "Ne, nezajímá", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true, null, null, false));
            dialog.Add(new Dialogues("Foltest", "Wyzima je pořád v torskách. Budu se ale snažit ji vrátit zpět do své krásy", 2, "Jak se daří Wyzimě?", "Talk", "Foltest", true, null, null, false));

            //3
            dialog.Add(new Dialogues("Geralt", "Mám informace o vrahovi?", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Skvělé už víš odkud pocházel a co byl zač?", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Vím jen to, že se poslední dobou nacházel poblíž Novigradu", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "A přesně tam se vydám", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Proč tak náhlá změna názoru?", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Ještě před chvílí jsi nebyl, tak odhodlán", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Vrah by mohl mít něco společného se Salamandrou a to chci zjistit", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Dobře. Budeš potřebovat pomoct?", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Ne", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Tohle zvládnu sám", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Dobře. Ať je po tvém", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Pokud se ti o vrahovi podaří zjistit více, tak mi to přijeď sdělit. Budu tady ve Wyzimě", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Samozřejmě tu na tebe bude čekat odměna pokud se ti to podaří vyřešit", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Samozřejmě králi", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Hodně štěstí Zaklínači", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));

            //leave
            dialog.Add(new Dialogues("Geralt", "Nashle králi", 2, "Nashle", "Talk", "Foltest", true, null, null, false));
            dialog.Add(new Dialogues("Foltest", "Nashle Zaklínači", 2, "Nashle", "Talk", "Foltest", true, null, null, false));
            //----------------------TRISS----------------
            //greet
            dialog.Add(new Dialogues("Triss", "Ano Geralte?", 1, "Pozdrav", "Greet", "Triss", true, null, null, false));

            //1
            dialog.Add(new Dialogues("Geralt", "Zjistila jsi něco nového o tom vrahovi?", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Triss", "Všechno co vím je že to byl zaklínač, abych zjistila něco více budu potřebovat více času", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Já už tu ale déle být nechci Triss. Mám už plné zuby politiky a intriků. Triss pojď semnou, vypadneme odsud", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Triss", "Geralte, však víš, že já nemůžu jen tak odejít dokud mě Foltest nepustí.", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Ty ani já nic Foltestovi nedlužíme. Jakmile zjistíš něco o tom vrahovi, tak odsud vypadneme a nikdy se nevrátíme", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Triss", "Až něco zjistím, tak uvidíme. Mezitím by jsi mohl pomoct místnímu kováři Yavenovi Briggsovi. Toho znáš ne?", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Znám ho potkali jsme se v lese poblíž Brokilonu. To už je tak dlouho, jako kdyby to bylo v minulém životě. Pomůžu mu", 1, "Zjistila jsi něco nového o tom vrahovi?", "Talk", "Triss", false, "Kovářova zrůda", null, false));

            //2
            dialog.Add(new Dialogues("Geralt", "Získala jsi už nějaké informace o vrahovi?", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Ano", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Ale to co ti teď řeknu se ti nebude líbit", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Co se děje?", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Z jeho vzpomínek jsem zjistila, že pár hodin před útokem s někým komunikoval.", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Nevím o čem, protože jeho vzpomínky velice rychle mizí", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Triss..", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "S kým mluvil?", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Mluvil s Lambertem", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "S Lambertem?!", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "To není možné", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Lambert by se nikdy nezapletl s vrahy a jinou bídou. Natož aby s nimi spolupracoval", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Já vím Geralte, také mi to nedává smysl, ale určitě mluvil s Lambertem. To jsem si jistá", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Celá tahle situace se mi vůbec nelíbí, už jen kvůli tomu, že jsem do toho zapletený já, Lambert a Foltest", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Geralte nerada to říkám, ale asi budeš muset pomoct Foltestovi", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Ano já vím.. Ale rozhodně to nebudu dělat kvůli Foltestovi. Kdyby do toho nebyl zapletený Lambert, tak už tu dávno nejsem", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Víš alespoň, kde se Lambert nachází?", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Vrahovi vzpomínky jsou už hodně slabé, takže si nemůžu být jistá..", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Vím jen, že se nacházel poblíž Novigradu. Je možné že i v Novigradu", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "To mi bude muset pro začátek stačit", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Vydám se tedy na cestu", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Bylo by dobré říct Foltestovi, že vyrážíš a alespoň ho seznámit s tím co víme", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "To nech na mě. Řeknu mu jen to důležité", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Triss", "Něž odejdeš přijď se semnou rozloučit", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Samozřejmě", 1, "Získala jsi už nějaké informace o vrahovi?", "Talk", "Triss", false, "Něco končí, něco začíná", null, false));

            //3
            dialog.Add(new Dialogues("Triss", "Opravdu jsi připravený Geralte?", 1, "Vyrazit na cestu", "Talk", "Triss", false, null, null, false));
            dialog.Add(new Dialogues("Triss", "Pokud tu máš něco rozdělaného, tak pochybuji, že to tu zůstane, když se vrátíš", 1, "Vyrazit na cestu", "Talk", "Triss", false, null, null, false));
            dialog.Add(new Dialogues("Geralt", "Ano, jsem připravený", 1, "Vyrazit na cestu", "Talk", "Triss", false, "Něco končí, něco začíná", "Vyrazit", true));
            dialog.Add(new Dialogues("Triss", "Dobře, jdeme", 1, "Vyrazit na cestu", "Talk", "Triss", false, "Něco končí, něco začíná", "Vyrazit", false));
            dialog.Add(new Dialogues("Geralt", "Ne, ještě něco vyřídím", 1, "Vyrazit na cestu", "Talk", "Triss", false, null, "Zůstat", true));
            dialog.Add(new Dialogues("Triss", "Až budeš připravený, tak přijď", 1, "Vyrazit na cestu", "Talk", "Triss", false, null, "Zůstat", false));

            //4
            dialog.Add(new Dialogues("Geralt", "Děkuji ti za pomoc s těmi elfy", 2, "Děkuji za pomoc", "Talk", "Triss", true, null, null, false));
            dialog.Add(new Dialogues("Triss", "Máš štěstí, že jsem zrovna procházela kolem", 2, "Děkuji za pomoc", "Talk", "Triss", true, null, null, false));
            dialog.Add(new Dialogues("Triss", "Foltest mě pověřil ať dávám pozor na to co se ve Wyzimě děje", 2, "Děkuji za pomoc", "Talk", "Triss", true, null, null, false));
            dialog.Add(new Dialogues("Geralt", "Wyzima není bezpečná. Musím co nejdříve odejít", 2, "Děkuji za pomoc", "Talk", "Triss", true, null, null, false));
            dialog.Add(new Dialogues("Geralt", "Marigolda jsem už poslal pryč", 2, "Děkuji za pomoc", "Talk", "Triss", true, null, null, false));
            dialog.Add(new Dialogues("Triss", "To jsi udělal dobře. Marigold by nám tu moc nepomohl", 2, "Děkuji za pomoc", "Talk", "Triss", true, null, null, false));
            //leave
            dialog.Add(new Dialogues("Geralt", "Měj se Triss", 2, "Nashle", "Talk", "Triss", true, null, null, false));
            dialog.Add(new Dialogues("Triss", "Dávej na sebe pozor Geralte", 2, "Nashle", "Talk", "Triss", true, null, null, false));

            //----------------------YAVEN(BLACKSMITH)----------------
            //greet
            dialog.Add(new Dialogues("Yaven", "Vítaj Vědmáku", 1, "Pozdrav", "Greet", "Yaven", true, null, null, false));

            //1
            dialog.Add(new Dialogues("Geralt", "Slyšel jsem, že máš problém s příšerou", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "Geralt? Tož si to ty? To jest překvapení teba som tak dlho neviděl! Kde ty sa tu bereš.", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "To je na delší povídání a na to teď není čas. Mám tu práci", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "Jasně jasně, Tak co o tej krásce chceš vědět? Velká hnusná hlava, několik ostrejch zubisek a tož pařáty má taky skvostný", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Takže by to mohl být Ghůl", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "Geralt, co já sakra vím. Jsem snad jakejsi vědmák či co? To by si mal vědět ty ne?", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Ano já vím, to jsem si říkal jen pro sebe.", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "80", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "80? čeho sakra, zas tak starej nejsem", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "80 orénů", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "Ach tak, no víš Geralte tož já nejsem zrovna při penězích a po tom všem co se tu ve Wyzimě přihodilo. To taky moc nepomáhá. 50 by šlo?", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Dobře, souhlasím. Tak, kde že je ta tvoje 'stvůra'? ", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Tož tadydhlenc ve sklepě, když dáš pryč ty bedny a to harampádí", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "Dobře, ale peníze měj připravené, protože přežiju. Zachvíli jsem zpět. Pokud by jsem se však po 5 minutách nevrátil, tak ty bedny dej radši zpátky", 1, "Problém s příšerou", "Talk", "Yaven", false, "Kovářova zrůda", null, false));

            //2
            dialog.Add(new Dialogues("Geralt", "Ghůl je mrtvý.", 2, "Odměna za Ghůla", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Geralt", "*Hází hlavu Ghůla na zem*", 2, "Odměna za Ghůla", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "U svaté melitelé tož dej tu zrůdnost pryč, však já ti věřím", 2, "Odměna za Ghůla", "Talk", "Yaven", false, "Kovářova zrůda", null, false));
            dialog.Add(new Dialogues("Yaven", "Díky ti za pomoc Geralt. Tož konečně možu jít zase do sklepa pro posledné věci, Kdybys něco potřeboval, tak přijď ještě mi tu nějaké věci zůstali", 2, "Odměna za Ghůla", "Talk", "Yaven", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Yaven", "Ach a tadyhlenc je těch tvejch 50 orénů", 2, "Odměna za Ghůla", "Talk", "Yaven", false, "Kovářova zrůda", null, false));

            //3
            dialog.Add(new Dialogues("Geralt", "Prodáváš tu stále ještě něco?", 3, "Obchod", "Talk", "Yaven", true, null, null, false));
            dialog.Add(new Dialogues("Yaven", "No tož Geralt... Ono není komu prodávat hehe.", 3, "Obchod", "Talk", "Yaven", true, null, null, false));
            dialog.Add(new Dialogues("Yaven", "Tož občas tedy někdo přivalcuje, ale to spíš chtějí jen mít kde přespat.", 3, "Obchod", "Talk", "Yaven", true, null, null, false));
            dialog.Add(new Dialogues("Yaven", "Já už sa tu chystám na odjezd, jako každý. Tož nemá tu cenu zůstat.", 3, "Obchod", "Talk", "Yaven", true, null, null, false));
            dialog.Add(new Dialogues("Yaven", "Ale Geralt pokud bys měl o jakejsi nástroj zájem, tak mi to pořád něco zbylo. ", 3, "Obchod", "Talk", "Yaven", true, null, null, false));
            dialog.Add(new Dialogues("Geralt", "Dobře, rád se někdy podívám", 3, "Obchod", "Talk", "Yaven", true, null, null, false));

            //leave
            dialog.Add(new Dialogues("Geralt", "Měj se Yavene", 2, "Nashle", "Talk", "Yaven", true, null, null, false));
            dialog.Add(new Dialogues("Yaven", "Tož dávaj na seba pozor Geralt!", 2, "Nashle", "Talk", "Yaven", true, null, null, false));

            //----------------------PŘEŽÍVŠÍ1----------------

            //greet
            dialog.Add(new Dialogues("Přeživší", "Zdravím", 1, "Pozdrav", "Greet", "Přeživší", true, null, null, false));

            //1
            dialog.Add(new Dialogues("Geralt", "Ve městě není bezpečno. Měl byste odsud co nejrychleji odjet", 1, "Co tu děláte?", "Talk", "Přeživší", true, "Strašidelný dům", null, false));
            dialog.Add(new Dialogues("Přeživší", "No neříkejte! Už bych tu dávno nebyl, kdyby mi do domu nezačali lézt příšery!", 1, "Co tu děláte?", "Talk", "Přeživší", true, "Strašidelný dům", null, false));
            dialog.Add(new Dialogues("Přeživší", "Poslyšte...", 1, "Co tu děláte?", "Talk", "Přeživší", true, "Strašidelný dům", null, false));
            dialog.Add(new Dialogues("Přeživší", "Vy vypadáte jako někdo, kdo se s tou ocelí na zádech umí ohánět..", 1, "Co tu děláte?", "Talk", "Přeživší", true, "Strašidelný dům", null, false));
            dialog.Add(new Dialogues("Přeživší", "Dokázal byste mi pomoct?", 1, "Co tu děláte?", "Talk", "Přeživší", true, "Strašidelný dům", null, false));
            dialog.Add(new Dialogues("Geralt", "Nevím", 1, "Co tu děláte?", "Talk", "Přeživší", true, "Strašidelný dům", null, false));
            dialog.Add(new Dialogues("Geralt", "Nemám moc času, takže by se mi to muselo vyplatit", 1, "Co tu děláte?", "Talk", "Přeživší", true, "Strašidelný dům", null, false));
            dialog.Add(new Dialogues("Přeživší", "A za 60 orénů, byste mi pomohl?", 1, "Co tu děláte?", "Talk", "Přeživší", true, "Strašidelný dům", null, false));

            dialog.Add(new Dialogues("Geralt", "Dobře pomůžu vám", 1, "Co tu děláte?", "Talk", "Přeživší", true, "Strašidelný dům", "Přijmout", true));
            dialog.Add(new Dialogues("Přeživší", "Děkuji vám moc!", 1, "Co tu děláte?", "Talk", "Přeživší", true, "Strašidelný dům", "Přijmout", false));
            dialog.Add(new Dialogues("Přeživší", "Můj dům je hned naproti. Vedle kovářova", 1, "Co tu děláte?", "Talk", "Přeživší", true, "Strašidelný dům", "Přijmout", false));
            dialog.Add(new Dialogues("Přeživší", "Ani nemusíte zabíjet tu příšeru. Stačí mi když mi donesete prsten ze skříně", 1, "Co tu děláte?", "Talk", "Přeživší", true, "Strašidelný dům", "Přijmout", false));
            dialog.Add(new Dialogues("Geralt", "Dobře, podívam se po tom prstenu", 1, "Co tu děláte?", "Talk", "Přeživší", true, "Strašidelný dům", "Přijmout", false));

            dialog.Add(new Dialogues("Geralt", "Jak jsem řekl nemohu si dovolit ztrácet čas.", 1, "Co tu děláte?", "Talk", "Přeživší", true, null, "Odejít", true));
            dialog.Add(new Dialogues("Přeživší", "A k čemu tedy máte ty meče?! To mě tu opravdu necháte?", 1, "Co tu děláte?", "Talk", "Přeživší", true, null, "Odejít", false));
            dialog.Add(new Dialogues("Geralt", "Nemohu si dovolit ztrácet čas", 1, "Co tu děláte?", "Talk", "Přeživší", true, null, "Odejít", false));

            //2
            dialog.Add(new Dialogues("Geralt", "Našel jsem ten prsten, který jste chtěl", 1, "Našel jsem prsten", "Talk", "Přeživší", false, "Strašidelný dům", null, false));
            dialog.Add(new Dialogues("Geralt", "A taky zabil tu příšeru", 1, "Našel jsem prsten", "Talk", "Přeživší", false, "Strašidelný dům", null, false));
            dialog.Add(new Dialogues("Geralt", "Byl to Ghůl, ale to bylo jasné, protože je jich tu hodně", 1, "Našel jsem prsten", "Talk", "Přeživší", false, "Strašidelný dům", null, false));
            dialog.Add(new Dialogues("Přeživší", "Ghůl!?", 1, "Našel jsem prsten", "Talk", "Přeživší", false, "Strašidelný dům", null, false));
            dialog.Add(new Dialogues("Přeživší", "u svaté melitelé. Děkuji vám moc za pomoc tady máte těch slíbených 40 orénů a ještě 20 za to že jste se postaral o tu příšernost", 1, "Našel jsem prsten", "Talk", "Přeživší", false, "Strašidelný dům", null, false));

            //leave
            dialog.Add(new Dialogues("Geralt", "Sbohem", 2, "Nashle", "Talk", "Přeživší", true, null, null, false));
            dialog.Add(new Dialogues("Přeživší", "Nashle", 2, "Nashle", "Talk", "Přeživší", true, null, null, false));

            //----------------------------------------CHAPTER I------------------------------------
            // VILLAGER
            //greet
            dialog1.Add(new Dialogues("Vesničan", "Nejsi tu vítán", 1, "Pozdrav", "Greet", "Vesničan", true, null, null, false));

            //1
            dialog1.Add(new Dialogues("Vesničan", "Jdi pryč!", 1, "Co se děje", "Talk", "Vesničan", true, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Vesničan", "Takový jako ty tu nechceme!", 1, "Co se děje", "Talk", "Vesničan", true, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Máte tu hodně takových psů?", 1, "Co se děje", "Talk", "Vesničan", true, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Vesničan", "Nic ti neřeknu ty špinavá zrůdo! Jarek měl pravdu přinášíte jen smůlu a neštěstí!", 1, "Co se děje", "Talk", "Vesničan", true, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Vesničan", "Takový jako ty tu nechceme!", 1, "Co se děje", "Talk", "Vesničan", true, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Pokud jich tu máte více, tak vesnice není bezpečná", 1, "Co se děje", "Talk", "Vesničan", true, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Vesničan", "Řekl jsem ať jdeš pryč!", 1, "Co se děje", "Talk", "Vesničan", true, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Vesničan", "Stačí nám, že tu byl už jeden jako ty a toho jsme se zbavili", 1, "Co se děje", "Talk", "Vesničan", true, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Vesničan", "Takže odsud zmiz", 1, "Co se děje", "Talk", "Vesničan", true, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "On tu byl předemnou jiný zaklínač?", 1, "Co se děje", "Talk", "Vesničan", true, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Vesničan", "Ano, ale byl taky poslední", 1, "Co se děje", "Talk", "Vesničan", true, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Vesničan", "Takže zmiz", 1, "Co se děje", "Talk", "Vesničan", true, "Záhadná vesnice", null, false));

            //leave
            dialog1.Add(new Dialogues("Geralt", "Sbohem", 2, "Nashle", "Talk", "Vesničan", true, null, null, false));
            dialog1.Add(new Dialogues("Vesničan", "Už se nikdy nevracej", 2, "Nashle", "Talk", "Vesničan", true, null, null, false));

            //INKEEPER OLAF
            //greet
            dialog1.Add(new Dialogues("Hospodský Olaf", "Co chceš?", 1, "Pozdrav", "Greet", "Olaf", true, null, null, false));

            //1
            dialog1.Add(new Dialogues("Geralt", "Sháním informace o zaklínači, který tu byl předemnou", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Shánět informace můžeš, ale žádný nedostaneš", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Vysvětlili jsme ti jasně, že tě tu nechceme a ani nepotřebujeme", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Ti psi venku mi říkají něco jiného", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Myslím si, že zrovna tahle vesnice potřebuje zaklínače nejvíce", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Co se stalo s tím zaklínačem?", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Já ti nic neřeknu ty všiváku, takže zmiz!", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Dneska nemám moc dobrou náladu, takže mě poslouchej", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Buď mi něco řekneš o tom zaklínači", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "A nebo tě zabiju", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "A věř mi, že se nestihneš ani pohnout", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Takže?", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Sakra! já nic nevím. Si myslíš, že jsem místní drbna nebo co?!", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Poslouchám", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", ".....", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Přišel sem asi před 5 dny a hledal práci a úkryt.", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Starosta ho samozřejmě ihned vyhnal. Stejně jako tebe", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Nebo se teda pokusil, protože stačilo pár výhrůžek od starosty a ten magor se rozhodl, že zabije stráže starosty a bude vyhrožovat jemu", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Nevím jestli mu starosta něco řekl, ale ten všivák pak odešel a už se neukázal. Starosta za nim pak poslal další hlídky, ale ti už se nevrátili", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Jméno toho zaklínače?", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Jak to mám asi vědět?! Si myslíš, že jsme si povídali?!", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Myslím, že se snad nikdy nepředstavil", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "A ten místní starosta?", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Jeho jméno?", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Turman", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Turman Hogman", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Díky", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Jdi už, mám tu i jiné lidi", 1, "Sháním informace", "Talk", "Olaf", false, "Záhadná vesnice", null, false));

            //2
            dialog1.Add(new Dialogues("Geralt", "Neviděl jsi někde kolem vesnice Barghesta?", 1, "Neviděl jsi někde Barghesta?", "Talk", "Olaf", false, "Vzteklý pes", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Barghestů je kolem vesnice plno, ale nedoporučoval bych je jako mazlíčky", 1, "Neviděl jsi někde Barghesta?", "Talk", "Olaf", false, "Vzteklý pes", null, false));
            dialog1.Add(new Dialogues("Geralt", "Jeden z Barghestů se přibližuje hodně k vesnici. O něm jsi taky nic neslyšel?", 1, "Neviděl jsi někde Barghesta?", "Talk", "Olaf", false, "Vzteklý pes", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Jo to je pěknej parchant. Zabíjí mi slepice", 1, "Neviděl jsi někde Barghesta?", "Talk", "Olaf", false, "Vzteklý pes", null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Lidi říkaj, že v nějaký kryptě ve vesnici, ale já o žádný kryptě nevím", 1, "Neviděl jsi někde Barghesta?", "Talk", "Olaf", false, "Vzteklý pes", null, false));
            dialog1.Add(new Dialogues("Geralt", "Dobře, díky", 1, "Neviděl jsi někde Barghesta?", "Talk", "Olaf", false, "Vzteklý pes", null, false));
            //leave
            dialog1.Add(new Dialogues("Geralt", "Sbohem", 2, "Nashle", "Talk", "Olaf", true, null, null, false));
            dialog1.Add(new Dialogues("Hospodský Olaf", "Běž už", 2, "Nashle", "Talk", "Olaf", true, null, null, false));

            //MAYOR TURMAN HOGMAN
            dialog1.Add(new Dialogues("Starosta Turman", "Ani se ke mně nepřibližuj zrůdo!", 1, "Pozdrav", "Greet", "Turman", true, null, null, false));

            //1
            dialog1.Add(new Dialogues("Geralt", "Slyšel jsem, že jsi mluvil se zaklínačem, který tu byl předemnou", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Starosa Turman", "A to jsi slyšel jako vodkoho?", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Starosta Turman", "Protože ti ten dotyčný pravděpodobně neřekl, že se nebavím se zavšivenýma zrůdama", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Tato informace se ke mně donesla", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Jediné co chci jsou informace o té 'zavšivené zrůdě', takže nevidím problém proč byste mi to nemohl říct", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Starosta Turman", "Vy doopravdy jste úplně tupý", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Starosta Turman", "Jsi neslyšel? Řekl jsem vypadni a už se nevracej.", 1, "Zaklínač", "Talk", "Turman", false, "ZNa stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Starosta Turman", "Tohle je moje vesnice ve které nejsi vítaný", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Dobře", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Jak si přejete", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Ale", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Ti psi tam venku", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Ti psi vás jednou zahubí. Může to být za 2 roky, za 2 měsíce a nebo taky jenom 2 DNY", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "A já moc dobře vím, na jakých místech se barghesti objevují", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Mohl bych vám pomoct a zbavit vás tich psů", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Chci jen vědět kam šel ten Zaklínač", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Starosta Turman", "*rozklepaným hlasem* Zmiz", 1, "Zaklínač", "Talk", "Turman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Starosta Turman", "Odsud", 1, "Zaklínač", "Talk", "Turman", false, "Na stopě zaklínači", null, false));

            //2
            dialog1.Add(new Dialogues("Geralt", "Co si myslíš, že děláš?", 1, "Co si myslíš, že děláš", "Talk", "Turman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Starosta Turman", "Starám se o svojí vesnici!", 1, "Co si myslíš, že děláš", "Talk", "Turman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Tomuhle říkáš starání se o vesnici?", 1, "Co si myslíš, že děláš", "Talk", "Turman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Starosta Turman", "Už tě mám plné zuby mutante! chcípni", 1, "Co si myslíš, že děláš", "Talk", "Turman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Starosta Turman", "Stráže zabte ho!", 1, "Co si myslíš, že děláš", "Talk", "Turman", false, "Záhadná vesnice", null, false));

            //leave
            dialog1.Add(new Dialogues("Geralt", "Sbohem, pane starosto", 2, "Nashle", "Talk", "Turman", true, null, null, false));
            dialog1.Add(new Dialogues("Starosta Turman", "...", 2, "Nashle", "Talk", "Turman", true, null, null, false));

            //ZOLTAN
            dialog1.Add(new Dialogues("Zoltan", "Zdravím Geralte!", 1, "Pozdrav", "Greet", "Zoltan", true, null, null, false));

            //1
            dialog1.Add(new Dialogues("Geralt", "Zoltane neslyšel jsi náhodou něco o zaklínači, který tu byl předemnou?", 1, "Lambert", "Talk", "Zoltan", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Aha! Tak je to pravda", 1, "Lambert", "Talk", "Zoltan", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Zprva som nevěděl esli to mám baštit, ale když to hlásíš i ty", 1, "Lambert", "Talk", "Zoltan", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Popravdě Geralte, toho nevím o moc víc než ty", 1, "Lambert", "Talk", "Zoltan", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Lidi v tejhle vesnici sů divný a nerozebírají zrovna zajímavý témata", 1, "Lambert", "Talk", "Zoltan", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Ale tož slyšel jsem o místním šílencovi a třeba by ti on mohl pomoct", 1, "Lambert", "Talk", "Zoltan", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Hmm", 1, "Lambert", "Talk", "Zoltan", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Víš o něm něco?", 1, "Lambert", "Talk", "Zoltan", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Vůbec nic", 1, "Lambert", "Talk", "Zoltan", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Protože som ho tu eště neviděl, takže ti bohužal nemožu ani říct, kde by jsi ho mohl najít", 1, "Lambert", "Talk", "Zoltan", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Jinou stopu než tohle nemám, takže nemám na vybranou", 1, "Lambert", "Talk", "Zoltan", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Díky Zoltane", 1, "Lambert", "Talk", "Zoltan", false, "Na stopě zaklínači", null, false));

            //2
            dialog1.Add(new Dialogues("Geralt", "Zoltane?", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Co ty tu děláš?", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Zoltan", "Tož to bych sa měl ptát já Geralte!", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Zoltan", "Takhle blízko Novigradu. Navíc když jsi byl nedávno ve Wyzimě", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Zoltan", "Jak tož vlastně všecko dopadlo? Dopad jsi Salamandru?", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Snad ano, ale upřímně ani nevím. To bude asi důvod proč tu jsem", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Spíš co tu děláš ty Zoltane", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Zoltan", "Po tom co Wyzima začala plápolat, tak tož mi bylo jasno, že nemám smysl tam zostavat", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Zoltan", "Chtěl som... jít zpět do Mahakamu, ale tož naskytli sa potíže a musil som vyrazit do Novigradu", 1, "Co tě sem přivádí?", "Talk", "Zoltan", false, null, null, false));
            dialog1.Add(new Dialogues("Zoltan", "Tu som zastavil jen abych doplnil energiju a napil sa, ale tady ani kořalku dobrů nemajů", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Zoltan", "Víš Geralte..", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Zoltan", "Tahleta vesnice nejni rychtyg", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Zoltan", "A tebe Geralte tož tebe tu ale vobec nemajů v lásce", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Zoltan", "Měl by si odsud co nejdříve vymáznot", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Neplánuji se tu zdržet moc dlouho", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "A ani nechci", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "To samé platí i pro tebe Zoltane. Nezdržuj se tu zbytečně dlouho. Není tu bezpečno", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Zoltan", "Neboj sa Geralte nemám to v plánu", 1, "Co tě sem přivádí?", "Talk", "Zoltan", true, null, null, false));

            //3
            dialog1.Add(new Dialogues("Geralt", "Zoltane jsi v pořádku?", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Tož Geralt nikdá mi nebylo lépe", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Zápach krve a prošlého jídla mi vyhovuje", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Kdo je ta žena?", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Nevím, ale byla tu, když mě sem přivedli", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Není spoutaná.. narozdíl od tebe", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Geralte... prohlížet si ji můžeš, až když mě rozvážeš", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Samozřejmě", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", ".......", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Ehmm Gerale?", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Ano?", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Co s ňou? Berem ju sebó?", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Nechat ji tady nemůžeme", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Pojďme odsud. Není tu bezpečno", 1, "Jsi v pořádku?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));

            //4
            dialog1.Add(new Dialogues("Geralt", "Zoltane potřebuji, aby jsi se o ní postaral. Zvládneš to?", 1, "Postaráš se o ní?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Zoltane", "Tož jasně Geralte, ale kam s ňů?", 1, "Postaráš se o ní?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Zoltane", "Ani nevíme odkaď je, nebo kde bydlí", 1, "Postaráš se o ní?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Vem ji do opuštěné budovy na okraji vesnice. Tam může zůstat dokud se nevzbudí", 1, "Postaráš se o ní?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Geralte... Myslíš, že je ještě vůbec živá? Kdo ví co ji provedli", 1, "Postaráš se o ní?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Normálně bych ti lehce odpověděl, ale problém je, že u ní nevím", 1, "Postaráš se o ní?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Můj medailon na ní zvláštně reaguje..", 1, "Postaráš se o ní?", "Talk", "Zoltan", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Buď opatrný Zoltane. Vrátím se za tebou, až najdu Lamberta", 1, "Postaráš se o ní?", "Talk", "Zoltan", false, "Záhadná vesnice", null, false));

            //5
            dialog1.Add(new Dialogues("Geralt", "Jak jí je?", 1, "Jak jí je?", "Talk", "Zoltan", false, "Cesta do Novigradu", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Tož už je ji lépe", 1, "Jak jí je?", "Talk", "Zoltan", false, "Cesta do Novigradu", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Chtěla si s tebou promluvit", 1, "Jak jí je?", "Talk", "Zoltan", false, "Cesta do Novigradu", null, false));
            dialog1.Add(new Dialogues("Geralt", "Hmmmm", 1, "Jak jí je?", "Talk", "Zoltan", false, "Cesta do Novigradu", null, false));
            dialog1.Add(new Dialogues("Geralt", "Dobře, Tak tedy sbohem a hodně štěstí Zoltane", 1, "Jak jí je?", "Talk", "Zoltan", false, "Cesta do Novigradu", null, false));
            dialog1.Add(new Dialogues("Zoltan", "Hodně štěstí i tobě Geralte", 1, "Jak jí je?", "Talk", "Zoltan", false, "Cesta do Novigradu", null, false));
            //leave
            dialog1.Add(new Dialogues("Geralt", "Měj se Zoltane", 2, "Nashle", "Talk", "Zoltan", true, null, null, false));
            dialog1.Add(new Dialogues("Zoltan", "Hodně štěstí Geralte", 2, "Nashle", "Talk", "Zoltan", true, null, null, false));

            //MADMAN
            dialog1.Add(new Dialogues("Šílenec", "Zdravíčko pane", 1, "Pozdrav", "Greet", "Madman", true, null, null, false));

            //1
            dialog1.Add(new Dialogues("Geralt", "Ty jsi ten šílenec o kterém každý mluví?", 1, "Informace o Lambertovi", "Talk", "Madman", false, null, null, false));
            dialog1.Add(new Dialogues("Šílenec", "Hahahaha", 1, "Informace o Lambertovi", "Talk", "Madman", false, null, null, false));
            dialog1.Add(new Dialogues("Šílenec", "Já? a šílenec? NIKDY", 1, "Informace o Lambertovi", "Talk", "Madman", false, null, null, false));
            dialog1.Add(new Dialogues("Šílenec", "Já jsem zcela zdravý a má mysl také", 1, "Informace o Lambertovi", "Talk", "Madman", false, null, null, false));
            dialog1.Add(new Dialogues("Šílenec", "Jen občas ze spánku mě noční můry budí", 1, "Informace o Lambertovi", "Talk", "Madman", false, null, null, false));
            dialog1.Add(new Dialogues("Šílenec", "Hahahah", 1, "Informace o Lambertovi", "Talk", "Madman", false, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Nevěděl bys něco o tom zaklínači, který tu byl předemnou?", 1, "Informace o Lambertovi", "Talk", "Madman", false, null, null, false));
            dialog1.Add(new Dialogues("Šílenec", "To nejsi ty? Ty jsi zase nějakej další? HHAHA", 1, "Informace o Lambertovi", "Talk", "Madman", false, null, null, false));
            dialog1.Add(new Dialogues("Šílenec", "Zaklínači se množí rychleji jak krysy", 1, "Informace o Lambertovi", "Talk", "Madman", false, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Víš o tom zaklínači něco? Třeba kam šel?", 1, "Informace o Lambertovi", "Talk", "Madman", false, null, null, false));
            dialog1.Add(new Dialogues("Šílenec", "Slyšíš to?", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Šílenec", "To psí štěkání je každým dnem hrozivější. Obvzláště v noci", 1, "Informace o Lambertovi", "Talk", "Madman", false, null, null, false));
            //choice barghest
            dialog1.Add(new Dialogues("Geralt", "To jsou Barghesti", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", true));
            dialog1.Add(new Dialogues("Šílenec", "Ó Ano Ano", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Strašné to zrůdy", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Hahahaha", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Zelené svítící stvůry a ten štěkot", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Jako křik démonů...", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "Barghesti", false));
            dialog1.Add(new Dialogues("Geralt", "Barghesti se ale nezačínají objevovat jen tak", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Geralt", "Jsou znamením smrtí a zla", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "AHAHHAHAHH", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Geralt", "neznačí nic dobrého", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Přesně tak haha", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Vy jste velice chytrý muž pane...", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Geralt", "Geralt", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Maxmilián", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Ale zdejší mi říkají šílenec, magor, vyvrhel", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "HAHAHA", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "A to jen kvůli tomu, že nevěřím na ta jejich pravidla a víry", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Však oni nevědí, že šílenost je znamením inteligence a chytrosti", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "hahaha", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Geralt", "Ve vesnici žije plno zkažených a zlých lidí", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Ano ano. Ale lidé takovýhle nebývali. Ne. Ne", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Vesnice byla dříve jiná", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Plná lásky a radosti", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Starosta vše změnil...přivedl sem jakési vyvrheli a lidi z východu...", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Začal vyhánět poctivé lidi a ti kteří nesouhlasili byli šílenci", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Začal zabíjet neviné lidi...", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "TEN POKRYTEC", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "TA SVINĚ ZAČALA ZABÍJET LIDI A RODINY!", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "JEN TAK JEN PRO ZÁBAVU!", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "...", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Geralt", "...", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Tohle bývala zcelá jiná vesnice", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Ten zaklínač ho měl zabít na místě", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "A ne si jen tak odejít. Jako zbabělec", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "hahahah", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Geralt", "Víš kam šel?", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Starosta mě blizko k hospodě a hlavní budově nepouští", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Šílenec", "Nevím kam šel, ale několikrát ze zastavil u mistra lovčího", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            dialog1.Add(new Dialogues("Geralt", "Děkuji ti Maxmiliáne. A přeju ti mír i klid v duši", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "To jsou Barghesti", false));
            //choice evil
            dialog1.Add(new Dialogues("Geralt", "Nemám čas na tvoje kraviny, kam šel ten zaklínač a kde je", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "Nemám čas na tvoje kraviny", true));
            dialog1.Add(new Dialogues("Šílenec", "Hahahahah", 1, "Informace o Lambertovi", "Talk", "Madman", false, null, "Nemám čas na tvoje kraviny", false));
            dialog1.Add(new Dialogues("Šílenec", "Z toho štěkotu i mráz po zádech proběhne haha", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "Nemám čas na tvoje kraviny", false));
            dialog1.Add(new Dialogues("Geralt", "Kam šel ten zaklínač. Mluv", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "Nemám čas na tvoje kraviny", false));
            dialog1.Add(new Dialogues("Šílenec", "Tolik zloby a vzteku haha", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "Nemám čas na tvoje kraviny", false));
            dialog1.Add(new Dialogues("Geralt", "Ptám se na posled", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "Nemám čas na tvoje kraviny", false));
            dialog1.Add(new Dialogues("Geralt", "Kam šel!", 1, "Informace o Lambertovi", "Talk", "Madman", false, null, "Nemám čas na tvoje kraviny", false));
            dialog1.Add(new Dialogues("Šílenec", "Nemám tušení zeptej se třeba těch psů", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "Nemám čas na tvoje kraviny", false));
            dialog1.Add(new Dialogues("Šílenec", "Nemám přístup ke krčmě nevím nic o dění ve vesnici", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "Nemám čas na tvoje kraviny", false));
            dialog1.Add(new Dialogues("Šílenec", "VŽDYŤ JSEM JEN OBYČEJNÝ ŠÍLENEC HAHAHAHAH", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "Nemám čas na tvoje kraviny", false));
            dialog1.Add(new Dialogues("Geralt", "Jsi jen ztráta času. Dávej si pozor na psi ať nejsi další", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "Nemám čas na tvoje kraviny", false));
            dialog1.Add(new Dialogues("Geralt", "A nebo taky na zaklínače bez trpělivosti", 1, "Informace o Lambertovi", "Talk", "Madman", false, "Na stopě zaklínači", "Nemám čas na tvoje kraviny", false));

            //2
            dialog1.Add(new Dialogues("Geralt", "Co se děje?", 1, "Co se děje?", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Mistře zaklínači!", 1, "Co se děje?", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Starosta se dočista pomátl!", 1, "Co se děje?", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Je v hospodě a unáší lidi!", 1, "Co se děje?", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "haha", 1, "Co se děje?", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Je mezi nimi i ten trpaslík", 1, "Co se děje?", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Zoltan?!", 1, "Co se děje?", "Talk", "Madman", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Já toho hajzla zabiju", 1, "Co se děje?", "Talk", "Madman", false, "Záhadná vesnice", null, false));

            //3
            dialog1.Add(new Dialogues("Geralt", "Neviděl jsi náhodou, kam zavedli toho trpaslíka?", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Ano samozřejmě, že ano", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Na tuto velkou událost jsem čekal velice dlouho", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Velice", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Je to důležité", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Vzali je do staré části vesnice", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Tam se nikdo neodváží", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Tam totiž sídlí vesnická nestvůra", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Takže odtamtud přichází Barghesti?", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Haha", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Kéž by Barghesti pane", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Barghesti jsou oproti téhle stvůře hmyz", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Tohle je totiž ten důvod proč tam starosta bere lidi", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Šílenec", "Aby ji nakrmil...", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));
            dialog1.Add(new Dialogues("Geralt", "Děkuji je na čase, abych se tam vydal", 1, "Zoltan", "Talk", "Madman", false, "Záhadná vesnice", null, false));


            dialog1.Add(new Dialogues("Geralt", "Sbohem", 2, "Nashle", "Talk", "Madman", true, null, null, false));
            dialog1.Add(new Dialogues("Šílenec", "Sbohem pane", 2, "Nashle", "Talk", "Madman", true, null, null, false));

            //BRICKMAKER
            dialog1.Add(new Dialogues("Cihlář", "Dobrej", 1, "Pozdrav", "Greet", "BrickMaker", true, null, null, false));

            //1
            dialog1.Add(new Dialogues("Geralt", "Co dělá cihlář v takovéhle vesnici?", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Cihlář sem bejval. Teď už nejsem", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Makám tady na polích", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "A proč už nejsi cihlář?", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Protože se změnila vesnice", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Od té doby co tu je na krvežíznivá bestie, tak se vše změnilo", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Tohle bejvala veselá vesnice", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Ale potom co se tady vobjevil ten zaklínačskej, tak se z téhle vesnice stalo bitevní pole", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Pokud vím, tak ten zaklínač moc dlouho nebyl", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Nemyslím toho kterej tu byl nedávno. Tamten zaklínač přišel asi před rokem", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Od té doby tu žije ta bestie", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Jaká bestie? Barghesti?", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Víte co pane, do tohohle se vůbec nepleťte. Budou z toho jen problémy", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Zaklínačský nám přinesli jen potíže", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Co se tu stalo?", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Už jsem toho řekl až moc", 1, "Co dělá cihlář v takovéhle vesnici", "Talk", "BrickMaker", true, null, null, false));

            //2
            dialog1.Add(new Dialogues("Geralt", "Nějaká práce pro zaklínače", 1, "Práce pro zaklínače", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Tady ti nikdo práci nenabídne zaklínačskej", 1, "Práce pro zaklínače", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Leda tak možná Mistr lovčí. Toho se zeptej", 1, "Práce pro zaklínače", "Talk", "BrickMaker", true, null, null, false));

            //3
            dialog1.Add(new Dialogues("Geralt", "Vypadáš, že tu žiješ už dlouho", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Řekl bys mi něco o zdejších lidech?", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Je to vopravdu nutný?", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Rád bych věděl s jakými lidmi mám tu čest", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "No kde bych začal?..", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Máme tu mistra lovčího, kterej nám pomáhá s těma čoklama", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Pak je tu Jarek, kterej nemá zaklínačský moc v lásce, tak za nim ani nechoď", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Samozřejmě tu máme i starostu, který je poslední dobou...no.. nesvůj", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Žije tu i šílenec. Vo něm se říká, že přežil starou vesnici..... a asi to bude i pravda", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Neměl to v životě lehký", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Přežil starou vesnici? Co to znamená?", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "To nejni...", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "To nejni důležitý", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Nejni to tvoje starost zaklínačskej", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "No a pak se nám tu zastavil nějakej vobchodník", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Dobře", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Kde najdu toho šílence?", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Poslední dobou je někde zalezlej a nevychází ven", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Být tebou ho nechám být. Ten člověk toho viděl dost", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Právě proto s ním chci mluvit", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Myslím, že většinou se tu vobjevuje kolem pátý nebo čtvrtý. Už nevím", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Dobře, děkuji ti za informace", 1, "O vesnici", "Talk", "BrickMaker", true, null, null, false));

            dialog1.Add(new Dialogues("Geralt", "Sbohem", 2, "Nashle", "Talk", "BrickMaker", true, null, null, false));
            dialog1.Add(new Dialogues("Cihlář", "Nashle", 2, "Nashle", "Talk", "BrickMaker", true, null, null, false));

            //TRADER
            dialog1.Add(new Dialogues("Obchodník", "Dobrý den", 1, "Pozdrav", "Greet", "Trader", true, null, null, false));

            //1
            dialog1.Add(new Dialogues("Geralt", "Co dělá člověk v takovéhlem oblečení v takovéhle vesnici?", 1, "Co tu děláte?", "Talk", "Trader", true, null, null, false));
            dialog1.Add(new Dialogues("Obchodník", "No co bych tu dělal", 1, "Co tu děláte?", "Talk", "Trader", true, null, null, false));
            dialog1.Add(new Dialogues("Obchodník", "Myslel jsem, že bych tu mohl chvíli zůstat, ale to nepřipadá v úvahu", 1, "Co tu děláte?", "Talk", "Trader", true, null, null, false));
            dialog1.Add(new Dialogues("Obchodník", "Zrůdy všude a ty lidi ještě horší. Obvzlášť ten starosta", 1, "Co tu děláte?", "Talk", "Trader", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Co je se starostou?", 1, "Co tu děláte?", "Talk", "Trader", true, null, null, false));
            dialog1.Add(new Dialogues("Obchodník", "Vyháněl mě ještě dřív, než jsem stačil říct dobrý den", 1, "Co tu děláte?", "Talk", "Trader", true, null, null, false));
            dialog1.Add(new Dialogues("Obchodník", "Už abych byl v Novigradu", 1, "Co tu děláte?", "Talk", "Trader", true, null, null, false));
            dialog1.Add(new Dialogues("Obchodník", "Kdybyste si však chtěl něco koupit, tak tu mám celkem pěkné meče", 1, "Co tu děláte?", "Talk", "Trader", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Dobře, beru na vědomí", 1, "Co tu děláte?", "Talk", "Trader", true, null, null, false));

            dialog1.Add(new Dialogues("Geralt", "Sbohem", 2, "Nashle", "Talk", "Trader", true, null, null, false));
            dialog1.Add(new Dialogues("Obchodník", "Nashledanou", 2, "Nashle", "Talk", "Trader", true, null, null, false));

            //JAREK
            dialog1.Add(new Dialogues("Jarek", "No nazdar", 1, "Pozdrav", "Greet", "Jarek", true, null, null, false));

            //1
            dialog1.Add(new Dialogues("Geralt", "Ty jsi Jarek?", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Jakej Jarek?!", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Pro tebe PAN Jarek chlapečku", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Si myslíš, že když znáš moje jméno, tak jsme kamarádi?!", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Právě naopak chlapečku", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Vyznáš se tady?", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Jasně, že se tady vyznám, když tu bydlím už vod plenek", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "A co ty jseš? nějakej turista nebo co?", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Tohle místo není žádná turistická atrakce", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Právě naopak", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Jestli jsi přišel jen, aby jsi šel vomkrnout tu zrůdu ve starý, tak prosím. O jednoho blbečka méně", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Zrůda? Ve starý?", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Takhle říkáme tvý bábě všiváku", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Vode mě se nic nedozvíš", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Už vím co jsem potřeboval", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Jak že víš co jsi potřeboval?! Vždyť jsem ti nic neřekl!", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Hej!", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Nic nevíš! Ani nevíš co tam venku ve starý žije!", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Tak mi to řekni", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Co?!", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Ne ne ne!", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Já ti nic neřeknu chlapečku!", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Divím se, že tě starosta, ještě nevyhodil po zkušenostech s vědmákama", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "A jaký máte zkušenosti?", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "ŠPATNÝ!", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Hodně špatný a víc vědět nepotřebuješ", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "A zmiz už, akorát kvůli tobě dostanu infarkt", 1, "Ty jsi Jarek?", "Talk", "Jarek", true, null, null, false));

            dialog1.Add(new Dialogues("Geralt", "Sbohem", 2, "Nashle", "Talk", "Jarek", true, null, null, false));
            dialog1.Add(new Dialogues("Jarek", "Běž už ty všiváku!", 2, "Nashle", "Talk", "jarek", true, null, null, false));

            //MASTER HUNTER
            dialog1.Add(new Dialogues("Mistr lovčí", "Zdravíčko pane", 1, "Pozdrav", "Greet", "MasterHunter", true, null, null, false));

            //1
            dialog1.Add(new Dialogues("Geralt", "Slyšel jsem, že jsi mluvil se zaklínačem, který tu byl předemnou", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Geralt", "Potřeboval bych vědět, kam šel a co ti říkal", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Hmm", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "A to víš odkud?", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Geralt", "Záleží na tom?", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Řekl bych, že ano. Ten zaklínač zaplatil dobře, abych byl potichu", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Co po něm chceš?", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Geralt", "Osobní záležitost do které ti nic není", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Hmm", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Víš jak se jmenuje?", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Geralt", "Lambert", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Tak tedy dobrá", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Ale než ti řeknu to co chceš slyšet, tak od tebe budu taky něco potřebovat", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Než Lambert zmizel, tak mi slíbil, že se postará o tu havěť  ve vesnici", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Geralt", "O barghesty?", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Ne o barghesty ne. Ti psi už mě nezajímají. Plánuji odsud co nejdříve odejít. Není tu bezpečno", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Jde mi o ghúli zaklínači. Je jich tu čím dál tím více", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Dobře postarám se o ty ghúli, ale nesnaž se mě podvést", 1, "Co víš o tom zaklínači", "Talk", "MasterHunter", false, "Problém s ghúly", null, false));
            //2
            dialog1.Add(new Dialogues("Geralt", "Zabil jsem ty ghúly. Kde je Lambert?", 1, "Kde je Lambert?", "Talk", "MasterHunter", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Že ti to ale trvalo zaklínači", 1, "Kde je Lambert?", "Talk", "MasterHunter", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Lambert se ukryl na sever od vesnice. Je tam takový starý dům", 1, "Kde je Lambert?", "Talk", "MasterHunter", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Ukryl?", 1, "Kde je Lambert?", "Talk", "MasterHunter", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "On před někým utíká?", 1, "Kde je Lambert", "Talk", "MasterHunter", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Co já vím. Moc mi toho neřekl", 1, "Kde je Lambert?", "Talk", "MasterHunter", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Dobře děkuji. A radši tuto vesnici, co nerychleji opusť", 1, "Kde je Lambert?", "Talk", "MasterHunter", false, "Na stopě zaklínači", null, false));

            //3
            dialog1.Add(new Dialogues("Geralt", "Je tu nějaká práce pro zaklínače?", 1, "Nějaká práce pro zaklínače?", "Talk", "MasterHunter", true, null, null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "V téhle vesnici je práce pro zaklínače dostatek", 1, "Nějaká práce pro zaklínače?", "Talk", "MasterHunter", true, null, null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Ale měl bych tu jeden zvláštní kousek", 1, "Nějaká práce pro zaklínače?", "Talk", "MasterHunter", true, null, null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Všiml jsem si, že jeden Barghest se až moc přibližuje k vesnici", 1, "Nějaká práce pro zaklínače?", "Talk", "MasterHunter", false, null, null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Myslím si, že by to mohl být vůdce smečky", 1, "Nějaká práce pro zaklínače?", "Talk", "MasterHunter", true, null, null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Dám ti za něj 80 orénů", 1, "Nějaká práce pro zaklínače?", "Talk", "MasterHunter", true, null, null, false));
            dialog1.Add(new Dialogues("Geralt", "Dobře, beru", 1, "Nějaká práce pro zaklínače?", "Talk", "MasterHunter", true, "Vzteklý pes", "Beru", true));
            dialog1.Add(new Dialogues("Mistr lovčí", "Bohužel nevím, kde by se ten Barghest mohl nacházet. Zkus se zeptat ve vesnici, jestli ho někdo neviděl", 1, "Nějaká práce pro zaklínače?", "Talk", "MasterHunter", true, "Vzteklý pes", "Beru", false));
            //4
            dialog1.Add(new Dialogues("Geralt", "Barghest je mrtvý", 1, "Zabil jsem toho psa", "Talk", "MasterHunter", false, "Vzteklý pes", null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Dobrá práce zaklínači. Tady je tvých 80 orénů", 1, "Zabil jsem toho psa", "Talk", "MasterHunter", false, "Vzteklý pes", null, false));

            dialog1.Add(new Dialogues("Geralt", "Nemám zájem", 1, "Nějaká práce pro zaklínače?", "Talk", "MasterHunter", true, null, "Nemám zájem", true));
            dialog1.Add(new Dialogues("Mistr lovčí", "Pokud si to rozmyslíš, tak se vrať", 1, "Nějaká práce pro zaklínače?", "Talk", "MasterHunter", true, null, "Nemám zájem", false));
            //leave
            dialog1.Add(new Dialogues("Geralt", "Sbohem", 2, "Nashle", "Talk", "MasterHunter", true, null, null, false));
            dialog1.Add(new Dialogues("Mistr lovčí", "Sbohem", 2, "Nashle", "Talk", "MasterHunter", true, null, null, false));

            //LAMBERT
            dialog1.Add(new Dialogues("Lambert", "Zdravím vlku", 1, "Pozdrav", "Greet", "Lambert", true, null, null, false));

            //1
            dialog1.Add(new Dialogues("Geralt", "Dlouho jsme se neviděli", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Nečekal jsem, že tu uvidím zrovna tebe Geralte", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Co tu děláš? Nemáš být náhodou ve Wyzimě?", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Ve Wyzimě jsem byl, ale už jsem naše tajemství získal zpět", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Takže co děláš tady?", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Slyšel jsem, že jsi se bavil s vrahem, který zaútočil na Foltesta", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Odkdy jsi Foltestova chůva", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Jsi se nepochlubil", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Jsem tu kvůli tobě Lamberte, protože to vypadá, že máš problém", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Ne já žádný problém nemám. Jen po mě jdou nějací šaškové z Novigradu", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Víš kdo jsou?", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Geralte já nepotřebuji tvojí pomoc. Dokážu se o sebe postarat", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Myslím si, že můžou být spojeni s těmi, kteří ukradli naše tajemství", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Mě je jedno co si myslíš", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Když jsem pátral v Novigradu, tak jsem se dostal až k nim a jim se nelíbil můj zájem o ně", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Tak si se mnou domluvili komunikaci přes megaskop a snažili se mě nalákat do pasti", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Tu schůzku jsem přijmul, ale nikdo tam nepřišel. Hádám, že měl přijít ten, který se pokusil o zabití Foltesta", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Tak jsem se chtěl vrátit zpět do Novigradu, ale po cestě jsem narazil na jejich další skupinku", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Od té skupinky jsem se dozvěděl, že jejich velitel by měl být v téhle vesnici, tak jsem si myslel, že je to ten starosta, ale asi ne", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "No a teď se tady schovávám", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Takže se musím vydat do Novigradu", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Až tam budeš, tak se ptej na člověka jménem Bolehlav. Je to jejich takový malý šéf.", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Ale dávej si pozor, když zjistí, že je hledá zaklínač, tak si domyslí co tam děláš", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Ty nepůjdeš se mnou?", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Lambert", "Ne už mě nebaví být neustále n útěku. Asi se vrátím do Kaer Morhen", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));
            dialog1.Add(new Dialogues("Geralt", "Dobře, tak tedy hodně štěstí", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Cesta do Novigradu", null, false));
            dialog1.Add(new Dialogues("Lambert", "Uvidíme se na Kaer Morhen vlku", 1, "Dlouho jsme se neviděli", "Talk", "Lambert", false, "Na stopě zaklínači", null, false));


            //leave
            dialog1.Add(new Dialogues("Geralt", "Sbohem", 2, "Nashle", "Talk", "Lambert", true, null, null, false));
            dialog1.Add(new Dialogues("Lambert", "Sbohem vlku", 2, "Nashle", "Talk", "Lambert", true, null, null, false));

            //MORENN
            dialog1.Add(new Dialogues("Morenn", "Zaklínači", 1, "Pozdrav", "Greet", "Morenn", true, null, null, false));

            //1
            dialog1.Add(new Dialogues("Geralt", "Jak ti je?", 1, "Jak ti je?", "Talk", "Morenn", false, "Cesta do Novigradu", null, false));
            dialog1.Add(new Dialogues("Morenn", "Je mi lépe a to jen díky vám. Děkuji", 1, "Jak ti je?", "Talk", "Morenn", false, "Cesta do Novigradu", null, false));
            dialog1.Add(new Dialogues("Geralt", "Máš kam jít .. ?", 1, "Jak ti je?", "Talk", "Morenn", false, "Cesta do Novigradu", null, false));
            dialog1.Add(new Dialogues("Morenn", "Morenn. Jmenuji se Morenn", 1, "Jak ti je?", "Talk", "Morenn", false, "Cesta do Novigradu", null, false));
            dialog1.Add(new Dialogues("Geralt", "Geralt z Rivie", 1, "Jak ti je?", "Talk", "Morenn", false, "Cesta do Novigradu", null, false));
            dialog1.Add(new Dialogues("Morenn", "Ne dokážu se o sebe postarat Geralte", 1, "Jak ti je?", "Talk", "Morenn", false, "Cesta do Novigradu", null, false));
            dialog1.Add(new Dialogues("Geralt", "Dobře, tak tedy Sbohem Morenn", 1, "Jak ti je?", "Talk", "Morenn", false, "Cesta do Novigradu", null, false));
            dialog1.Add(new Dialogues("Morenn", "Sbohem Geralte", 1, "Jak ti je?", "Talk", "Morenn", false, "Cesta do Novigradu", null, false));

            //leave
            dialog1.Add(new Dialogues("Geralt", "Sbohem", 2, "Nashle", "Talk", "Morenn", true, null, null, false));
            dialog1.Add(new Dialogues("Morenn", "Sbohem Geralte", 2, "Nashle", "Talk", "Morenn", true, null, null, false));
            manager.SaveDialogues(dialog1, path1);
            manager.SaveDialogues(dialog, path);
        }
        

    }
    
}
