using UnityEngine;
using System.Collections;

public enum CardinalDirection {
    NORTH, SOUTH, EAST, WEST
}

public class RoomControl : MonoBehaviour {

    private SudokuPuzzle puzzleRef;
    private int clusterIndex, innerIndex;
    public RoomExit NorthDoor, SouthDoor, WestDoor, EastDoor;
    public UILabel minimapNumber;

    public void Init(SudokuPuzzle puzz, int clusterIndex, int innerIndex) {
        puzzleRef = puzz;
        this.clusterIndex = clusterIndex;
        this.innerIndex = innerIndex;

        #region DoorExit-Setting
        NorthDoor.Init(new Vector2(0f, 5f));
        SouthDoor.Init(new Vector2(0f, -5f));
        EastDoor.Init(new Vector2(5f, 0f));
        WestDoor.Init(new Vector2(-5f, 0f));
        #endregion

        #region DoorActive-Setting
        switch(clusterIndex) {
            case 0:
                switch(innerIndex) {
                    case 0:
                        NorthDoor.gameObject.SetActive(false);
                        WestDoor.gameObject.SetActive(false);
                        break;
                    case 1:
                        NorthDoor.gameObject.SetActive(false);
                        break;
                    case 2:
                        WestDoor.gameObject.SetActive(false);
                        break;
                    case 3:
                    default:
                        break;
                }
                break;
            case 1:
                switch(innerIndex) {
                    case 1:
                        NorthDoor.gameObject.SetActive(false);
                        EastDoor.gameObject.SetActive(false);
                        break;
                    case 0:
                        NorthDoor.gameObject.SetActive(false);
                        break;
                    case 3:
                        EastDoor.gameObject.SetActive(false);
                        break;
                    case 2:
                    default:
                        break;
                }
                break;
            case 2:
                switch(innerIndex) {
                    case 2:
                        SouthDoor.gameObject.SetActive(false);
                        WestDoor.gameObject.SetActive(false);
                        break;
                    case 0:
                        WestDoor.gameObject.SetActive(false);
                        break;
                    case 3:
                        SouthDoor.gameObject.SetActive(false);
                        break;
                    case 1:
                    default:
                        break;
                }
                break;
            case 3:
                switch(innerIndex) {
                    case 3:
                        SouthDoor.gameObject.SetActive(false);
                        EastDoor.gameObject.SetActive(false);
                        break;
                    case 1:
                        EastDoor.gameObject.SetActive(false);
                        break;
                    case 2:
                        SouthDoor.gameObject.SetActive(false);
                        break;
                    case 0:
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        #endregion

        Transform minimap = GameObject.Find("Minimap").transform;

        Transform minimapQuarter;
        switch(clusterIndex) {
            case 0:
                minimapQuarter = minimap.FindChild("TopLeft");
                break;
            case 1:
                minimapQuarter = minimap.FindChild("TopRight");
                break;
            case 2:
                minimapQuarter = minimap.FindChild("BottomLeft");
                break;
            case 3:
                minimapQuarter = minimap.FindChild("BottomRight");
                break;
            default:
                return;
        }

        switch(innerIndex) {
            case 0:
                minimapNumber = minimapQuarter.FindChild("TLCell").GetComponent<UILabel>();
                break;
            case 1:
                minimapNumber = minimapQuarter.FindChild("TRCell").GetComponent<UILabel>();
                break;
            case 2:
                minimapNumber = minimapQuarter.FindChild("BLCell").GetComponent<UILabel>();
                break;
            case 3:
                minimapNumber = minimapQuarter.FindChild("BRCell").GetComponent<UILabel>();
                break;
            default:
                return;
        }
    }

    public void CameraRoomLink() {
        switch(innerIndex) {
            case 0:
                SouthDoor.CameraRoomLink(puzzleRef[clusterIndex].blRoom);
                EastDoor.CameraRoomLink(puzzleRef[clusterIndex].trRoom);
                if(clusterIndex % 2 == 1) {
                    WestDoor.CameraRoomLink(puzzleRef[clusterIndex - 1].trRoom);
                }
                if(clusterIndex > 1) {
                    NorthDoor.CameraRoomLink(puzzleRef[clusterIndex - 2].blRoom);
                }
                return;
            case 1:
                SouthDoor.CameraRoomLink(puzzleRef[clusterIndex].brRoom);
                WestDoor.CameraRoomLink(puzzleRef[clusterIndex].tlRoom);
                if(clusterIndex % 2 == 0) {
                    EastDoor.CameraRoomLink(puzzleRef[clusterIndex + 1].tlRoom);
                }
                if(clusterIndex > 1) {
                    NorthDoor.CameraRoomLink(puzzleRef[clusterIndex - 2].brRoom);
                }
                return;
            case 2:
                NorthDoor.CameraRoomLink(puzzleRef[clusterIndex].tlRoom);
                EastDoor.CameraRoomLink(puzzleRef[clusterIndex].brRoom);
                if(clusterIndex % 2 == 1) {
                    WestDoor.CameraRoomLink(puzzleRef[clusterIndex - 1].brRoom);
                }
                if(clusterIndex < 2) {
                    SouthDoor.CameraRoomLink(puzzleRef[clusterIndex + 2].tlRoom);
                }
                return;
            case 3:
                NorthDoor.CameraRoomLink(puzzleRef[clusterIndex].trRoom);
                WestDoor.CameraRoomLink(puzzleRef[clusterIndex].blRoom);
                if(clusterIndex % 2 == 0) {
                    EastDoor.CameraRoomLink(puzzleRef[clusterIndex + 1].blRoom);
                }
                if(clusterIndex < 2) {
                    SouthDoor.CameraRoomLink(puzzleRef[clusterIndex + 2].trRoom);
                }
                return;
            default:
                return;
        }
    }

    void Update() {
        int myNumber = puzzleRef[clusterIndex][innerIndex];
        if(minimapNumber != null) {
            minimapNumber.text = (myNumber == 0 ? " " : "" + myNumber);
        }
    }

    public void IncrementNumber() {
        int myNumber = puzzleRef[clusterIndex][innerIndex];
        myNumber++;
        if(myNumber > 5) {
            myNumber -= 4;
        }
        puzzleRef[clusterIndex][innerIndex] = myNumber;
    }
}