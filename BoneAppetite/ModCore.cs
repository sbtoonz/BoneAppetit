using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using ItemManager;
using PieceManager;
using ServerSync;
using UnityEngine;
using CraftingTable = PieceManager.CraftingTable;

namespace BoneAppetite
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class BoneAppetitMod : BaseUnityPlugin
    {
        internal const string ModName = "BoneAppetit";
        internal const string ModVersion = "3.2.4";
        private const string ModGUID = "com.rockerkitten.boneappetit";
        private static Harmony harmony = null!;
        ConfigSync configSync = new(ModGUID) 
            { DisplayName = ModName, CurrentVersion = ModVersion, MinimumRequiredVersion = ModVersion};
        internal static ConfigEntry<bool> ServerConfigLocked = null!;
        ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description, bool synchronizedSetting = true)
        {
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = configSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }
        ConfigEntry<T> config<T>(string group, string name, T value, string description, bool synchronizedSetting = true) => config(group, name, value, new ConfigDescription(description), synchronizedSetting);
        public void Awake()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            harmony = new(ModGUID);
            harmony.PatchAll(assembly);
            ServerConfigLocked = config("1 - General", "Lock Configuration", true, "If on, the configuration is locked and can be changed by server admins only.");
            configSync.AddLockingConfigEntry(ServerConfigLocked);

            var grillbundle = Utilities.LoadAssetBundle("grill");
            var assetList = grillbundle!.LoadAllAssets<GameObject>();
            foreach (var GO in assetList)
            {
                if (GO.GetComponent<Piece>() != null)
                {
                    BuildPiece piece = new BuildPiece(GO.name);
                    piece.Name.English(Localization.instance.Localize(GO.GetComponent<Piece>().name));
                    piece.Description.English("");
                    piece.RequiredItems.Add("Wood", 1, true);
                    piece.Category.Add(BuildPieceCategory.Misc);
                    piece.Crafting.Set(CraftingTable.None);
                }

                if (GO.GetComponent<ItemDrop>() != null)
                {
                    Item item = new Item(GO, false);
                    item.Name.English(
                        Localization.instance.Localize(GO.GetComponent<ItemDrop>().m_itemData.m_shared.m_name));
                    item.Description.English("");
                    item.Crafting.Add(ItemManager.CraftingTable.Inventory,0);
                    item.RequiredItems.Add("Wood", 1);
                }
            }
            grillbundle.Unload(false);

            var customfoodBundle = Utilities.LoadAssetBundle("customfood");
            assetList = customfoodBundle!.LoadAllAssets<GameObject>();
            foreach (var GO in assetList)
            {
                if (GO.GetComponent<Piece>() != null)
                {
                    BuildPiece piece = new BuildPiece(GO.name);
                    piece.Name.English(Localization.instance.Localize(GO.GetComponent<Piece>().name));
                    piece.Description.English("");
                    piece.RequiredItems.Add("Wood", 1, true);
                    piece.Category.Add(BuildPieceCategory.Misc);
                    piece.Crafting.Set(CraftingTable.None);
                }

                if (GO.GetComponent<ItemDrop>() != null)
                {
                    Item item = new Item(GO, false);
                    item.Name.English(
                        Localization.instance.Localize(GO.GetComponent<ItemDrop>().m_itemData.m_shared.m_name));
                    item.Description.English("");
                    item.Crafting.Add(ItemManager.CraftingTable.Inventory,0);
                    item.RequiredItems.Add("Wood", 1);
                }
            }
            customfoodBundle.Unload(false);
        }
    }
}
