using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SearchService;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class exp_VBF_core : MonoBehaviour
{
    public string gameType = "annihilation";//�w�]�������

    public squad_VBF expSquad;
    public Mercenaries mers;
    public enemys_VFB enemys;
    public GameObject SquadEditPatten;
    public Text NodeName;
    public Text GameTypeName;
    
    public bool winAlready = false;

    public Node theNodeLoading;
    public int[] nodeEntry;
    public int submittingEntry = -1;

    WaitForSeconds wait = new WaitForSeconds(0.1f);
    public bool editCluging = true;
    public bool characterRunningClugging = true;
    public bool playerActingClugging = false;
    public bool mobActingClugging = false;

    public List<mob_VBF> allowActMobs = new List<mob_VBF>();
    public List<Mercenaries> allowActMers = new List<Mercenaries>();

    public bool asyncForMyself = false;

    public bool mobClug = false;

    private void Awake()
    {
        winAlready = false;
        SquadEditPatten.SetActive(false);
    }
    void Start()
    {
        //Load Node
        if (theNodeLoading != null)
        {
            //���J�`�I���
            theNodeLoading.RanderChunksForVBF();
            NodeName.text = theNodeLoading.nodeName;
            if (gameType == "annihilation")
            {
                //���d�����������
                GameTypeName.text = "�����";
            }
        }


        //���J�ħL���
        //���J�ĤH��� �ñN���m����a��


        //�M��node�����C��chunk �p�G�ݩ�entry �hnodeEnrty�ƶq+1

        StartCoroutine(StartJudgeCoroutine());
    }

    void Update()
    {
        
    }

    public void startGameFunction()
    {
        //�i���ˬd1 ���a�w�g��w�}�l����m
        //�i���ˬd2 �����ħL�w�g�b�ݾ���m

        editCluging = false;
        SquadEditPatten.SetActive(false);
    }

    IEnumerator StartJudgeCoroutine()
    {
        SquadEditPatten.SetActive(true);
        Debug.Log("���\���a�s�趤��");
        while (editCluging) { yield return wait; }
        Debug.Log("����s�觹��");

        //�ħL�ƾڪ�l��//

        StartCoroutine(RoundJudge());
    }
    IEnumerator RoundJudge()
    {
        mobActingClugging = false;
        playerActingClugging = false;
        yield return wait;

        //�ӧQ����P�_
        if (gameType == "annihilation")//�����
        {
            //�ĤH���Ʀ��`�ά�����
            bool gameWin = true;
            for (int i = 0; i < enemys.mobs.Length; i++)
            {
                //������@��mob�٦s���� ���~��C�� �Y�Ҧ�mob�B�󦺤`���A �h�C������
                if (enemys.mobs[i].isDead == false) { gameWin = false; }
            }
            winAlready = gameWin;
        }

        //Condition Judge If player win
        if (winAlready == true)
        {
            Debug.Log("�����ٮ��ߧAĹ�F");
            //execute gameEnd function;
            //LetCoroutineEnd;
        }
        else
        {
            asyncForMyself = true;
            StartCoroutine(SpeedJudge());

            while (asyncForMyself)
            {
                yield return wait;
            }

            //���ݪ��a���clug����
            while (playerActingClugging)
            {
                //�i��p�� �}�l��
                yield return wait;

            }
            //����mob���clug����
            while (mobActingClugging)
            {
                //��Coroutine���_�� ���� �}����
                //�I�smob���p�j���N�X
                yield return wait;
            }

            //�i��U�@�^�X
            StartCoroutine(RoundJudge());
        }
        //
    }
    
    IEnumerator SpeedJudge()
    {
        allowActMobs = new List<mob_VBF>();
        allowActMers = new List<Mercenaries>();

        yield return null;
        characterRunningClugging = true;
        //�p�⵲���� �w��Ҧ�������i��P���[�t
        while(characterRunningClugging) {
            bool hitSec = false;

            List<Mercenaries> ListMerc = new List<Mercenaries>();
            List<mob_VBF> ListMob = new List<mob_VBF>();

            //�P�_�p�G������i�H�i���ʤF �����̧֪�����
            foreach (Mercenaries mers in expSquad.mercenaries)
            {
                if (mers.inGameSpeed > mers.inGameSpeedHit)
                {
                    hitSec = true;
                    ListMerc.Add(mers);

                    playerActingClugging = true;
                }
            }
            foreach (mob_VBF mob in enemys.mobs)
            {
                if (mob.GameSpeed > mob.SpeedHit)
                {
                    hitSec = true;
                    ListMob.Add(mob);

                    mobActingClugging = true;
                }
            }

            if (hitSec == true)
            {
                characterRunningClugging = false;

                allowActMers = ListMerc;
                allowActMobs = ListMob;

                asyncForMyself = false;

                // ���p���a�i�ʶħL�ƶq>0 �h���\���a����� ����������A�Ǫ����
                break;
            }

            //������ �P�_�U�^�Xmob�M���a�i��ʼƶq �Y�U�Ӧ�ʬ��ڤ�mercenaries �i���ާ@
            //�F���I���� �n�����I���o�\��?
            //���� �|�ܺG��
            //�S�t��
            //���t �F �A���n�÷d
            //������ �n���M�A�Ӽg��
            //... ���Pı�کM�@�s��p�ͦb�@�_���C��
            //�n�զn�� �A�̨�Ӥ��n�n �o�F�谵�X�ӯu���ӽ����F�� ���I���������]�����
            //��ť���J��
            //�n�a ť�L��
            //�A�ݧڬO�諸 �L���P�N��
            //����...


            //�Y�S�� �h�i��[�t
            foreach (Mercenaries mers in expSquad.mercenaries)
            {
                mers.inGameSpeed += mers.gameSpeed;
            }
            foreach (mob_VBF mob in enemys.mobs)
            {
                mob.GameSpeed += mob.Speed;
            }

            asyncForMyself = false;

            Debug.Log("�^�X���� �Ҧ��}��i��[�t");
            yield return wait; }
    }

    public void mercenariesAllowMove()
    {
        //�̾ڶ}�Ҫ��ħL�ǦC�}�ҥi��ʶħL
        for (int i = 0; i < allowActMers.Count; i++)
        {
            
        }
    }

    IEnumerator CallMobBrain()
    {
        for (int i = 0; i < allowActMobs.Count; i++)
        {
            mobClug = true;
            //�̧���mob�i����
            allowActMobs[i].useLittleBrain();
            while (mobClug)
            {
                yield return wait;
            }
        }
        yield return wait;
    }
}

[System.Serializable]
public class squad_VBF
{
    public Mercenaries[] mercenaries = new Mercenaries[3];

}

/*[SerializeField]
public class mob_VBF
{
    public float hp = 10f;
    public float Strength;
    public float Mentle;
    public float Agility;

    public float Speed;


    //�@�ǼƾڮھڪZ�� �{�b���Ȯɼg�өU�������洫��

    public void useLittleBrain()
    {
        //���X�U�@�B�M��
    }
    public void die()
    {

    }
}*/

[System.Serializable]
public class enemys_VFB
{
    public mob_VBF[] mobs = new mob_VBF[3];
}

[System.Serializable]
public class mob_VBF_File
{
    public float hp = 10f;
    public float Strength = 10f;
    public float Ether = 10f;
    public float Mentle = 10f;
    public float Agility = 10f;

    public float Speed = 10f;
    public float SpeedHit = 100;
    public float GameSpeed = 0;
}

[System.Serializable]
public class nodeEntry_VBF
{
    
}