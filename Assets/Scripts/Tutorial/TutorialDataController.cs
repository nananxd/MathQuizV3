using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDataController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialDataPref;
    [SerializeField] private Transform tutorialParent;
    [SerializeField] private GameObject nextButton, prevButton;

    private List<GameObject> tutorialData = new List<GameObject>();
    private List<TutorialElementController> tutorialElementControllers = new List<TutorialElementController>();
    private int counter = 0;

    public void Setup(TutorialSlidesData tutSlidesData)
    {
        for (int i = 0; i < tutSlidesData.tutorialSlides.Count; i++)
        {
            var go = Instantiate(tutSlidesData.tutorialSlides[i]);
            go.transform.SetParent(tutorialParent,false);
            go.SetActive(true);
            var tutorial = go.GetComponent<TutorialElementController>();
            go.name = "Tutorial" + i.ToString();
            tutorial.Setup();
            tutorial.HideOnStart();
            tutorialElementControllers.Add(tutorial);
            go.SetActive(false);
            SetStartTutorial();
        }
        
    }
    public void Setup(TutorialData currentTutorial)
    {
        for (int i = 0; i < currentTutorial.tutorialData.Count; i++)
        {
            var go = Instantiate(tutorialDataPref);
            go.transform.SetParent(tutorialParent, false);
            go.SetActive(true);
            var tutorial = go.GetComponent<TutorialDataUI>();
            
            tutorial.Setup(
                currentTutorial.tutorialData[i].explanation,
                currentTutorial.tutorialData[i].example, 
                currentTutorial.tutorialData[i].sprite
                );
            go.name = "TUTORIAL" + i.ToString();
            tutorialData.Add(go);
            go.SetActive(false);
            SetStartTutorial();
            
        }
        prevButton.SetActive(false);
    }
    public void UnSetup()
    {
        if (tutorialElementControllers == null)
        {
            return;
        }

        foreach (var item in tutorialElementControllers)
        {
            Destroy(item.gameObject);
        }
        tutorialElementControllers.Clear();
    }
    public void Unsetup()
    {
        if (tutorialData == null)
        {
            return;
        }
        foreach (var item in tutorialData)
        {
            Destroy(item);
        }

        tutorialData.Clear();
    }

    public void SetStartTutorial()
    {
        //tutorialData[0].SetActive(true);
        tutorialElementControllers[0].gameObject.SetActive(true);
        tutorialElementControllers[0].Setup();
    }
    public void NextTutorial()
    {
        if (!tutorialElementControllers[counter].IsAllElementReveal())
        {
            tutorialElementControllers[counter].Setup();
            tutorialElementControllers[counter].RevealElement();
            return;
        }
        if (counter >= tutorialElementControllers.Count)
        {
            nextButton.SetActive(false);
            return;
        }
        prevButton.SetActive(true);
        
        if (tutorialElementControllers[counter] != null)
        {
            tutorialElementControllers[counter].gameObject.SetActive(false);
        }
        counter++;
        if (counter < tutorialElementControllers.Count && tutorialElementControllers[counter] != null)
        {
            tutorialElementControllers[counter].gameObject.SetActive(true);
        }
        else
        {
            counter = 0;
            var screen = SceneManager.Instance.GetSceneByName("Tutorial Screen");
            var screenMap = SceneManager.Instance.GetSceneByType<MapScreen>();
            var screenMapUI = SceneManager.Instance.GetSceneByType<MapUiScreen>();
            screen.Hide();
            screenMap.Show();
            screenMapUI.Show();
        }

        SoundManager.Instance.PlaySound("PanelSound");
        
    }

    public void PreviousTutorial()
    {
        SoundManager.Instance.PlaySound("PanelSound");
        if (counter <= 0)
        {
            prevButton.SetActive(false);
            return;
        }

        nextButton.SetActive(true);
        if (tutorialElementControllers[counter] != null)
        {
            tutorialElementControllers[counter].HideAllElement();
            tutorialElementControllers[counter].gameObject.SetActive(false);
        }
        counter--;
        if (counter >= 0 && tutorialElementControllers[counter] != null)
        {
            tutorialElementControllers[counter].gameObject.SetActive(true);
        }
        else
        {

        }
       
       
    }
}
