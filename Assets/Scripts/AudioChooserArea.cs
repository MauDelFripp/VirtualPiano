using UnityEngine;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioChooserArea : SystemArea
{

    private static string audioBankFolder;
    private float itemListOffset = 50f;
    private float initialListContentHeight = 236.7f;
    public GameObject listContent;
    public static AudioChooserArea instance;
    public RectTransform listItemInitialTransform;
	public List<Button> buttonList;
	public int selectedItemIndex;

    void Start()
    {
		this.selectedItemIndex = -1;
        instance = this;
        audioBankFolder = Application.dataPath + "/StreamingAssets/AudioBanks";
        if (Directory.Exists(audioBankFolder))
        {
            FileSystemWatcher watcher = new FileSystemWatcher()
            {
                Path = audioBankFolder,
                Filter = "*.sf2"
            };
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
            UpdateAudioChooserList();
        }
    }

    void Update()
    {

    }

    private static void UpdateAudioChooserList()
    {
		instance.buttonList = new List<Button>();		
		foreach(Transform child in instance.listContent.transform)
		{
			Destroy(child.gameObject);
		}

        RectTransform rectTransform = instance.listContent.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, instance.initialListContentHeight);
        string[] fileEntries = Directory.GetFiles(audioBankFolder).Where(f => f.EndsWith(".sf2", StringComparison.OrdinalIgnoreCase)).OrderBy(f => f).ToArray();
        for (int i = 0; i < fileEntries.Length; i++)
        {
			int j = i;
            string fileName = fileEntries[i].Split('\\')[1].Split('.')[0];
            GameObject newItem = Instantiate(Resources.Load<GameObject>("Prefabs/ListItemAudioChooser"));
            newItem.GetComponent<RectTransform>().localPosition = instance.listItemInitialTransform.localPosition;
            newItem.GetComponent<RectTransform>().localScale = instance.listItemInitialTransform.localScale;
            newItem.transform.SetParent(instance.listContent.transform, false);
            newItem.transform.GetChild(0).FindChild("Text").GetComponent<Text>().text = fileName;
            newItem.GetComponent<RectTransform>().localPosition -= new Vector3(0, instance.itemListOffset * i, 0);
			instance.buttonList.Add(newItem.GetComponent<Button>());
            newItem.GetComponent<Button>().onClick.AddListener(delegate () { instance.OnListItemClick(fileName, j); });
            if (i >= 5)
            {
                rectTransform.sizeDelta += new Vector2(0, instance.itemListOffset);
            }
			if(i == instance.selectedItemIndex || (instance.selectedItemIndex == -1 && fileName.Equals("Steinway")))
			{
				instance.selectedItemIndex = i;
				instance.setPathFileToLoad(fileEntries[i]);		
				instance.changeButtonColor(instance.buttonList.Last(), Color.black);
			}
        }
    }

    private static void OnChanged(object source, FileSystemEventArgs e)
    {
        UpdateAudioChooserList();
    }

    private void OnListItemClick(string fileName, int buttonIndex)
    {
        string filePath = audioBankFolder + "/" + fileName + ".sf2";
		this.setPathFileToLoad(filePath);
        Color color = new Color();
        ColorUtility.TryParseHtmlString ("#941B1BC8", out color);
        this.changeButtonColor(this.buttonList[this.selectedItemIndex], color);
		this.changeButtonColor(this.buttonList[buttonIndex], Color.black);
    }

	private void changeButtonColor(Button button, Color color)
	{
		var colors = button.colors;
		colors.normalColor = color;
		button.colors = colors;
	}

	private void setPathFileToLoad(string filePath)
	{
		StageArea.filePathToLoad = filePath;
	}

	public void BackToMainMenu() {
		this.Navigation.NavigateTo(Constants.MainPosition);
		this.enabled = false;
	}
	
	public override void ActivateArea() {
		this.enabled = true;
	}

    public override bool IsSameType(string type){
		if(type.Equals(Constants.SoundChooserPosition)){
			return true;
		}
		return false;
	}
}
