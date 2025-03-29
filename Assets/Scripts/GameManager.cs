using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject stonePrefab; 
    public Transform stoneSpawnPoint;
    public GameObject Camera; 
    public GameObject gameWinPanel; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ResetStone(GameObject stone)
    {
        yield return new WaitForSeconds(1f); 
        GameObject newStone = Instantiate(stonePrefab, stoneSpawnPoint.position, Quaternion.identity);
        Camera.GetComponent<CameraFollow>().target = newStone.transform; 
        Destroy(stone);
    }

    public void WinGame()
    {
        gameWinPanel.SetActive(true);
    } 

}
