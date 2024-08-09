using HarmonyLib;

namespace Blasphemous.AtriumOfAtonement.Levels;

/// <summary>
/// Makes ModInteractable activate when being interacted
/// </summary>
[HarmonyPatch(typeof(CustomInteraction), "OnUse")]
class CustomInteraction_OnUse_ModInteractable_Patch
{
    public static void Postfix(CustomInteraction __instance)
    {
        var interactable = __instance.GetComponent<ModInteractable>();
        if (interactable != null)
            interactable.Interact();
    }
}
