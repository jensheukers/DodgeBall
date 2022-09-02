using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_RailLerpAttach : MonoBehaviour {

    [SerializeField]
    public GameObject attach;

    void Awake() {
        if (!this.GetComponent<DB_RailLerpSystem>()) {
            Debug.LogError("DB_RailLerpAttach requires a DB_RailLerpSystem component");
            return;
        }

        this.GetComponent<DB_RailLerpSystem>().SetAttachedObject(attach);
    }
}
