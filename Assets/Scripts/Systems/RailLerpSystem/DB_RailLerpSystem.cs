using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_RailLerpSystem : MonoBehaviour
{
    [SerializeField]
    private List<Transform> lerpPoints;

    private GameObject attachedObject;

    [SerializeField]
    private float lerpSpeed = 0.5f;

    [SerializeField]
    private float lerpValue = 0.0f;

    [SerializeField]
    private bool bounce = false;

    [SerializeField]
    private bool move = true;

    private int currentLerpPointIndex = 0;

    private bool _lerpRetreat = false; // if true currentLerpPointIndex will be subtracted every frame, if false it will be incremented

    void Update() {
        if (!attachedObject || !move) return;
        if (lerpValue > 1) {
            lerpValue = 0;
            if (_lerpRetreat) currentLerpPointIndex--;
            else currentLerpPointIndex++;
        }
        else {
            if (!_lerpRetreat) {
                lerpValue += lerpSpeed * Time.deltaTime;
                attachedObject.transform.position = Vector3.Lerp(lerpPoints[currentLerpPointIndex].position, lerpPoints[currentLerpPointIndex + 1].position, lerpValue);   
            }
            else {
                lerpValue += lerpSpeed * Time.deltaTime;
                attachedObject.transform.position = Vector3.Lerp(lerpPoints[currentLerpPointIndex].position, lerpPoints[currentLerpPointIndex - 1].position, lerpValue);
            }
        }

        if (bounce) {
            if (currentLerpPointIndex >= lerpPoints.Count - 1) {
                currentLerpPointIndex = lerpPoints.Count - 1;
                _lerpRetreat = true;
            }
            else if (currentLerpPointIndex <= 0) {
                currentLerpPointIndex = 0;
                _lerpRetreat = false;
            }
        }
    }

    private void OnDrawGizmos() {
        for (int i = 0; i < lerpPoints.Count; i++) {
            if (i != (lerpPoints.Count - 1)) {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(lerpPoints[i].position, lerpPoints[i + 1].position);
            }
        }
    }

    public void SetLerpValue(float value) {
        this.lerpValue = value;
    }

    public void SetAttachedObject(GameObject obj) {
        this.attachedObject = obj;
    }

    public float GetLenght() {
        float lenght = 0;
        for (int i = 0; i < lerpPoints.Count; i++) {
            if (i + 1 != lerpPoints.Count) {
                lenght += Vector3.Distance(lerpPoints[i].transform.position, lerpPoints[i + 1].transform.position);
            }
        }
        return lenght;
    }
}
