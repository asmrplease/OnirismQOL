using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace OnirismQOL;
public class OnScreenDebug : MonoBehaviour
{
    static Text debugText;
    static CarolController controller;

    void Awake()
    {
        debugText = Hud.hud
            .transform
            .Find("Canvas/SneakHUD/DETECTION")
            .GetComponent<Text>();
        debugText.gameObject.SetActive(true);

        controller = Entity.players[0].carolController;
    }

}
