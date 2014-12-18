using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Iris
{
    public static class config
    {
        public static int radialHotkey;

        const int MOD_ALT = 0x0001;
        const int MOD_CONTROL = 0x0002;
        const int MOD_SHIFT = 0x0004;
        const int MOD_KEYUP = 0x1000;
        const int MOD_NOREPEAT = 0x4000; 

        public static bool radialHotkeyAlt;
        public static bool radialHotkeyCtrl;
        public static bool radialHotkeyShift;

        public static bool radialCloseOnOutsideClick;

        public static bool radialIsStatic;
        public static Point radialStaticXY;

        public static int pingHotkey;

        public static bool pingHotkeyAlt;
        public static bool pingHotkeyCtrl;
        public static bool pingHotkeyShift;

        public static Rectangle radialCenterImageRect;
        public static Rectangle radialIconRect;

        public static int radialPadding;

        public static string radialFontName;
        public static int radialFontSize;
        public static uint radialFontColor;
        public static uint radialFontSelectColor;

        public static int radialBaseRadius;

        public static string region;
        public static string summonerName;

        public static int radialHotkeyMods
        {
            get
            {
                int mods = 0;

                if(radialHotkeyAlt == true)
                {
                    mods += MOD_ALT;
                }

                if(radialHotkeyCtrl == true)
                {
                    mods += MOD_CONTROL;
                }

                if(radialHotkeyShift == true)
                {
                    mods += MOD_SHIFT;
                }

                mods += MOD_NOREPEAT;

                return mods;
            }
        }


        public static void initialize()
        {
            XDocument configXML = XDocument.Load(Directory.GetCurrentDirectory() + "\\config.xml");

            radialHotkey = Convert.ToInt32(configXML.Root.Element("RadialMenu").Element("Hotkey").Attribute("Key").Value);
            radialBaseRadius = Convert.ToInt32(configXML.Root.Element("RadialMenu").Attribute("BaseRadius").Value);
            radialCloseOnOutsideClick = Convert.ToBoolean(configXML.Root.Element("RadialMenu").Attribute("CloseOnOutsideClick").Value);
            radialIsStatic = Convert.ToBoolean(configXML.Root.Element("RadialMenu").Attribute("IsStatic").Value);
            radialStaticXY.X = Convert.ToInt32(configXML.Root.Element("RadialMenu"). Attribute("StaticX").Value);
            radialStaticXY.Y = Convert.ToInt32(configXML.Root.Element("RadialMenu").Attribute("StaticY").Value);
            radialHotkeyAlt = Convert.ToBoolean(configXML.Root.Element("RadialMenu").Element("Hotkey").Attribute("Alt").Value);
            radialHotkeyCtrl = Convert.ToBoolean(configXML.Root.Element("RadialMenu").Element("Hotkey").Attribute("Ctrl").Value);
            radialHotkeyShift = Convert.ToBoolean(configXML.Root.Element("RadialMenu").Element("Hotkey").Attribute("Shift").Value);
            pingHotkey = Convert.ToInt32(configXML.Root.Element("PingHotkey").Attribute("Key").Value);
            pingHotkeyAlt = Convert.ToBoolean(configXML.Root.Element("PingHotkey").Attribute("Alt").Value);
            pingHotkeyCtrl = Convert.ToBoolean(configXML.Root.Element("PingHotkey").Attribute("Ctrl").Value);
            pingHotkeyShift = Convert.ToBoolean(configXML.Root.Element("PingHotkey").Attribute("Shift").Value);
            radialCenterImageRect = new Rectangle(0, 0, Convert.ToInt32(configXML.Root.Element("RadialMenu").Element("CenterImage").Attribute("Width").Value),
                Convert.ToInt32(configXML.Root.Element("RadialMenu").Element("CenterImage").Attribute("Height").Value));
            radialIconRect = new Rectangle(0,0, Convert.ToInt32(configXML.Root.Element("RadialMenu").Element("Icons").Attribute("Width").Value),
                Convert.ToInt32(configXML.Root.Element("RadialMenu").Element("Icons").Attribute("Height").Value));
            radialFontName = configXML.Root.Element("RadialMenu").Element("Font").Attribute("Name").Value;
            radialFontSize = Convert.ToInt32(configXML.Root.Element("RadialMenu").Element("Font").Attribute("Size").Value);
            radialFontColor = ColorToUInt(ColorTranslator.FromHtml(configXML.Root.Element("RadialMenu").Element("Font").Attribute("Color").Value));
            radialFontSelectColor = ColorToUInt(ColorTranslator.FromHtml(configXML.Root.Element("RadialMenu").Element("Font").Attribute("SelectColor").Value));
            radialPadding = Convert.ToInt32(configXML.Root.Element("RadialMenu").Attribute("Padding").Value);
            summonerName = configXML.Root.Element("Summoner").Attribute("Name").Value;
            region = configXML.Root.Element("Summoner").Attribute("Region").Value;
        }

        private static uint ColorToUInt(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) |
                          (color.G << 8) | (color.B << 0));
        }

        public static void save()
        {
            XDocument configXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Config",
                    new XElement("Summoner",
                        new XAttribute("Name", summonerName),
                        new XAttribute("Region", region)
                    ),
                    new XElement("PingHotkey",
                        new XAttribute("Key", pingHotkey),
                        new XAttribute("Alt", pingHotkeyAlt),
                        new XAttribute("Shift", pingHotkeyShift),
                        new XAttribute("Ctrl", pingHotkeyCtrl)
                    ),
                    new XElement("RadialMenu",
                        new XAttribute("BaseRadius", radialBaseRadius),
                        new XAttribute("Padding", radialPadding),
                        new XAttribute("CloseOnOutsideClick", radialCloseOnOutsideClick),
                        new XAttribute("IsStatic", radialIsStatic),
                        new XAttribute("StaticX", radialStaticXY.X),
                        new XAttribute("StaticY", radialStaticXY.Y),
                        new XElement("Hotkey",
                            new XAttribute("Key", radialHotkey),
                            new XAttribute("Alt", radialHotkeyAlt.ToString()),
                            new XAttribute("Ctrl", radialHotkeyAlt.ToString()),
                            new XAttribute("Shift", radialHotkeyAlt.ToString())
                        ),
                        new XElement("Font",
                            new XAttribute("Name", radialFontName),
                            new XAttribute("Size", radialFontSize),
                            new XAttribute("Color", ColorTranslator.ToHtml(Color.FromArgb((int)radialFontColor))),
                            new XAttribute("SelectColor", ColorTranslator.ToHtml(Color.FromArgb((int)radialFontSelectColor)))
                        ),
                        new XElement("Icons",
                            new XAttribute("Width", radialIconRect.Width),
                            new XAttribute("Height", radialIconRect.Height)
                        ),
                        new XElement("CenterImage",
                            new XAttribute("Width", radialCenterImageRect.Width),
                            new XAttribute("Height", radialCenterImageRect.Height)
                        )
                    )
                )
            );

            configXML.Save(Directory.GetCurrentDirectory() + "\\config.xml");            
        }
    }
}
