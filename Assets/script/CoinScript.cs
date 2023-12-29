using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 initialSpeed;
    private float time = 0;
    public float timeBeforeStop;
    public float launchSpeed;
    public float gravity;
    public float drag; //drag is percentage, if drag = 0.01, speed will lose 1% every frame

    private bool canHover = false;
    public float hoverHeight;
    public float hoverSpeed;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        launchSpeed = Random.Range(launchSpeed/2, launchSpeed);
        // timeBeforeStop = Random.Range(timeBeforeStop/2, timeBeforeStop * 1.5f);
        float angle = Random.Range(0, 2 * Mathf.PI);
        initialSpeed = new Vector3(launchSpeed * Mathf.Cos(angle), launchSpeed * Mathf.Sin(angle), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(canHover)
            Hover();
        else if(time <= timeBeforeStop)
            Splash();
        else
        {
            initialPosition = transform.position;
            canHover = true;
        }
    }

    void Hover()
    {
        float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        Vector3 newPosition = initialPosition;
        newPosition.y += hoverOffset;
        transform.position = newPosition;
    }

    void Splash()
    {
        Vector3 newPosition = initialPosition;
        time += Time.deltaTime;
        initialSpeed.x *= 1 - drag * Time.deltaTime;
        initialSpeed.y *= 1 - drag * Time.deltaTime;
        newPosition.x = initialPosition.x + initialSpeed.x * time;
        newPosition.y = initialPosition.y + initialSpeed.y * time - (gravity / 2 * Mathf.Pow(time, 2));
        transform.position = newPosition;
    }
}
