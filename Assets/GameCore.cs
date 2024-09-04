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
using Unity.VisualScripting;

public class GameCore : MonoBehaviour
{


    //
    //                       _oo0oo_
    //                      o8888888o
    //                      88" . "88
    //                      (| -_- |)
    //                      0\  =  /0
    //                    ___/`---'\___
    //                  .' \\|     |// '.
    //                 / \\|||  :  |||// \
    //                / _||||| -:- |||||- \
    //               |   | \\\  -  /// |   |
    //               | \_|  ''\---/''  |_/ |
    //               \  .-\__  '-'  ___/-. /
    //             ___'. .'  /--.--\  `. .'___
    //          ."" '<  `.___\_<|>_/___.' >' "".
    //         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
    //         \  \ `_.   \_ __\ /__ _/   .-` /  /
    //     =====`-.____`.___ \_____/___.-`___.-'=====
    //                       `=---='
    //
    //
    //     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //
    //               �򯪫O��         �õLBUG
    //
    //
    //

    static public string campSyssSavePath;

    static public CampSystem Camp = new CampSystem();
    static public SaveSystem saveSystem = new SaveSystem();

    public GameObject defultWeaponStore;
    public GameObject defultItemStore;
    public GameObject defultNodeStore;

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

        //Node Files
        tempCamp.tempWorldMapNodeFile = GameCore.Camp.tempWorldMapNodeFile;

        //��ɭԻݭn���ܪZ���x�s�MŪ���覡
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

