using UnityEngine;
using UnityEngine.InputSystem;

public class scrInput : MonoBehaviour
{
    // InputAction은 Unity의 새로운 InputSystem을 사용하여 입력을 처리하는 데 사용됩니다.
    // 현재 조이스틱으로 입력을 받도록 설정되어 있습니다.
    // 조이스틱으로 입력을 받을 경우 그 값은 Vector2 형태입니다. (x축, y축)
    public InputAction InputAction;

    // IsHorizontalInput은 수평(x축) 입력을 받을지 여부를 결정합니다.
    // True면 수평(x축, 빨강) 입력을, False면 수직(y축, 파랑) 입력을 받습니다.
    public bool IsHorizontalInput;

    // ScaleDivisor와 ScaleMultiplier는 그래프를 그릴 때 사용되는 규모 관련 변수입니다.
    public float ScaleDivisor = 10f;
    public float ScaleMultiplier = 3f;

    // FollowCamera는 카메라를 따라가는 기능을 구현하기 위한 변수입니다.
    // Unity에서 카메라는 3D 공간에서 물체를 바라보는 역할을 합니다.
    // 일종의 뷰포트(viewport)로, 카메라가 바라보는 방향과 위치에 따라 화면에 보이는 장면이 달라집니다.
    public Camera FollowCamera;

    // _lineRenderer는 LineRenderer 컴포넌트를 사용하여 그래프를 그리는 데 사용됩니다.
    // LineRenderer는 Unity에서 특정한 갯수의 Vector3 3차원 위치(x,y,z)를 가진 점들이 있을 때,
    // 이 점들을 선으로 연결하여 시각적으로 표현하는 데 사용됩니다.
    private LineRenderer _lineRenderer;

    // Position은 현재 그래프의 위치(시작으로부터 흐른 프레임 수)를 나타내는 변수입니다.
    public int Position;

    // Start is called before the first frame update
    // Start() 함수는 이 물체가 활성화 될 때 (프로그램이 시작될 때) 한 번 호출됩니다.
    void Start()
    {
        // LineRenderer 컴포넌트를 가져와서 _lineRenderer 변수에 할당합니다.
        _lineRenderer = GetComponent<LineRenderer>();

        // 조이스틱의 입력을 받기 위해 InputAction을 활성화합니다.
        InputAction.Enable();
    }

    // Update is called once per frame
    // Update() 함수는 매 프레임 (화면이 다시 그려질 때마다) 호출됩니다.
    void Update()
    {
        // IsHorizontalInput (수평(x축)의 입력을 받을 것인지 여부)에 따라 InputAction의 x축 또는 y축 값을 읽어옵니다.
        // 이 값은 조이스틱의 입력 값으로, -1에서 1 사이의 값을 가집니다.
        var axis = IsHorizontalInput ? InputAction.ReadValue<Vector2>().x : InputAction.ReadValue<Vector2>().y;

        // LineRenderer에 점을 추가하기 위해 점이 들어갈 수 있는 배열의 크기를 증가시킵니다.
        _lineRenderer.positionCount++;

        // 배열의 맨 마지막 위치에 새로운 점을 추가합니다.
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1,
            // 여기서 점의 위치는 x는 0 고정, y는 높낮이를 표현하고자 axis 값에 그래프의 시인성을 위해 ScaleMultiplier를 곱하고,
            // z는 Position을 ScaleDivisor로 나눈 값(그래프의 흐름을 표현하기 위해)으로 설정합니다.
                                  new Vector3(0, axis * ScaleMultiplier, Position / ScaleDivisor));

        if (FollowCamera != null)
            // FollowCamera의 위치를 업데이트합니다.
            // 이때 카메라의 위치는:
            // x는 설정된 -5f, y는 0f로 고정하고, (에디터에서 설정된 값)
            // z는 Position을 ScaleDivisor로 나눈 값으로 설정하여 카메라가 그래프를 따라가도록 합니다.
            // 또 여기서 카메라의 회전 값은 음의 x축을 향하게 설정합니다 (-90,0,0).
            FollowCamera.transform.position = new Vector3(FollowCamera.transform.position.x,
                                                          FollowCamera.transform.position.y,
                                                          Position / ScaleDivisor);

        // Position을 증가시켜 다음 프레임에서 그래프의 다음 위치를 나타내도록 합니다.
        Position++;
    }
}
