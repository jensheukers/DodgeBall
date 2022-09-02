using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_GameMode : MonoBehaviour {
    private List<GameObject> players;

    private void Awake() {
        players = new List<GameObject>();
        GameObject[] _list = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < _list.Length; i++) {
            players.Add(_list[i]);
        }
    }

    public List<GameObject> GetPlayers() {
        return players;
    }
}
