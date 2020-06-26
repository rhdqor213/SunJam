using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ToGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ToStory()
    {
        SceneManager.LoadScene("StoryScene");
    }
}
