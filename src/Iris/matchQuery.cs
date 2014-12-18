using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unirest_net.http;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Forms;

namespace Iris
{
    public static class matchQuery
    {
        private static long lastQuery;
        public static bool dataAcquired;

        private const long queryWait = 50000000;// hundreds of nanoseconds

        private enum SummonerSpells
        {
            Cleanse = 1,
            Clairvoyance = 2,
            Exhaust = 3,
            Flash = 4,
            Ghost = 6,
            Heal = 7,
            Revive = 10,
            Smite = 11,
            Teleport = 12,
            Clarity = 13,
            Ignite = 14,
            Garrison = 17,
            Barrier = 21
        }

        public static Dictionary<int, string> championNames;
        public static Dictionary<string, string> championPassives;
        public static Dictionary<string, List<string>> championSpells;

        private static bool champsProcessed;

        private static void processChamps()
        {
            championNames = new Dictionary<int, string>();
            championPassives = new Dictionary<string, string>();
            championSpells = new Dictionary<string, List<string>>();

            string champData = System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "\\champions.json");
            JObject champs = (JObject) JObject.Parse(champData).GetValue("data");

            IList<string> keys = champs.Properties().Select(p => p.Name).ToList();           

            foreach(string key in keys)
            {
                JObject champ = (JObject) champs.GetValue(key);
                championNames.Add((int) champ.GetValue("id"), (string) champ.GetValue("name"));           
                
                JArray spellList = (JArray) champ.GetValue("spells");
                List<string> champSpells = new List<string>();

                foreach(JToken spell in spellList)
                {
                    
                    JObject spellO = (JObject)spell;

                    string spellName = spellO.GetValue("name").ToString();

                    if (spellName.Contains("/"))
                    {
                        string spellName1 = spellName.Substring(0, spellName.IndexOf("/") - 1);
                        string spellName2 = spellName.Substring(spellName.IndexOf("/") + 2, spellName.Length - (spellName.IndexOf("/") + 2));

                        champSpells.Add(spellName1);
                        champSpells.Add(spellName2);

                    }else
                    {
                        champSpells.Add(spellName);
                    }                    
                }


                

                championSpells.Add(champ.GetValue("name").ToString(), champSpells);
                
                //passive

                JObject passive = (JObject) champ.GetValue("passive");

                championPassives.Add(champ.GetValue("name").ToString(), passive.GetValue("name").ToString());
            }
        }

        public class champion
        {
            public string summonerName;
            public string summonerInternalName;
            public string name;
            public int id;
            public int summonerSpell1;
            public int summonerSpell2;
            public List<string> abilities;
            public string passive;
        }

        public static List<champion> enemies;
        public static List<champion> friends;

        public static void initialize()
        {
            if (champsProcessed == false)
            {
                processChamps();
            }
            
            champsProcessed = true;
            lastQuery = 0;
            enemies = new List<champion>();
            friends = new List<champion>();
        }

        private static bool isPlayerOnTeam(List<champion> team, string summonerInternalName)
        {
            foreach(champion champ in team)
            {
                if(champ.summonerInternalName == summonerInternalName)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool checkMatch()
        {
            if (System.DateTime.Today.Ticks - lastQuery < queryWait)
            {
                return false;
            }
            else
            {
                lastQuery = System.DateTime.Today.Ticks;
            }

            enemies.Clear();
            friends.Clear();

            HttpResponse<string> response = Unirest.get("https://community-league-of-legends.p.mashape.com/api/v1.0/" + config.region + "/summoner/retrieveInProgressSpectatorGameInfo/" + config.summonerName).header("X-Mashape-Authorization", "SsNckuwRAv3hFUeM6LHwrG8Ub3WEKXBP").asString();
            JObject parsed;
            
            try{
                parsed = JObject.Parse(response.Body);
            }
            catch
            {
                return false;
            }
            
            if((string) parsed.GetValue("success") == "false")
            {
                return false;
            }
            else
            {
                int foundUs = 0;

                List<champion> teamOne = new List<champion>();

                if(parsed.GetValue("game")["teamOne"] != null)
                {
                    foreach (JToken playerT in parsed.GetValue("game")["teamOne"]["array"].Children())
                    {
                        JObject player = (JObject)playerT;
                        champion addChamp = new champion();
                        addChamp.summonerName = (string) player.GetValue("summonerName");
                        addChamp.summonerInternalName = (string) player.GetValue("summonerInternalName");

                        teamOne.Add(addChamp);

                        if(config.summonerName == addChamp.summonerName)
                        {
                            foundUs = 1;
                        }
                    }
                }

                List<champion> teamTwo = new List<champion>();

                if (parsed.GetValue("game")["teamTwo"] != null)
                {
                    foreach (JToken playerT in parsed.GetValue("game")["teamTwo"]["array"].Children())
                    {
                        JObject player = (JObject)playerT;

                        champion addChamp = new champion();
                        addChamp.summonerName = (string)player.GetValue("summonerName");
                        addChamp.summonerInternalName = (string)player.GetValue("summonerInternalName");

                        if (config.summonerName == addChamp.summonerName)
                        {
                            foundUs = 2;
                        }

                        teamTwo.Add(addChamp);
                    }
                }

                foreach (JObject player in parsed.GetValue("game")["playerChampionSelections"]["array"])
                {
                    if (isPlayerOnTeam(teamOne, (string)player.GetValue("summonerInternalName")))
                    {
                        foreach (champion champ in teamOne)
                        {
                            if (champ.summonerInternalName == (string)player.GetValue("summonerInternalName"))
                            {
                                champ.summonerSpell1 = (int)player.GetValue("spell1Id");
                                champ.summonerSpell2 = (int)player.GetValue("spell2Id");
                                champ.id = (int)player.GetValue("championId");
                                champ.name = championNames[champ.id];
                            }
                        }
                    }
                    else
                    {
                        foreach (champion champ in teamTwo)
                        {
                            if (champ.summonerInternalName == (string)player.GetValue("summonerInternalName"))
                            {
                                champ.summonerSpell1 = (int)player.GetValue("spell1Id");
                                champ.summonerSpell2 = (int)player.GetValue("spell2Id");
                                champ.id = (int)player.GetValue("championId");
                                champ.name = championNames[champ.id];
                            }
                        }
                    }
                }
                                
                if (foundUs == 1)
                {
                    friends = teamOne;
                    enemies = teamTwo;
                }
                else
                {
                    friends = teamTwo;
                    enemies = teamOne;
                }
                
                dataAcquired = true;
                

                return true;               
            }
        }
    }
}
