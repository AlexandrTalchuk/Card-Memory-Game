using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameController : MonoBehaviour
{
    public List<Button> btns = new List<Button>();
    [SerializeField]
    private Sprite bgImage;
    public Sprite[] puzzels;
    public List<Sprite> gamePuzzles = new List<Sprite>();

    private bool firstGuess, secondGuess;
    private int countGuesses;
    private int countCorrectGuess;
    private int gameGuess;

    private string FirstGuessPuzzle, SecondGuessPuzzle;
    private int FirstGuessIndex, SecondGuessINdex;
    ScoreManager scr;
    private void Awake()
    {
        puzzels = Resources.LoadAll<Sprite>("Sprites/png");
    }
    private void Start()
    {
        GetButtons();
        AddListeners();
        AddGamesPuzzles();
        Shuffle(gamePuzzles);
        gameGuess = gamePuzzles.Count / 2;
        scr = FindObjectOfType<ScoreManager>();
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGamesPuzzles()
    {
        int looper = btns.Count;
        int index = 0;
        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzels[index]);
            index++;
        }
    }
    void AddListeners()
    {
        foreach(Button bt in btns)
        {
            bt.onClick.AddListener(() => PickUpPuzzle());
        }    
    }

    public void PickUpPuzzle()
    {
        if(!firstGuess)
        {
            firstGuess = true;
            FirstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            FirstGuessPuzzle = gamePuzzles[FirstGuessIndex].name;
            btns[FirstGuessIndex].image.sprite = gamePuzzles[FirstGuessIndex];
        }
        else if(!secondGuess)
        {
            secondGuess = true;
            SecondGuessINdex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            SecondGuessPuzzle = gamePuzzles[SecondGuessINdex].name;
            btns[SecondGuessINdex].image.sprite = gamePuzzles[SecondGuessINdex];
            countGuesses++;
            StartCoroutine(CheckIfthePuzzlesMatch());
        }
    }
    IEnumerator CheckIfthePuzzlesMatch()
    {
        yield return new WaitForSeconds(1f);
        if (FirstGuessPuzzle == SecondGuessPuzzle)
        {
            yield return new WaitForSeconds(.5f);
            btns[FirstGuessIndex].interactable = false;
            btns[SecondGuessINdex].interactable = false;
            btns[FirstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[SecondGuessINdex].image.color = new Color(0, 0, 0, 0);
            scr.Plus();
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            btns[FirstGuessIndex].image.sprite = bgImage;
            btns[SecondGuessINdex].image.sprite = bgImage;
        }
        yield return new WaitForSeconds(.5f);
        firstGuess = secondGuess = false; 
    }

    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}


