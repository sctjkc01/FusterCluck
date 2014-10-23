using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
    public PlayerControl player;
    public RoomManager rm;
    public GameObject cam;

    public void Start2() {
        StartTheGame(2);
    }

    public void Start3() {
        StartTheGame(3);
    }

    public void StartTheGame(int size) {
        RoomManager.GameOverRef = cam.GetComponent<GameOver>();
        rm.puzzleSize = size;
        rm.Initialize();

        player.enabled = true;
        cam.SetActive(false);
    }
}
