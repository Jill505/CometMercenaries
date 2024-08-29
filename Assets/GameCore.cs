using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UIElements.Experimental;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor.ShaderGraph.Internal;

public class GameCore : MonoBehaviour
{
    static public string campSyssSavePath;

    static public CampSystem Camp = new CampSystem();
    static public SaveSystem saveSystem = new SaveSystem();

    public GameObject defultWeaponStore;

    private void Awake()
    {
        //��l��
        campSyssSavePath = Application.persistentDataPath + "/saveFiles.json";

        Camp = new CampSystem();
        Camp.worldMapNodeList = new List<Node>();
        Camp.MercenariesList = new List<Mercenaries>();
        Camp.tempWorldMapNodeList = Camp.worldMapNodeList.ToArray();
        Camp.tempMercenariesList = Camp.MercenariesList.ToArray();

        Debug.Log("������l��");

        int testNum = GameCore.Camp.MercenariesList.Count;
        Debug.Log(testNum);


        Load();
    }

    private void Start()
    {
    }

    private void Update()
    {
        string swapEdit = "�J�ԡG"+ Camp.Kroan +"\n�������G"+ Camp.C_Coin + "\n�n��G"+Camp.popularity + "\n";
        testCodeInfo.text = swapEdit; 
    }

    static public void Save()
    {
        saveSystem.savePlayerFile();
    }
    static public void Load()
    {
        saveSystem.loadPlayerFile();
    }

    //�H�U�����եΥN�X ���Ω󥿦��C��
    public Text testCodeInfo;
    public void testCodeGainKroan()
    {
        Camp.Kroan += 30f;
        Save();
    }
}


[System.Serializable]
public class Mercenaries
{
    public string characterName = "Defult Name";
    //�����¦�ݩ�
    public float level = 0;
    public float MaxLevel = 40;
    public float weightCapacity = 10;
    public float exp = 0;
    public float nextLevelRequireExp = 10;
    public float expertExp = 0;

    public float health = 10;
    public float maxHealth = 10;
    public float speed = 20;

    public float physicDefend = 0;
    public float etherDefend = 0;

    //����Z��
    public weapon mainWeapon;
    public weapon secondaryWeapon;

    //����˳�
    public Gear HeadGear;//�Y���˳�
    public Gear ChestGear;//�ݳ��˳�
    public Gear FootGear;//�}���˳�
    public Gear tokenGear;//�H��


    //����¾�~�ޯ�
    public float characterPropotyPoints = 4;
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

    public delegate void voidDelegate();
    public voidDelegate gearCalDeleate; 

    public void calculateGearAddition()
    {
        gearCalDeleate = null;

        //�I�s�˳ƭq�\�p�⤺�e
        //gearCalDeleate += Gear.Function or what else bla bla bla

        gearCalDeleate();
    }

    public void calculateMaxHealth()
    {
        //��q�����G 50+����O�q*0.8+�˳Ʀ�q+�ޯ�[��
        maxHealth = (int)(50 +characterStrength * 0.8f);
    }

    public void calculateSpeed()
    {
        //�t�פ����G 80+����t��+�˳Ƴt��+
        gameSpeed = (int)(80 + speed);
    }

    public void calculatePhysicDefend()
    {
        physicDefend = (int)(0 + characterStrength * 0.5f + characterAgility * 0.3f + characterMentle * 0.2f);
    }

    public void calculateEtherDefend()
    {
        etherDefend = (int)(0 + characterEther * 0.2f + characterAgility * 0.015f * characterMentle * 0.15f);
    }
    public void calculateCharacterWeightCapacity()
    {
        weightCapacity = (int)(30 + characterStrength * 0.8f + characterAgility * 0.2f + characterMentle * 0.3f);
    }


    public void GainExp(int expNumber)
    {
        if (level <= MaxLevel)
        {
            exp += expNumber;
            while (exp > nextLevelRequireExp)
            {
                exp -= nextLevelRequireExp;
                LevelUp();
            }
        }
        else
        {
            expertExp += expNumber;
        }
    }
    public void LevelUp()
    {
        if (level % 2 == 0)
        {
            characterSkillPoints += 1;
        }

        characterPropotyPoints += 4;

        level++;
    }

}

