using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{

    public List<int> burnTicks = new List<int>();
    public List<int> poisonTicks = new List<int>();
    public List<int> bleedTicks = new List<int>();

    [SerializeField] int damage;
    AudioSource burn;
    enemyAi enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<enemyAi>();
    }


    public void ApplyAffect(int ticks, List<int> TickCount)
    {
        if (TickCount.Count <= 0)
        {
            TickCount.Add(ticks);
            StartCoroutine(StatusAffect(TickCount));
        }
        else
        {
            TickCount.Add(ticks);
        }
    }

    IEnumerator StatusAffect(List<int> TickCount)
    {

        if (TickCount == burnTicks)
        {
            damage = 5;
        }
        else if (TickCount == poisonTicks)
        {
            damage = 1;
        }
        else if (TickCount == bleedTicks)
        {
            damage = 2;
        }

        while (TickCount.Count > 0)
        {

            for (int i = 0; i < TickCount.Count; i++)
            {
                TickCount[i]--;


            }
            if (gameObject.CompareTag("Player"))
            {

                GameManager.instance.playerScript.HP -= damage;
                GameManager.instance.playerScript.playerGrunt.Play();
                GameManager.instance.playerScript.updatePLayerHud();
            }
            else
            {
                enemy.takeDamage(damage);
            }
            TickCount.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(1);

        }
        if (TickCount.Count == 0)
        {
            GameManager.instance.playerScript.playerGrunt.Stop();
            //burn.Stop();
        }
    }

}
