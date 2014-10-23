using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    public GameObject EndScreen;
    public UILabel label;

    public void GameDone(bool win) {
        this.gameObject.SetActive(true);
        EndScreen.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerControl>().enabled = false;

        label.text = win ? "Conglaturations!" : "Game Over.\nYou Died.";
    }

}
