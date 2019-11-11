using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    Button myButton;
    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(ResetLevelFunc);
    }

    public void ResetLevelFunc()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
