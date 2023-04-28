using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;


public enum EKillRange
{
    Short, Normal, Long
}

//public enum ETaskBarUpdates
//{
//    Always, Meetings, Never
//}


public struct RuleData
{
    public bool confirmEjects;
    public int emergencyMeetings;
    public int emergencyMeetingCD;
    public int meetingTime;
    public int voteTime;
    //public bool anonymousVotes;
    public float moveSpeed;
    //public float crewSight;
    public float killCD;
    public EKillRange killRange;
    //public bool visualTasks;
    //public ETaskBarUpdates taskBarUpdates;
    //public int commonTask;
    //public int complexTask;
    //public int simpleTask;

}

public class RuleSetting : NetworkBehaviour
{

    [SyncVar]
    private bool isRecommendRule;
    [SerializeField]
    private Toggle isRecommendToggle;
    public void OnRecommendToggle(bool value)
    {
        isRecommendRule = value;
        if(isRecommendRule)
        {
            SetRecommendRule();
        }
    }

    [SyncVar]
    private bool confirmEjects;
    [SerializeField]
    private Toggle confirmEjectsToggle;
    public void OnConfirmEject(bool value)
    {
        isRecommendRule = false;
        isRecommendToggle.isOn = false;
        confirmEjects = value;
    }

    [SyncVar]
    private int emergencyMeetings;
    [SerializeField]
    private TMP_Text emergencyMeetingsText;
    public void OnChange_emergencyMeetings(bool isPlus)
    {
        emergencyMeetings = Mathf.Clamp(emergencyMeetings + (isPlus ? 1 : -1), 0, 9);
        isRecommendRule = false;
        isRecommendToggle.isOn = false;
        emergencyMeetingsText.text = emergencyMeetings.ToString();

    }

    [SyncVar]
    private int emergencyMeetingCD;
    [SerializeField]
    private TMP_Text emergencyMeetingCDText;
    public void OnChange_emergencyMeetingCD(bool isPlus)
    {
        emergencyMeetingCD = Mathf.Clamp(emergencyMeetingCD + (isPlus ? 5 : -5), 0, 60);
        isRecommendRule = false;
        isRecommendToggle.isOn = false;
        emergencyMeetingCDText.text = emergencyMeetingCD.ToString();

    }

    [SyncVar]
    private int meetingTime;
    [SerializeField]
    private TMP_Text meetingTimeText;
    public void OnChange_meetingTime(bool isPlus)
    {
        meetingTime = Mathf.Clamp(meetingTime + (isPlus ? 5 : -5), 0, 120);
        isRecommendRule = false;
        isRecommendToggle.isOn = false;
        meetingTimeText.text = meetingTime.ToString();

    }

    [SyncVar]
    private int voteTime;
    [SerializeField]
    private TMP_Text voteTimeText;
    public void OnChange_voteTime(bool isPlus)
    {
        voteTime = Mathf.Clamp(voteTime + (isPlus ? 5 : -5), 0, 200);
        isRecommendRule = false;
        isRecommendToggle.isOn = false;
        voteTimeText.text = voteTime.ToString();

    }

    [SyncVar]
    private float moveSpeed;
    [SerializeField]
    private TMP_Text moveSpeedText;
    public void OnChange_moveSpeed(bool isPlus)
    {
        moveSpeed = Mathf.Clamp(moveSpeed + (isPlus ? 0.25f : -0.25f), 0.5f, 3f);
        isRecommendRule = false;
        isRecommendToggle.isOn = false;
        moveSpeedText.text = moveSpeed.ToString();

    }

    [SyncVar]
    private float killCD;
    [SerializeField]
    private TMP_Text killCDText;
    public void OnChange_killCD(bool isPlus)
    {
        killCD = Mathf.Clamp(killCD + (isPlus ? 2.5f : -2.5f), 5f, 60f);
        isRecommendRule = false;
        isRecommendToggle.isOn = false;
        killCDText.text = killCD.ToString();

    }

    [SyncVar]
    private EKillRange killRange;
    [SerializeField]
    private TMP_Text killRangeText;
    public void OnChange_killRange(bool isPlus)
    {
        killRange = (EKillRange)Mathf.Clamp((int)killRange + (isPlus ? 1 : -1), 0, 2);
        isRecommendRule = false;
        isRecommendToggle.isOn = false;
        killRangeText.text = killRange.ToString();

    }

    private void SetRecommendRule()
    {
        isRecommendRule = true;
        confirmEjects = true;
        confirmEjectsToggle.isOn = true;
        emergencyMeetings = 1;
        emergencyMeetingsText.text = emergencyMeetings.ToString();
        emergencyMeetingCD = 15;
        emergencyMeetingCDText.text = emergencyMeetingCD.ToString();
        meetingTime = 15;
        meetingTimeText.text = meetingTime.ToString();
        voteTime = 120;
        voteTimeText.text = voteTime.ToString();
        moveSpeed = 1f;
        moveSpeedText.text = moveSpeed.ToString();
        killCD = 5f;
        killCDText.text = killCD.ToString();
        killRange = EKillRange.Normal;
        killRangeText.text = killRange.ToString();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            SetRecommendRule();
        }
    }
}
