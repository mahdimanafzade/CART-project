using UnityEngine;
using System.Collections;

public class CameraZoomControl : MonoBehaviour {

    public int inverse_direction=-1;
    public float sensivity_factor = 2f;
    public float min_zoom = 2f;
    public float max_zoom = 5f;
    public float speed_zoom = 5f;

    float XMove_sensivity=1;
    float YMove_sensivity=1;
    
    

    
    float deltaposx;
    float deltaposy;
    Camera camera;
    Transform _transform;
    Touch[] touches;

    Vector3 offset;
    Vector3 prev_touch_pos0;
    Vector3 prev_touch_pos1;

    float screen_ratio = (float) (Screen.width)/((float) Screen.height);
	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        _transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        
        XMove_sensivity = camera.orthographicSize / sensivity_factor;
        YMove_sensivity = camera.orthographicSize / sensivity_factor;

        touches=Input.touches;
        if (Input.touchCount == 1) {
            if (touches[0].phase == TouchPhase.Moved )
            {
                deltaposx = touches[0].deltaPosition.x;
                deltaposy = touches[0].deltaPosition.y;
                offset=inverse_direction * new Vector3(Time.deltaTime * deltaposx * XMove_sensivity, Time.deltaTime * deltaposy * YMove_sensivity, 0f);

                

                if(Mathf.Abs(_transform.position.y+offset.y) <= (max_zoom-camera.orthographicSize) &&
                Mathf.Abs(_transform.position.x+offset.x) <= (screen_ratio*(max_zoom - camera.orthographicSize))){
                    
                    _transform.position = _transform.position + offset;
                }


            }
        }
        else if (Input.touchCount == 2)
        {
            if (touches[0].phase == TouchPhase.Moved || touches[1].phase == TouchPhase.Moved)
            {
                prev_touch_pos0 = touches[0].position - touches[0].deltaPosition;
                prev_touch_pos1 = touches[1].position - touches[1].deltaPosition;

                float prev_touch_deltaMag = (prev_touch_pos0 - prev_touch_pos1).magnitude;
                float touch_deltaMag = (touches[0].position - touches[1].position).magnitude;
                float diff_deltaMag = prev_touch_deltaMag - touch_deltaMag;
                camera.orthographicSize += diff_deltaMag * speed_zoom * Time.deltaTime;
                camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, min_zoom, max_zoom);                

                _transform.position = new Vector3(Mathf.Clamp(_transform.position.x, -screen_ratio * (max_zoom - camera.orthographicSize), screen_ratio * (max_zoom - camera.orthographicSize)),
                 Mathf.Clamp(_transform.position.y,-(max_zoom - camera.orthographicSize), (max_zoom - camera.orthographicSize)),
                 _transform.position.z);

            }
        }

	}
}
