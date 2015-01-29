using UnityEngine;
using System.Collections;

public class LevelLoader0 : MonoBehaviour
{
        // Use this for initialization
    void Start()
    {
        GameManager.Instance.ResetCurrentFrame();
        ResetLevel();
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
        foreach(Transform enemySpawner in GameObject.Find("Enemy Spawners").transform){
            enemySpawner.GetComponent<EnemySpawner>().ResetSpawner();
        }
    }

    private void LoadPlayer()
    {
        GameObject player = Instantiate(Resources.Load("Player"), new Vector3(0, 0.01f, 0), Quaternion.identity) as GameObject;
        Camera.main.GetComponent<TargetFollower>().Target = player.transform;
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
