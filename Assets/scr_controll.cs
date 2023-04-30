using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_controll : MonoBehaviour
{
    Rigidbody body;
    public float spe = 0.0f;
    public Vector3 jump = new Vector3(0.0f,2.0f,0.0f);
    public float jumpstrengh = 2.0f;
    public bool on_ground;

    public float neu_x = 0.0f;
    public float neu_y = 0.0f;
    public float neu_z = 0.0f;

    public CharacterController controller;
    public Text posit;
    public Text speed_game;
    public GameObject player;
    public GameObject go_to;
    public GameObject to_start;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        neu_x = transform.position.x;
        neu_y = transform.position.y;
        neu_z = transform.position.z;
        
    }

    void OnCollisionStay(){
        on_ground = true;
    }

    // Update is called once per frame

    void Update()
    {
        //Erhalte aktuelle Position
        neu_x = transform.position.x;
        neu_y = transform.position.y;
        neu_z = transform.position.z;

        //schreibe Position
        string positText = "Position: (" + neu_x.ToString("#.00") + "," + neu_y.ToString("#.00") + "," + neu_z.ToString("#.00") + ")";
        posit.text = positText;
        //posit.text = "Pos: " + neu_x.ToString("#.00");

        //Bedingung für Bewegung. (optional)
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Vector3 direct = new Vector3 (horiz,0.0f,vert);


        //Allgemeine Bewegung
        if(direct.magnitude >= 0.1f){
            //body.AddForce(direct*spe*Time.deltaTime);
            if(Input.GetKey(KeyCode.A)){
                body.AddForce(Vector3.left);
            }
            if(Input.GetKey(KeyCode.D)){
                body.AddForce(Vector3.right);
            }
            if(Input.GetKey(KeyCode.W)){
                body.AddForce(Vector3.forward);
            }
            if(Input.GetKey(KeyCode.S)){
                body.AddForce(Vector3.back);
            }
        }



        //Springen
        if(Input.GetKeyDown(KeyCode.Space) && on_ground == true){
            on_ground = false; //Hier wollte ich zuerst einen Doppelsprung verhinden zum testen. Ein Doppelsprung (oder mindestens zwei) wollte ich hinzufügen. Warum ein Doppelsprung hier entsteht ist mir jedoch immernoch unergründlich. Rein logisch betrachtet sollte es nicht geschehen.
            body.AddForce(jump*jumpstrengh,ForceMode.Impulse);
            on_ground = false;
        }



        //berechne und schreibe Geschwindigkeit
        spe = Vector3.Magnitude(body.velocity);
        speed_game.text = "Speed: " + spe.ToString("#.00");

        
        
    }

    //Teleport
    private void OnTriggerEnter(Collider collision){
        if(collision.gameObject.CompareTag("Lava")){
            player.transform.position = go_to.transform.position;
        }
       
    }
}
