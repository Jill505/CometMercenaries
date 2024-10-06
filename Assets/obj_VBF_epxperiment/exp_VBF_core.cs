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
    public string gameType = "annihilation";//預設為殲滅戰

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
            //載入節點資料
            theNodeLoading.RanderChunksForVBF();
            NodeName.text = theNodeLoading.nodeName;
            if (gameType == "annihilation")
            {
                //關卡類型為殲滅戰
                GameTypeName.text = "殲滅戰";
            }
        }


        //載入傭兵資料
        //載入敵人資料 並將其放置於場地中


        //遍歷node中的每個chunk 如果屬於entry 則nodeEnrty數量+1

        StartCoroutine(StartJudgeCoroutine());
    }

    void Update()
    {
        
    }

    public void startGameFunction()
    {
        //進行檢查1 玩家已經選定開始的位置
        //進行檢查2 全部傭兵已經在待機位置

        editCluging = false;
        SquadEditPatten.SetActive(false);
    }

    IEnumerator StartJudgeCoroutine()
    {
        SquadEditPatten.SetActive(true);
        Debug.Log("允許玩家編輯隊伍");
        while (editCluging) { yield return wait; }
        Debug.Log("隊伍編輯完成");

        //傭兵數據初始化//

        StartCoroutine(RoundJudge());
    }
    IEnumerator RoundJudge()
    {
        mobActingClugging = false;
        playerActingClugging = false;
        yield return wait;

        //勝利條件判斷
        if (gameType == "annihilation")//殲滅戰
        {
            //敵人全數死亡及為結束
            bool gameWin = true;
            for (int i = 0; i < enemys.mobs.Length; i++)
            {
                //有任何一隻mob還存活著 及繼續遊戲 若所有mob處於死亡狀態 則遊戲結束
                if (enemys.mobs[i].isDead == false) { gameWin = false; }
            }
            winAlready = gameWin;
        }

        //Condition Judge If player win
        if (winAlready == true)
        {
            Debug.Log("欸欸欸恭喜你贏了");
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

            //等待玩家行動clug結束
            while (playerActingClugging)
            {
                //進行計算 開始為
                yield return wait;

            }
            //等待mob行動clug結束
            while (mobActingClugging)
            {
                //把Coroutine掛起來 等待 腳色行動
                //呼叫mob的小大腦代碼
                yield return wait;
            }

            //進行下一回合
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
        //計算結束後 針對所有的角色進行同等加速
        while(characterRunningClugging) {
            bool hitSec = false;

            List<Mercenaries> ListMerc = new List<Mercenaries>();
            List<mob_VBF> ListMob = new List<mob_VBF>();

            //判斷如果有角色可以進行行動了 給予最快的角色
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

                // 假如玩家可動傭兵數量>0 則允許玩家先行動 等結束之後再怪物行動
                break;
            }

            //結束後 判斷下回合mob和玩家可行動數量 若下個行動為我方mercenaries 可雙操作
            //幹有點複雜 要不晚點做這功能?
            //笑死 會很慘喔
            //沒差啦
            //有差 幹 你不要亂搞
            //咧咧咧 要不然你來寫啊
            //... 怎麼感覺我和一群國小生在一起做遊戲
            //好啦好啦 你們兩個不要吵 這東西做出來真的太複雜了啦 晚點推掉重做也不遲阿
            //我聽里克的
            //好吧 聽他的
            //你看我是對的 他都同意我
            //媽的...


            //若沒有 則進行加速
            foreach (Mercenaries mers in expSquad.mercenaries)
            {
                mers.inGameSpeed += mers.gameSpeed;
            }
            foreach (mob_VBF mob in enemys.mobs)
            {
                mob.GameSpeed += mob.Speed;
            }

            asyncForMyself = false;

            Debug.Log("回合結算 所有腳色進行加速");
            yield return wait; }
    }

    public void mercenariesAllowMove()
    {
        //依據開啟的傭兵序列開啟可行動傭兵
        for (int i = 0; i < allowActMers.Count; i++)
        {
            
        }
    }

    IEnumerator CallMobBrain()
    {
        for (int i = 0; i < allowActMobs.Count; i++)
        {
            mobClug = true;
            //依序讓mob進行行動
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


    //一些數據根據武器 現在先暫時寫個垃圾機制當交換用

    public void useLittleBrain()
    {
        //做出下一步決策
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