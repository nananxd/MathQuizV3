using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogListContainer
{
    public List<Dialouge> dialogContainer = new List<Dialouge>();
}

public class DialogDataManager : MonoBehaviour
{
    [NonReorderable]
    public List<Dialouge> storyDialog;

    private static DialogDataManager instance = null;
    public static DialogDataManager Instance
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

    public void SaveDialogData()
    {
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, "dialogData.json");
        DialogListContainer dialogList = new DialogListContainer { dialogContainer = storyDialog };
        //string json = JsonUtility.ToJson(this);
        string json = JsonUtility.ToJson(dialogList);
        System.IO.File.WriteAllText(filePath, json);

        Debug.Log("DILAOG DATA SAVED");
    }

    public void LoadDialogData()
    {
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, "dialogData.json");

        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            //JsonUtility.FromJsonOverwrite(json, this);
            var dialog = JsonUtility.FromJson<DialogListContainer>(json);
            storyDialog = dialog.dialogContainer;
            foreach (var story in storyDialog)
            {
                if (!string.IsNullOrEmpty(story.spriteBase64))
                {
                    story.spriteBase64 = story.spriteBase64.Trim();
                    byte[] bytes = System.Convert.FromBase64String(story.spriteBase64);
                    Texture2D tex = new Texture2D(1, 1);
                    tex.LoadImage(bytes);
                    story.speakerImage = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height),Vector2.zero);
                }
            }
        }

        Debug.Log("DILAOG DATA LOADED");
    }

    public Dialouge GetDialog()
    {
        for (int i = 0; i < storyDialog.Count; i++)
        {
            if (storyDialog[i].alreadyRead == false)
            {
                //return storyDialog[i];
                return storyDialog[i];
             
            }
            
        }
       
        return null;
    }
}
