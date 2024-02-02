using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepSummaryController : MonoBehaviour
{
    [SerializeField] private GameObject stepPrefab;
    [SerializeField] private Transform stepParent;
    [SerializeField] private Button closeButton;
    [SerializeField] private List<TutorialSlidesData> slidesDatas;
    [SerializeField] private TutorialSlidesData tutorialSlidesDatas;
    private List<GameObject> currentSteps = new List<GameObject>();
    private TutorialSlidesData currentSlidesData;

    
    public void Setup()
    {
        currentSlidesData = GetStep();
       // tutorialSlidesDatas = TutorialsDataManager.Instance.GetTutorialSlidesByTopic(GameManager.Instance.currentTopic);
        for (int i = 0; i < currentSlidesData.tutorialSlides.Count; i++)
        {
            //var go = Instantiate(stepPrefab);
            var go = Instantiate(currentSlidesData.tutorialSlides[i]);
            go.transform.SetParent(stepParent, false);
            go.SetActive(true);
            currentSteps.Add(go);
            //var step = go.GetComponent<StepUI>();
            //step.Setup(
            //    currentStepData.steps[i].explanation,
            //    currentStepData.steps[i].example
            //    currentStepData.steps[i].sprite);
        }
        
    }

    public void Unsetup()
    {
        for (int i = 0; i < currentSteps.Count; i++)
        {
            Destroy(currentSteps[i]);
        }

        currentSteps.Clear();
    }

    private TutorialSlidesData GetStep()
    {
        for (int i = 0; i < slidesDatas.Count; i++)
        {
            if (slidesDatas[i].topic == GameManager.Instance.currentTopic)
            {
                return slidesDatas[i];
            }
        }

        return null;
    }
}
