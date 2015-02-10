using UnityEngine;
using System.Collections;

public class LevelLoader0 : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        ResetLevel();
        StartCoroutine(StartCountdown());
        LoadPlayer();
        LoadGhosts();
    }

    IEnumerator StartCountdown()
    {
        Time.timeScale = 0.01f;
        Timer timer = GameObject.FindGameObjectWithTag("Timer").gameObject.GetComponent<Timer>();
        timer.ResetTimer();
        GameObject countdownTimer = Instantiate(Resources.Load("Red Three")) as GameObject;
        yield return new WaitForSeconds(1 * Time.timeScale);
        Destroy(countdownTimer);
        countdownTimer = Instantiate(Resources.Load("Orange Two")) as GameObject;
        yield return new WaitForSeconds(1 * Time.timeScale);
        Destroy(countdownTimer);
        countdownTimer = Instantiate(Resources.Load("Green One")) as GameObject;
        yield return new WaitForSeconds(1 * Time.timeScale);
        Destroy(countdownTimer);
        Time.timeScale = 1;
        timer.StartTimer();
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
        foreach (Transform enemySpawner in GameObject.Find("Enemy Spawners").transform)
        {
            enemySpawner.GetComponent<EnemySpawner>().ResetSpawner();
        }
    }

    private void LoadPlayer()
    {
        GameObject player = Instantiate(Resources.Load("Player"), Vector3.zero, Quaternion.identity) as GameObject;
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
