using System;
using Harmony;
using TMPro;
using UnityEngine;
using UBOAT.Game.Scene.UI;
using DWS.Common.Resources;
using UBOAT.ModAPI.Core.InjectionFramework;
using UBOAT.ModAPI;

public class JapaneseLocalization : IUserMod
{
    public void OnLoaded()
    {
        Debug.Log("Japanese Laungage Mod(ver2.0) Loaded!!");
        // Harmony パッチの適用
        var harmonry = HarmonyInstance.Create("net.ipipip0129.jlm-path");
        harmonry.PatchAll();
    }
}

/**
 * 日本語化Harmonyパッチ
 * 事前準備 -> Manifest.jsonにHarmonyライブラリを使用することを明記(Permissionに"Reflection"を追加)
 * 日本語化手順 -> FallbackFont登録形式
 * ロード回数 -> LocalizedTextクラスが呼ばれる事
 * 問題点 -> LocalizedTextクラスを介さないテキストは適用漏れが起こることもある
 * 問題点について -> 今のところフォントの適用漏れはなさそう
 **/

[HarmonyPatch(typeof(LocalizedText))]
[HarmonyPatch("Start")]
class JapaneseLocalizationPath
{
    [Inject] private static ResourceManager resourceManager;

    static bool Prefix(LocalizedText __instance)
    {
        var afterFontAsset = resourceManager.RetrieveAsset<TMP_FontAsset>("UI/JapaneseUI/cinecaption226 SDF", __instance);
        // Debug.Log("Testing" + __instance.name);

        TextMeshProUGUI tmp = __instance.GetComponent<TextMeshProUGUI>();
        if (tmp != null && tmp.font != afterFontAsset)
        {
            if (!tmp.font.fallbackFontAssets.Contains(afterFontAsset))
            {
                // 最優先で登録
                tmp.font.fallbackFontAssets.Insert(0, afterFontAsset);
                tmp.UpdateFontAsset();
            }
        }

        tmp.font.fallbackFontAssets.Add(afterFontAsset);

        TMP_SubMeshUI tMP_Sub = __instance.GetComponent<TMP_SubMeshUI>();
        if (tMP_Sub != null && tMP_Sub.fontAsset != afterFontAsset)
        {
            if (!tMP_Sub.fontAsset.fallbackFontAssets.Contains(afterFontAsset))
            {
                tMP_Sub.fontAsset.fallbackFontAssets.Insert(0, afterFontAsset);
            }
        }

        return true;
    }
}
