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

    public float SecondsBetweenWaves;

    public Transform Enemies;
    public List<Wave> Waves = new List<Wave>();
    public Transform[] HealthPositions;
    private int _currentWave = 0;
    private int _currentEnemyAmount = 0;

    public ExtraHealth HealthItem;

    void Start()
    {
        StartCoroutine(SpawnWave(_currentWave));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnWave(int waveIdx) 
    {
        yield return new WaitForSeconds(SecondsBetweenWaves);
        ExpressionManager.Instance.CreateExpression("WAVE " + (_currentWave+1).ToString()+"/"+Waves.Count.ToString(),Color.white,1f,125);
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
                _currentWave++;
                SpawnHealths();
                StartCoroutine(SpawnWave(_currentWave));


            }
            else 
            {
                RoomManager.NextRoom();
            
            }
        }
    }

    private void SpawnHealths() 
    {
        int amount = Random.Range(0,4);
        for(int i = 0; i < amount; i++) 
        {
            Instantiate(HealthItem, HealthPositions[Random.Range(0, HealthPositions.Length - 1)]);
        }
    }
}
