
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public Camera camera;
    public GameObject paintBall;
    public ParticleSystem shotEffect;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))//default Fire1 by Unity is the left mouse button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        shotEffect.Play();
        FindObjectOfType<AudioManager>().Play("gunShot");
        RaycastHit hit;//(ray=keren laser)
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))//(start position for 
            //the ray, direction for the ray, insert info to hit variable, the range of the ray- how far/long it would be)
            //the function reutrns true if we hit something and that's why it's in if
        { 
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target!=null)//if the hitten object has the target script
            {
                target.changeColor(paintBall.transform.GetComponent<Renderer>().sharedMaterial.color);//change it's color to paintball's color
            }
        }
    }
}
