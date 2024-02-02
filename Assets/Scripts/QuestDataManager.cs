using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDataManager : MonoBehaviour
{
    [SerializeField] private List<QuestData> quests;
    private static QuestDataManager instance = null;
    public static QuestDataManager Instance
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

    public QuestData GetQuestByTopic(Topic currentTopic)
    {
        //hints = new List<HintData>();
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].topic == currentTopic)
            {
                
                return quests[i];
            }
        }
        return null;
    }
}
