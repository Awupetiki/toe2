using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrefabStorage
{
    private List<GameObject> _prefabs = new List<GameObject>();

    public int Push(GameObject go)
    {
        int index = _prefabs.Count;
        
        Deactivate(go);
        _prefabs.Add(go);
        
        return index;
    }

    public GameObject Seek(int index)
    {
        return _prefabs[index];
    }

    public GameObject Pop(int index)
    {
        var go = _prefabs[index];
        _prefabs[index] = null;
        
        go.SetActive(true);
        SceneManager.MoveGameObjectToScene(go, SceneManager.GetActiveScene());
        
        return go;
    }

    public GameObject Duplicate(int index)
    {
        var go = Object.Instantiate(_prefabs[index], null, true);
        go.SetActive(true);
        SceneManager.MoveGameObjectToScene(go, SceneManager.GetActiveScene());
        return go;
    }

    private static void Deactivate(GameObject go)
    {
        go.transform.parent = null;
        go.SetActive(false);
        Object.DontDestroyOnLoad(go);
    }
}
