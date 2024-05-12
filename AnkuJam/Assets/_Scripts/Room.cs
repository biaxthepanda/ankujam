using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


[System.Serializable]
public class Wave
{
    public List<GameObject> prefabs;
    public List<Transform> locations;
    public float CoolDown;
}

public class Room : MonoBehaviour
{
    public RoomManager RoomManager;

    public Transform Enemies;
    public List<Wave> Waves = new List<Wave>();
    private int _currentWave = 0;
    private int _currentEnemyAmount = 0;


    void Start()
    {
        SpawnWave(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWave(int waveIdx) 
    {
        _currentEnemyAmount = Waves[waveIdx].prefabs.Count;
        List<Transform> locationsTemp = Waves[waveIdx].locations;
        foreach (var objectToSpawn in Waves[waveIdx].prefabs) 
        {
            Vector2 loc = Waves[waveIdx].locations[Random.Range(0, Waves[waveIdx].locations.Count)].position;
            GameObject spawned = Instantiate(objectToSpawn, loc, Quaternion.identity);
            spawned.GetComponent<EnemyCharacter>().OnEnemyDied += EnemyDied;
            
            spawned.transform.parent = Enemies.transform;
        }
    }

    public void EnemyDied() 
    {
        Debug.Log("ÖLDÜ");
        _currentEnemyAmount--;
        if(_currentEnemyAmount <= 0) 
        {
            if(_currentWave < Waves.Count-1) 
            {
                SpawnWave(_currentWave);
                _currentWave++;
        
            }
            else 
            {
                Debug.Log("OPEN DOORS");
            
            }
        }
    }
}
