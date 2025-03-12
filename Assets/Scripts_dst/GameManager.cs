 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
 {
    public void Restart()
    {
        Debug.Log("Restart game");
        SceneManager.LoadScene("Manor");
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit game");
    }
    //     public static GameManager Instance;

    //     public GameState State;

    //     void Awake() {
    //         Instance = this;
    //     }

    //     // Start is called before the first frame update
    //     void Start()
    //     {

    //     }

    //     // Update is called once per frame
    //     void Update()
    //     {
    //         UpdateGameState(GameState.Title);
    //     }

    //     public void UpdateGameState(GameState newState) {
    //         State = newState;

    //         switch(newState) {
    //             case GameState.Title:
    //                 break;
    //             case GameState.Settings:
    //                 break;
    //             case GameState.Playing:
    //                 break;
    //             case GameState.Victory:
    //                 break;
    //             case GameState.Lose:
    //                 break;
    //             default:
    //                 throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    //         }
    //         onGameStateChanged?.invoke(newState);
    //     }

    //     public enum GameState {
    //         Title,
    //         Settings,
    //         Playing,
    //         Victory,
    //         Lose
}
// }


