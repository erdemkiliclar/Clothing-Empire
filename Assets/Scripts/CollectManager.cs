using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    private GameObject wm;
    
    public List<GameObject> clothList = new List<GameObject>();
    public GameObject clothPrefab;
    public Transform collectPoint;


    public List<GameObject> dirtyList = new List<GameObject>();
    public GameObject dirtyPrefab;

    private int clothLimit = 10;
    
    private void OnEnable()
    {
        TriggerManager.OnClothCollect += GetCloth;
        TriggerManager.OnClothGive += GiveCloth;
        TriggerManager.OnDirtyCollect += GetDirty;
        TriggerManager.OnDirtyGive += GiveDirty;
    }

    private void OnDisable()
    {
        TriggerManager.OnClothCollect -= GetCloth;
        TriggerManager.OnClothGive -= GiveCloth;
        TriggerManager.OnDirtyCollect -= GetDirty;
        TriggerManager.OnDirtyGive -= GiveDirty;
    }

    private void Awake()
    {
        wm = GameObject.FindGameObjectWithTag("CollectArea");
    }

    void GetCloth()
    {
        if (clothList.Count<=clothLimit)
        {
            for (int i = 0; i < wm.GetComponent<WMManager>().clothList.Count(); i++)
            {
                GameObject temp = Instantiate(clothPrefab, collectPoint);
                temp.transform.position = new Vector3(collectPoint.position.x,1f+((float)clothList.Count/5), collectPoint.position.z);
                clothList.Add(temp);
                if (TriggerManager.wmManager!=null)
                {
                    TriggerManager.wmManager.RemoveLast();
                }
            }
            
        }
    }

    void GetDirty()
    {
        if (dirtyList.Count<=clothLimit)
        {
            GameObject temp = Instantiate(dirtyPrefab, collectPoint);
            temp.transform.position = new Vector3(collectPoint.position.x, 1f + ((float)dirtyList.Count / 5),
                collectPoint.position.z);
            dirtyList.Add(temp);
            if (TriggerManager.dirtyClothManager!=null)
            {
                TriggerManager.dirtyClothManager.RemoveLast();
            }
        }
    }

    void GiveDirty()
    {
        if (dirtyList.Count>0)
        {
            TriggerManager.wmManager.GetDirty();
            RemoveLastDirty();
        }
    }
    
    void GiveCloth()
    {
        if (clothList.Count>0)
        {
            TriggerManager.buildManager.GetCloth();
            RemoveLast();
        }
    }
    
    
    public void RemoveLast()
    {
        if (clothList.Count>0)
        {
            Destroy(clothList[clothList.Count-1]);
            clothList.RemoveAt(clothList.Count-1);
        }
    }
    
    
    public void RemoveLastDirty()
    {
        if (dirtyList.Count>0)
        {
            Destroy(dirtyList[dirtyList.Count-1]);
            dirtyList.RemoveAt(dirtyList.Count-1);
        }
    }
    
}
