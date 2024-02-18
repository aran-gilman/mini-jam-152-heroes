using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private float delay = 0.0f;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneInternal(sceneName));
    }

    private IEnumerator LoadSceneInternal(string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}
