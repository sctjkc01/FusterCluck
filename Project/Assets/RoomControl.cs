﻿using UnityEngine;
using System.Collections;

public enum CardinalDirection {
    NORTH, SOUTH, EAST, WEST
}

public class RoomControl : MonoBehaviour {

    private SudokuPuzzle puzzleRef;
    private int x, y;
    public RoomExit NorthDoor, SouthDoor, WestDoor, EastDoor;
    public UILabel minimapNumber;

    public void Init(SudokuPuzzle puzz, int x, int y) {
        puzzleRef = puzz;
        this.x = x;
        this.y = y;
        Debug.Log(x + ", " + y, this.gameObject);

        #region DoorExit-Setting
        NorthDoor.Init(new Vector2(0f, 5f));
        SouthDoor.Init(new Vector2(0f, -5f));
        EastDoor.Init(new Vector2(5f, 0f));
        WestDoor.Init(new Vector2(-5f, 0f));
        #endregion

        #region DoorActive-Setting
        WestDoor.gameObject.SetActive(this.x != 0);
        EastDoor.gameObject.SetActive(this.x != puzzleRef.size * puzzleRef.size - 1);
        NorthDoor.gameObject.SetActive(this.y != 0);
        SouthDoor.gameObject.SetActive(this.y != puzzleRef.size * puzzleRef.size - 1);
        #endregion

        GameObject.Find("Minimap").GetComponent<MinimapControl>().Init(puzzleRef);
    }

    void Update() {
        int myNumber = puzzleRef[x, y];
        if(minimapNumber != null) {
            minimapNumber.text = (myNumber == 0 ? " " : "" + myNumber);
        }
    }

    public void IncrementNumber() {
        int myNumber = puzzleRef[x, y];
        myNumber++;
        if(myNumber > 5) {
            myNumber -= 4;
        }
        puzzleRef[x, y] = myNumber;
    }
}