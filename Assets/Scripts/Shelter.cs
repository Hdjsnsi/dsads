using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : MonoBehaviour
{
    public GameObject soldier;
    [Header("Shelter status")]
    public float radius,spawnTime;
    public int shelterLevel,canSpawn;
    private float spawnCountdown;
    private int onSpawn;
    private List<GameObject> soldiersHaveSpawn;
    public AiSystem.LineUpDirection lineUpDirection;

    void Start()
    {   
        spawnCountdown = spawnTime;
    }
            
    // Update is called once per frame
    void FixedUpdate()
    {   
        if(spawnCountdown > 0) 
        {
            spawnCountdown -= Time.deltaTime;
        }
        
        if(spawnCountdown < 0 && onSpawn < canSpawn)
        {
            SpawnSoldier();
            spawnCountdown = spawnTime;
        }

    }
    void CheckingOurForce()
    {
        Transform[] forceOfTheArmed = GetComponentsInChildren<Transform>();
        soldiersHaveSpawn = new List<GameObject>();
        onSpawn = 0;
        foreach(Transform child in forceOfTheArmed)
        {
            if(child.parent == transform)
            {
                soldiersHaveSpawn.Add(child.gameObject);
                onSpawn++;
            }
        }

    }
    void SpawnSoldier()
    {
        
        GameObject game = Instantiate(soldier,transform.position,soldier.transform.rotation);
        game.transform.SetParent(transform);
        CheckingOurForce();
        GetInTheLine();
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,radius);
        for(int i = 0; i < 10; i++)
        {
            Gizmos.DrawWireSphere(LineUp(i),.5f);
        }
    }
    void GetInTheLine()
    {
        for(int i = 0; i < soldiersHaveSpawn.Count; i++)
        {
            soldiersHaveSpawn[i].transform.position = LineUp(i);
        }
    }
    Vector3 LineUp(int i)
    {
        LayerMask groundLayer;
        groundLayer = LayerMask.GetMask("What is Ground");
        
        Vector3 location = AiSystem.WhereToMove(transform.position,lineUpDirection);
        Vector3 newPos = new Vector3();
        if(lineUpDirection == AiSystem.LineUpDirection.Forward || lineUpDirection == AiSystem.LineUpDirection.Back)
        {
            newPos = new  Vector3(location.x + i * 1.5f ,location.y,location.z);

        }else if(lineUpDirection == AiSystem.LineUpDirection.Right || lineUpDirection == AiSystem.LineUpDirection.Left)
        {
            newPos = new  Vector3(location.x ,location.y,location.z + i * 1.5f);
        }
        if(Physics.Raycast(newPos,Vector3.down,out RaycastHit hit, 2000, groundLayer))
        {
            newPos.y = hit.point.y + .5f;
        }
        return newPos;
    }
    
}
