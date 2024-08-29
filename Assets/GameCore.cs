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
        //初始化
        campSyssSavePath = Application.persistentDataPath + "/saveFiles.json";

        Camp = new CampSystem();
        Camp.worldMapNodeList = new List<Node>();
        Camp.MercenariesList = new List<Mercenaries>();
        Camp.tempWorldMapNodeList = Camp.worldMapNodeList.ToArray();
        Camp.tempMercenariesList = Camp.MercenariesList.ToArray();

        Debug.Log("完成初始化");

        int testNum = GameCore.Camp.MercenariesList.Count;
        Debug.Log(testNum);


        Load();
    }

    private void Start()
    {
    }

    private void Update()
    {
        string swapEdit = "克朗："+ Camp.Kroan +"\n紀念幣："+ Camp.C_Coin + "\n聲望："+Camp.popularity + "\n";
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

    //以下為測試用代碼 不用於正式遊戲
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
    //角色基礎屬性
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

    //角色武器
    public weapon mainWeapon;
    public weapon secondaryWeapon;

    //角色裝備
    public Gear HeadGear;//頭部裝備
    public Gear ChestGear;//胸部裝備
    public Gear FootGear;//腳部裝備
    public Gear tokenGear;//信物


    //角色職業技能
    public float characterPropotyPoints = 4;
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

    public delegate void voidDelegate();
    public voidDelegate gearCalDeleate; 

    public void calculateGearAddition()
    {
        gearCalDeleate = null;

        //呼叫裝備訂閱計算內容
        //gearCalDeleate += Gear.Function or what else bla bla bla

        gearCalDeleate();
    }

    public void calculateMaxHealth()
    {
        //血量公式： 50+角色力量*0.8+裝備血量+技能加成
        maxHealth = (int)(50 +characterStrength * 0.8f);
    }

    public void calculateSpeed()
    {
        //速度公式： 80+角色速度+裝備速度+
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
            Debug.Log("克朗數"+swapCamp.Kroan);

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

[System.Serializable]
public class Item
{
    public string ItemType = "defult item type"; //可以被用於子彈的類型？ 也許要建置一個excel表格
    public float ItemSort = -1;//道具編號 可以快速找查對應資料位置
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

        public int weaponRegisterNumber;//註冊序列
    
        public string weaponAmmoType = "noAmmoType";
        public string weaponType = "defultWeaponType";
        public string weaponImageName = "defultWeaponImage";
        public string weaponBuffsString = "";

        public float weaponRadius = 1f;//武器作用半徑

        public int weaponMagzineSize = -1;//-1等於無消耗
        public int weaponMagazineCount = -1;//-1 等於無消耗

        public float weaponDamage;//需要額外計算

        public float weaponBasicDamage = 0;//基礎傷害
        //數據乘數
        public float weaponStrengthMutiply = 0f;
        public float weaponEtherMutiply = 0f;
        public float weaponAgilityMutiply = 0f;
        public float weaponMentleMutiply = 0f;

        public string weaponDamageFormulaTelling;
        public void calculateWeaponDamageFormulaTelling()
        {
            string printString = "";
            printString += "武器名稱：" + weaponName +weaponBuffsString + "\n";
            printString += "武器類型：" + weaponType + "\n";

            if (weaponMagzineSize < 0)
            {
                printString += "此武器為無消耗類型";
            }
            else
            {
                printString += "武器子彈需求類型：" + weaponAmmoType + "\n";
                printString += "武器彈匣量：" + weaponMagazineCount + "/" + weaponMagzineSize;
            }
     
            printString += "造成傷害："+weaponBasicDamage;
            if (weaponStrengthMutiply != 0f)
            {
                printString += "+ 角色力量*" + weaponStrengthMutiply;
            }
            if (weaponEtherMutiply != 0f)
            {
                printString += "+ 角色智慧*" + weaponEtherMutiply;
            }
            if (weaponAgilityMutiply != 0f)
            {
                printString += "+ 角色敏捷*" + weaponAgilityMutiply;
            }
            if (weaponMentleMutiply != 0f)
            {
                printString += "+ 角色精神*" + weaponMentleMutiply;
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

    //遊戲開始後 依照type/sort 給予按鈕dalegate
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