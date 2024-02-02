using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public List<BaseScene> scenes;

    private static SceneManager instance = null;
    public static SceneManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public BaseScene GetSceneByName(string name)
    {
        for (int i = 0; i < scenes.Count; i++)
        {
            if (scenes[i].sceneName == name)
            {
                return scenes[i];
            }
        }

        return null;
    }

    public List<BaseScene> GetAllScenes()
    {
        return scenes;
    }

    public void Initialize()
    {
        for (int i = 0; i < scenes.Count; i++)
        {
            if (scenes[i].showAtStart)
            {
                scenes[i].Show();
            }
            else
            {
                scenes[i].Hide();
            }
        }
    }

    public T GetSceneByType<T>()
    {
        BaseScene foundScreen = scenes.FirstOrDefault(s => s is T);

        if (foundScreen)
        {
            return (T)(object)foundScreen;
        }
        else
        {
            Debug.LogWarning("Couldn't find a screen by Type [" + typeof(T).Name + "]");
            return default(T);
        }
    }
}
