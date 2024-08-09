using Blasphemous.Framework.Levels;
using Blasphemous.Framework.Levels.Modifiers;
using Gameplay.GameControllers.Entities;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Blasphemous.AtriumOfAtonement.Levels;

/// <summary>
/// Modifier for the Penitence Statue
/// </summary>
internal class PenitenceStatueModifier : IModifier
{
    public PenitenceStatueModifier() { }

    public void Apply(GameObject obj, ObjectData data)
    {
        obj.name = $"PenitenceStatue_{data.id}";
        GameObject interactable = obj.transform.Find("Interactable Animation").gameObject;
        interactable.SetActive(true);
        interactable.GetComponent<BoxCollider2D>().enabled = true;
        interactable.GetComponent<BoxCollider2D>().isTrigger = true;
        interactable.GetComponent<CollisionSensor>().enabled = true;
        interactable.GetComponent<Animator>().enabled = true;
        GameObject lights = obj.transform.Find("AltarLamps").gameObject;
        lights.SetActive(true);
    }
}

/// <summary>
/// Modifier for objects with a custom static sprite (from mod files) 
/// and therefore needs disabling (if any) original sprites attached. 
/// File path for the sprite is passed as "file_path" argument in "properties" 
/// of the ObjectData in the level's JSON file.
/// </summary>
internal class StaticSpriteModifier : ModifierWithProperties, IModifier
{

    public void Apply(GameObject obj, ObjectData data)
    {
        // get the file path of the custom sprite from the ObjectData's properties
        _validPropertyArguments = new()
        {
            { "offset", x => Regex.IsMatch(x, "\\(\\s?\\d+\\s?,\\s?\\d+\\s?\\)") }
        };
        _defaultPropertyArguments = new()
        {
            { "offset", "(0, 0)" }
        };
        _properties = UnzipProperties(data.properties);
        string filePath = _properties["file_path"];

        // offset is stored in format of string "(123 , 456)", unzip it
        string offsetString = _properties["offset"];
        int commaIndex = offsetString.IndexOf(',');
        Vector2 offset = new(
            int.Parse(offsetString.Substring(1, commaIndex - 1).Trim()),
            int.Parse(offsetString.Substring(commaIndex + 1, offsetString.Length - commaIndex - 2).Trim()));

        // destroys all original SpriteRenderers first
        SpriteRenderer[] spriteRenderers = obj.GetComponents<SpriteRenderer>();
        foreach (var elem in spriteRenderers)
        {
            UnityEngine.Object.Destroy(elem);
        }

        // Load the custom sprite
        Main.AtriumOfAtonement.FileHandler.LoadDataAsSprite(
            filePath,
            out Sprite customSprite,
            new ModdingAPI.Files.SpriteImportOptions());
        var spriteObject = new GameObject($"sprite_of_{data.id}");
        spriteObject.transform.parent = obj.transform;
        spriteObject.AddComponent<SpriteRenderer>();
        var sr = spriteObject.GetComponent<SpriteRenderer>();
        sr.sprite = customSprite;
        spriteObject.transform.position = offset;
    }
}