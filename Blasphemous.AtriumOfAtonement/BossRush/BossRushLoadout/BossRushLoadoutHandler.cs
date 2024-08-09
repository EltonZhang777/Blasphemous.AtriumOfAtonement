using Blasphemous.ModdingAPI;
using Framework.Managers;
using System.Collections.Generic;
using System.Linq;

namespace Blasphemous.AtriumOfAtonement.BossRush.BossRushLoadout;

/// <summary>
/// Contains all the loadout information in BossRush
/// </summary>
public class BossRushLoadoutHandler
{
    public List<BossRushLoadoutData> loadoutDatas = new();
    private BossRushLoadoutHandler_Config _config;

    /// <summary>
    /// BossRush loadouts are only accessible inside BossRush hub
    /// </summary>
    public bool isAccessible
    {
        get
        {
            bool result = Core.GameModeManager.GetCurrentGameMode() == GameModeManager.GAME_MODES.BOSS_RUSH
                && Core.LevelManager.currentLevel.LevelName.Equals("D22Z01S00");
            if (!result)
            {
                ModLog.Warn("Attempted access of BossRush loadout outside of bossrush mode");
            }
            return result;
        }
    }

    public BossRushLoadoutHandler(Config config)
    {
        _config = config.BossRushLoadoutHandler;
        loadoutDatas = [.. Enumerable.Repeat(new BossRushLoadoutData(), _config.maxLoadoutSlots)];
    }

    /// <summary>
    /// Save the current state of TPO to the specified loadout slot
    /// </summary>
    public void SaveCurrentStateToSlot(int slotNumber)
    {
        if (!IsSlotIndexInbounds(slotNumber)) return;

        loadoutDatas[slotNumber].health = Core.Logic.Penitent.Stats.Life.Current;
        loadoutDatas[slotNumber].fervour = Core.Logic.Penitent.Stats.Fervour.Current;
        loadoutDatas[slotNumber].flaskCount = (int)Core.Logic.Penitent.Stats.Flask.Current;
        loadoutDatas[slotNumber].beads = new List<string>();
        for (int i = 0; i < 8; i++)
        {
            loadoutDatas[slotNumber].beads.Add(Core.InventoryManager.GetRosaryBeadInSlot(i).id);
        }
        loadoutDatas[slotNumber].swordHeart = Core.InventoryManager.GetSwordInSlot(0).id;
        loadoutDatas[slotNumber].prayer = Core.InventoryManager.GetPrayerInSlot(0).id;
    }

    public void SaveCurrentStateToLoadout(ref BossRushLoadoutData loadoutData)
    {
        loadoutData.health = Core.Logic.Penitent.Stats.Life.Current;
        loadoutData.fervour = Core.Logic.Penitent.Stats.Fervour.Current;
        loadoutData.flaskCount = (int)Core.Logic.Penitent.Stats.Flask.Current;
        loadoutData.beads = new List<string>();
        for (int i = 0; i < 8; i++)
        {
            loadoutData.beads.Add(Core.InventoryManager.GetRosaryBeadInSlot(i).id);
        }
        loadoutData.swordHeart = Core.InventoryManager.GetSwordInSlot(0).id;
        loadoutData.prayer = Core.InventoryManager.GetPrayerInSlot(0).id;
    }

    public void LoadSlotToCurrentState(int slotNumber)
    {
        if (!IsSlotIndexInbounds(slotNumber)) return;
        LoadLoadoutToCurrentState(loadoutDatas[slotNumber]);
    }

    public void LoadLoadoutToCurrentState(BossRushLoadoutData loadoutData)
    {
        Core.Logic.Penitent.Stats.Life.Current = loadoutData.health;
        Core.Logic.Penitent.Stats.Fervour.Current = loadoutData.fervour;
        Core.Logic.Penitent.Stats.Flask.Current = loadoutData.flaskCount;
        for (int i = 0; i < 8; i++)
        {
            Core.InventoryManager.SetRosaryBeadInSlot(i, loadoutData.beads[i]);
        }
        Core.InventoryManager.SetSwordInSlot(0, loadoutData.swordHeart);
        Core.InventoryManager.SetPrayerInSlot(0, loadoutData.prayer);

    }

    public bool IsSlotIndexInbounds(int slotNumber)
    {
        if (!(slotNumber >= 0 && slotNumber < loadoutDatas.Count))
        {
            ModLog.Error($"Slot index {slotNumber} out of bounds! Choose from slot 0 to slot {loadoutDatas.Count - 1}");
            return false;
        }
        return true;
    }
}

public class BossRushLoadoutHandler_Config
{
    public int maxLoadoutSlots = 10;
}