using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour {
    public int puzzleSize;

    public SudokuPuzzle puzzle;
    public List<RoomTheme> themes;

    void Start() {
        Initialize(); // In case we want to allow the user to change the size of the puzzle...
    }


    void Initialize() {

        puzzle = new SudokuPuzzle(puzzleSize);
        GameObject.Find("Minimap").GetComponent<MinimapControl>().Init(puzzle);

        for(int cluster = 0; cluster < puzzleSize * puzzleSize; cluster++) {
            RoomTheme thisTheme = Pick(themes);

            for(int i = 0; i < puzzleSize * puzzleSize; i++) {
                int x = i % puzzleSize + (cluster % puzzleSize) * puzzleSize;
                int y = i / puzzleSize + (cluster / puzzleSize) * puzzleSize;

                RoomControl newbie = ((GameObject)GameObject.Instantiate(Pick(thisTheme.rooms), new Vector3(10f * x, -10f * y, 0f), Quaternion.identity)).GetComponent<RoomControl>();
                newbie.Init(puzzle, x, y);
            }
        }

        GameObject.Find("Main Camera").GetComponent<MainCameraControl>().target = Vector2.zero;
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

    public SudokuPuzzle(int s) {
        size = s * s;
        tiles = new int[size, size];
        factorial = 1;
        summation = 0;

        for(int i = 0; i < size; i++) {
            factorial *= (i + 1);
            summation += (i + 1);

            for(int j = 0; j < size; j++) {
                tiles[i, j] = 0;
            }
        }
    }

    public int this[int i, int j] {
        get {
			return tiles[i, j];
        }

        set {
            tiles[i, j] = value;
        }
    }

    bool colTest() {
        for(int i = 0; i < size; i++) {
            int sum = 0;
            int fact = 1;

            for(int j = 0; j < size; j++) {
                sum += tiles[i, j];
                fact *= tiles[i, j];
            }

            if(sum != summation && fact != factorial) {
                return false;
            }
        }

        return true;
    }

    bool rowTest() {
        for(int j = 0; j < size; j++) {
            int sum = 0;
            int fact = 1;

            for(int i = 0; i < size; i++) {
                sum += tiles[i, j];
                fact *= tiles[i, j];
            }

            if(sum != summation && fact != factorial) {
                return false;
            }
        }

        return true;
    }

    bool chunkTest() 
    {
	    for (int i = 0; i < size; i += Math.Sqrt(size))
	    {
	        for (int j = 0; j < size; j += Math.Sqrt(size))
	        {
	            int sum = 0;
	            int fact = 1;

	            for (int k = 0; k < 3; k++) {
	                for (var l = 0; l < 3; l++) {
	                    sum += tiles[i + k, j + l];
	                    fact *= tiles[i + k, k + l];
	                }
	            }

	            if (sum != summation && fact != factorial) {
	                return false;
	            }
	        }
	    }

	    return true;
	}
}

[System.Serializable]
public class RoomTheme {
    public List<GameObject> rooms;
}