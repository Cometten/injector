using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHrdinove : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    private int score = 0;

    public GameObject KonecHryPopUp;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        puzzles = Resources.LoadAll<Sprite>("img/hrdinove");
    }
    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("puzzleBtn");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }


    }

    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    void AddListeners()
    {
        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => PickPuzzle());
        }
    }

    public void PickPuzzle()
    {
        //string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if (!firstGuess)
        {
            audioManager.PlaySFX(audioManager.card);
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;

            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if (!secondGuess)
        {
            audioManager.PlaySFX(audioManager.card);
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            if (firstGuessPuzzle == secondGuessPuzzle)
            {
                audioManager.PlaySFX(audioManager.point);
                //print("Vybrali jste správnou dvojici"); 
                score++;
                scoreText.text = "Vaše Skóre: " + score;

            }
            else
            {
                //print("Vybrali jste špatnou dvojici");
            }

            StartCoroutine(checkThePuzzleMatch());
        }
    }

    IEnumerator checkThePuzzleMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(0.5f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;


            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckTheGameFinished();
        }
        else
        {
            audioManager.PlaySFX(audioManager.wrong);
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }
        yield return new WaitForSeconds(0.5f);

        firstGuess = secondGuess = false;
    }

    void CheckTheGameFinished()
    {
        countCorrectGuesses++;

        if (countCorrectGuesses == gameGuesses)
        {
            print("konec hry");
            audioManager.PlaySFX(audioManager.gameover);
            KonecHryPopUp.SetActive(true);

            print("zabralo vám to" + countGuesses + "");
        }
    }

    public void ZpetDoMenu()
    {
        print("zpìt do menu");
        SceneManager.LoadScene(0);
    }

    public void ZkusitZnovu()
    {
        print("Zkusit Znovu");
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

    public void OnRetryButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [SerializeField]
    private TextMesh scoreText;





}
