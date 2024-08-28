using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditSystem : MonoBehaviour
{
    public GameCore gameCore;
    public CampSystem campSystem;
    public Mercenaries mercenariesLoading;

    public int loadingMercenariesSort;
    public int MaxmentMercenareisNumberInCamp= 0;

    //ShowingInformationSync
    public Text SyncTextShowingLoadingMercenariesSort;
    public bool editingName;
    public GameObject editNamePatten;

    public Text SyncTextMercenariesNameShow;

    public Text SyncTextMercenariesHealth;
    public Text SyncTextMercenariesLevel;
    public Text SyncTextMercenariesExp;
    public Text SyncTextMercenariesSpeed;
    public Text SyncTextMercenariesPhysicDef;
    public Text SyncTextMercenariesEtherDef;

    public Text SyncTextMercenariesStrength;
    public Text SyncTextMercenareisEther;
    public Text SyncTextMercenariesAgility;
    public Text SyncTextMercenariesMentle;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (mercenariesLoading != null)
        {
            SyncTextShowingLoadingMercenariesSort.text = loadingMercenariesSort.ToString();
            SyncTextMercenariesNameShow.text = mercenariesLoading.characterName;
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
}
