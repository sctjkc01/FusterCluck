using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour {
    public SudokuPuzzle puzzle;
    public List<RoomTheme> themes;

    // Use this for initialization
    void Start() {

        puzzle = new SudokuPuzzle(2);

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
    public int[,] tiles;
    public int factorial, summation, size;

    public SudokuPuzzle(int s) 
    {
        size = s;
        tiles = new array[size,size];
        factorial = 1;
        summation = 0;

        for (int i = 0; i < size; i++ )
        {
            factorial *= i;
            summation += i;

            for (int j = 0; j < size; j++)
            {
                tiles[i,j] = 0;
            }
        }
    }

    public int this[int i, int j]
    {
        get
        {
            return tiles[i, j];
        }

        set
        {
            tiles[i, j] = value;
        }
    }

    bool colTest()
    {
        for (int i = 0; i < size; i++)
        {
            var sum = 0;
            var fact = 1;

            for (int j = 0; j < size; j++)
            {
                sum += tiles[i][j];
                fact *= tiles[i][j];
            }

            if (sum != summation && fact != factorial)
            {
                return false;
            }
        }

        return true;
    }

    bool rowTest()
    {
        for (int j = 0; j< size; j++)
        {
            var sum = 0;
            var fact = 1;

            for (int i = 0; i < size; i++)
            {
                sum += tiles[i][j];
                fact *= tiles[i][j];
            }

            if (sum != summation && fact != factorial)
            {
                return false;
            }
        }

        return true;
    }
}

[System.Serializable]
public class RoomTheme {
    public List<GameObject> rooms;
}