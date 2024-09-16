using HarmonyLib;

namespace OnirismQOL;
public static class CarolControllerPatch
{
    public record KeyChanges
    {
        public bool DiveInputDown;
        public bool RollInputUp;
        public bool DodgeInputDown;
        public bool RollInputDown;
        public bool JumpInputDown;
        public bool SprintInputDown;
        public bool SneakInputDown;
    }

    [HarmonyPatch(typeof(CarolController), "Update")]
    public static class UpdatePatch
    {
        [HarmonyPrefix()]
        public static void Prefix(CarolController __instance, ref KeyChanges __state)
        {
            if (__instance.virtualPlayer) return;

            //store the values from last frame before we read the new inputs
            __state = new()
            {
                DiveInputDown = __instance.diveInputDown,
                RollInputUp = __instance.rollInputUp,
                DodgeInputDown = __instance.dodgeInputDown,
                RollInputDown = __instance.rollInputDown,
                JumpInputDown = __instance.jumpInputDown,
                SprintInputDown = __instance.sprintInputDown,
                SneakInputDown = __instance.sprintInputDown,
            };
            //Harmony passes __state variables between Prefix and Postfix
        }

        [HarmonyPostfix()]
        public static void Postfix(CarolController __instance, ref KeyChanges __state)
        {
            if (__instance.virtualPlayer) return;

            //if the value we stored or the value read this frame is true, save the value as true.
            __instance.diveInputDown |= __state.DiveInputDown;
            __instance.rollInputUp |= __state.RollInputUp;
            __instance.dodgeInputDown |= __state.DodgeInputDown;
            __instance.rollInputDown |= __state.RollInputDown;
            __instance.jumpInputDown |= __state.JumpInputDown;
            __instance.sprintInputDown |= __state.SprintInputDown;
            __instance.sneakInputDown |= __state.SneakInputDown;
            //Note to the devs:
            //To fix this problem directly,
            //OR the values read in CarolController.Update() with the previous frame's values
            //Then set them to false at the end of CarolController.FixedUpdate()
            //this.diveInputDown |= InputManager.manager.GetInputDown(this.playerNumber, InputManager.InputType.Dive);
        }
    }

    [HarmonyPatch(typeof(CarolController), "FixedUpdate")]
    public static class FixedUpdatePatch
    {
        [HarmonyPostfix]
        public static void Postfix(CarolController __instance)
        {
            if (__instance.virtualPlayer) return;

            //after FixedUpdate has run, unlatch the keypress values
            __instance.diveInputDown = false;
            __instance.rollInputUp = false;
            __instance.dodgeInputDown = false;
            __instance.rollInputDown = false;
            __instance.jumpInputDown = false;
            __instance.sprintInputDown = false;
            __instance.sneakInputDown = false;
        }
    }
}
