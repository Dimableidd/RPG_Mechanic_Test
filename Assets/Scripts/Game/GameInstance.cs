using Levels;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameInstance : MonoBehaviour
    {
        [SerializeField] private int _firstLevelSceneID = 1;

        private void Awake()
        {
            Application.targetFrameRate = 120;

            DontDestroyOnLoad(gameObject);

            LoadScene(_firstLevelSceneID);
        }

        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(LoadSceneAsync(sceneIndex));
        }

        private IEnumerator LoadSceneAsync(int sceneIndex)
        {
            var loadSceneAsync = SceneManager.LoadSceneAsync(sceneIndex);

            while (loadSceneAsync.isDone == false)
            {
                yield return null;
            }

            var levelInstance = FindObjectOfType<LevelInstance>();
            levelInstance?.Init(this);
        }
    }
}
