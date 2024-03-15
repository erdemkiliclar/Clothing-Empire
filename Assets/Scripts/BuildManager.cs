using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public List<GameObject> clothList = new List<GameObject>();
    public List<GameObject> moneyList = new List<GameObject>();
    public Transform clothPoint,moneyDropPoint;
    public GameObject clothPrefab,moneyPrefab;
    private int stackCount = 5;

    private void Start()
    {
        StartCoroutine(GenerateMoney());
    }

    IEnumerator GenerateMoney()
    {
        while (true)
        {
            float moneyCount = moneyList.Count;
            int rowCount = (int)moneyCount / stackCount;
            if (clothList.Count>0)
            {
                GameObject temp = Instantiate(moneyPrefab);
                temp.transform.position = new Vector3(moneyDropPoint.position.x+((float)rowCount*1.5f), (moneyCount % stackCount)/5, moneyDropPoint.position.z);
                moneyList.Add(temp);
                _animator.SetBool("_isTshirt",true);
                RemoveLast();
            }
            else
            {
                _animator.SetBool("_isTshirt",false);
            }
            
            yield return new WaitForSeconds(1f);
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

    public void GetCloth()
    {
        GameObject temp = Instantiate(clothPrefab);
        temp.transform.position = new Vector3(clothPoint.position.x, ((float)clothList.Count/5), clothPoint.position.z);
        clothList.Add(temp);
    }


}
