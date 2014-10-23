using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class RoomManager : MonoBehaviour {
	public static List<string> colorValues = new List<string>{
		"FF0000", "00FF00", "0000FF", "FFFF00", "FF00FF", "00FFFF", "000000", 
		"800000", "008000", "000080", "808000", "800080", "008080", "808080", 
	};

    public int puzzleSize;

    public SudokuPuzzle puzzle;
    public List<RoomTheme> themes;

    public static GameOver GameOverRef;

    public void Initialize() {
        puzzle = new SudokuPuzzle(puzzleSize);
        GameObject.Find("Minimap").GetComponent<MinimapControl>().Init(puzzle);

        for(int cluster = 0; cluster < puzzleSize * puzzleSize; cluster++) {
            RoomTheme thisTheme = Pick(themes);
			for(int i = 0; i < thisTheme.rooms.Capacity; i++)
			{
				thisTheme.rooms[i].transform.Find("Floor").GetComponent<SpriteRenderer>().color = HexToColor(Pick(colorValues));
			}


            for(int i = 0; i < puzzleSize * puzzleSize; i++) {
                int x = i % puzzleSize + (cluster % puzzleSize) * puzzleSize;
                int y = i / puzzleSize + (cluster / puzzleSize) * puzzleSize;

                RoomControl newbie = ((GameObject)GameObject.Instantiate(Pick(thisTheme.rooms), new Vector3(10f * x, -10f * y, 0f), Quaternion.identity)).GetComponent<RoomControl>();
                newbie.Init(puzzle, x, y);
                if(!puzzle.canChange[x, y]) {
                    Destroy(newbie.transform.FindChild("Button").gameObject);
                }
            }
        }

        GameObject.Find("Main Camera").GetComponent<MainCameraControl>().target = Vector2.zero;
    }

    T Pick<T>(List<T> from) {
        if(from.Count == 0) throw new Exception("Cannot pick from an empty list!");
        return (from[Random.Range(0, from.Count)]);
    }
	public Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r,g,b, 255);
	}
}

[System.Serializable]
public class SudokuPuzzle {
    public int[,] tiles;
    public bool[,] canChange;
    public int factorial, summation, size;

    public SudokuPuzzle(int s) {
        size = s * s;
        tiles = new int[size, size];
        canChange = new bool[size, size];
        factorial = 1;
        summation = 0;

        for(int i = 0; i < size; i++) {
            factorial *= (i + 1);
            summation += (i + 1);

            for(int j = 0; j < size; j++) {
                tiles[i, j] = 0;
                canChange[i, j] = true;
            }
        }

        createPuzzle();
    }

    public int this[int i, int j] {
        get {
			return tiles[i, j];
        }

        set {
            Debug.Log("Setting [" + i + "," + j + "] to " + value);
            tiles[i, j] = value;

            if(colTest() && rowTest() && chunkTest()) {
                Debug.Log("Tests passed.");
                RoomManager.GameOverRef.GameDone(true);
            } else {
                Debug.Log("Tests failed.");
            }
        }
    }

    void createPuzzle()
	{
        List<int> num = new List<int>();
        
        for (int i = 0; i < size; i++)
        {
            num.Add(i + 1);
        }
        
        for (int i = 0; i < size; i += (int)Math.Sqrt(size))
        {
            for (int j = 0; j < size; j += (int)Math.Sqrt(size))
            {
                int quad1 = Random.Range(0, (int)Math.Sqrt(size));
                int quad2 = Random.Range(0, (int)Math.Sqrt(size));
                int index = Random.Range(0, num.Count);
        
                tiles[i + quad1, j + quad2] = num[index];
                canChange[i + quad1, j + quad2] = false;
                num.RemoveAt(index);
            }
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
        int sqrtSize = (int)Math.Sqrt(size);

	    for (int i = 0; i < size; i += sqrtSize)
	    {
	        for (int j = 0; j < size; j += sqrtSize)
	        {
	            int sum = 0;
	            int fact = 1;

	            for (int k = 0; k < sqrtSize; k++) {
	                for (var l = 0; l < sqrtSize; l++) {
	                    sum += tiles[i + k, j + l];
	                    fact *= tiles[i + k, j + l];
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