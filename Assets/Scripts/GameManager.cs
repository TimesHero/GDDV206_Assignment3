using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject stonePrefab; 
    public GameObject blockerStonePrefab; 
    public Transform stoneSpawnPoint;
    public GameObject Camera; 
    public GameObject gameWinPanel; 
    public Dictionary<int, GameObject> curlingStoneInventoryDictionary = new Dictionary<int, GameObject>();

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            curlingStoneInventoryDictionary.Add(i, stonePrefab);
        }

        for (int i = 4; i < 8; i++)
        {
            curlingStoneInventoryDictionary.Add(i, blockerStonePrefab);
        }
        Debug.Log(curlingStoneInventoryDictionary.Count);
    }

    void Update()
    {
        
    }

  

    public IEnumerator ResetStone(GameObject stone, bool burned)
    {
        yield return new WaitForSeconds(1f);
        GameObject newStone =Instantiate(stonePrefab, stoneSpawnPoint.position, Quaternion.identity);
        Camera.GetComponent<CameraFollow>().target=newStone.transform;
        if (burned == true)
        {
            Destroy(stone);
        }
    }

    public void WinGame()
    {
        gameWinPanel.SetActive(true);
    }
}
