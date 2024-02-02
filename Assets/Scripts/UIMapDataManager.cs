using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIMapDataManager : MonoBehaviour
{
    private static UIMapDataManager instance = null;
    public List<UIMapData> uIMapDatas;
    public static UIMapDataManager Instance
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

    public UIMapData GetMapByTopic(Topic topic)
    {
        var map = uIMapDatas.Find(x => x.topic == topic);
        if (map != null)
        {
            return map;
        }
        return null;
    }
}
