using UnityEngine;
using System.Collections;

public class LevelLoader0 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        ResetLevel();
        GameManager.Instance.ResetCurrentFrame();
        LoadPlayer();
        LoadGhosts();
        Destroy(this);
    }

    private void ResetLevel()
    {
        foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }
        foreach (Structure structure in GameObject.FindObjectsOfType<Structure>())
        {
            Destroy(structure.gameObject);
        }
    }

    private void LoadPlayer()
    {
        Instantiate(Resources.Load("Builder"));
    }

    private void LoadGhosts()
    {
        foreach (Transform ghost in GameObject.Find("Ghosts").transform)
        {
            ghost.gameObject.SetActive(true);
            ghost.transform.position = Vector3.zero;
        }
    }
}
