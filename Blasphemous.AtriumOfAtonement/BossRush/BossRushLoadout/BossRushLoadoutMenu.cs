// WIP

/*
using Blasphemous.Framework.Menus;
using Blasphemous.Framework.Menus.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Blasphemous.Framework.UI;
using UnityEngine.UI;
using Framework.FrameworkCore.Attributes;

namespace Blasphemous.AtriumOfAtonement.BossRush;

/// <summary>
/// Menu that allows save/load custom loadouts at the start of BossRush
/// </summary>
public class BossRushLoadoutMenu : ModMenu
{
    protected override int Priority { get; } = (int)1e3;

    private ArrowOption _slot;
    private ArrowOption _action;
    private Text _titleText;
    private Text _loadoutText;

    public List<BossRushLoadoutSaveStateData> saveStateDatas;

    protected override void CreateUI(Transform ui)
    {
        ArrowCreator arrow = new(this);
        TextCreator text = new(this) 
        {
            TextSize = 50,
            LineSize = 200
        };

        _titleText = UIModder.Create(new RectCreationOptions()
        {
            Name = "title",
            Parent = ui,
            XRange = Vector2.zero,
            YRange = Vector2.one,
            Pivot = new Vector2(0, 1),
            Position = new Vector2(0, 200),
            Size = new Vector2(250, 250)
        }).AddText(new TextCreationOptions()
        {
            Contents = Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.title"),
            Font = UIModder.Fonts.Blasphemous
        });

        _slot = arrow.CreateOption(
            "Loadout slot selection", 
            ui, 
            new Vector2(0, 100),
            Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.current_slot"), 
            Enumerable.Range(1, 3).Select(x => Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.slot") + x.ToString()).ToArray());

        _loadoutText = UIModder.Create(new RectCreationOptions()
        {
            Name = "loadout text",
            Parent = ui,
            XRange = Vector2.zero,
            YRange = Vector2.one,
            Pivot = new Vector2(0, 1),
            Position = new Vector2(0, 50),
            Size = new Vector2(250, 250)
        }).AddText(new TextCreationOptions()
        {
            Font = UIModder.Fonts.Blasphemous
        });
        RefreshLoadoutTexts();

        _action = arrow.CreateOption(
            "Action selection",
            ui,
            new Vector2(0, -200),
            "",
            new string[]
            {
                Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.action_save"),
                Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.action_load"),
                Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.action_auto_load")
            });
    }

    public override void OnOptionsChanged()
    {
        base.OnOptionsChanged();
        RefreshLoadoutTexts();
    }

    private void RefreshLoadoutTexts()
    {
        var currentSaveState = saveStateDatas[_slot.CurrentOption];
        _loadoutText.text = Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.health") + currentSaveState.health.ToString() + "  "
            + Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.fervour") + currentSaveState.fervour.ToString() + "  "
            + Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.flask_count") + currentSaveState.flaskCount.ToString() + "\n"
            + Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.beads");
        foreach (string bead in currentSaveState.beads)
        {
            _loadoutText.text += " " + bead;
        }
        _loadoutText.text += "\n" + Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.sword_heart") + currentSaveState.swordHeart + "  "
            + Main.AtriumOfAtonement.LocalizationHandler.Localize("BossRushLoadoutMenu.prayer") + currentSaveState.prayer;
    }
}

*/