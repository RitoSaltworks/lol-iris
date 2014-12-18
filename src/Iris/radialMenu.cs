using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Iris
{
    public static class radialMenu
    {
        private static bool liveUpdate;

        private static List<menuItem> menuItems;

        private static Point menuXY;

        private static bool menuActive;
        private static bool pingerActive;
        private static bool pingerDown;
        private static string chatString;

        private static Font menuFont;

        private static int centerImage;
        private static int centerImageHover;
        private static Rectangle centerImageRect;
        private static Rectangle iconRect;
        private static int ringImage;

        private static int baseRadius;

        private static Boolean hotkeyAwaitingRelease;

        private const int keySleepLength = 10;

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
            Barrier = 21,
            ToTheKing = 30,
            PoroToss = 31,
        }

        private static System.Windows.Input.Key WinformsToWPFKey(System.Windows.Forms.Keys inputKey)
        {
            try
            {
                return (System.Windows.Input.Key)Enum.Parse(typeof(System.Windows.Input.Key), inputKey.ToString());
            }
            catch
            {  
                return System.Windows.Input.Key.None;
            }
        } 

        private static int distance(Point p1, Point p2)
        {
            return (int) Math.Sqrt(Math.Pow(((double)p1.X - (double)p2.X), 2) + Math.Pow(((double)p1.Y - (double)p2.Y), 2)); 
        }

        private static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private class menuItem
        {
            public string name;
            public string action;
            public string content;

            public bool subMenuActivated; //is my submenu the active menu?

            public int ovTxtID;
            public int ovIconID;

            public List<menuItem> subItems;

            public void deinitialize()
            {
                DX9Overlay.TextDestroy(ovTxtID);
                DX9Overlay.ImageDestroy(ovIconID);

                if(subItems == null)
                {
                    return;
                }

                foreach (menuItem mItem in subItems)
                {
                    mItem.deinitialize();
                }
            }

            public void hide()
            {
                DX9Overlay.TextSetShown(this.ovTxtID, false);
                DX9Overlay.ImageSetShown(this.ovIconID, false);

                if (this.action == "SubMenu")
                {
                    foreach (menuItem mItem in subItems)
                    {
                        mItem.hide();
                    }
                }
            }

            public void clearActivations()
            {
                this.subMenuActivated = false;

                if (this.action == "SubMenu")
                {
                    foreach (menuItem mItem in subItems)
                    {
                        mItem.clearActivations();
                    }
                }
            }

            public menuItem(string itemXML)
            {
                XElement itemElement = XElement.Parse(itemXML);
                name = itemElement.Attribute("Name").Value;
                action = itemElement.Attribute("Action").Value;
                content = itemElement.Value;

                subItems = new List<menuItem>();

                ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, name, true, false);
                ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\radialMenu\\" + itemElement.Attribute("Icon").Value, 0, 0, 0, 0, false);


                if (action == "SubMenu")
                {
                    foreach (XElement subElement in itemElement.Elements())
                    {
                        subItems.Add(new menuItem(subElement.ToString()));
                    }
                }
            }

            public menuItem(int summonerSpell, string championName)
            {
                ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, Enum.GetName(typeof(SummonerSpells), summonerSpell), true, false);
                ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\summonerSpells\\" + Enum.GetName(typeof(SummonerSpells), summonerSpell) + ".png", 0, 0, 0, 0, false);
                name = Enum.GetName(typeof(SummonerSpells), summonerSpell);
                content = championName + " used cooldown " + name;
                action = "PingAndChat";
            }

            public menuItem(string championName, int spell1, int spell2)
            {
                ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, championName, true, false);
                ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\championSquares\\" + RemoveSpecialCharacters(championName) + "Square.png", 0, 0, 0, 0, false);
                subItems = new List<menuItem>();
                name = championName;
                action = "SubMenu";

                if(spell1 != -1 && spell2 != -1)
                {
                     menuItem iSpell1 = new menuItem(spell1, name);
                     menuItem iSpell2 = new menuItem(spell2, name);
                     subItems.Add(iSpell1);
                     subItems.Add(iSpell2);
                }

                menuItem passive = new menuItem();
                passive.name = matchQuery.championPassives[championName];
                passive.ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, passive.name, true, false);
                passive.ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\abilities\\" + passive.name.Replace(" ", "_").Replace(":", "-") + ".png", 0, 0, 0, 0, false);
                passive.action = "SubMenu";
                passive.subItems = new List<menuItem>();

                menuItem passiveDown = new menuItem();
                passiveDown.name = "Passive Down";
                passiveDown.ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, "Passive Down", true, false);
                passiveDown.ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\radialMenu\\" + "cooldownDown.png", 0, 0, 0, 0, false);
                passiveDown.action = "PingAndChat";
                passiveDown.content = championName + "'s passive (" + passive.name + ") is down";

                menuItem passiveUp = new menuItem();
                passiveUp.name = "Passive Up";
                passiveUp.ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, "Passive Up", true, false);
                passiveUp.ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\radialMenu\\" + "cooldownUp.png", 0, 0, 0, 0, false);
                passiveUp.action = "PingAndChat";
                passiveUp.content = championName + "'s passive (" + passive.name + ") is up";

                menuItem passiveSoon = new menuItem();
                passiveSoon.name = "Passive Soon";
                passiveSoon.ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, "Passive Soon", true, false);
                passiveSoon.ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\radialMenu\\" + "cooldownUpSoon.png", 0, 0, 0, 0, false);
                passiveSoon.action = "PingAndChat";
                passiveSoon.content = championName + "'s passive (" + passive.name + ") is up soon";

                passive.subItems.Add(passiveDown);
                passive.subItems.Add(passiveUp);
                passive.subItems.Add(passiveSoon);

                subItems.Add(passive);

                foreach(string ability in matchQuery.championSpells[championName])
                {
                    menuItem itemAbility = new menuItem();
                    itemAbility.name = ability;
                    itemAbility.ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, ability, true, false);
                    itemAbility.ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\abilities\\" + ability.Replace(" ", "_").Replace(":", "-") + ".png", 0, 0, 0, 0, false);
                    Console.WriteLine(Directory.GetCurrentDirectory() + "\\gfx\\abilties\\" + ability.Replace(" ", "_") + ".png");
                    itemAbility.action = "SubMenu";
                    itemAbility.subItems = new List<menuItem>();

                    menuItem abilityDown = new menuItem();
                    abilityDown.name = "Ability Down";
                    abilityDown.ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, "ability Down", true, false);
                    abilityDown.ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\radialMenu\\" + "cooldownDown.png", 0, 0, 0, 0, false);
                    abilityDown.action = "PingAndChat";
                    abilityDown.content = championName + "'s " + ability + " is down";

                    menuItem abilityUp = new menuItem();
                    abilityUp.name = "Ability Up";
                    abilityUp.ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, "ability Up", true, false);
                    abilityUp.ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\radialMenu\\" + "cooldownUp.png", 0, 0, 0, 0, false);
                    abilityUp.action = "PingAndChat";
                    abilityUp.content = championName + "'s " + ability + " is up";

                    menuItem abilitySoon = new menuItem();
                    abilitySoon.name = "Ability Soon";
                    abilitySoon.ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, "ability Soon", true, false);
                    abilitySoon.ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\radialMenu\\" + "cooldownUpSoon.png", 0, 0, 0, 0, false);
                    abilitySoon.action = "PingAndChat";
                    abilitySoon.content = championName + "'s " + ability + " is up soon";

                    itemAbility.subItems.Add(abilityDown);
                    itemAbility.subItems.Add(abilityUp);
                    itemAbility.subItems.Add(abilitySoon);

                    subItems.Add(itemAbility);
                }                         
            }

            public menuItem()
            {
                ovTxtID = 0;
                ovIconID = 0;
                subItems = null;
                action = null;
            }
        }
        public static bool hotkeyDown()
        {
            

            if (Keyboard.IsKeyDown(WinformsToWPFKey((Keys) config.radialHotkey)))
            {
                bool bShift = false;
                bool bAlt = false;
                bool bCtrl = false;

                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    bShift = true;
                }

                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    bCtrl = true;
                }

                if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                {
                    bAlt = true;
                }

                if (bShift == config.radialHotkeyShift && bCtrl == config.radialHotkeyCtrl && bAlt == config.radialHotkeyAlt)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool wasHotkeyPressed()
        {
            if (Keyboard.IsKeyDown(WinformsToWPFKey((Keys)config.radialHotkey)))
            {
                bool bShift = false;
                bool bAlt = false;
                bool bCtrl = false;

                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    bShift = true;
                }

                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    bCtrl = true;
                }

                if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                {
                    bAlt = true;
                }

                if (bShift == config.radialHotkeyShift && bCtrl == config.radialHotkeyCtrl && bAlt == config.radialHotkeyAlt)
                {
                    if (hotkeyAwaitingRelease == true)
                    {
                        return false;
                    }
                    else
                    {
                        hotkeyAwaitingRelease = true;
                        return true;
                    }
                }
            }

            hotkeyAwaitingRelease = false;
            return false;
        }

        private static void renderMenu(List<menuItem> subItems, IntPtr lolHandle, bool hotkeyPressed)
        {
            if (subItems.Count < 1)
            {
                foreach (menuItem toHide in menuItems)
                {
                    toHide.hide();
                }

                menuActive = false;
                DX9Overlay.ImageSetShown(centerImageHover, false);
                DX9Overlay.ImageSetShown(centerImage, false);
                DX9Overlay.ImageSetShown(ringImage, false);

                return;
            }

            foreach (menuItem mItem in subItems)
            {
                if (mItem.subMenuActivated == true)
                {
                    renderMenu(mItem.subItems, lolHandle, hotkeyPressed);
                    return;
                }
            }

            int currentMenuItem = 0;
            bool itemSelected = false;

            foreach (menuItem mItem in subItems)
            {


                double itemRadians = ((Convert.ToDouble(currentMenuItem) / Convert.ToDouble(subItems.Count)) * (2 * Math.PI));

                Point iconXY = new Point(Convert.ToInt32((Convert.ToDouble(baseRadius) * Math.Sin(itemRadians)) + (menuXY.X - (Convert.ToDouble(iconRect.Width / 2)))),
                Convert.ToInt32((Convert.ToDouble(baseRadius) * Math.Cos(itemRadians)) + (menuXY.Y - (Convert.ToDouble(iconRect.Height / 2)))));

                Point iconScaleXY = DX9Overlay.convertToOverlayPoint(iconXY, lolHandle);

                DX9Overlay.ImageSetShown(mItem.ovIconID, true);
                DX9Overlay.ImageSetPos(mItem.ovIconID, iconScaleXY.X, iconScaleXY.Y);


                Size textSize = TextRenderer.MeasureText(mItem.name, menuFont);
                textSize.Width *= 2;

                DX9Overlay.TextSetShown(mItem.ovTxtID, true);

                Rectangle rectIcon = new Rectangle(iconXY.X, iconXY.Y, iconRect.Width, iconRect.Height); //this isnt the definitional rect. this one is specifically for checking intersections

                Rectangle rectItem = new Rectangle(); //for mouse intersection purposes
                rectItem.Height = Math.Max(textSize.Height, iconRect.Height);
                rectItem.Width = textSize.Width + iconRect.Width + config.radialPadding;

                rectItem.Y = Math.Min(iconXY.Y, iconXY.Y - (textSize.Height - rectIcon.Height));

                int txtY = ((rectItem.Height - textSize.Height) / 2)  + rectItem.Y;

                if (iconXY.X >= menuXY.X)
                {
                    rectItem.X = iconXY.X;
                    DX9Overlay.TextSetPos(mItem.ovTxtID, DX9Overlay.convertToOverlayX(rectItem.X + rectIcon.Width + config.radialPadding, lolHandle), DX9Overlay.convertToOverlayY(txtY, lolHandle));
                }
                else
                {
                    rectItem.X = iconXY.X - textSize.Width - config.radialPadding;
                    DX9Overlay.TextSetPos(mItem.ovTxtID, DX9Overlay.convertToOverlayX(rectItem.X, lolHandle), DX9Overlay.convertToOverlayY(txtY, lolHandle));
                }

                if (rectIcon.Contains(System.Windows.Forms.Cursor.Position) | rectItem.Contains(System.Windows.Forms.Cursor.Position) && menuActive == true)
                {
                    if (itemSelected == false)
                    {
                        itemSelected = true;

                        DX9Overlay.TextSetColor(mItem.ovTxtID, config.radialFontSelectColor);

                        if (hotkeyPressed)
                        {
                            foreach (menuItem toHide in menuItems)
                            {
                                toHide.hide();
                            }

                            if (mItem.action == "SubMenu")
                            {
                                mItem.subMenuActivated = true;
                            }
                            else if (mItem.action == "Chat")
                            {
                                menuActive = false;
                                DX9Overlay.ImageSetShown(centerImage, false);
                                DX9Overlay.ImageSetShown(ringImage, false);
                                DX9Overlay.ImageSetShown(centerImageHover, false);
                                typeSequence(mItem.content);

                            }
                            else if (mItem.action == "PingAndChat")
                            {
                                menuActive = false;
                                DX9Overlay.ImageSetShown(ringImage, false);
                                DX9Overlay.ImageSetShown(centerImageHover, false);
                                pingerActive = true;
                                pingerDown = false;
                                chatString = mItem.content;
                            }
                            else
                            {
                                menuActive = false;
                                DX9Overlay.ImageSetShown(centerImage, false);
                                DX9Overlay.ImageSetShown(centerImageHover, false);
                                DX9Overlay.ImageSetShown(ringImage, false);
                            }

                            return;
                        }
                    }
                }
                else
                {
                    DX9Overlay.TextSetColor(mItem.ovTxtID, config.radialFontColor);
                }

                currentMenuItem++;
            }

            if (hotkeyPressed && distance(System.Windows.Forms.Cursor.Position, menuXY) <= iconRect.Width)
            {
                //we clicked on the center, so close
                foreach (menuItem toHide in menuItems)
                {
                    toHide.hide();
                }

                menuActive = false;
                DX9Overlay.ImageSetShown(centerImage, false);
                DX9Overlay.ImageSetShown(centerImageHover, false);
                DX9Overlay.ImageSetShown(ringImage, false);

                return;
            }

            Rectangle rectCenterIcon = new Rectangle(menuXY.X - (centerImageRect.Width / 2), menuXY.Y - (centerImageRect.Height / 2), centerImageRect.Width, centerImageRect.Height);

            if (rectCenterIcon.Contains(System.Windows.Forms.Cursor.Position))
            {
                DX9Overlay.ImageSetShown(centerImage, false);
                DX9Overlay.ImageSetShown(centerImageHover, true);
            }
            else
            {
                DX9Overlay.ImageSetShown(centerImageHover, false);
                DX9Overlay.ImageSetShown(centerImage, true);
            }

            //option for clicking anywhere no tin menu to close menu

            if (hotkeyPressed && config.radialCloseOnOutsideClick)
            {
                foreach (menuItem toHide in menuItems)
                {
                    toHide.hide();
                }

                menuActive = false;
                DX9Overlay.ImageSetShown(centerImage, false);
                DX9Overlay.ImageSetShown(centerImageHover, false);
                DX9Overlay.ImageSetShown(ringImage, false);

                return;
            }

        }

        private static void updateLiveData()
        {
            menuItem itemCDs = new menuItem();
            itemCDs.name = "Cooldowns";
            itemCDs.action = "SubMenu";
            itemCDs.ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, itemCDs.name, true, false);
            itemCDs.ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\radialMenu\\cooldowns.png", 0, 0, 0, 0, false);
            itemCDs.subItems = new List<menuItem>();

            menuItem itemFriends = new menuItem();
            itemFriends.action = "SubMenu";
            itemFriends.name = "Friends";
            itemFriends.ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, itemFriends.name, true, false);
            itemFriends.ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\radialMenu\\cooldownsFriend.png", 0, 0, 0, 0, false);
            itemFriends.subItems = new List<menuItem>();

            foreach (matchQuery.champion champ in matchQuery.friends)
            {
                menuItem champItem = new menuItem(champ.name, champ.summonerSpell1, champ.summonerSpell2);
                itemFriends.subItems.Add(champItem);
            }

            itemCDs.subItems.Add(itemFriends);

            menuItem itemEnemies = new menuItem();
            itemEnemies.action = "SubMenu";
            itemEnemies.name = "Enemies";
            itemEnemies.ovTxtID = DX9Overlay.TextCreate(config.radialFontName, config.radialFontSize, false, false, 0, 0, config.radialFontColor, itemEnemies.name, true, false);
            itemEnemies.ovIconID = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\radialMenu\\cooldownsEnemy.png", 0, 0, 0, 0, false);
            itemEnemies.subItems = new List<menuItem>();

            foreach (matchQuery.champion champ in matchQuery.enemies)
            {
                menuItem champItem = new menuItem(champ.name, champ.summonerSpell1, champ.summonerSpell2);
                itemEnemies.subItems.Add(champItem);
            }

            itemCDs.subItems.Add(itemEnemies);

            menuItems.Add(itemCDs);
        }

        public static void deinitialize()
        {
            DX9Overlay.ImageSetShown(centerImage, false);
            DX9Overlay.ImageSetShown(ringImage, false);

            foreach (menuItem mItem in menuItems)
            {
                mItem.deinitialize();
            }
        }
        public static void typeSequence(string sequence)
        {
            InputManager.Keyboard.KeyPress(Keys.Enter, 0);
            System.Threading.Thread.Sleep(keySleepLength);

            for (int c = 0; c < sequence.Length; c++)
            {
                bool doShift = false;

                char character = sequence.Substring(c, 1).ToCharArray()[0];

                if (char.IsUpper(character))
                {
                    Console.WriteLine("upper char: " + character);
                    doShift = true;
                    character = char.ToLower(character);
                    System.Threading.Thread.Sleep(keySleepLength);
                }

                Keys key = Win32Helper.ConvertCharToVirtualKey(character);

                if(key.HasFlag(System.Windows.Forms.Keys.Shift))
                {
                    doShift = true;
                    Console.WriteLine("shift flag for " + key.ToString());
                }

                if(doShift == true)
                {
                    InputManager.Keyboard.ShortcutKeys(new Keys [] {Keys.LShiftKey, key});
                }else
                {
                    InputManager.Keyboard.KeyPress(key, 0);
                }

                if(key == Keys.Oemtilde)
                {
                    InputManager.Keyboard.KeyPress(Keys.Oemtilde,0);
                }

                System.Threading.Thread.Sleep(keySleepLength);
            }

            System.Threading.Thread.Sleep(keySleepLength * 2);
            InputManager.Keyboard.KeyPress(Keys.Enter, 0);
        }

        public static void loop(IntPtr lolHandle)
        {
            if (!liveUpdate) //no 
            {
                if (matchQuery.dataAcquired)
                {
                    updateLiveData();
                    liveUpdate = true;
                }
            }

            bool hotkeyPressed = wasHotkeyPressed();

            if (menuActive == true)
            {
                renderMenu(menuItems, lolHandle, hotkeyPressed);
            }
            else if (pingerActive == true)
            {
                if (pingerDown == false)
                {
                    if (hotkeyPressed)
                    {
                        pingerDown = true;

                        if (config.pingHotkeyAlt == true)
                        {
                            InputManager.Keyboard.KeyDown(Keys.Alt);
                        }
                        if (config.pingHotkeyShift == true)
                        {
                            InputManager.Keyboard.KeyDown(Keys.Shift);
                        }
                        if (config.pingHotkeyCtrl == true)
                        {
                            InputManager.Keyboard.KeyDown(Keys.Control);
                        }

                        InputManager.Keyboard.KeyDown((Keys)config.pingHotkey);
                    }
                    else
                    {
                        DX9Overlay.ImageSetPos(centerImage, DX9Overlay.convertToOverlayX(System.Windows.Forms.Cursor.Position.X - (centerImageRect.Width / 2), lolHandle),
                        DX9Overlay.convertToOverlayY(System.Windows.Forms.Cursor.Position.Y - (centerImageRect.Height / 2), lolHandle));
                    }
                }
                else
                {
                    if (!hotkeyDown())
                    {
                        pingerDown = false;
                        pingerActive = false;
                        DX9Overlay.ImageSetShown(centerImage, false);

                        if (config.pingHotkeyAlt == true)
                        {
                            InputManager.Keyboard.KeyUp(Keys.Alt);
                        }
                        if (config.pingHotkeyShift == true)
                        {
                            InputManager.Keyboard.KeyUp(Keys.Shift);
                        }
                        if (config.pingHotkeyCtrl == true)
                        {
                            InputManager.Keyboard.KeyUp(Keys.Control);
                        }

                        InputManager.Keyboard.KeyUp((Keys)config.pingHotkey);

                        if (chatString != "")
                        {
                            typeSequence(chatString);
                            chatString = "";
                        }
                    }
                }
            }
            else
            {
                if (hotkeyPressed)
                {
                    menuActive = true;

                    foreach (menuItem mItem in menuItems)
                    {
                        mItem.clearActivations();
                    }

                    if (config.radialIsStatic)
                    {
                        menuXY = config.radialStaticXY;
                    }
                    else
                    {
                        menuXY = System.Windows.Forms.Cursor.Position;
                    }

                    DX9Overlay.ImageSetPos(centerImage, DX9Overlay.convertToOverlayX(menuXY.X - (centerImageRect.Width / 2), lolHandle), DX9Overlay.convertToOverlayY(menuXY.Y - (centerImageRect.Height / 2), lolHandle));
                    DX9Overlay.ImageSetShown(centerImage, true);
                    DX9Overlay.ImageSetPos(centerImageHover, DX9Overlay.convertToOverlayX(menuXY.X - (centerImageRect.Width / 2), lolHandle), DX9Overlay.convertToOverlayY(menuXY.Y - (centerImageRect.Height / 2), lolHandle));
                    DX9Overlay.ImageSetShown(centerImageHover, false);
                    DX9Overlay.ImageSetShown(ringImage, true);
                    DX9Overlay.ImageSetPos(ringImage, DX9Overlay.convertToOverlayX(menuXY.X - baseRadius, lolHandle), DX9Overlay.convertToOverlayY(menuXY.Y - baseRadius, lolHandle));
                }
            }
        }

        public static void initialize(IntPtr lolHandle)
        {
            liveUpdate = false;

            menuActive = false;
            pingerActive = false;
            pingerDown = false;

            hotkeyAwaitingRelease = true;

            menuFont = new Font(config.radialFontName, config.radialFontSize, GraphicsUnit.Pixel);

            menuItems = new List<menuItem>();

            XDocument xmlDoc = XDocument.Load(Directory.GetCurrentDirectory() + "\\radialMenu.xml");

            centerImage = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\radialMenu\\centerImage.png", 0, 0, 0, 0, false);
            centerImageHover = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\radialMenu\\centerImageHover.png", 0, 0, 0, 0, false);
            centerImageRect = config.radialCenterImageRect;
            iconRect = config.radialIconRect;
            ringImage = DX9Overlay.ImageCreate(Directory.GetCurrentDirectory() + "\\gfx\\radialMenu\\ring.png", 0, 0, 0, 0, false);
            baseRadius = config.radialBaseRadius;

            foreach (XElement xmlElement in xmlDoc.Root.Elements())
            {
                menuItems.Add(new menuItem(xmlElement.ToString()));
            }
        }
    }
}
