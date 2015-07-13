using System;
using System.Drawing;
using System.Media;
using LeagueSharp;
using LeagueSharp.Common;

namespace GankHelper
{
    internal class Program
    {
        private static Menu Config;
        private static int lastSound = 0;
        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += OnGameLoad;
        }

        private static void OnGameLoad(EventArgs args)
        {
            Config = new Menu("GankHelper", "GankHelper", true);
            Config.AddItem(new MenuItem("Enabled", "Enabled")).SetValue(true);
            Config.AddItem(new MenuItem("Sound", "Beep if enemy low")).SetValue(true);
            Config.AddItem(new MenuItem("PositionX", "Position X").SetValue(new Slider(20, 0, Drawing.Width - 20)));
            Config.AddItem(new MenuItem("PositionY", "Position Y").SetValue(new Slider(20, 0, Drawing.Height - 20)));
            Config.AddToMainMenu();

            Drawing.OnDraw += OnDraw;
        }

        private static void OnDraw(EventArgs args)
        {
            if (!Config.Item("Enabled").GetValue<bool>()) return;
            var movetop = 1;
            var movemid = 1;
            var movebot = 1;
            var posX = Config.Item("PositionX").GetValue<Slider>().Value;
            var posY = Config.Item("PositionY").GetValue<Slider>().Value;

            foreach (var enemy in HeroManager.Enemies)
            {

                if (SummonersRift.TopLane.Top_Zone.IsInside(enemy))
                {
                    if (enemy.HealthPercent >= 75)
                    {
                        Drawing.DrawText(posX, posY + movetop*15, Color.GreenYellow,
                            "TOP: " + enemy.ChampionName + " " + Math.Round(enemy.HealthPercent, 0) + "% health");
                        movetop++;
                    }
                    else if (enemy.HealthPercent >= 51)
                    {
                        Drawing.DrawText(posX, posY + movetop*15, Color.Yellow,
                            "TOP: " + enemy.ChampionName + " " + Math.Round(enemy.HealthPercent, 0) + "% health");
                        movetop++;
                    }
                    else if (enemy.HealthPercent >= 1)
                    {
                        Drawing.DrawText(posX, posY + movetop*15, Color.Red,
                            "TOP: " + enemy.ChampionName + " " + Math.Round(enemy.HealthPercent, 0) + "% health");
                        movetop++;
                        if (Config.Item("Sound").GetValue<bool>())
                        {
                            if (Utils.TickCount - lastSound < 5000)
                            {
                            }
                            else
                            {
                                lastSound = Utils.TickCount;
                                SystemSounds.Exclamation.Play();
                            }
                        }
                    }
                    else if (enemy.HealthPercent < 1)
                    {
                        Drawing.DrawText(posX, posY + movetop*15, Color.Black,
                            "TOP: " + enemy.ChampionName + " " + Math.Round(enemy.HealthPercent, 0) + "% health");
                        movetop++;
                    }
                }

                else if (SummonersRift.MidLane.Mid_Zone.IsInside(enemy))
                {
                    if (enemy.HealthPercent >= 75)
                    {
                        Drawing.DrawText(posX, posY + 75 + movemid*15, Color.GreenYellow,
                            "MID: " + enemy.ChampionName + " " + Math.Round(enemy.HealthPercent, 0) + "% health");
                        movemid++;
                    }
                    else if (enemy.HealthPercent >= 51)
                    {
                        Drawing.DrawText(posX, posY + 75 + movemid*15, Color.Yellow,
                            "MID: " + enemy.ChampionName + " " + Math.Round(enemy.HealthPercent, 0) + "% health");
                        movemid++;
                    }
                    else if (enemy.HealthPercent >= 1)
                    {
                        Drawing.DrawText(posX, posY + 75 + movemid*15, Color.Red,
                            "MID: " + enemy.ChampionName + " " + Math.Round(enemy.HealthPercent, 0) + "% health");
                        movemid++;
                        if (Config.Item("Sound").GetValue<bool>())
                        {
                            if (Utils.TickCount - lastSound < 5000)
                            {
                            }
                            else
                            {
                                lastSound = Utils.TickCount;
                                SystemSounds.Exclamation.Play();
                            }
                        }
                    }
                    else if (enemy.HealthPercent < 1)
                    {
                        Drawing.DrawText(posX, posY + 75 + movemid*15, Color.Black,
                            "MID: " + enemy.ChampionName + " " + Math.Round(enemy.HealthPercent, 0) + "% health");
                        movemid++;
                    }
                }

                else if (SummonersRift.BottomLane.Bottom_Zone.IsInside(enemy))
                {
                    if (enemy.HealthPercent >= 75)
                    {
                        Drawing.DrawText(posX, posY + 150 + movebot*15, Color.GreenYellow,
                            "BOT: " + enemy.ChampionName + " " + Math.Round(enemy.HealthPercent, 0) + "% health");
                        movebot++;
                    }
                    else if (enemy.HealthPercent >= 51)
                    {
                        Drawing.DrawText(posX, posY + 150 + movebot*15, Color.Yellow,
                            "BOT: " + enemy.ChampionName + " " + Math.Round(enemy.HealthPercent, 0) + "% health");
                        movebot++;
                    }
                    else if (enemy.HealthPercent >= 1)
                    {
                        Drawing.DrawText(posX, posY + 150 + movebot*15, Color.Red,
                            "BOT: " + enemy.ChampionName + " " + Math.Round(enemy.HealthPercent, 0) + "% health");
                        movebot++;
                        if (Config.Item("Sound").GetValue<bool>())
                        {
                            if (Utils.TickCount - lastSound < 5000)
                            {
                            }
                            else
                            {
                                lastSound = Utils.TickCount;
                                SystemSounds.Exclamation.Play();
                            }
                        }
                    }
                    else if (enemy.HealthPercent < 1)
                    {
                        Drawing.DrawText(posX, posY + 150 + movebot*15, Color.Black,
                            "BOT: " + enemy.ChampionName + " " + Math.Round(enemy.HealthPercent, 0) + "% health");
                        movebot++;
                    }
                }
            }
        }
    }
}