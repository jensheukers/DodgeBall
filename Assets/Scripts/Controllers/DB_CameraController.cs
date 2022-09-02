using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_CameraController : MonoBehaviour {

    private List<GameObject> targets;

    private Vector3 centrePointRef;

    private void Start() {
        targets = GameObject.FindGameObjectWithTag("DB_GameMode").GetComponent<DB_GameMode>().GetPlayers();
        Debug.Log(targets.Count);
    }

    // Update is called once per frame
    void Update() {
        Vector3 centrePoint = new Vector3();
        for (int i = 0; i < targets.Count; i++) {
            centrePoint += targets[i].transform.position;
        }

        centrePoint /= targets.Count;

        transform.LookAt(centrePoint);

        centrePointRef = centrePoint;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(centrePointRef, 0.1f);
    }
}
