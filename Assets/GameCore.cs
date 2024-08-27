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
    //�����¦�ݩ�
    public float level = 0;
    public float MaxLevel = 40;
    public float exp = 0;
    public float nextLevelRequireExp = 10;

    public float health = 10;
    public float maxHealth = 10;

    //����˳�
    public Gear HeadGear;//�Y���˳�
    public Gear ChestGear;//�ݳ��˳�
    public Gear FootGear;//�}���˳�
    public Gear tokenGear;//�H��


    //����¾�~�ޯ�
    public float characterPropotyPoints = 0;
    public float characterSkillPoints = 2; //��l�֦�2

    //����|�j�ݩ�
    /// <summary>
    /// ��� ��� (�i�ԧ��� - ��q - �t��)
    /// �F�� ���z (�A�ӧ��� - ���z�����ݩ�)
    /// ���i �ӱ� (���a�t�� - ���j�ˮ` - �����ˮ`)
    /// �~�O �믫 (�ɦ�q - ��|�u�@�Ĳv)
    /// </summary>
    public float characterStrength = 0;
    public float characterEther = 0;
    public float characterAgility = 0;
    public float characterMentle = 0;

    //�C�����Ѽ� 
    public float violentEnergyRadius = 2;
    public float gameSpeed = 0;//�C�����t�� �Ω�p��
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
    public int GearType = 0; //0 = defult 1=�Y�� 2=���� 3=�}�� 4=�H��

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
    /// 0 = Defult �L�γ~
    /// 1 = �����ʪZ��
    ///
    public float ItemSort = -1;// -1 =�L�ĹD��

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

    //�C���}�l�� �̷�type/sort �������sdalegate
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