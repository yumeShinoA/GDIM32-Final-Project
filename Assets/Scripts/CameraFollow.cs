using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] public GameObject[] players;
    public float DampTime = 0.2f;
    public float ScreenEdgeBuffer = 4f;
    public float MinSize = 6.5f;

    private Camera m_Camera;
    private Vector3 DesiredPosition;
    private float ZoomSpeed;
    private Vector3 MoveVelocity;

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(players.Length);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(players.Length == 0)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }
        Move();

        Zoom();
        //transform.position = player.transform.position + new Vector3(0, 1, -5);
    }

    private void Move()
    {
        FindAveragePosition();

        transform.position = Vector3.SmoothDamp(transform.position, DesiredPosition, ref MoveVelocity, DampTime);
    }

    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        for(int i = 0; i < players.Length; i++)
        {
            if (!players[i].gameObject.activeSelf)
                continue;
            averagePos += players[i].transform.position;
            numTargets++;
        }
        if (numTargets > 0)
            averagePos /= numTargets;

        averagePos.z = transform.position.z;
        DesiredPosition = averagePos;
    }

    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref ZoomSpeed, DampTime);
    }

    private float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(DesiredPosition);
        float size = 0f;
        for(int i = 0; i < players.Length; i++)
        {
            if (!players[i].gameObject.activeSelf)
                continue;
            Vector3 targetLocalPos = transform.InverseTransformPoint(players[i].transform.position);
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
        }
        size += ScreenEdgeBuffer;
        size = Mathf.Max(size, MinSize);
        return size;
    }
    public void GetAllPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(players.Length);
    }
}
