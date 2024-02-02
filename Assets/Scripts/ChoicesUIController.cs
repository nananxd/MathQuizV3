using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;

public class ChoicesUIController : UIPanel
{
    [SerializeField] private GameObject choicesPrefab;
    [SerializeField] private GameObject choiceDuplicate;

    [SerializeField] private ChoicesData choicesData;

    [SerializeField] private TextMeshProUGUI questionText;

    public RectTransform parent;
    public RectTransform canvasParent;

    private List<ChoicesUI> instantiatedChoices = new List<ChoicesUI>();

    private void Start()
    {
        //Setup();
    }
    public void Setup()
    {
        for (int i = 0; i < choicesData.choices.Count; i++)
        {
            var go = Instantiate(choicesPrefab, parent);
            var choicesUI = go.GetComponent<ChoicesUI>();
            choicesUI.Setup(choicesData.choices[i]);
            choicesUI.isDuplicate = false;
            choicesUI.index = i;
            go.transform.name = choicesData.choices[i].name;
        }

        
    }

    public void Unsetup()
    {
        instantiatedChoices = parent.transform.GetComponentsInChildren<ChoicesUI>().ToList();
        
        for (int i = 0; i < instantiatedChoices.Count; i++)
        {
            Destroy(instantiatedChoices[i].gameObject);
        }

        instantiatedChoices.Clear();
       
    }

    public void InstantiateSelectedChoices(Choices selectedChoice)
    {
        
        var go = Instantiate(choiceDuplicate, canvasParent);// parent to canvas 
        go.transform.position = Input.mousePosition;
        var choicesUI = go.GetComponent<ChoicesDuplicate>();
        //choicesUI.isDuplicate = true;
        choicesUI.isDragging = true;
        choicesUI.Setup(selectedChoice);
        EventSystem.current.SetSelectedGameObject(go);
    }

    public void SpawnChoices(int index)
    {
        var go = Instantiate(choicesPrefab, parent);
        var choicesUI = go.GetComponent<ChoicesUI>();
        choicesUI.transform.SetSiblingIndex(index);
        choicesUI.Setup(choicesData.choices[index]);
        choicesUI.index = index;
        go.transform.name = choicesData.choices[index].name;
    }

   
}
