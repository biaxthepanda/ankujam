using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public Room[] Rooms;
    private Room _currentRoom;
    private int _currentRoomIndex = -1;
    public PlayerCharacter Player;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter.OnPlayerDied += FailedRoom;
        Player = LevelManager.Player.GetComponent<PlayerCharacter>();
        NextRoom();
    }

    public void NextRoom() 
    {
        _currentRoomIndex++;
        SpawnRoomCurrentRoom();
    }

    public void FailedRoom() 
    {
        Destroy(_currentRoom.gameObject);
        StartCoroutine(StartRoomAgain());
    }


    IEnumerator StartRoomAgain()
    {
        yield return new WaitForSeconds(2f);
        SpawnRoomCurrentRoom();
    }

    private void SpawnRoomCurrentRoom() 
    {
        _currentRoom = Instantiate(Rooms[_currentRoomIndex], Vector3.zero, Quaternion.identity);
        _currentRoom.RoomManager = this;
    }

}
