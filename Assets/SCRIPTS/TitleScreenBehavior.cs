using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenBehavior : MonoBehaviour
{
    public GameObject canvas;
    public GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        for(int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            Debug.Log(i);
            GameObject levelButton = Instantiate<GameObject>(buttonPrefab, canvas.transform);
            levelButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-150 + (50 * i), 0);
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(path);
            levelButton.GetComponentInChildren<Text>().text = sceneName;
            levelButton.GetComponent<Button>().onClick.AddListener(() => this.ChangeScene(path));
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    void ChangeScene(string sceneName)
    {
        Debug.Log(sceneName);
        SceneManager.LoadScene(sceneName);

    }
}
