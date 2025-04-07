using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject stonePrefab; 
    public GameObject blockerStonePrefab; 
    public Transform stoneSpawnPoint;
    public GameObject Camera; 
    public GameObject gameWinPanel; 
    public GameObject gameLosePanel; 
    public Dictionary<GameObject, int> curlingStoneInventoryDictionary = new Dictionary<GameObject, int>();
    private bool normalStonesUsed;
    private bool blockerStonesUsed; 
    private int currentStoneIndex = 0;  
    private GameObject currentStone;    
    public TextMeshProUGUI normalStones; 
    public TextMeshProUGUI blockerStones; 
    void Start()
    {
        curlingStoneInventoryDictionary.Add(stonePrefab, 4);
        curlingStoneInventoryDictionary.Add(blockerStonePrefab, 4);

        Debug.Log(curlingStoneInventoryDictionary.Count);

        currentStone = Instantiate(GetPrefabByIndex(currentStoneIndex), stoneSpawnPoint.position, Quaternion.identity);
        Camera.GetComponent<CameraFollow>().target = currentStone.transform;
    }

    void Update()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            ChangeSelectedStone(scroll);
        }

    }

    private void ChangeSelectedStone(float scrollInput)
    {

        if (scrollInput > 0)
        {
            currentStoneIndex = Mathf.Min(currentStoneIndex + 1, curlingStoneInventoryDictionary.Count - 1);
        }
        else if (scrollInput < 0)
        {
            currentStoneIndex = Mathf.Max(currentStoneIndex - 1, 0);
        }
        if (blockerStonesUsed)
        {
            currentStoneIndex=0;
        }
        if (normalStonesUsed)
        {
            currentStoneIndex=0;
        }
    

        Debug.Log(currentStoneIndex);

        Destroy(currentStone);

        currentStone = Instantiate(GetPrefabByIndex(currentStoneIndex), stoneSpawnPoint.position, Quaternion.identity);
        Camera.GetComponent<CameraFollow>().target = currentStone.transform;
    }


   public IEnumerator ResetStone(GameObject stone, bool burned)
    {
        yield return new WaitForSeconds(1f);

        if (burned)
        {
            Destroy(stone);
        }

        ReduceByIndex(currentStoneIndex);
        if (HasStoneType(currentStoneIndex)==false)
        {
            if (currentStoneIndex==0)
            {
                currentStoneIndex=1;
                normalStonesUsed=true;
                Debug.Log("NORMAL USED");
            }
            if (currentStoneIndex==1)
            {
                currentStoneIndex=0;
                blockerStonesUsed=true;
                Debug.Log("BLOCKER USED");
            }
            if (normalStonesUsed&&blockerStonesUsed)
            {
                LoseGame(); 
            }
        }
        GameObject newStone = Instantiate(GetPrefabByIndex(currentStoneIndex), stoneSpawnPoint.position, Quaternion.identity);
        Camera.GetComponent<CameraFollow>().target = newStone.transform;
        currentStone = newStone;
        
    }

    public void WinGame()
    {
        gameWinPanel.SetActive(true);
    }
    public void LoseGame()
    {
        gameLosePanel.SetActive(true);
    }
    private GameObject GetPrefabByIndex(int index)
    {
        List<GameObject> dictionaryKeys = new List<GameObject>(curlingStoneInventoryDictionary.Keys);
        return dictionaryKeys[index];
    } 

    private void ReduceByIndex(int index)
    {
        GameObject itemToReduce = GetPrefabByIndex(index);
        curlingStoneInventoryDictionary[itemToReduce]--;
        

    }

    public bool HasStoneType(int index)
    {
        GameObject checkType = GetPrefabByIndex(index);
        return curlingStoneInventoryDictionary[checkType] > 0; //Returns as true if more than 0

    }
}
