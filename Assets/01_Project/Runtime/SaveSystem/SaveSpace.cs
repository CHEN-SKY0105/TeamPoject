using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSpace : MonoBehaviour
{
    //TODOError: �٨S�T�{�o�䦳�S�����` �s�� �л\
    [SerializeField] private TMP_Text timeLabel;
    private Button saveBtn;
    private TextAsset saveFile;

    private void Awake()
    {
        saveBtn = GetComponent<Button>();
    }

    private void Start()
    {
        saveBtn.onClick.AddListener(Save);
    }

    public void Init(TextAsset saveFile)
    {
        this.saveFile = saveFile;
        if (saveFile == null)
        {
            timeLabel.text = "";
            return;
        }
        GameSaveData gameSave = JsonUtility.FromJson<GameSaveData>(saveFile.text);
        timeLabel.text = gameSave.time;
    }

    private void Save()
    {
        string date = DateTime.Now.ToString();
        timeLabel.text = date;
        SaveManager.Inst.Save();
    }
}