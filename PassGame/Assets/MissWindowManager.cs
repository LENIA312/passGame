using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissWindowManager : MonoBehaviour
{
    [SerializeField] Button retryButton = default;

    // Start is called before the first frame update
    void Start()
    {
        retryButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
