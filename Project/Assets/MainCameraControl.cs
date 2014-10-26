using UnityEngine;
using System.Collections;

public class MainCameraControl : MonoBehaviour {
    public static bool Started;

    public Vector2 target;
    public UI2DSprite URHereMarker;
    public int URHereMarkerTargetX, URHereMarkerTargetY;

    private MinimapControl mmc;

    void Start() {
        Started = false;

        mmc = GameObject.Find("Minimap").GetComponent<MinimapControl>();
        URHereMarkerTargetX = 0;
        URHereMarkerTargetY = 0;
    }

    void Update() {
        if(Started) {
            Vector3 targetPos = (Vector3)target + new Vector3(0f, 0f, -17.525f);

            if(Vector3.Distance(transform.position, targetPos) < 0.01f) {
                transform.position = targetPos;
            } else {
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10f);
            }

            targetPos = mmc.UILabels[URHereMarkerTargetX, URHereMarkerTargetY].transform.position;

            if(Vector3.Distance(URHereMarker.transform.position, targetPos) < 0.01f) {
                URHereMarker.transform.position = targetPos;
            } else {
                URHereMarker.transform.position = Vector3.Lerp(URHereMarker.transform.position, targetPos, Time.deltaTime * 10f);
            }
        }
    }
}
