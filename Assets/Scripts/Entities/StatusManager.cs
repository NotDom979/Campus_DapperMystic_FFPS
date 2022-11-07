using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{

    public List<int> burnTicks = new List<int>();
    public List<int> poisonTicks = new List<int>();
    public List<int> bleedTicks = new List<int>();


    public bool started;
    [SerializeField] int damage;
    AudioSource burn;
    enemyBase enemy;

    // Start is called before the first frame update
    //void Start()
    //{
    //    started = false;
    //    poisonTicks.Clear();
    //    burnTicks.Clear();
    //    bleedTicks.Clear();
    //    enemy = GetComponent<enemyBase>();
    //}


    //public void ApplyAffect(int ticks, List<int> TickCount)
    //{

    //    if (TickCount.Count <= 0)
    //    {
    //        TickCount.Add(ticks);
    //        StartCoroutine(StatusAffect(TickCount));
    //    }
    //    else if (TickCount.Count > 0 && started == false)
    //    {
    //        TickCount.Add(ticks);
    //        StartCoroutine(StatusAffect(TickCount));
    //    }
    //    else
    //    {

    //        TickCount.Add(ticks);
    //    }
    //}

    //IEnumerator StatusAffect(List<int> TickCount)
    //{
    //    if (TickCount != null)
    //    {
    //        started = true;

    //        if (TickCount == burnTicks)
    //        {
    //            damage = 5;
    //        }
    //        else if (TickCount == poisonTicks)
    //        {

    //            damage = 1;
    //        }
    //        else if (TickCount == bleedTicks)
    //        {
    //            damage = 2;
    //        }

    //        while (TickCount.Count > 0)
    //        {

    //            for (int i = 0; i < TickCount.Count; i++)
    //            {

    //                TickCount[i]--;
    //            }
    //            if (gameObject.CompareTag("Player"))
    //            {

    //                if (TickCount == poisonTicks && GameManager.instance.playerScript.HP > 5)
    //                {

    //                    GameManager.instance.playerScript.HP -= damage;
    //                    GameManager.instance.playerScript.playerGrunt.Play();
    //                    GameManager.instance.playerScript.updatePLayerHud();
    //                }
    //                else if (TickCount == bleedTicks && GameManager.instance.playerScript.HP > 1)
    //                {
    //                    for (int i = 0; i < 2; i++)
    //                    {
    //                        GameManager.instance.playerScript.HP -= damage;
    //                        GameManager.instance.playerScript.playerGrunt.Play();
    //                        GameManager.instance.playerScript.updatePLayerHud();
    //                        yield return new WaitForSeconds(.1f);
    //                    }
    //                }
    //                else if (GameManager.instance.playerScript.HP <= 0)
    //                {
    //                    TickCount.Clear();
    //                    GameManager.instance.playerScript.playerGrunt.volume = 1;
    //                    GameManager.instance.playerScript.playerGrunt.pitch = 1;
    //                    GameManager.instance.playerScript.playerGrunt.Play();
    //                    GameManager.instance.playerDeadMenu.SetActive(true);
    //                    GameManager.instance.cursorLockPause();
    //                }
    //            }
    //            else
    //            {
    //                enemy.takeDamage(damage);
    //            }
    //            TickCount.RemoveAll(i => i == 0);
    //            yield return new WaitForSeconds(1);

    //        }
    //        if (TickCount.Count == 0)
    //        {
    //            GameManager.instance.playerScript.playerGrunt.Stop();
    //            //burn.Stop();
    //            started = false;
    //        }
    //    }
    //}

    void Start()
    {
        enemy = GetComponent<enemyBase>();
    }


    #region //bleed
    public void ApplyBleed(int ticks)
    {
        if (bleedTicks.Count <= 0)
        {
            bleedTicks.Add(ticks);
            StartCoroutine(Bleed());
        }
        else
        {
            bleedTicks.Add(ticks);
        }
    }

    IEnumerator Bleed()
    {
        while (bleedTicks.Count > 0)
        {
            damage = 1;
            for (int i = 0; i < bleedTicks.Count; i++)
            {
                bleedTicks[i]--;
            }
            if (GameManager.instance.playerScript.HP <= 0)
            {
                bleedTicks.Clear();
                GameManager.instance.playerScript.playerGrunt.volume = 1;
                GameManager.instance.playerScript.playerGrunt.pitch = 1;
                GameManager.instance.playerScript.playerGrunt.Play();
                GameManager.instance.playerDeadMenu.SetActive(true);
                GameManager.instance.cursorLockPause();
            }

            for (int i = 0; i < 2; i++)
            {
                GameManager.instance.playerScript.HP -= damage;
	            GameManager.instance.playerScript.updatePLayerHud();
	            StartCoroutine(GameManager.instance.bleedflash());
	            GameManager.instance.BleedAlert.SetActive(true);
                bleedTicks.RemoveAll(i => i == 0);
                yield return new WaitForSeconds(.1f);
            }
                yield return new WaitForSeconds(1);
	    		GameManager.instance.BleedAlert.SetActive(false);
        }
    }
    #endregion

    #region //poison
    public void ApplyPosion(int ticks)
    {
        if (poisonTicks.Count <= 0)
        {
            poisonTicks.Add(ticks);
	        StartCoroutine(Poison());
	      
        }
        else
        {
            poisonTicks.Add(ticks);
        }
    }

    IEnumerator Poison()
    {
        while (poisonTicks.Count > 0)
        {
            damage = 1;
            for (int i = 0; i < poisonTicks.Count; i++)
            {
                poisonTicks[i]--;
            }
            if (GameManager.instance.playerScript.HP > 5)
            {

                GameManager.instance.playerScript.HP -= damage;
	            GameManager.instance.playerScript.updatePLayerHud();
	            StartCoroutine(GameManager.instance.poisonflash());
	            GameManager.instance.PoisonAlert.SetActive(true);
                poisonTicks.RemoveAll(i => i == 0);
                yield return new WaitForSeconds(1);
            }
            else
            {
	            poisonTicks.Clear();
            }
        }
	    GameManager.instance.PoisonAlert.SetActive(false);
    }
    #endregion

    #region //burn
    public void ApplyBurn(int ticks)
    {
        if (burnTicks.Count <= 0)
        {
            burnTicks.Add(ticks);
            StartCoroutine(Burn());
        }
        else
        {
            burnTicks.Add(ticks);
        }
    }


    IEnumerator Burn()
    {
        while (burnTicks.Count > 0)
        {
            for (int i = 0; i < burnTicks.Count; i++)
            {
                burnTicks[i]--;
            }
            GameManager.instance.playerScript.HP -= damage;
            GameManager.instance.playerScript.updatePLayerHud();
            burnTicks.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(1);
        }
    }
    #endregion

}
