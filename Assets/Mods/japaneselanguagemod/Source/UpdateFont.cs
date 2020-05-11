using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DWS.Common.Resources;
using UBOAT.ModAPI;
using UBOAT.ModAPI.Core.InjectionFramework;
using System.IO;
using UBOAT.Game.Core.Mods;
using UBOAT.Game.Core.Data;

public class UpdateFont : IUserMod {

    [Inject] private static ResourceManager resourceManager;
    [Inject] private static GameData gameData;
    [Inject] private static ModManager modManager;
    [Inject] private static Locale locale;

    public void OnLoaded()
    {
        Debug.Log("Japanese Laungage Mod Loaded!");

        // Get Translation File Path
        // Mod mod = modManager.GetMod("JapanLanguageMod");
        // gameData.LoadSpreadsheet(Path.Combine(mod.Path, "Data Sheets/Locales.xlsx"));

        // Update Translation Data Base
        // locale.AwakeSingleton();

        // Add FontUpdater Instans
        // var fontUpdaterPrefab = resourceManager.RetrieveAsset<GameObject>("UI/JapaneseUI/FontUpdater", this);
        var fontUpdaterPrefab = new GameObject("FontUpdater");
        fontUpdaterPrefab.AddComponent<FontUpdater>();
        
        resourceManager.Instantiate(fontUpdaterPrefab);
    }
}
