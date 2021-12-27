using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {


	public Vector3 target=new Vector3(0,0,0);
	public GameObject collisionExplosion;
	public float speed;

	void Update()
	{
		transform.position=transform.position+target*speed*Time.deltaTime;
		Destroy(this,2.0f);
	}

	public void set_target(Vector3 vstup)
	{
		target=vstup;
	}

	void explode()
	{
		/*if(collisionExplosion !=null)
		{
			GameObject explosion=(GameObject)Instantiate(collisionExplosion,transform.position,transform.rotation);
			Destroy(gameObject);
			Destroy(explosion,1.0f);
		}*/
	}
}
