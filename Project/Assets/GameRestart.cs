using UnityEngine;
using System.Collections;

public class GameRestart : MonoBehaviour {
    public void RestartGame() {
        Application.LoadLevel(0);
    }
}
