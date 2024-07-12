using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{    //Tao list de luu cac button
    public AudioClip rightSound;
    public AudioClip wrongSound;
    private AudioSource source;
    [SerializeField]
    private Text score;
    [SerializeField]
    private Sprite backgroundImg;
    private List<Button> btnList = new List<Button>();
    [SerializeField]
    public Sprite[] SourceSprites;
    public List<Sprite> GameSprite = new List<Sprite>();
    private bool firstGuess, secondGuess;
    string firstname, secondname;
    int firstIndex, secondIndex, TotalGuess, NoOfGuess, CorrectGuess;
    void Awake()
    {
        SourceSprites = Resources.LoadAll<Sprite> ("Sprites/GameImg"); 
        source = GetComponent<AudioSource>();
        //Ham thu
        if (source == null)
        {
            Debug.LogError("AudioSource is missing on the GameObject.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetButtons();
        TotalGuess = btnList.Count / 2;
        AddListener();
        AddSprites();
        Shuffle(GameSprite);

        // Kiểm tra và thông báo nếu âm thanh chưa được gán
        if (rightSound == null)
        {
            Debug.LogError("Right sound is not assigned in the inspector.");
        }
        if (wrongSound == null)
        {
            Debug.LogError("Wrong sound is not assigned in the inspector.");
        }
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
             if (source != null && rightSound != null)
            {
                source.PlayOneShot(rightSound);
            }
            //Ham thu
             else
            {
                Debug.LogError("Wrong sound or AudioSource is missing.");
            }
            CorrectGuess++;
            btnList[firstIndex].interactable = false;
            btnList[secondIndex].interactable = false;

            btnList[firstIndex].image.color = new Color(0, 0, 0, 0);
            btnList[secondIndex].image.color = new Color(0, 0, 0, 0);
        }
        else
        {
           if (source != null && wrongSound != null)
            {
                source.PlayOneShot(wrongSound);
            }
            // Ham thu
            else
            {
                Debug.LogError("Right sound or AudioSource is missing.");
            }
            btnList[firstIndex].image.sprite = backgroundImg;
            btnList[secondIndex].image.sprite = backgroundImg;
        }
        firstGuess = secondGuess = false;
        if (score != null)
        {
            score.text = "Score: " + CorrectGuess.ToString();
        }
          CheckIfFinished();
      }
      void CheckIfFinished()
      {
        if(CorrectGuess == TotalGuess)
        {
            Debug.Log("Win with " + NoOfGuess);
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
