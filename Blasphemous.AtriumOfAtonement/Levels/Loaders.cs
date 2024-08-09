using Blasphemous.Framework.Levels.Loaders;
using System.Collections;
using UnityEngine;

namespace Blasphemous.AtriumOfAtonement.Levels;

/// <summary>
/// Object loader for interactable component
/// </summary>
public class InteractableLoader : ILoader
{
    public GameObject Result { get; private set; }

    public enum InteractableType
    {
        Dialogue,
        UI
    };

    private InteractableType _currentInteractableType { get; set; }

    public InteractableLoader(InteractableType type)
    {
        _currentInteractableType = type;
    }

    public IEnumerator Apply()
    {
        var loader = new SceneLoader("D05Z01S23_LOGIC", "LOGIC/ACT_CorpseDLC");
        yield return loader.Apply();

        var obj = loader.Result;
        switch (_currentInteractableType)
        {
            case InteractableType.Dialogue:
                obj.transform.GetChild(2).gameObject.AddComponent<ModInteractableWithDialogue>();
                break;
            case InteractableType.UI:
                obj.transform.GetChild(2).gameObject.AddComponent<ModInteractableWithUI>();
                break;
        }


        Object.Destroy(obj.transform.GetChild(1).gameObject);
        Object.Destroy(obj.transform.GetChild(0).gameObject);
        Object.Destroy(obj.GetComponent<PlayMakerFSM>());

        Result = obj;
        yield break;
    }
}
