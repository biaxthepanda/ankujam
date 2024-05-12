using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    #region Singleton
    public static RoomManager Instance { get; private set; }



    private void Awake()
    {
        // Bir örnek varsa ve ben deðilse, yoket.

        if (Instance != null && Instance != this)
        {
            return;
        }
        Instance = this;
    }

    #endregion

    public Room[] Rooms;
    private Room _currentRoom;
    private int _currentRoomIndex = -1;
    public PlayerCharacter Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = LevelManager.Player.GetComponent<PlayerCharacter>();
        NextRoom();
    }

    public void NextRoom() 
    {
        _currentRoomIndex++;
        if(_currentRoomIndex >= Rooms.Length) 
        {
            Debug.LogWarning("YOU WON");
        }
        else
        {
           StartCoroutine(SpawnRoomCurrentRoom());
        }
    }


    public void FailedRoom() 
    {
        Destroy(_currentRoom.gameObject);
        StartCoroutine(StartRoomAgain());
    }


    IEnumerator StartRoomAgain()
    {
        yield return new WaitForSeconds(3f);
        Player.transform.position = Vector2.zero;
        StartCoroutine(SpawnRoomCurrentRoom());
        Player.ResetPlayer();
    }

    IEnumerator SpawnRoomCurrentRoom() 
    {
        yield return new WaitForSeconds(2f);

        Player.ResetPlayer();
        SoundManager.Instance.PlayOneShot(SoundManager.Sounds.doorClose);
        _currentRoom = Instantiate(Rooms[_currentRoomIndex], Vector3.zero, Quaternion.identity);
        _currentRoom.RoomManager = this;
        ExpressionManager.Instance.CreateExpression("FLOOR: " + (_currentRoomIndex+1).ToString(),Color.white,2f);
    }

}
