using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{

    public List<int> burnTicks = new List<int>();
    [SerializeField] int damage;

    // Start is called before the first frame update
    void Start()
    {

    }


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

            if (GameManager.instance.playerScript.HP > 0)
            {

                GameManager.instance.playerScript.HP -= damage;
                GameManager.instance.playerScript.updatePLayerHud();
                burnTicks.RemoveAll(i => i == 0);
                yield return new WaitForSeconds(1);
            }
        }
    }

}
