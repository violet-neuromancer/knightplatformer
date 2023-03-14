using UnityEngine;
using UnityEngine.SceneManagement;

public class Completed : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey) SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}