using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int countFinishedCharacters;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("NotFirstOpen2"))
        {
            PlayerPrefs.DeleteAll();
        }
        else
        {
            PlayerPrefs.SetString("NotFirstOpen2", "FirstOpen");
        }
    }

    private void OnEnable()
    {
        GameEvents.OnFinished += CheckWin;
    }

    private void OnDisable()
    {
        GameEvents.OnFinished -= CheckWin;
    }

    private void CheckWin()
    {
        countFinishedCharacters++;
        if (countFinishedCharacters == 2)
        {
            GameEvents.OnLevelCompleted?.Invoke();
        }
    }
}
