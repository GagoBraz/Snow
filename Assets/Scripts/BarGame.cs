using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BarGame : MonoBehaviour
{
    #region THUMB_VARIABLES
    [SerializeField]
    RectTransform topPivot;
    [SerializeField]
    RectTransform bottomPivot;
    [SerializeField]
    RectTransform thumb;


    float _thumbPosition;
    float _thumbDestination;
    float _thumbTimer;

    [SerializeField]
    float timerMultiplicator = 3f;
    [SerializeField]
    float smoothMotion = 1f;
    [SerializeField]
    float thumbSpeed = 1f;
    #endregion

    #region HOOK_VARIABLES
    [SerializeField]
    RectTransform hookTransform = null;
    float _hookPosition;
    [SerializeField]
    float hookPower = 0.5f;
    float _hookProgress;
    float _hookPullVelocity;
    [SerializeField]
    float hookPullPower = 0.01f;
    float hookGravityPower = 0.005f;
    float hookProgressDegradationPower = 0.1f;

    #endregion

    float _progressIndicator = 0.5f;

    [SerializeField]
    float progressSum = 0.05f;

    [SerializeField]
    Image background;

    [SerializeField]
    Color32 maxColor;
    [SerializeField]
    Color32 minColor;
    Color32 _currentColor;

    public UnityAction onMiniGameLose;

    private void OnEnable()
    {
        _progressIndicator = 0.5f;
        _thumbPosition = 0f;

    }

    private void Update()
    {
        MoveThumb();
        MoveHook();
        CheckBounds();
        InterpolateColor();
    }

    private void MoveThumb()
    {
        _thumbTimer -= Time.deltaTime;
        if (_thumbTimer < 0f)
        {
            _thumbTimer = Random.value * timerMultiplicator;
            _thumbDestination = Random.value;
        }

        _thumbPosition = Mathf.SmoothDamp(_thumbPosition, _thumbDestination, ref thumbSpeed, smoothMotion);
        thumb.position = Vector3.Lerp(bottomPivot.position, topPivot.position, _thumbPosition);
    }

    private void MoveHook()
    {
        if (InputController.hidingBarControl)
        {
            if(_hookPullVelocity < -0.01f)
            {
                _hookPullVelocity = 0;
            }

            _hookPullVelocity += hookPullPower * Time.deltaTime;
        }
        else
        {
            if(_hookPullVelocity > 0.01f)
            {
                _hookPullVelocity = 0;
            }
        }
        _hookPullVelocity -= hookGravityPower * Time.deltaTime;

        _hookPullVelocity = Mathf.Clamp(_hookPullVelocity, -0.03f, 0.03f);

        _hookPosition += _hookPullVelocity;
        _hookPosition = Mathf.Clamp(_hookPosition, 0, 1);

        hookTransform.position = Vector3.Lerp(bottomPivot.position, topPivot.position, _hookPosition);

    }

    private void CheckBounds()
    {
        float min = hookTransform.offsetMin.x;
        float max = hookTransform.offsetMax.x;

        float thumbX = thumb.anchoredPosition.x;

        if( thumbX > min && thumbX < max) {
            _progressIndicator += progressSum * 2 * Time.deltaTime;
        }
        else
        {
            _progressIndicator -= progressSum * Time.deltaTime;
        }

        _progressIndicator = Mathf.Clamp(_progressIndicator, -0.1f, 1);
        
        if(_progressIndicator < 0)
        {
            LoseMiniGame();
        }
    }


    private void InterpolateColor()
    {
        _currentColor = Color32.Lerp(minColor, maxColor, _progressIndicator);
        background.color = _currentColor;
    }
    

    private void LoseMiniGame()
    {
        onMiniGameLose.Invoke();
    }
}
