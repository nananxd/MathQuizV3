using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsDataManager : MonoBehaviour
{
    public List<TutorialData> tutorialDatas;
    public List<TutorialSlidesData> tutorialSlides;

    private static TutorialsDataManager instance = null;
    public static TutorialsDataManager Instance
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
    public TutorialData GetTutorialDataByTopic(Topic topics)
    {
        for (int i = 0; i < tutorialDatas.Count; i++)
        {
            if (tutorialDatas[i].topic == topics)
            {
                return tutorialDatas[i];
            }
        }

        return null;
    }

    public TutorialSlidesData GetTutorialSlidesByTopic(Topic topic)
    {
        for (int i = 0; i < tutorialSlides.Count; i++)
        {
            if (tutorialSlides[i].topic == topic)
            {
                return tutorialSlides[i];
            }
        }
        return null;
    }
}
