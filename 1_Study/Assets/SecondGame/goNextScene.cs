using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goNextScene : MonoBehaviour
{
    public void goScene(int SceneNumber)
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