[System.Serializable]
public class SaveSystem
{
    public void savePlayerFile()
    {
        GameCore.Camp.syncTempData_ForSave();

        CampSystem tempCamp = new CampSystem();

        tempCamp.C_Coin = GameCore.Camp.C_Coin;
        tempCamp.Kroan = GameCore.Camp.Kroan;
        tempCamp.popularity = GameCore.Camp.popularity;

        tempCamp.tempMercenariesList = GameCore.Camp.tempMercenariesList;
        tempCamp.tempWorldMapNodeList = GameCore.Camp.tempWorldMapNodeList;

        tempCamp.tempWeaponStorehouseList = GameCore.Camp.tempWeaponStorehouseList;

        string saveFiles = JsonUtility.ToJson(tempCamp);
        File.WriteAllText(GameCore.campSyssSavePath,saveFiles);

        Debug.Log("SavingPlayerFiles");
    }
    public void loadPlayerFile()
    {
        Debug.Log("LoadingPlayerFiles");
        if (File.Exists(GameCore.campSyssSavePath))
        {
            //load
            CampSystem swapCamp = JsonUtility.FromJson<CampSystem>(File.ReadAllText(GameCore.campSyssSavePath));

            GameCore.Camp.C_Coin = swapCamp.C_Coin;
            GameCore.Camp.Kroan = swapCamp.Kroan;
            GameCore.Camp.popularity = swapCamp.popularity;
            Debug.Log("�J�Լ�"+swapCamp.Kroan);

            if (swapCamp.tempMercenariesList != null)
            {
                GameCore.Camp.tempMercenariesList = swapCamp.tempMercenariesList;
                GameCore.Camp.MercenariesList = GameCore.Camp.tempMercenariesList.ToList<Mercenaries>();
            }
            else
            {
                GameCore.Camp.MercenariesList = new List<Mercenaries>();
            }

            if (swapCamp.tempWorldMapNodeList != null)
            {
                GameCore.Camp.tempWorldMapNodeList = swapCamp.tempWorldMapNodeList;
                GameCore.Camp.worldMapNodeList = GameCore.Camp.tempWorldMapNodeList.ToList<Node>();
            }
            else
            {
                GameCore.Camp.worldMapNodeList= new List<Node>();
            }

            if (swapCamp.tempWeaponStorehouseList != null)
            {
                GameCore.Camp.tempWeaponStorehouseList = swapCamp.tempWeaponStorehouseList;
                GameCore.Camp.weaponStorehouseList = GameCore.Camp.tempWeaponStorehouseList.ToList<weapon>();
            }
            else
            {
                GameCore.Camp.weaponStorehouseList = new List<weapon>();
            }
        }
        else
        {
            Debug.Log("File Not Exist not able to load LOL");
            Debug.Log("Create New Save File");

            GameCore.Camp = new CampSystem();

            savePlayerFile();
        }
    }
}

[System.Serializable]
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

[System.Serializable]
public class Item
{
    public string ItemType = "defult item type"; //�i�H�Q�Ω�l�u�������H �]�\�n�ظm�@��excel���
    public float ItemSort = -1;//�D��s�� �i�H�ֳt��d������Ʀ�m
    public string ItemImageName = "defultItemImage";
    public string ItemName = "defultItemName";

    public float ItemAmount = 0;

    public float eachWeightOfAItem = 0.1f;


}

    [System.Serializable]
    public class weapon
    {
        public string weaponName = "defult weapon name";
        public string weaponDescribtion = "no describtion";
        public Color weaponNameColor = Color.white;

        public int weaponRegisterNumber;//���U�ǦC
    
        public string weaponAmmoType = "noAmmoType";
        public string weaponType = "defultWeaponType";
        public string weaponImageName = "defultWeaponImage";
        public string weaponBuffsString = "";

        public float weaponRadius = 1f;//�Z���@�Υb�|

        public int weaponMagzineSize = -1;//-1����L����
        public int weaponMagazineCount = -1;//-1 ����L����

        public float weaponDamage;//�ݭn�B�~�p��

        public float weaponBasicDamage = 0;//��¦�ˮ`
        //�ƾڭ���
        public float weaponStrengthMutiply = 0f;
        public float weaponEtherMutiply = 0f;
        public float weaponAgilityMutiply = 0f;
        public float weaponMentleMutiply = 0f;

        public string weaponDamageFormulaTelling;
        public void calculateWeaponDamageFormulaTelling()
        {
            string printString = "";
            printString += "�Z���W�١G" + weaponName +weaponBuffsString + "\n";
            printString += "�Z�������G" + weaponType + "\n";

            if (weaponMagzineSize < 0)
            {
                printString += "���Z�����L��������";
            }
            else
            {
                printString += "�Z���l�u�ݨD�����G" + weaponAmmoType + "\n";
                printString += "�Z���u�X�q�G" + weaponMagazineCount + "/" + weaponMagzineSize;
            }
     
            printString += "�y���ˮ`�G"+weaponBasicDamage;
            if (weaponStrengthMutiply != 0f)
            {
                printString += "+ ����O�q*" + weaponStrengthMutiply;
            }
            if (weaponEtherMutiply != 0f)
            {
                printString += "+ ���ⴼ�z*" + weaponEtherMutiply;
            }
            if (weaponAgilityMutiply != 0f)
            {
                printString += "+ ����ӱ�*" + weaponAgilityMutiply;
            }
            if (weaponMentleMutiply != 0f)
            {
                printString += "+ ����믫*" + weaponMentleMutiply;
            }
        }

        public float weaponAddDamageBuff = 0f;
        public float weaponMutiplyDamageBuff = 1f;
    }

[System.Serializable]
public class ViolentEnergyGear
{
    public float violentGearType = 0;
    public int violentGearSort = 0;
    public float violentGearRadius = 0;
    public float violentGearFunctionNumber = 0;

    public float violentEnergyVPComsume = 1;

    //�C���}�l�� �̷�type/sort �������sdalegate
    public void propotyGive()
    {

    }
    public void sniper()
    {

    }
}

[System.Serializable]
public class chunk
{
        float anyway;
}