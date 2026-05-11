using ShibaGTGenesis.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ShibaGTGenesis.Menu
{
    public class Settings
    {
        private static readonly string SaveFolder =
            Path.Combine(Application.persistentDataPath, "Genesis");

        private static readonly string PrefsFile =
            Path.Combine(SaveFolder, "preferences.txt");

        private static readonly string SaveFile =
            Path.Combine(SaveFolder, "enabledButtons.txt");

        public static void ChangeLayout(bool loading)
        {
            try
            {
                if (!loading)
                {
                    Menu.MenuLayout++;
                    if (Menu.MenuLayout > 3)
                        Menu.MenuLayout = 0;
                }
                ButtonInfo button = Menu.Instance?.GetButton("Change Menu Layout");
                switch (Menu.MenuLayout)
                {
                    case 0:
                        if (button != null)
                            button.buttonText = "Change Menu Layout: ShibaGT";
                        break;

                    case 1:
                        if (button != null)
                            button.buttonText = "Change Menu Layout: Side";
                        break;

                    case 2:
                        if (button != null)
                            button.buttonText = "Change Menu Layout: Trigger";
                        break;

                    case 3:
                        if (button != null)
                            button.buttonText = "Change Menu Layout: Bottom";
                        break;
                }

                if (button != null)
                    button.enabled = false;
                if (Menu.Instance != null)
                {
                    Menu.Instance.DestroyMenu();
                    Menu.Instance.Draw();
                }
            }
            catch (Exception ex)
            {
                MelonLoader.MelonLogger.Error($"[GENESIS] ChangeLayout Error:\n{ex}");
            }
        }
        public static void ChangeMenuTheme(bool loading)
        {
            try
            {
                if (!loading)
                {
                    Menu.Theme++;
                    if (Menu.Theme > 1)
                        Menu.Theme = 0;
                }

                ButtonInfo button = Menu.Instance?.GetButton("Change Menu Theme");
                switch (Menu.Theme)
                {
                    case 0:
                        if (button != null)
                            button.buttonText = "Change Menu Theme: Dark";
                            Menu.Instance.buttoncolor = new Color(0.22f, 0.22f, 0.22f, 1f);
                            Menu.Instance.disconnectandpagebuttoncolor = Color.black;
                            Menu.Instance.menucolor = Color.black;
                            Menu.Instance.textoffcolor = Color.black;
                            Menu.Instance.textoncolor = Color.white;
                        break;
                    case 1:
                        if (button != null)
                            button.buttonText = "Change Menu Theme: Genesis";
                            Menu.Instance.buttoncolor = new Color32(0, 0, 235, 255);
                            Menu.Instance.disconnectandpagebuttoncolor = new Color32(0, 0, 235, 255);
                            Menu.Instance.menucolor = new Color32(0, 0, 210, 255);
                            Menu.Instance.textoffcolor = Color.black;
                            Menu.Instance.textoncolor = Color.white;
                        break;
                }
                if (button != null)
                    button.enabled = false;
                if (Menu.Instance != null)
                {
                    Menu.Instance.DestroyMenu();
                    Menu.Instance.Draw();
                }
            }
            catch (Exception ex)
            {
                MelonLoader.MelonLogger.Error($"[GENESIS] ChangeMenuTheme Error:\n{ex}");
            }
        }

        public static void ChangeSpeedboost(bool loading)
        {
            try
            {
                if (!loading)
                {
                    Menu.Speed++;
                    if (Menu.Speed > 2)
                        Menu.Speed = 0;
                }
                ButtonInfo button = Menu.Instance?.GetButton("Change Speed Boost");
                switch (Menu.Speed)
                {
                    case 0:
                        if (button != null)
                            button.buttonText = "Change Speed Boost: Mosa";
                        break;
                    case 1:
                        if (button != null)
                            button.buttonText = "Change Speed Boost: Super";
                        break;
                    case 2:
                        if (button != null)
                            button.buttonText = "Change Speed Boost: Insane";
                        break;
                }
                if (button != null)
                    button.enabled = false;
                if (Menu.Instance != null)
                {
                    Menu.Instance.DestroyMenu();
                    Menu.Instance.Draw();
                }
            }
            catch (Exception ex)
            {
                MelonLoader.MelonLogger.Error($"[GENESIS] ChangeSpeedboost Error:\n{ex}");
            }
        }

        public static void SavePreferences()
        {
            try
            {
                if (!Directory.Exists(SaveFolder))
                    Directory.CreateDirectory(SaveFolder);
                List<string> lines = new List<string>
                {
                    $"MenuLayout={Menu.MenuLayout}",
                    $"Theme={Menu.Theme}",
                    $"Speed={Menu.Speed}"
                };

                File.WriteAllLines(PrefsFile, lines);
                SaveEnabledButtons();
            }
            catch (Exception ex)
            {
                MelonLoader.MelonLogger.Error($"[GENESIS] SavePreferences Error:\n{ex}");
            }
        }

        public static void LoadPreferences()
        {
            try
            {
                if (!File.Exists(PrefsFile))
                    return;
                string[] lines = File.ReadAllLines(PrefsFile);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    string[] split = line.Split('=');
                    if (split.Length < 2)
                        continue;
                    string key = split[0];
                    string value = split[1];
                    switch (key)
                    {
                        case "MenuLayout":
                            int.TryParse(value, out Menu.MenuLayout);
                            break;
                        case "Theme":
                            int.TryParse(value, out Menu.Theme);
                            break;
                        case "Speed":
                            int.TryParse(value, out Menu.Speed);
                            break;
                    }
                }

                ChangeLayout(true);
                ChangeMenuTheme(true);
                ChangeSpeedboost(true);
                LoadEnabledButtons();
            }
            catch (Exception ex)
            {
                MelonLoader.MelonLogger.Error($"[GENESIS] LoadPreferences Error:\n{ex}");
            }
        }

        public static void SaveEnabledButtons()
        {
            try
            {
                if (!Directory.Exists(SaveFolder))
                    Directory.CreateDirectory(SaveFolder);
                List<string> enabledButtons = new List<string>();
                foreach (ButtonInfo[] info in Buttons.buttons)
                {
                    foreach (ButtonInfo button in info)
                    {
                        if (button.enabled)
                            enabledButtons.Add(button.buttonText);
                    }
                }
                File.WriteAllText(SaveFile, string.Join(";;", enabledButtons));
            }
            catch (Exception ex)
            {
                MelonLoader.MelonLogger.Error($"[GENESIS] SaveEnabledButtons Error:\n{ex}");
            }
        }
        public static void LoadEnabledButtons()
        {
            try
            {
                if (!File.Exists(SaveFile))
                    return;
                string data = File.ReadAllText(SaveFile);
                if (string.IsNullOrWhiteSpace(data))
                    return;
                string[] enabledButtons = data.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);
                HashSet<string> enabledSet = new HashSet<string>(enabledButtons);
                foreach (ButtonInfo[] info in Buttons.buttons)
                {
                    foreach (ButtonInfo button in info)
                    {
                        if (enabledSet.Contains(button.buttonText))
                        {
                            if (!button.enabled)
                            {
                                button.enabled = true;
                                try
                                {
                                    button.enableMethod?.Invoke();
                                    if (button.enableMethod == null)
                                        button.method?.Invoke();
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MelonLoader.MelonLogger.Error($"[GENESIS] LoadEnabledButtons Error:\n{ex}");
            }
        }
    }
}