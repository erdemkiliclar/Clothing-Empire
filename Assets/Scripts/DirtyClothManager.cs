using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyClothManager : MonoBehaviour
{
    public List<GameObject> dirtyList = new List<GameObject>();
    public GameObject dirtyPrefab;
    public Transform exitPoint;
    private bool isWorking;
    private int stackCount = 10;

    private void Start()
    {
        StartCoroutine(ClothSpawn());
    }

    public void RemoveLast()
    {
        if (dirtyList.Count>0)
        {
            Destroy(dirtyList[dirtyList.Count-1]);
            dirtyList.RemoveAt(dirtyList.Count-1);
        }
    }
    
    
    
    IEnumerator ClothSpawn()
    {
        while (true)
        {
            float dirtyclothCount = dirtyList.Count;
            if (isWorking==true)
            {
                GameObject temp = Instantiate(dirtyPrefab);
                temp.transform.position = new Vector3(exitPoint.position.x, exitPoint.position.y , exitPoint.position.z);
                dirtyList.Add(temp);
                if (dirtyList.Count>=1)
                {
                    isWorking = false;
                }
            }
            
            else if (dirtyList.Count<1)
            {
                isWorking = true;
            }
            yield return new WaitForSeconds(3f);
        }
    }

}
