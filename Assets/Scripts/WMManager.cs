using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMManager : MonoBehaviour
{


    public List<GameObject> clothList = new List<GameObject>();
    public List<GameObject> dirtyList = new List<GameObject>();
    public GameObject clothPrefab,dirtyPrefab;
    public Transform exitPoint,dirtyPoint;
    private bool isWorking;
    private int stackCount = 10;

    private void Start()
    {
        StartCoroutine(GenerateCloth());
    }

    public void RemoveLast()
    {
        if (clothList.Count>0)
        {
            Destroy(clothList[clothList.Count-1]);
            clothList.RemoveAt(clothList.Count-1);
        }
    }
    
    
    
    IEnumerator GenerateCloth()
    {
        while (true)
        {
            float clothCount = clothList.Count;
            int rowCount = (int)clothCount / stackCount;
            if (dirtyList.Count>0)
            {
                GetComponent<Animator>().SetBool("_isWM",true);
                GameObject temp = Instantiate(clothPrefab);
                temp.transform.position = new Vector3(exitPoint.position.x+((float)rowCount/1.5f), (clothCount % stackCount)/5 , exitPoint.position.z);
                clothList.Add(temp);
                RemoveLastDirty();
            }
            else
            {
                GetComponent<Animator>().SetBool("_isWM",false);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    
    public void GetDirty()
    {
        GameObject temp = Instantiate(dirtyPrefab);
        temp.transform.position = new Vector3(dirtyPoint.position.x, ((float)dirtyList.Count/5), dirtyPoint.position.z);
        dirtyList.Add(temp);
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
