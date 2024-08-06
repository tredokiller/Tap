using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene(1);
    }
}
