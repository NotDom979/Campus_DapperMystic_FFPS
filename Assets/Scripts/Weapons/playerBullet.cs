using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
	[SerializeField] Rigidbody rb;
	[SerializeField] public int damage;
	[SerializeField] int speed;
	[SerializeField] int destroyTime;
	public float spread;
	public float spreadCount;
	GameObject enemy;
	public playerController player;
	int dmg;
	// Start is called before the first frame update
	void Start()
	{
		rb.velocity = transform.forward * speed;
		Destroy(gameObject, destroyTime);
		StartCoroutine(Spread());
	}
	void Update(){
	}

	private void OnTriggerEnter(Collider other)
	{
		
		if (other.CompareTag("enemy"))
		{
			other.gameObject.GetComponent<IDamage>().takeDamage(damage);
			Destroy(gameObject);
		}
		Destroy(gameObject);
	}
	IEnumerator Spread()
	{
		for (int i = 0; i < spreadCount; i++) {
			Vector3 dir = transform.forward + new Vector3(Random.Range(-spread,spread),Random.Range(-spread,spread),Random.Range(-spread,spread));
			rb.AddForce(dir);
		}
		yield return new WaitForSeconds(.1f);
	}
}
