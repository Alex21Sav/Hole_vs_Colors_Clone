using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level Instance;

    [SerializeField] private Transform _objectsParent;

    public int ObjectsInScene;
    public int TotalObjects;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CountObjects();
    }

    private void CountObjects()
    {
        TotalObjects = _objectsParent.childCount;
        ObjectsInScene = TotalObjects;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
