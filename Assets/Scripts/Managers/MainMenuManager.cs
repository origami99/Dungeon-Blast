using Assets.Scripts.Systems.Save;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private SaveSystem _saveSystem;
        
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private Button _continueButton;

        private void Start()
        {
            _menuPanel.SetActive(true);
            _continueButton.interactable = _saveSystem.DoesSnapshotExists();
        }

        public void StartNewGame() 
        {
            _saveSystem.SetupInitialSnapshot();
            _menuPanel.SetActive(false);
        }

        public void StartSaveGame()
        {
            _saveSystem.LoadSnapshot();
            _menuPanel.SetActive(false);
        }

        public void Quit() => Application.Quit();
    }
}
