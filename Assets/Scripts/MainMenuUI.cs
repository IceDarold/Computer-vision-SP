using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject error_message;
    [SerializeField] private GameObject loading;
    [SerializeField] private DataTransfer dataTransfer;
    private float startTime = 0;
    void Start()
    {
        if (dataTransfer.error)
        {
            error_message.SetActive(true);
            startTime = Time.time;
        }
        loading.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            loading.SetActive(true);
            SceneManager.LoadScene("MainGame");
            Debug.Log("Load");
        }
        if (error_message.activeSelf && Time.time - startTime >= 10)
        {
            error_message.SetActive(false);
        }
    }
}
