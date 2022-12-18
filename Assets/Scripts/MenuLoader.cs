using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour {

    public void LoadScene(string _nameScene)
    {
        SceneManager.LoadScene(_nameScene);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        Debug.Log("Quit");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
