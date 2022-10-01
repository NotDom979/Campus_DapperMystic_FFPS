using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAi : MonoBehaviour, IDamage
{
	[SerializeField] int HP;
	[SerializeField] int speed;
	private bool dirRight = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
		//Move the enemy back and forth
	    if (dirRight)
	    {
	    	transform.Translate(Vector2.right * speed * Time.deltaTime);
	    }
	    
	    else
	    {
	    	transform.Translate(-Vector2.right * speed * Time.deltaTime);
	    }
	    
	    if (transform.position.x >= 6.0f)
	    {
	    	dirRight = false;
	    }
	    
	    if (transform.position.x <= -6.0f)
	    {
	    	dirRight = true;
	    }
    }
	public void takeDamage(int dmg)
	{
		HP -= dmg;
		if (HP <= 0)
		{
			Destroy(gameObject);
		}
	}
}
