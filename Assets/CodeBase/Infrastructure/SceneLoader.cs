using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private ICoroutineRunner coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            this.coroutineRunner = coroutineRunner;

        public void Load(string sceneName, Action onLoaded = null) => 
            coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoaded));

        private IEnumerator LoadScene(string sceneName, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitSceneLoaded = SceneManager.LoadSceneAsync(sceneName);

            while (!waitSceneLoaded.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}