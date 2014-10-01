using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour {
    public SudokuPuzzle puzzle;
    public List<RoomTheme> themes;

    // Use this for initialization
    void Start() {

        puzzle = new SudokuPuzzle();

        // FU TODO: do Sudoku-gen here.

        List<RoomControl> rooms = new List<RoomControl>();

        for(int cluster = 0; cluster < 4; cluster++) {
            RoomTheme thisTheme = Pick(themes);

            Vector3 topLeft = new Vector3((cluster % 2) * 20f, (cluster > 1 ? -20f : 0f), 0f);

            RoomControl newbie = (GameObject.Instantiate(Pick(thisTheme.rooms), topLeft, Quaternion.identity) as GameObject).GetComponent<RoomControl>();
            newbie.Init(puzzle, cluster, 0);
            puzzle[cluster].tlRoom = newbie;
            rooms.Add(newbie);

            newbie = (GameObject.Instantiate(Pick(thisTheme.rooms), topLeft + new Vector3(10f, 0f), Quaternion.identity) as GameObject).GetComponent<RoomControl>();
            newbie.Init(puzzle, cluster, 1);
            puzzle[cluster].trRoom = newbie;
            rooms.Add(newbie);

            newbie = (GameObject.Instantiate(Pick(thisTheme.rooms), topLeft + new Vector3(0f, -10f), Quaternion.identity) as GameObject).GetComponent<RoomControl>();
            newbie.Init(puzzle, cluster, 2);
            puzzle[cluster].blRoom = newbie;
            rooms.Add(newbie);

            newbie = (GameObject.Instantiate(Pick(thisTheme.rooms), topLeft + new Vector3(10f, -10f), Quaternion.identity) as GameObject).GetComponent<RoomControl>();
            newbie.Init(puzzle, cluster, 3);
            puzzle[cluster].brRoom = newbie;
            rooms.Add(newbie);
        }

        foreach(RoomControl alpha in rooms) {
            alpha.CameraRoomLink();
        }

        GameObject.Find("Main Camera").GetComponent<MainCameraControl>().target = rooms[0].GetComponent<RoomControl>();
    }

    T Pick<T>(List<T> from) {
        if(from.Count == 0) throw new Exception("Cannot pick from an empty list!");
        return (from[Random.Range(0, from.Count)]);
    }
}

[System.Serializable]
public class SudokuPuzzle {
    public SudokuChunk topLeft, topRight, bottomLeft, bottomRight;

    public SudokuPuzzle() {
        topLeft = new SudokuChunk();
        topRight = new SudokuChunk();
        bottomLeft = new SudokuChunk();
        bottomRight = new SudokuChunk();
    }

    /// <summary>
    /// Gets a Chunk based on an int index.  Zero is top-left, and continues in American reading-order.
    /// </summary>
    /// <param name="i">Int 0 - 3</param>
    public SudokuChunk this[int i] {
        get {
            switch(i) {
                case 0:
                    return topLeft;
                case 1:
                    return topRight;
                case 2:
                    return bottomLeft;
                case 3:
                    return bottomRight;
                default:
                    throw new Exception("Bad Sudoku Puzzle index!");
            }
        }
    }
}

[System.Serializable]
public class SudokuChunk {
    public int topLeft, topRight, bottomLeft, bottomRight;
    public RoomControl tlRoom, trRoom, blRoom, brRoom;

    public SudokuChunk() {
        topLeft = 0;
        topRight = 0;
        bottomLeft = 0;
        bottomRight = 0;
    }

    /// <summary>
    /// Gets a value based on an int index.  Zero is top-left, and continues in American reading-order.
    /// </summary>
    /// <param name="i">Int 0 - 3</param>
    public int this[int i] {
        get {
            switch(i) {
                case 0:
                    return topLeft;
                case 1:
                    return topRight;
                case 2:
                    return bottomLeft;
                case 3:
                    return bottomRight;
                default:
                    throw new Exception("Bad Sudoku Chunk index!");
            }
        }
        set {
            switch(i) {
                case 0:
                    topLeft = value;
                    break;
                case 1:
                    topRight = value;
                    break;
                case 2:
                    bottomLeft = value;
                    break;
                case 3:
                    bottomRight = value;
                    break;
                default:
                    throw new Exception("Bad Sudoku Chunk index!");
            }
        }
    }
}

[System.Serializable]
public class RoomTheme {
    public List<GameObject> rooms;
}