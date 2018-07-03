using UnityEngine;
using System.Collections;

public class CurveWalker : MonoBehaviour {

	public BezierCurve curve;
	
	public float duration;
	
	private float progress;
	
	private void Update () {
		progress += Time.deltaTime / duration;
		if (progress > 1f) {
			progress = 1f;
		}
		transform.localPosition = curve.GetPoint(progress);
	}
}
