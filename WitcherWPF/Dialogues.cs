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
            string prolog = @"../../dialogues/DialoguePrologue.json";
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
            string prolog = @"../../dialogues/DialoguePrologue.json";
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
            
            string prolog = @"../../dialogues/DialoguePrologue.json";
            
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
            string path = @"../../dialogues/DialoguePrologue.json";
            //----------------------FOLTEST----------------
            //greet
            dialog.Add(new Dialogues("Foltest", "Vítej zpět Zaklínači", 1, "Pozdrav", "Greet", "Foltest", true, null, null, false));

            //1
            dialog.Add(new Dialogues("Foltest", "Geralte, zjistil si už něco o tom vrahovi?", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Ne králi, zatím ne", 1, "Co potřebujete králi?", "Talk", "Foltest", true, null, null, false));
            dialog.Add(new Dialogues("Foltest", "Tak to by sis měl pospíšit Geralte. Přeci jen jde o tvojí reputaci", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "To že se jeden Zaklínač pokusí o zabití krále, ihned neznamená, že takový jsou všichni zaklínači", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Takhle to ale u prostého lidu nefunguje. Lidem stačí, aby se jeden Zaklínač pokusil o vraždu a bude nenávidět všechny", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Potřebuji mít tuto záležitost co nejrychleji zasebou Zaklínači.", 1, "Co potřebujete králi?", "Talk", "Foltest", true, "Něco končí, něco začíná", null, false));
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
            dialog.Add(new Dialogues("Geralt", "Mám své důvody", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "A mohl bych tvé důvody znát?", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Ne", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Nemohl", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Dobře. Ať je po tvém", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Pokud se ti o vrahovi podaří zjistit více, tak mi to přijeď sdělit. Budu tady ve Wyzimě", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Foltest", "Samozřejmě tu na tebe bude čekat odměna pokud se ti to podaří vyřešit", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "hm", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
            dialog.Add(new Dialogues("Geralt", "Samozřejmě", 1, "Mám informace o vrahovi", "Talk", "Foltest", false, "Něco končí, něco začíná", null, false));
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


            manager.SaveDialogues(dialog, path);
        }
        

    }
    
}
