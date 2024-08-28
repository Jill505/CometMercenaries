using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditSystem : MonoBehaviour
{
    public GameCore gameCore;
    public CampSystem campSystem;
    public Mercenaries mercenariesLoading;

    public GameObject PropotyPatten;
    public bool PropotyPattenActiveState = false;

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
        Debug.Log("EditSystemTest�G\n"+GameCore.Camp.MercenariesList);
        MaxmentMercenareisNumberInCamp = GameCore.Camp.MercenariesList.Count;

        LoadMercenaries(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (mercenariesLoading != null)
        {
            SyncTextShowingLoadingMercenariesSort.text = loadingMercenariesSort.ToString();
            SyncTextMercenariesNameShow.text = mercenariesLoading.characterName;

            SyncTextMercenariesStrength.text = "�O�q�G" + mercenariesLoading.characterStrength.ToString();
            SyncTextMercenariesEther.text = "���z�G" + mercenariesLoading.characterEther.ToString();
            SyncTextMercenariesAgility.text = "�ӱ��G" + mercenariesLoading.characterAgility.ToString();
            SyncTextMercenariesMentle.text = "�믫�G" + mercenariesLoading.characterMentle.ToString();

            mercenariesLoading.calculatePhysicDefend();
            SyncTextMercenariesPhysicDef.text = "���z���m�G" + mercenariesLoading.physicDefend.ToString();
            mercenariesLoading.calculateEtherDefend();
            SyncTextMercenariesEtherDef.text = "�A�Ө��m�G" + mercenariesLoading.etherDefend.ToString() + "%";

            mercenariesLoading.calculateMaxHealth();
            SyncTextMercenariesHealth.text = "��q�G" + mercenariesLoading.health.ToString() + "/" + mercenariesLoading.maxHealth.ToString();
            SyncTextMercenariesLevel.text = "���šG" + mercenariesLoading.level;
            SyncTextMercenariesExp.text = "�g��ȡG" + mercenariesLoading.exp + "/" + mercenariesLoading.nextLevelRequireExp;
            mercenariesLoading.calculateSpeed();
            SyncTextMercenariesSpeed.text = "�t�סG" + mercenariesLoading.speed;

            SyncTextMercenariesPropotyPoints.text = "�i���ݩ��I�G\n" + mercenariesLoading.characterPropotyPoints;
            SyncTextMercenariesSkillPoints.text = "�i�Χޯ��I�G\n" + mercenariesLoading.characterSkillPoints;
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
            Debug.Log("�S���ħL�A�n���J�T�p");
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
}
