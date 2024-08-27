using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UIElements.Experimental;

public class GameCore : MonoBehaviour
{
    static public CampSystem Camp;
    public SaveSystem saveSystem = new SaveSystem();


    private void Start()
    {
        Load();
    }

    public void Save()
    {
        saveSystem.savePlayerFile();
    }
    public void Load()
    {
        saveSystem.loadPlayerFile();
    }
}


public class Mercenaries
{
    //角色基礎屬性
    public float level = 0;
    public float MaxLevel = 40;
    public float exp = 0;
    public float nextLevelRequireExp = 10;

    public float health = 10;
    public float maxHealth = 10;

    //角色裝備
    public Gear HeadGear;//頭部裝備
    public Gear ChestGear;//胸部裝備
    public Gear FootGear;//腳部裝備
    public Gear tokenGear;//信物


    //角色職業技能
    public float characterPropotyPoints = 0;
    public float characterSkillPoints = 2; //初始擁有2

    //角色四大屬性
    /// <summary>
    /// 血肉 體能 (進戰攻擊 - 血量 - 負重)
    /// 靈魂 智慧 (乙太攻擊 - 智慧相關屬性)
    /// 順熵 敏捷 (玩家速度 - 火槍傷害 - 暴擊傷害)
    /// 業力 精神 (補血量 - 醫院工作效率)
    /// </summary>
    public float characterStrength = 0;
    public float characterEther = 0;
    public float characterAgility = 0;
    public float characterMentle = 0;

    //遊戲內參數 
    public float violentEnergyRadius = 2;
    public float gameSpeed = 0;//遊戲內速度 用於計算
    public float violentEnergyPointws = 0;

}

public class SaveSystem
{
    public void savePlayerFile()
    {
        Debug.Log("SavingPlayerFiles");
    }
    public void loadPlayerFile()
    {
        Debug.Log("LoadingPlayerFiles");
    }
}

public class Gear
{
    public int GearType = 0; //0 = defult 1=頭部 2=身體 3=腳部 4=信物

    public float durability = 100;
    public float MaxDurability = 100;

    public float gearAdditionCharacterStrength = 0;
    public float gearAdditionCharacterEther = 0;
    public float gearAdditionCharacterAgility = 0;
    public float gearAdditionCharacterMentle = 0;
    public void gearFunction()
    {

    }
}

public class Item
{
    public float ItemType = 0;
    ///
    /// 0 = Defult 無用途
    /// 1 = 攻擊性武器
    ///
    public float ItemSort = -1;// -1 =無效道具

    public float ItemAmount = 0;
    public float ItemMaxmentStackAmount = 64;
}

public class weapon
{
    public float weaponName;
    public float weaponBasicDamage = 0;
    public string weaponType = "Defult Weapon Type";
}

public class ViolentEnergyGear
{
    public float violentGearType = 0;
    public int violentGearSort = 0;
    public float violentGearRadius = 0;
    public float violentGearFunctionNumber = 0;

    //遊戲開始後 依照type/sort 給予按鈕dalegate
    public void propotyGive()
    {

    }
    public void sniper()
    {

    }
}

public class chunk
{

}