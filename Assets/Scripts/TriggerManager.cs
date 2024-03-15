using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    private GameObject wm, build;
    
    
    public delegate void OnCollectArea();
    public static event OnCollectArea OnClothCollect;
    public static WMManager wmManager;

    public delegate void OnBuildArea();
    public static event OnBuildArea OnClothGive;
    public static BuildManager buildManager;

    public delegate void OnDirtyArea();
    public static event OnDirtyArea OnDirtyCollect;
    public static DirtyClothManager dirtyClothManager;

    public delegate void OnDirtyGiveArea();
    public static event OnDirtyGiveArea OnDirtyGive;

    

    public delegate void OnMoneyArea();
    public static event OnMoneyArea OnMoneyCollect;
    
    private bool isCollecting;
    private bool isDirtyCollecting;
    private bool isGiving;
    private bool isDirtyGiving;
    

    private void Start()
    {
        build = GameObject.FindGameObjectWithTag("GiveArea");
        wm = GameObject.FindGameObjectWithTag("CollectArea");
        StartCoroutine(CollectEnum());
    }


    IEnumerator CollectEnum()
    {
        while (true)
        {
            if (isCollecting==true)
            {
                OnClothCollect();
            }

            if (isGiving==true)
            {
                OnClothGive();
            }

            if (isDirtyCollecting==true)
            {
                OnDirtyCollect();
            }

            if (isDirtyGiving==true)
            {
                OnDirtyGive();
            }

            
            yield return new WaitForSeconds(0.1f);
        }    
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
            OnMoneyCollect();
            buildManager = build.GetComponent<BuildManager>();
            buildManager.moneyList.RemoveAt(buildManager.moneyList.Count-1);
            Destroy(other.gameObject);
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
            
            isDirtyGiving = true;
            wmManager =other.gameObject.GetComponent<WMManager>();
        }
        if (other.gameObject.CompareTag("GiveArea"))
        {
            isGiving = true;
            buildManager = other.gameObject.GetComponent<BuildManager>();
        }

        if (other.gameObject.CompareTag("DirtyBox"))
        {
            isDirtyCollecting = true;
            dirtyClothManager = other.gameObject.GetComponent<DirtyClothManager>();
        }
        if (other.gameObject.CompareTag("ExitArea"))
        {
            isCollecting = true;
            wmManager = wm.gameObject.GetComponent<WMManager>();
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
           
            isDirtyGiving = false;
            wmManager = null;
            
        }
        if (other.gameObject.CompareTag("GiveArea"))
        {
            isGiving = false;
            buildManager = null;
        }

        if (other.gameObject.CompareTag("DirtyBox"))
        {
            isDirtyCollecting = false;
            dirtyClothManager = null;
        }
        if (other.gameObject.CompareTag("ExitArea"))
        {
            isCollecting = false;
            wmManager = null;

        }
    }
}