            if (swapCamp.tempWorldMapNodeFile != null)
            {
                GameCore.Camp.tempWorldMapNodeFile = swapCamp.tempWorldMapNodeFile;

                //�T�{���Defult Node�ƶq
                //�p�G�ƶq��Defult Node�� �N���sNode���� ��GameCore.Camp.tempWorldMapNodeFile�[�J�snode
                ///
                /// �snode�Ա��G
                /// 1.�إߤ@�ӷsnode file <list> ��ثe��Node File����L�h
                /// 2.��{����tempWorldMapNodeFile���M��
                /// 3.for(int i = �{��node file list��count; i<Defult Node���q; i++){ ���ư��� ��s���e�[�J�i�h}
                /// 4.tempWorldMapNodeFile = �snode file.toArray()
                ///

                Debug.Log("�{�bFile������ƶq"+GameCore.Camp.tempWorldMapNodeFile.Length);
                Debug.Log("�w�]�t�Τ�����ƶq" + GameObject.Find("GameCore").GetComponent<GameCore>().defultNodeStore.GetComponent<SystemDefultNodeHouse>().defultNodeFiles.Count);

                if (GameCore.Camp.tempWorldMapNodeFile.Length < GameObject.Find("GameCore").GetComponent<GameCore>().defultNodeStore.GetComponent<SystemDefultNodeHouse>().defultNodeFiles.Count)
                {
                    Debug.Log("�o�̤֤F�Ǹ`�I");
                    List<NodeFile> swapNodeFiles = new List<NodeFile>(GameCore.Camp.tempWorldMapNodeFile);

                    for (int i = GameCore.Camp.tempWorldMapNodeFile.Length; i < GameObject.Find("GameCore").GetComponent<GameCore>().defultNodeStore.GetComponent<SystemDefultNodeHouse>().defultNodeFiles.Count; i++)
                    {
                        swapNodeFiles.Add(GameObject.Find("GameCore").GetComponent<GameCore>().defultNodeStore.GetComponent<SystemDefultNodeHouse>().defultNodeFiles[i]);
                    }

                    GameCore.Camp.tempWorldMapNodeFile = swapNodeFiles.ToArray();
                }

                //GameCore.Camp.worldMapNodeList = GameCore.Camp.tempWorldMapNodeList.ToList<Node>();

                ///
                //GameCore.Camp.worldMapNodeList.Clear();

                int counter = 0;
                foreach (NodeFile NF in GameCore.Camp.tempWorldMapNodeFile)
                {
                    //Debug.Log("���J�`�I��");
                    GameObject nodeObject = new GameObject("Node_" + counter);
                    Node swapNode = nodeObject.AddComponent<Node>();

                    GameCore.Camp.nodeGameObjecgts.Add(nodeObject);

                    //Node swapNode = new Node();

                    swapNode.MapXNum = NF.MapXNum;
                    swapNode.MapYNum = NF.MapYNum;

                    swapNode.chunkInfo = NF.chunkInfo;
                    swapNode.chunkViolentEnergyInfo = NF.chunkViolentEnergyInfo;
                    swapNode.ChunkInfoArrayCol = NF.ChunkInfoArrayCol;

                    swapNode.NodeSort = NF.NodeSort;

                    swapNode.nodeType = NF.nodeType;
                    swapNode.nodeFaction = NF.nodeFaction;
                    swapNode.nodeName = NF.nodeName;

                    GameCore.Camp.worldMapNodeList.Add(swapNode);

                    /*
                    int counterCol = 0;
                    foreach (chunk[] chunkLoading in NF.chunkItself)
                    {
                        int counterRow = 0;
                        foreach (chunk chunkLoading2 in chunkLoading)
                        {
                            if (chunkLoading2.chunkType == "")
                            {
                                chunkLoading2.chunkType = "DefultType";
                            }
                            chunkLoading2.myX = counterRow;
                            chunkLoading2.myY = counterCol;
                            counterRow++;
                        }
                        counterCol++;
                    }
                    */



                    counter++;
                }

                //�̷�linking nodes
                counter = 0;
                foreach (NodeFile NF in swapCamp.tempWorldMapNodeFile)
                {
                    //Debug.Log(counter + "times run the foreach");
                    //Debug.Log(JsonUtility.ToJson(GameCore.Camp.worldMapNodeList));
                    int secondCounter = 0;
                    foreach (int swapLinkingNode in NF.linkingNodesSort)
                    {
                        if (swapLinkingNode >= GameCore.Camp.worldMapNodeList.Count)
                        {
                            Debug.Log("�W�X��ܽd��");
                            break;
                        }

                        //GameCore.Camp.worldMapNodeList[counter].linkingNodes[secondCounter] = GameCore.Camp.worldMapNodeList[swapLinkingNode];
                        GameCore.Camp.worldMapNodeList[counter].linkingNodes.Add(GameCore.Camp.worldMapNodeList[swapLinkingNode]);
                        secondCounter++;
                    }
                    counter++;
                }
                ///
            }
            else
            {
                GameCore.Camp.worldMapNodeList= new List<Node>();

                //�i��Node��l�Ƹѥ]
                GameObject.Find("GameCore").GetComponent<GameCore>().defultNodeStore.GetComponent<SystemDefultNodeHouse>().nodeGiveInitialization();
                GameCore.Camp.worldMapNodeList = GameObject.Find("GameCore").GetComponent<GameCore>().defultNodeStore.GetComponent<SystemDefultNodeHouse>().defultNodes;

                GameCore.Save();
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
    public string ItemName = "defultItemName";
    public string ItemNarrative = "defult Narrowtive";
    public string ItemType = "defult item type"; //�i�H�Q�Ω�l�u�������H �]�\�n�ظm�@��excel���
    public float ItemSort = -1;//�D��s�� �i�H�ֳt��d������Ʀ�m
    public string ItemImageName = "defultItemImage";

    public float ItemAmount = 0;
    public float ItemStackMaxmentAmount = 1;//�̤j���|�ƶq

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
    public float myX;
    public float myY;
    public float myViolentEnergy = 1;

    public string chunkType = "defultChunkType";
    /// <summary>
    /// chunkType = N/A
    /// 
    /// </summary>
    public int itemFieldProvideNumber = 0;

    public void setDefult()
    {
        if (chunkType == "level_1_savingBox")
        {
            itemFieldProvideNumber = 50;
        }
        else if (chunkType == "playerBackPack")
        {
            itemFieldProvideNumber = 30;
        }
    }

    public Item[] itemContext = new Item[0];
    public void itemExtend()
    {
        List<Item> itemSwap = new List<Item>(itemContext);
        for (int i = itemSwap.Count; i < itemFieldProvideNumber; i++)
        {
            Item nullItem = new Item() { ItemType = "blankType" };
            itemSwap.Add(nullItem);
        }
        itemContext = itemSwap.ToArray();
    }
}