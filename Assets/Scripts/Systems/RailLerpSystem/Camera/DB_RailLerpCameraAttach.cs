using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_RailLerpCameraAttach : DB_RailLerpAttach {
    DB_GameMode gameMode;

    DB_RailLerpSystem _railSystem;
    private void Start() {
        gameMode = GameObject.FindGameObjectWithTag("DB_GameMode").GetComponent<DB_GameMode>();
        _railSystem = GetComponent<DB_RailLerpSystem>();
    }

    void Update() {
        // Get distance between players
        List<GameObject> players = gameMode.GetPlayers();

        float _distance = 0;
        for (int i = 0; i < players.Count; i++) {
            _distance += Vector3.Distance(attach.transform.position, players[i].transform.position);
        }

        _distance = _distance / players.Count;
        _distance = Mathf.Clamp(_distance / _railSystem.GetLenght(), 0, _railSystem.GetLenght());

        _railSystem.SetLerpValue(_distance);
    }
}
