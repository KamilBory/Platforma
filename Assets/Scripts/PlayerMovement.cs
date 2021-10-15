using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PressureControll;
using System.IO.Ports;
using System.IO;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 8f;
    public float gravity;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public Camera [] cameras;
    public GameObject hipPos;

    private Vector3 velocity;
    private bool isGrounded;
    private float jumpHeight = 2.5f;
    private bool canJump = true;
    private float jumpCooldown = 1f;
    private float jumpTimer = 0f;
    private VelocityControll.VelCheck velCheck = new VelocityControll.VelCheck();
    private float speed = 0f;
    private Vector3 vecRight = new Vector3(1.5f, 0, 0);
    private Vector3 vecLeft = new Vector3(-1.5f, 0, 0);
    private Vector3 vecMiddle = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        velCheck.init("COM1");
        velCheck.measurement_start();
        if (FindObjectOfType<DiffLevel>().getCameraLevel() == 0)
        {
            cameras[0].gameObject.SetActive(true);
            cameras[1].gameObject.SetActive(false);
        }
        else
        {
            cameras[0].gameObject.SetActive(false);
            cameras[1].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame 
    void Update()
    {
        if(FindObjectOfType<DiffLevel>().getSteerLevel() == 0)
        {
            //GetComponent<VelCheck>().init("COM1");
            //moveSpeed = (float)velCheck.getV();
            //Debug.Log(moveSpeed);
            //moveLeftRight();
            Vector3 vec = new Vector3(0, 0, 0);

            if(hipPos.transform.localPosition.x < -0.18)
            {
                vec = vecLeft;
            }
            else if(hipPos.transform.localPosition.x >= -0.18 && hipPos.transform.position.x <= 0.06)
            {
                vec = vecMiddle;
            }
            else if(hipPos.transform.localPosition.x > 0.06)
            {
                vec = vecRight;
            }
            //Debug.Log(hipPos.transform.localPosition.x);

            Vector3 tmp = ReadFile();

            controller.Move((vec + tmp) * Time.deltaTime);
            

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -7f;
            }
            velocity.y += 2 * gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            //transform.position = new Vector3(transform.position.x, transform.position.y + velocity.y * Time.deltaTime, transform.position.z);
        }
        else
        {
            if (jumpTimer >= jumpCooldown)
            {
                canJump = true;
            }
            else
            {
                jumpTimer += Time.deltaTime;
            }

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -7f;
            }

            float hAxis = Input.GetAxis("Horizontal");
            float vAxis = Input.GetAxis("Vertical");

            Vector3 moveVec = transform.right * hAxis + transform.forward * vAxis;

            controller.Move(moveVec * moveSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && canJump)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                canJump = false;
                jumpTimer = 0;
            }

            velocity.y += 2 * gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    /*private void moveLeftRight()
    {
        Zebris z = new Zebris();
        z.init();
        z.pressure_init(120);//częstotliwość próbkowania
        z.activate();

        while (true)
        {
            z.transfer();//zbieranie danych z bieżni - wymusza transfer do komputera
            List<PressurePoint> pts = z.CoP(z.getPressure());//zbieranie danych o pozycji środka człowieka - jeżeli będziesz wywoływał tą funkcję częściej niż
                                 S                               // bieżnia odpowiada, lista będzie mieć 0 lub 1 element - jeżeli rzadziej, będzie przechowywać
                                                                //wszystkie położenia od momentu ostatniego transferu.
            foreach (PressurePoint pt in pts)
            {
                if (!pt.isNaN())//jeżeli isNaN == true to nikt nie stoi na bieżni
                {
                    //Console.WriteLine(" Pozycja x:{0},\n Pozycja y:{1},\n Czas t:{2}", pt.x, pt.y, pt.t);//tylko x i t Cię interesują jeżeli chcesz znać 
                    //pozycję "lewo prawo". x - położenie środka w pozycji poziomej
                    //od lewej krawędzi bieżni wyrażone w metrach
                    //t - czas w sekundach od początku pomiaru
                    //Console.WriteLine("{0},{1}", pt.x / z.PData.width - 0.5, pt.y / z.PData.height - 0.5);//przykład normalizacji - zamiast współrzędnych, wynik 
                    //od -0.5 do 0.5 oznaczający jak bardzo w lewo/prawo jest 
                    //wychylona osoba

                    float dir = ((float)((float)pt.x / z.PData.width - 0.5));
                    velocity.x = transform.position.x + dir;
                    controller.Move(velocity * Time.deltaTime);
                }
            }
        }
    }*/

    private Vector3 ReadFile()
    {
        try
        {
            StreamReader reader = new StreamReader("Assets/output.txt");
            string number = reader.ReadToEnd();
            number = number.Replace('.', ',');
            //Debug.Log(number);
            speed = float.Parse(number);
            reader.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

        //Debug.Log(speed);
        Vector3 forward = new Vector3(0, 0, speed * 3);
        return forward;
    }
}
