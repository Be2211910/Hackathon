using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject gameoverText;
    public PlayerScript player;

    public void GameOver()
    {
        gameoverText.SetActive(true);
        Invoke("GameRestart", 2);
    }


    public void GameRestart()
    {
        // ���݂̃V�[�����擾���ă��[�h����
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    private void Update()
    {
        if (player.getIsDead()) GameOver();
    }
}
