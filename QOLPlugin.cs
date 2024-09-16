using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using UnityEngine.SceneManagement;

namespace OnirismQOL;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInProcess("Onirism.exe")]
public class QOLPlugin : BaseUnityPlugin
{
    public static ConfigEntry<float> zoom;
    public static QOLPlugin Instance { get; private set; }

    Harmony harmony;

    void Awake()
    {
        Instance = this;
        var configDescription = new ConfigDescription(
            "Camera zoom value", 
            new AcceptableValueRange<float>(1.0f, 5.0f), 
            null);
        zoom = this.Config.Bind<float>(
            "Settings",
            "zoom",
            2.0f,
            configDescription);
        
        harmony = new("QOLHarmony");
        harmony.PatchAll();

        SceneManager.sceneLoaded += HandleSceneLoad;
        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
    }

    void HandleSceneLoad(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == "Main_menu_new") return;
        if (scene.name == "INTRO_CUTSCENES") return;

        Console.WriteLine("PlaceplayersPostfix");
        StartCoroutine(ZoomPatches.SetZoom(zoom.Value));
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= HandleSceneLoad;
        harmony?.UnpatchSelf();
    }
}
