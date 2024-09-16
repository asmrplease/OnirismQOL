using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OnirismQOL;
public static class ZoomPatches
{
    public static float GetZoom()
    {
        float defaultZoom = -1f;

        var controller = Camera.main.gameObject.GetComponent<CameraController>();
        if (!controller) return defaultZoom;

        return controller.zoom.zoom;
    }

    public static IEnumerator SetZoom(float zoom)
    {
        Console.WriteLine($"SetZoom({zoom})");
        yield return new WaitUntil(() => Camera.main);
        yield return new WaitUntil(() => CheckPoints.saving == false);

        var controller = Camera.main.gameObject.GetComponent<CameraController>();
        if (!controller) yield break;

        controller.zoom.zoom = zoom;
        Console.WriteLine($"SetZoom Complete. ({zoom})");
    }

    //scene switch patch
    [HarmonyPatch(typeof(GameManager), nameof(GameManager.LoadScene))]
    public static class LoadScenePatch
    {
        [HarmonyPrefix] public static void Prefix()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "Main_menu_new") return;
            if (sceneName == "INTRO_CUTSCENES") return;

            Console.WriteLine("LoadscenePrefix");
            float test = GetZoom();
            if (test == -1f) return;

            QOLPlugin.zoom.Value = test;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(CheckPoints), nameof(CheckPoints.PlayAnim))]
    static IEnumerator AnimWrapper(IEnumerator result)
    {
        Console.WriteLine("WrapperPatch Start!");
        while (result.MoveNext()) { yield return result.Current; }

        Console.WriteLine("WrapperPatch Executed!");
        QOLPlugin.Instance.StartCoroutine(SetZoom(QOLPlugin.zoom.Value));
    }

}
