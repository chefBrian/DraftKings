using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using CsQuery;
using DraftKings.Models;
using Newtonsoft.Json;

namespace DraftKings
{
    public static class FD_Football
    {
        public static void FootballMain(string week, string groupId)
        {
            Console.WriteLine();
            var pointUrl = "https://api.fanduel.com/fixture-lists/" + groupId + "13189/players";
            var playerUrl = "http://football.fantasysports.yahoo.com/f1/1365059/playersearch?&search=######&stat1=S_PW_" + week + "&jsenabled=1";
            
            Console.WriteLine("Player projections link:");
            Console.WriteLine(playerUrl);
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("------------------PLAYERS------------------");
            Console.WriteLine("-------------------------------------------");

            var request = (HttpWebRequest)WebRequest.Create(pointUrl);
            var response = request.GetResponse();
            List<Player> playerList;
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                var playJson = reader.ReadToEnd();
                playerList = JsonConvert.DeserializeObject<Play>(playJson).playerList.ToList();
            }

            var count = 0;
            foreach (var p in playerList)
            {
                count++;
                var playerPointUrl = playerUrl.Replace("######", p.fn + "%20" + p.ln.Replace(" Jr.", string.Empty));
                request = (HttpWebRequest)WebRequest.Create(playerPointUrl);
                response = request.GetResponse();
                using (var responseStream = response.GetResponseStream())
                {
                    var reader = new StreamReader(responseStream, Encoding.UTF8);
                    var pointsData = reader.ReadToEnd();
                    var pointsDom = CQ.Create(pointsData);
                    try
                    {
                        var pointsSpan = pointsDom["td"][7].FirstChild.FirstChild.NodeValue;
                        p.ep = Convert.ToDecimal(pointsSpan);
                    }
                    catch (Exception exception)
                    {
                        Console.Write("Please Enter Projected Points For " + p.fn + "" + p.ln + ": ");
                        var points = Console.ReadLine();
                        p.ep = Convert.ToDecimal(points);
                    }
                }
                Console.WriteLine(count + ". " + p.ep + " - " + p.s + " - " + p.fn + " " + p.ln);
            }

            var teams = new List<List<Player>>();

            var validPlayers =
                new List<Player>();
            
            validPlayers.AddRange(playerList.Where(e => e.pn == "QB").OrderByDescending(e => e.ep).Take(5));
            validPlayers.AddRange(playerList.Where(e => e.pn == "RB").OrderByDescending(e => e.ep).Take(7));
            validPlayers.AddRange(playerList.Where(e => e.pn == "WR").OrderByDescending(e => e.ep).Take(7));
            validPlayers.AddRange(playerList.Where(e => e.pn == "TE").OrderByDescending(e => e.ep).Take(4));
            validPlayers.AddRange(playerList.Where(e => e.pn == "DST").OrderByDescending(e => e.ep).Take(4));
            
            Console.WriteLine();
            Console.WriteLine("Combinations Running...");
            var possibleTeams = DK_MathFun.Combs(validPlayers.OrderByDescending(p => p.pn).ToList(), 9).ToList();
            Console.WriteLine("Combinations Complete!");

            Console.WriteLine();

            possibleTeams = possibleTeams.Where(t => t != null).ToList();
            possibleTeams = possibleTeams.Where(t => t.Any(p => p.pn == "QB")).ToList();
            possibleTeams = possibleTeams.Where(t => t.Any(p => p.pn == "DST")).ToList();
            possibleTeams = possibleTeams.Where(
                t => (t.Count(p => p.pn == "RB") == 3 && t.Count(p => p.pn == "WR") == 3 && t.Count(p => p.pn == "TE") == 1) ||
                (t.Count(p => p.pn == "RB") == 2 && t.Count(p => p.pn == "WR") == 4 && t.Count(p => p.pn == "TE") == 1) ||
                (t.Count(p => p.pn == "RB") == 2 && t.Count(p => p.pn == "WR") == 3 && t.Count(p => p.pn == "TE") == 2)
                ).ToList();
            possibleTeams = possibleTeams.Where(t => t.Sum(p => p.s) < 50000).ToList();

            var top5 = possibleTeams.OrderByDescending(t => t.Sum(p => p.ep)).Take(5).ToList();

            
            Console.WriteLine(validPlayers.Count + " valid players.");
            Console.WriteLine(DK_MathFun.LoopCount + " possible teams.");
            Console.WriteLine(possibleTeams.Count + " valid teams.");

            Console.WriteLine();
            var teamPos = 0;

            foreach (var t in top5)
            {
                teamPos++;

                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Team:" + teamPos + " Salary:" + t.Sum(p => p.s) + " Projected Points:" + t.Sum(p => p.ep));
                Console.WriteLine("-------------------------------------------");
                foreach (var p in t)
                {
                    Console.WriteLine(p.pn + " : " + p.ep + " - " + p.s + " - " + p.fn + " " + p.ln);
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}