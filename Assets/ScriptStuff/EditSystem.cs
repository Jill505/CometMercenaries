using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class EditSystem : MonoBehaviour
{
    public GameCore gameCore;
    public CampSystem campSystem;
    public Mercenaries mercenariesLoading;

    public GameObject PropotyPatten;
    public bool PropotyPattenActiveState = false;
    public GameObject WeaponPatten;
    public bool WeaponPattenActiveState = false;

    public int loadingMercenariesSort;
    public int MaxmentMercenareisNumberInCamp= 0;

    //ShowingInformationSync
    public Text SyncTextShowingLoadingMercenariesSort;
    public bool editingName;
    public GameObject editNamePatten;

    public Text SyncTextMercenariesNameShow;

    public Text SyncTextMercenariesHealth;
    public Text SyncTextMercenariesLevel;
    public Text SyncTextMercenariesWeightCapacity;
    public Text SyncTextMercenariesExp;
    public Text SyncTextMercenariesSpeed;
    public Text SyncTextMercenariesPhysicDef;
    public Text SyncTextMercenariesEtherDef;

    public Text SyncTextMercenariesStrength;
    public Text SyncTextMercenariesEther;
    public Text SyncTextMercenariesAgility;
    public Text SyncTextMercenariesMentle;

    public Text SyncTextMercenariesPropotyPoints;
    public Text SyncTextMercenariesSkillPoints;

    public Text SyncTextMercenariesSkillA;
    public Image SyncImageMercenariesSkillA;
    public Text SyncTextMercenariesSkillB;
    public Image SyncImageMercenariesSkillB;
    public Text SyncTextMercenariesSkillC;
    public Image SyncImageMercenariesSkillC;

    public Text SyncTextMercenariesMainWeapon;
    public Image SyncImageMercenariesMainWeapon;
    public Text SyncTextMercenariesSecondaryWeapon;
    public Image SyncImageMercenariesSecondaryWeapon;
    public Text SyncTextMercenariesViolentEnergyGear;
    public Image SyncImageMercenariesViolentEnergyGear;

    public Text SyncTextMercenariesGear_Head;
    public Image SyncImageMercenariesGaer_Head;
    public Text SyncTextMercenariesGear_Chest;
    public Image SyncImageMercenariesGear_Chest;
    public Text SyncTextMercenariesGear_Foot;
    public Image SyncImageMercenariesGear_Foot;
    public Text SyncTextMercenariesGear_Token;
    public Image SyncImageMercenariesGear_Token;

    // Start is called before the first frame update
    void Start()
    {
        campSystem = GameCore.Camp;
        gameCore = GameObject.Find("GameCore").GetComponent<GameCore>();

        //test
        ///List<Mercenaries> testList = new List<Mercenaries>();
        ///Mercenaries[] testArray = testList.ToArray();
        ///Debug.Log(testArray.Length);

        loadingMercenariesSort = 0;
        Debug.Log("EditSystemTest：\n"+GameCore.Camp.MercenariesList);
        MaxmentMercenareisNumberInCamp = GameCore.Camp.MercenariesList.Count;

        LoadMercenaries(0);
        loadAllNodeItemInspactor();

    }

    // Update is called once per frame
    void Update()
    {
        if (mercenariesLoading != null)
        {
            SyncTextShowingLoadingMercenariesSort.text = loadingMercenariesSort.ToString();
            SyncTextMercenariesNameShow.text = mercenariesLoading.characterName;

            SyncTextMercenariesStrength.text = "力量：" + mercenariesLoading.characterStrength.ToString();
            SyncTextMercenariesEther.text = "智慧：" + mercenariesLoading.characterEther.ToString();
            SyncTextMercenariesAgility.text = "敏捷：" + mercenariesLoading.characterAgility.ToString();
            SyncTextMercenariesMentle.text = "精神：" + mercenariesLoading.characterMentle.ToString();

            mercenariesLoading.calculatePhysicDefend();
            SyncTextMercenariesPhysicDef.text = "物理防禦：" + mercenariesLoading.physicDefend.ToString();
            mercenariesLoading.calculateEtherDefend();
            SyncTextMercenariesEtherDef.text = "乙太防禦：" + mercenariesLoading.etherDefend.ToString() + "%";

            mercenariesLoading.calculateMaxHealth();
            SyncTextMercenariesHealth.text = "血量：" + mercenariesLoading.health.ToString() + "/" + mercenariesLoading.maxHealth.ToString();
            SyncTextMercenariesLevel.text = "等級：" + mercenariesLoading.level;
            SyncTextMercenariesExp.text = "經驗值：" + mercenariesLoading.exp + "/" + mercenariesLoading.nextLevelRequireExp;
            mercenariesLoading.calculateCharacterWeightCapacity();
            SyncTextMercenariesWeightCapacity.text = "負重提供：" + mercenariesLoading.weightCapacity.ToString();
            mercenariesLoading.calculateSpeed();
            SyncTextMercenariesSpeed.text = "速度：" + mercenariesLoading.speed;

            SyncTextMercenariesPropotyPoints.text = "可用屬性點：\n" + mercenariesLoading.characterPropotyPoints;
            SyncTextMercenariesSkillPoints.text = "可用技能點：\n" + mercenariesLoading.characterSkillPoints;

            SyncTextMercenariesMainWeapon.text = "主武器：\n" + mercenariesLoading.mainWeapon.weaponName;
            SyncTextMercenariesSecondaryWeapon.text = "副武器：\n" + mercenariesLoading.secondaryWeapon.weaponName;
        }
        if (GameCore.saveSystem != null)
        {
            resourceSyncText.text = "克朗：" + GameCore.Camp.Kroan + "\n紀念幣：" + GameCore.Camp.C_Coin + "\n聲望：" + GameCore.Camp.popularity + "\n";

            string weaponShow = "";
            foreach (weapon loadingWeapon in GameCore.Camp.weaponStorehouseList)
            {
                weaponShow += loadingWeapon.weaponName + "\n";
            }
            weaponSyncText.text = weaponShow;
        }

        if (campSystem != null)
        {
            checkAllSpace();
            //Load Alll Item？
        }
    }

    public void AddNewMercenaries()
    {
        Mercenaries newMercenaries = new Mercenaries();
        campSystem.MercenariesList.Add(newMercenaries);

        //UpdateMercenariesListAmount
        MaxmentMercenareisNumberInCamp = GameCore.Camp.MercenariesList.Count;

        GameCore.Save();
    }

    public void LoadMercenaries(int MercenariesSort)
    {
        if (GameCore.Camp.MercenariesList.Count != 0)
        {
            mercenariesLoading = GameCore.Camp.MercenariesList[MercenariesSort];
        }
        else
        {
            Debug.Log("沒有傭兵你要載入三小");
        }
        //Sync All Information;}
    }
        public void loadNextMercenaries()
    {
        loadingMercenariesSort++;
        if (loadingMercenariesSort >= MaxmentMercenareisNumberInCamp)
        {
            loadingMercenariesSort = 0;
        }
        LoadMercenaries(loadingMercenariesSort);
    }
    public void loadLastMercenaries()
    {
        loadingMercenariesSort--;
        if (loadingMercenariesSort == -1)
        {
            loadingMercenariesSort = MaxmentMercenareisNumberInCamp - 1;
        }
        LoadMercenaries(loadingMercenariesSort);
    }

    public void editNameButtonFunction()
    {
        editingName = !editingName;

        if (editingName == true)
        {
            editNamePatten.GetComponent<InputField>().text = mercenariesLoading.characterName;
        }
        mercenariesLoading.characterName = editNamePatten.GetComponent<InputField>().text;
        editNamePatten.SetActive(editingName);

        GameCore.Save();

        //if (editingName == false)
        //{
        //    mercenariesLoading.name = editNamePatten.GetComponent<InputField>().text;
        //}
    }

    public void editPropotyButtonFunction()
    {
        PropotyPattenActiveState = !PropotyPattenActiveState;
        PropotyPatten.SetActive(PropotyPattenActiveState);
    }

    public void AddStrength()
    {
        if (mercenariesLoading.characterPropotyPoints > 0)
        {
            mercenariesLoading.characterStrength += 1;
            mercenariesLoading.characterPropotyPoints -= 1;
            GameCore.Save();
        }
        else
        {
            PropotyPatten.SetActive(false);
            PropotyPattenActiveState = false;
        }
    }
    public void AddEther()
    {
        if (mercenariesLoading.characterPropotyPoints > 0)
        {
            mercenariesLoading.characterEther += 1;
            mercenariesLoading.characterPropotyPoints -= 1;
            GameCore.Save();
        }
        else
        {
            PropotyPatten.SetActive(false);
            PropotyPattenActiveState = false;
        }
    }
    public void AddAgility()
    {
        if (mercenariesLoading.characterPropotyPoints > 0)
        {
            mercenariesLoading.characterAgility += 1;
            mercenariesLoading.characterPropotyPoints -= 1;
            GameCore.Save();
        }
        else
        {
            PropotyPatten.SetActive(false);
            PropotyPattenActiveState = false;
        }
    }
    public void AddMentle()
    {
        if (mercenariesLoading.characterPropotyPoints > 0)
        {
            mercenariesLoading.characterMentle += 1;
            mercenariesLoading.characterPropotyPoints -= 1;
            GameCore.Save();
        }
        else
        {
            PropotyPatten.SetActive(false);
            PropotyPattenActiveState = false;
        }
    }

    public int selectingWeaponSort = -1; //0=主武器 1=副武器
    public GameObject randomButtonPrefab;
    public Transform layoutGroupTranfrom;
    public void OpenCharacterWeaponEditPatten(int selectingSort)
    {
        WeaponPattenActiveState = !WeaponPattenActiveState;
        selectingWeaponSort = selectingSort;

        if (WeaponPattenActiveState)
        {
            //load All weapon
            for (int i = 0; i < GameCore.Camp.tempWeaponStorehouseList.Length; i++)
            {
                GameObject swapWeaponButton = Instantiate(randomButtonPrefab);
                swapWeaponButton.transform.parent = layoutGroupTranfrom;

                int index = i;  // 创建一个局部变量，保存当前的索引
                swapWeaponButton.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(() => EditCharacterWeapon(index));

                swapWeaponButton.transform.GetChild(1).gameObject.GetComponent<Text>().text = GameCore.Camp.weaponStorehouseList[i].weaponName;
            }
        }
        else
        {
            foreach (Transform child in layoutGroupTranfrom)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        WeaponPatten.SetActive(WeaponPattenActiveState);
    }
    public void EditCharacterWeapon(int i)
    {
        //後面再加這個判斷 判斷該武器是否已經被其他傭兵持有 之類的 也許乾脆直接讓這個button失效?

        if (selectingWeaponSort == 0)
        {
            mercenariesLoading.mainWeapon = GameCore.Camp.weaponStorehouseList[i];
        }
        else if (selectingWeaponSort == 1)
        {
            mercenariesLoading.secondaryWeapon = GameCore.Camp.weaponStorehouseList[i];
        }
        else
        {
            Debug.Log("你在衝三小 系統沒有選中任何一個武器類型 你怎麼進到這個判斷式裡面的");
        }

        //Edit Finish, close patten
        foreach (Transform child in layoutGroupTranfrom)
        {
            GameObject.Destroy(child.gameObject);
        }

        WeaponPatten.SetActive(false);
        WeaponPattenActiveState = false;

        GameCore.Save();
    }

    ///
    ///==============================
    ///    Backpack Edit system
    ///==============================
    ///

    public Text resourceSyncText;
    public Text weaponSyncText;

    public int backPackNumber = 0;
    ///
    /// =============================
    ///      Node Edit System
    /// =============================
    ///

    public Node nodeLoading;
    public int nodeSort;
    

    public void loadLastNode()//-
    {
        nodeSort -= 1;
        if (nodeSort < 0)
        {
            nodeSort = GameCore.Camp.worldMapNodeList.Count -1;
        }
        loadThisNode();
    }
    public void loadNextNode()//+
    {
        nodeSort += 1;
        if (nodeSort >= GameCore.Camp.worldMapNodeList.Count)
        {
            nodeSort = 0;
        }
        loadThisNode();
    }
    public swapInspactNodeArray[] nodeRow = new swapInspactNodeArray[3];

    public void loadAllChunkInfo()
    {
        int count = 0;
        foreach (swapInspactNodeArray chunkLoading in nodeLoading.ChunkInfoArrayCol)
        {
            int count2 = 0;
            foreach (chunk chunkLoading2 in chunkLoading.chunkRow)
            {
                nodeRow[count].chunkRow[count2] = chunkLoading2;
                count2++;
            }
            count++;
        }
    }

    public void loadThisNode()
    {
        if (GameCore.Camp.worldMapNodeList.Count != 0)
        {
            nodeLoading = GameCore.Camp.worldMapNodeList[nodeSort];
            loadAllChunkInfo();
        }
        else
        {
            Debug.Log("沒有節點你要載入三小");
        }
    }

    public void loadNodeItem()
    {
        
    }


    public void loadAllNodeItemInspactor()
    {
        int emptySpace = 0;
        List<Item> itemInspactList = new List<Item>();
        List<Item> itemStore = GameObject.Find("GameCore").GetComponent<GameCore>().defultItemStore.GetComponent<SystemDefultItemHouse>().defultItems;
        for (int i = 0; i <  itemStore.Count; i++)
        {
            Item swapItem = new Item() { 
            ItemName = itemStore[i].ItemName,
            ItemSort = itemStore[i].ItemSort,
            ItemType = itemStore[i].ItemType,
            };
            itemInspactList.Add(swapItem);
        }

        foreach (Node nodeLoad in campSystem.worldMapNodeList)
        {
            if (nodeLoad.nodeFaction == "myFaction")
            {
                //節點屬於玩家 統計其中的內容)
                foreach (swapInspactNodeArray nodeArray in nodeLoad.ChunkInfoArrayCol)
                {
                    foreach (chunk chunkLoad in nodeArray.chunkRow)
                    {
                        if (chunkLoad.itemFieldProvideNumber > 0)
                        {
                            //Load
                            //喔幹我真的不懂
                            for (int i = 0; i < chunkLoad.itemFieldProvideNumber; i++)
                            {
                                if (chunkLoad.itemContext.Length != 0)
                                {
                                    if (chunkLoad.itemContext[i].ItemSort >= 0)
                                    {
                                        itemInspactList[(int)chunkLoad.itemContext[i].ItemSort].ItemAmount += chunkLoad.itemContext[i].ItemAmount;
                                    }
                                    else
                                    {
                                        emptySpace++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        Debug.Log("空白格子：" + emptySpace);
        Debug.Log(JsonUtility.ToJson(itemInspactList));
        for (int i = 0; i < itemInspactList.Count; i++)
        {
            Debug.Log(itemInspactList[i].ItemName +"數量："+itemInspactList[i].ItemAmount);
        }
    }
        /*//這個絕對要刪掉喔 留在這邊只是方便複製而已
        campSystem.inspactAllItemPlayerHave.Clear();
        for (int i = 0; i < backPackNumber; i++)
        {
            Item blankItem = new Item() { ItemType = "blankType" };
            campSystem.inspactAllItemPlayerHave.Add(blankItem);
        }*/

    public void checkAllSpace()
    {
        foreach (Node nodeInspacting in campSystem.worldMapNodeList)
        {
            backPackNumber += nodeInspacting.BoxSpaceProvide;
        }
    }
}