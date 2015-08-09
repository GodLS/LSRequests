using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace RivenAutoShield
{
    class Program
    {
        private static Spell E;
        private static readonly Menu Config = new Menu("RivenAutoShield", "RivenAutoShield", true);
        private static readonly Obj_AI_Hero Player = ObjectManager.Player;

        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += OnGameLoad;
        }

        private static void OnGameLoad(EventArgs args)
        {
            if (Player.CharData.BaseSkinName != "Riven") return;

            Config.AddItem(new MenuItem("percent", "Damage percentage").SetValue(new Slider(30, 1)));
            Config.AddItem(new MenuItem("enabled", "Enabled").SetValue(new KeyBind('Z', KeyBindType.Toggle)));

            Config.AddToMainMenu();

            Game.PrintChat("[Riven<font color='#79BAEC'>AutoShield</font>]: <font color='#FFFFFF'>" + "Loaded!</font>");

            E = new Spell(SpellSlot.E);

            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!args.Target.IsMe) return;
            if (!sender.IsEnemy || sender.Type != GameObjectType.obj_AI_Hero || !E.IsReady()) return;
            var percent = Config.Item("percent").GetValue<Slider>().Value * .01;
            var dmg = sender.GetDamageSpell(Player, args.SData.Name);

            if (args.SData.TargettingType != SpellDataTargetType.SelfAndUnit &&
                args.SData.TargettingType != SpellDataTargetType.Unit &&
                args.SData.TargettingType != SpellDataTargetType.SelfAoe) return;

            if (!(dmg.CalculatedDamage >= percent*Player.Health)) return;
            E.Cast(Game.CursorPos);
        }
    }
}
