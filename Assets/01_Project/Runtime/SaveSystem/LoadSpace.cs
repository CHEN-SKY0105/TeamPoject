using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadSpace : MonoBehaviour
{
    //TODOError: �٨S�T�{�o�䦳�S�����`Ū��
    [SerializeField] private TMP_Text timeLabel;
    private Button loadBtn;
    private TextAsset saveFile;

    private void Awake()
    {
        loadBtn = GetComponent<Button>();
    }

    private void Start()
    {
        loadBtn.onClick.AddListener(Load);
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

    private void Load()
    {
        if (saveFile == null)
        {
            Debug.Log("�S���s��");
            return;
        }
        SaveManager.Inst.Load(saveFile);
    }
}