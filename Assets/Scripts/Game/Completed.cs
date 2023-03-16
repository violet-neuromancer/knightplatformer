using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Completed : MonoBehaviour
    {
        private void Update()
        {
            if (Input.anyKey) SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}