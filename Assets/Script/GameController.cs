using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{    //Tao list de luu cac button
    [SerializeField] private Sprite backgroundImg;
    [SerializeField] public Sprite[] SourceSprites;
    [SerializeField] private Text timeText;
    [SerializeField] private float timer;
    [SerializeField] private float currentTimer;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject nextGamePanel;
    private List<Button> btnList = new List<Button>();
    public List<Sprite> GameSprite = new List<Sprite>();
    private bool firstGuess, secondGuess;
    private bool isGameFinished = false;
    private string firstname, secondname;
    private int firstIndex, secondIndex, TotalGuess, NoOfGuess, CorrectGuess;

    
    void Awake()
    {
        SourceSprites = Resources.LoadAll<Sprite> ("Sprites/GameImg"); 
    }
    // Start is called before the first frame update
    void Start()
    {
        SoundController.instance.PlayThisSound("music", 0.7f);
        GetButtons();
        TotalGuess = btnList.Count / 2;
        AddListener();
        AddSprites();
        Shuffle(GameSprite);
        currentTimer = timer;
    }
    private void Update() 
    {
        TimeUp();
    }
    private void TimeUp() 
    {
         if (isGameFinished) return;
         
         currentTimer -= Time.deltaTime;
    if (currentTimer < 0) 
    {
        currentTimer = 0; // Ensure the timer doesn't go below zero
        gameOverPanel.SetActive(true);
    }
    int remainingTime = Mathf.FloorToInt(currentTimer);
    timeText.text = remainingTime.ToString();
}

    void AddSprites()
    {
        int size = btnList.Count;
        int index = 0;
        for(int i = 0; i < size; i++)
        {
            if(i == size/2)
            {
            index = 0;
            }
            GameSprite.Add(SourceSprites[index]);
            index++;
        }
    }
    void GetButtons()
    {
        // lay het cac button hien co them vao LIST
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            btnList.Add(objects[i].GetComponent<Button>());
            btnList[i].image.sprite = backgroundImg;
        }
    }
    void AddListener()
    {
        foreach (Button btn in btnList)
        {
            btn.onClick.AddListener( () => PickPuzzle());
        }
    }
    void PickPuzzle()
    {
       if (!firstGuess)
       {
        firstGuess = true;
        firstIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        firstname = GameSprite [firstIndex].name;
        btnList[firstIndex].image.sprite = GameSprite[firstIndex];
        Debug.Log("1st index: " + firstIndex + " 1st name = " + firstIndex);
       } else if (!secondGuess)
       {
        secondGuess = true;
        secondIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        secondname = GameSprite [secondIndex].name;
        btnList[secondIndex].image.sprite = GameSprite[secondIndex];
        Debug.Log("2nd index: " + secondIndex + " 2nd name = " + secondname);
        NoOfGuess++;
        StartCoroutine(CheckIfPuzzleMatched());
       }
    }
     IEnumerator CheckIfPuzzleMatched()
      {
        yield return new WaitForSeconds (0.5f);
          if (firstname == secondname && firstIndex != secondIndex) 
        {
            SoundController.instance.PlayThisSound("rightSound", 1f);
            CorrectGuess++;
            btnList[firstIndex].interactable = false;
            btnList[secondIndex].interactable = false;

            btnList[firstIndex].image.color = new Color(0, 0, 0, 0);
            btnList[secondIndex].image.color = new Color(0, 0, 0, 0);
        }
        else
        {
            SoundController.instance.PlayThisSound("wrongSound", 0.5f);
            btnList[firstIndex].image.sprite = backgroundImg;
            btnList[secondIndex].image.sprite = backgroundImg;
        }
        firstGuess = secondGuess = false;
          CheckIfFinished();
      }
      void CheckIfFinished()
      {
        if(CorrectGuess == TotalGuess)
        {
            isGameFinished = true;
            Debug.Log("Win with " + NoOfGuess);
            nextGamePanel.SetActive(true);
        }
      }
      void Shuffle(List<Sprite> list)
      {
        Sprite temp;
        for(int i = 0; i < list.Count; i++)
        {
            temp = list[i];
            int random = Random.Range(i, list.Count);
            list[i] = list[random];
            list[random] = temp;
        }
      }
}
