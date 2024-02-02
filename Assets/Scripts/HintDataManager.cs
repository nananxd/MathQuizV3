using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintDataManager : MonoBehaviour
{
    [SerializeField] private List<HintData> hints;
    private static HintDataManager instance = null;
    public static HintDataManager Instance
    {
        get { return instance; }
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

    public HintData GetHintByTopic(Topic currentTopic)
    {
        //hints = new List<HintData>();
        for (int i = 0; i < hints.Count; i++)
        {
            if (hints[i].topic == currentTopic)
            {
                Debug.Log(hints[i].topic.ToString());
                return hints[i];
            }
        }
        return null;
    }
}
