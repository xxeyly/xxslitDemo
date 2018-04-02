using UnityEngine;
using System.Collections;

public class EmissionChange : MonoBehaviour {
	public float mini = 0.15f;
	public float maxi = 1.0f;
	public float TimeMultiplier = 0.5f;
	//public float Duration = 2.0f;
	void Update () {

		Renderer renderer = GetComponent<Renderer> ();
		UnityEngine.Material mat = renderer.material;
		float emission = mini + Mathf.PingPong (Time.time*TimeMultiplier, maxi - mini);
		//float emission = Mathf.PingPong (Time.time, 1.0f);
		Color baseColor = Color.white; //Replace this with whatever you want for your base color at emission level '1'
		
		Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
		
		mat.SetColor ("_EmissionColor", finalColor);
}
}