using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelController levelController;
    [SerializeField, Multiline(6)] string levelJSON;
    void Start()
    {
        Load();
    }

    void Load()
    {
        var data = JsonUtility.FromJson<LevelData>(levelJSON);
        levelController.Init(data);
    }

    public void Restart()
    {
        Load();
    }
}
