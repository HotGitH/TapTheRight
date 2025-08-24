using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;              // ADDED: to check if tap is on UI
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;               // ADDED: new Input System support
#endif

public class GameManager : MonoBehaviour
{
    // makes it possible to call this from every place in the game 
    public static GameManager instance;
    int score =0;

    [Header("Spawn")]
    //public GameObject target;
    public GameObject[] targets;
    public float maxPos =1.95f;
    public float spawnRate;
    public Transform spawnPoint;
    private int randIndex;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject menuUI;

    public int conNum;

    bool gameStarted = false;

    private void Awake()
    {
        instance = this;
        scoreText.gameObject.SetActive(false);  //close the score text
    }
   
    void Update()
    {
        if (!gameStarted && PressBeganThisFrame() && !PointerOverUI()) // not started and tapped on the screen then start the game 
        {
            GameStart(); //start the game
            gameStarted = true;  
            menuUI.SetActive(false); //close the menu
            scoreText.gameObject.SetActive(true); // open the score
        }
    }
    public void SetConNum(int num) // set the country number from the outside
    {
        conNum = num; 
        print(num + " gam");
    }
    public int GetConNum()
    {
        return conNum;
    }
    public void ScoreUp()
    {
        score++; //ups the score
        scoreText.text = score.ToString(); // updating the score 
    }
    public void ScoreDown()
    {
        score--; //ups the score
        scoreText.text = score.ToString(); // updating the score 
    }
    /* void SpawnTarget()
     {
         Vector3 spawnPos = spawnPoint.position; // create a new spawn point
         spawnPos.x = Random.Range(-maxPos, maxPos); //change the x value to be a random point from the center
         Instantiate(target, spawnPos, Quaternion.identity); // create a target 
     }*/
    void SpawnCountry()
    {
        Vector3 spawnPos = spawnPoint.position; // create a new spawn point
        spawnPos.x = Random.Range(-maxPos, maxPos); //change the x value to be a random point from the center
        // Pick a random prefab from the array
        randIndex = Random.Range(0, targets.Length);
        Instantiate(targets[randIndex], spawnPos, Quaternion.identity); // create a country 
    }
    public void GameStart()
    {
        InvokeRepeating(nameof(SpawnCountry), 1f, spawnRate);  // recall function every 1 sec * spawnRate
    }
    public void GameOver()
    {
        SceneManager.LoadScene("Game"); //load the game
        menuUI.SetActive(true); //show the menu 
        scoreText.gameObject.SetActive(false); //turn off the score
    }
    bool PressBeganThisFrame()
    {
        // Works on Editor + Android/iOS with either input backend
#if ENABLE_INPUT_SYSTEM
        bool mouse = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
        bool touch = Touchscreen.current != null &&
                     Touchscreen.current.primaryTouch.press.wasPressedThisFrame;
        return mouse || touch;
#else
        if (Input.GetMouseButtonDown(0)) return true;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) return true;
        return false;
#endif
    }

    bool PointerOverUI()
    {
        if (EventSystem.current == null) return false;

#if ENABLE_INPUT_SYSTEM
        if (Touchscreen.current != null)
            return EventSystem.current.IsPointerOverGameObject(
                Touchscreen.current.primaryTouch.touchId.ReadValue());
        return EventSystem.current.IsPointerOverGameObject(); // mouse
#else
        if (Input.touchCount > 0)
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        return EventSystem.current.IsPointerOverGameObject(); // mouse
#endif
    }

}
