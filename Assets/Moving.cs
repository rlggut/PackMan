using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

	public float dx,dy,dz,drx,dry;
	public float ang,sn;
	public string Mess;
	public GameObject camera,tex;
	public float speed_boost;
	public bool end;
	public int timerp;
	
	private int timer,count;
	private Vector4 t;
	private Vector3 offset;
	private int steps;

	void Start ()
	{
		count = 0;
		offset = transform.position;
		end = false;
		steps = 0;
		dx = 0;dy = 0;dz = 0;
		timer = 0;
		speed_boost = 1;		
		drx = Input.GetAxis ("Mouse X");dry = Input.GetAxis ("Mouse Y");
	}
	void FixedUpdate ()
	{
		ang =-camera.transform.localEulerAngles.y;// transform.rotation.y;
		ang = (ang / 180) * Mathf.PI;
		dz = 0;
		dy = 0;
		if (Input.GetKey(KeyCode.W))
		{
			dy=-0.1f*Mathf.Sin(ang);
			dz=0.1f*Mathf.Cos(ang);
		}
		if (Input.GetKey(KeyCode.S))
		{
			dy=0.1f*Mathf.Sin(ang);
			dz=-0.1f*Mathf.Cos(ang);
		}
		if (Input.GetKey(KeyCode.D))
		{
			dz=0.1f*Mathf.Sin(ang);
			dy=0.1f*Mathf.Cos(ang);
		}
		if (Input.GetKey(KeyCode.A))
		{
			dz=-0.1f*Mathf.Sin(ang);
			dy=-0.1f*Mathf.Cos(ang);
		}
		if ((timer==0)&&(Input.GetKey("space")))
		{
			dx=+0.1f;
			timer=20;
		}

		GetComponent<Rigidbody>().freezeRotation = true;//отключение "виляния" камеры при столкновениях


		drx = Input.GetAxis ("Mouse X");
		dry = -Input.GetAxis ("Mouse Y");
		Vector3 Rot = new Vector3 (dry, drx, -camera.transform.localEulerAngles.z);//последнее для стабилизации
		//		Vector3 R1 = new Vector3 (transform.rotation.x, transform.rotation.y, transform.rotation.z);
		camera.transform.Rotate (Rot);
		
		Vector3 Move = new Vector3 (dy*speed_boost, dx*speed_boost, dz*speed_boost);
		transform.position = transform.position + Move;
		if (Input.GetKey(KeyCode.C))
		{
			Vector3 nul = new Vector3 (-camera.transform.localEulerAngles.x, 0, 0);
			camera.transform.Rotate (nul);
		}
		if (timer > 0)
		{
			timer--;
		}
		if (timer == 0) {
			dx = 0;//;-0.1f;
		}
		if(timerp>0)
		{
			timerp--;
		}
		if (!(end)) {
			steps++;
		}
		if((t.x==1)&&(t.y==1)&&(t.z==1)&&(t.w==3)) end=true;
		if(steps>200) tex.SetActive(false);
	}
	void OnTriggerEnter(Collider other)
	{
		if ((other.gameObject.tag == "1 level")) {
			other.gameObject.SetActive(false);
			t.x++;
		}
		if ((other.gameObject.tag == "2 level")) {
			other.gameObject.SetActive(false);
			t.y++;
		}
		if ((other.gameObject.tag == "3 level")) {
			other.gameObject.SetActive(false);
			t.z++;
		}
		if ((other.gameObject.tag == "4 level")) {
			other.gameObject.SetActive(false);
			t.w++;
		}
	}

	void OnGUI() {
		Mess = GUILayout.TextField("Прошло: "+(steps/100).ToString()+":"+(steps%100).ToString(), 25);
		Mess = GUILayout.TextField("Первый этаж: "+(t.x).ToString()+" из "+(1).ToString(), 25);
		Mess = GUILayout.TextField("Второй этаж: "+(t.y).ToString()+" из "+(1).ToString(), 25);
		Mess = GUILayout.TextField("Третий этаж: "+(t.z).ToString()+" из "+(1).ToString(), 25);
		Mess = GUILayout.TextField("Четвертый этаж: "+(t.w).ToString()+" из "+(3).ToString(), 25);
	}
}
