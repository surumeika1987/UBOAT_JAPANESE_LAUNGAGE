using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DWS.Common.Resources;
using TMPro;
using UBOAT.ModAPI.Core.InjectionFramework;
using UnityEngine.SceneManagement;

public class FontUpdater : MonoBehaviour
{

    private TMP_FontAsset afterFontAsset;
    [Inject] private static ResourceManager resourceManager;

    private HashSet<Scene> sceneHash;
    private int frameCounter;

    // Use this for initialization
    void Start()
    {
        afterFontAsset = resourceManager.RetrieveAsset<TMP_FontAsset>("UI/JapaneseUI/cinecaption226 SDF", this);
        DontDestroyOnLoad(this);

        frameCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Improve Performance
        frameCounter++;
        
        if (1 <= frameCounter)
        {
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            {
                Scene scne = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
                foreach (GameObject obj in scne.GetRootGameObjects())
                {
                    UpdateFont(obj);
                    DoForAllunderobject(obj);
                }
            }
            frameCounter = 0;
        }
    }

    void DoForAllunderobject(GameObject obj)
    {
        if (obj != null)
        {
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                UpdateFont(obj.transform.GetChild(i).gameObject);
                DoForAllunderobject(obj.transform.GetChild(i).gameObject);
            }
        }
    }

    void UpdateFont(GameObject obj)
    {
        TextMeshProUGUI tmp = obj.GetComponent<TextMeshProUGUI>();
        // Debug.Log(obj.transform.GetChild(i).gameObject.name);
        if (tmp != null && tmp.font != afterFontAsset)
        {
            tmp.font = afterFontAsset;
            tmp.UpdateFontAsset();
        }

        TMP_SubMeshUI tMP_Sub = obj.GetComponent<TMP_SubMeshUI>();
        if (tMP_Sub != null && tMP_Sub.fontAsset != afterFontAsset)
        {
            tMP_Sub.fontAsset = afterFontAsset;
        }
    }
}