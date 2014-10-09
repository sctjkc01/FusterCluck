using UnityEngine;
using System.Collections;

public class MainCameraControl : MonoBehaviour {

    public Vector2 target;
    public UI2DSprite URHereMarker;

    void Update() {

        Vector3 targetPos = (Vector3)target + new Vector3(0f, 0f, -17.525f);

        if(Vector3.Distance(transform.position, targetPos) < 0.01f) {
            transform.position = targetPos;
        } else {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10f);
        }

        //targetPos = target.minimapNumber.transform.position;

        //if(Vector3.Distance(URHereMarker.transform.position, targetPos) < 0.01f) {
        //    URHereMarker.transform.position = targetPos;
        //} else {
        //    URHereMarker.transform.position = Vector3.Lerp(URHereMarker.transform.position, targetPos, Time.deltaTime * 10f);
        //}
    }
}
