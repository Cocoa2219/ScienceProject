using UnityEngine;
using UnityEngine.InputSystem;

public class scrInput : MonoBehaviour
{
    public InputAction InputAction;
    public bool IsHorizontalInput;
    public float ScaleDivisor = 10f;
    public float ScaleMultiplier = 3f;
    public Camera FollowCamera;

    private LineRenderer _lineRenderer;

    public int Position;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        InputAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        var axis = IsHorizontalInput ? InputAction.ReadValue<Vector2>().x : InputAction.ReadValue<Vector2>().y;

        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1,
                                  new Vector3(0, axis * ScaleMultiplier, Position / ScaleDivisor));

        if (FollowCamera != null)
            FollowCamera.transform.position = new Vector3(FollowCamera.transform.position.x,
                                                          FollowCamera.transform.position.y,
                                                          Position / ScaleDivisor);

        Position++;
    }
}
