using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CampSystem
{
    public List<Mercenaries> MercenariesList = new List<Mercenaries>();
    public Mercenaries[] tempMercenariesList;
    
    public List<Node> worldMapNodeList = new List<Node>();
    public Node[] tempWorldMapNodeList;

    public Mercenaries[] mercenariesSquad = new Mercenaries[3];

    public float C_Coin = -1;
    public float Kroan = -1;
    public float popularity = -1;

    public void syncTempData_ForSave()
    {
        tempMercenariesList = MercenariesList.ToArray();
        tempWorldMapNodeList = worldMapNodeList.ToArray();
    }
    public void syncTempData_ForLoad() {
        MercenariesList = tempMercenariesList.ToList<Mercenaries>();
        worldMapNodeList = tempWorldMapNodeList.ToList<Node>();
    }

    public void EditCharacterPattenOpen()
    {

    }
}