using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(SpriteRenderer))]
public class animationLoop : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private int framenum;
	private float internalTime;
     private Entity entity;

	public Sprite[] frames;
	[Tooltip("frames per second that will be cycled through")]
	private float frameRate {get {return entity.entityType.frameRate;} }
	public bool paused = false;

    void Awake() {
  		if ((spriteRenderer = GetComponent<SpriteRenderer>()) == null) {
			Debug.LogError("oops -- somehow this object doesn't have an SpriteRenderer");
		}
        if ((entity = GetComponent<Entity>()) == null) {
			Debug.LogError("oops -- somehow this object doesn't have an Entity");
        }
		internalTime=0.0f;
		framenum=0;
    }

	void Start() {
		UpdateFrame();
   	}

    void Update() {
        if (paused)
            return;
        internalTime += Time.deltaTime;
        int oldframenum = framenum;
        framenum = (int)Mathf.Floor(internalTime * frameRate);
        framenum = framenum % frames.Length;
        if (framenum != oldframenum)
            UpdateFrame();
    }

	private void UpdateFrame() {
		spriteRenderer.sprite = frames[framenum];
	}
}
