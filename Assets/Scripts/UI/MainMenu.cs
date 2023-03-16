using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class MainMenu : MonoBehaviour
    {
        private static readonly int TriggerOpen = Animator.StringToHash("Open");
    
        [SerializeField] private GameObject optionsWindow;
        private void Start()
        {
            GameController.Instance.AudioManager.PlayMusic(true);
            Time.timeScale = 1f;
        }

        public void Play()
        {
            GameController.Instance.AudioManager.PlayMusic(false);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

        public void OpenOptions()
        {
        }

        public void Exit()
        {
            Application.Quit();
        }
    
        public void SetSoundVolume(Slider slider)
        {
            GameController.Instance.AudioManager.SfxVolume = slider.value;
        }
        public void SetMusicVolume(Slider slider)
        {
            GameController.Instance.AudioManager.MusicVolume = slider.value;
        }
    
        public void ShowWindow(GameObject window)
        {
            optionsWindow.SetActive(true);
        }

        public void HideWindow(GameObject window)
        {
            optionsWindow.SetActive(false);
        }
    }
}