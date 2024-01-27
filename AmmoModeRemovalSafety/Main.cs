using HarmonyLib;
using InControl;
using SRML;
using SRML.Console;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using MonomiPark.SlimeRancher.DataModel;
using Console = SRML.Console.Console;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AmmoModeRemovalSafety
{
    public class Main : ModEntryPoint
    {
        internal static Assembly modAssembly = Assembly.GetExecutingAssembly();
        internal static string modName = $"{modAssembly.GetName().Name}";
        internal static string modDir = $"{System.Environment.CurrentDirectory}\\SRML\\Mods\\{modName}";
        public static Main instance;

        public Main() => instance = this;

        public override void PreLoad()
        {
            HarmonyInstance.PatchAll();
        }
        public static void Log(string message) => instance.ConsoleInstance.Log($"[{modName}]: " + message);
        public static void LogError(string message) => instance.ConsoleInstance.LogError($"[{modName}]: " + message);
        public static void LogWarning(string message) => instance.ConsoleInstance.LogWarning($"[{modName}]: " + message);
        public static void LogSuccess(string message) => instance.ConsoleInstance.LogSuccess($"[{modName}]: " + message);
    }
    [HarmonyPatch(typeof(PlayerModel),"Push")]
    static class Patch_PushPlayerModel
    {
        static void Prefix(PlayerModel __instance, Dictionary<PlayerState.AmmoMode, Ammo.Slot[]> ammoSlots)
        {
            foreach (var k in ammoSlots.Keys.ToArray())
                if (!__instance.ammoDict.ContainsKey(k))
                    ammoSlots.Remove(k);
        }
    }
}