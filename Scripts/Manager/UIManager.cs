using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public static UIManager instance = null;

    [SerializeField] private GameObject dialog;

    [Header("Setting")]
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private Button settingMenuBtn;
    [SerializeField] private Text bgmText;
    [SerializeField] private Button bgmOnBtn;
    [SerializeField] private Text bgmOnText;
    [SerializeField] private Button bgmOffBtn;
    [SerializeField] private Text bgmOffText;
    [SerializeField] private Button backIntroBtn;
    [SerializeField] private Button settingMenuCancelBtn;
    
    [Header("대화 문장 출력 간격")]
    [SerializeField] private float delay;

    [Header("재시작 버튼")]
    [SerializeField] private Button restartBtn;

    [Header("Link")]
    [SerializeField] private GameObject linkUI;
    [SerializeField] private GameObject linkMenu;
    [SerializeField] private Button linkMenuBtn;
    [SerializeField] private Button toNotionBtn;
    [SerializeField] private Button linkMenuCancelBtn;

    [Header("Finish")]
    [SerializeField] private GameObject finishUI;
    private bool isPrinted;
    private Color myGrey;
    private Text textNpcName;
    private Text textDialog;
    
    
    private IEnumerator curSenten;
    private IEnumerator skip;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        Init();
    }

    private void Init()
    {
        Text[] components = dialog.GetComponentsInChildren<Text>();
        textNpcName = components[0];
        textDialog = components[1];

        myGrey = bgmText.color;
        settingMenuBtn.onClick.AddListener(SettingUISetActive);
        bgmOnBtn.onClick.AddListener(() => BgmEnabled(true));
        bgmOffBtn.onClick.AddListener(() => BgmEnabled(false));
        restartBtn.onClick.AddListener(RestartGame);
        settingMenuCancelBtn.onClick.AddListener(SettingUISetActive);

        linkMenuBtn.onClick.AddListener(LinkUISetActive);
        toNotionBtn.onClick.AddListener(ToNotion);
        linkMenuCancelBtn.onClick.AddListener(LinkUISetActive);
        backIntroBtn.onClick.AddListener(BackIntro);
    }

    //  동상에 상호작용시 대화창을 출력하기 위한 함수    
    public void StartDialog(ref string name, ref string senten)
    {        
        HideButton();
        //  대화창이 출력되는 동안에 캐릭터가 움직이지 못하게 isStop을 true
        GameManager.instance.IsStop = true;
        //  대화창 출력을 위한 코루틴 시작, 대화창을 출력한 대상의 이름과 출력할 문장 전달
        StartCoroutine(DialogSystem(name, senten));
    }

    private IEnumerator DialogSystem(string name, string senten)
    {
        isPrinted = false;
        dialog.SetActive(true);
        textNpcName.text = name;
        curSenten = PrintSenten(senten);
        StartCoroutine(curSenten);
        yield return new WaitUntil(()=>{
            if (isPrinted) 
                return true;
            else 
                return false;
        });
        dialog.SetActive(false);
        yield break;
    }

    private IEnumerator PrintSenten(string senten)
    {
        skip = ClickWaitDialog(curSenten, senten);
        StartCoroutine(skip);
        textDialog.text = "";
        foreach (char letter in senten.ToCharArray())
        {
            textDialog.text += letter;
            yield return new WaitForSeconds(delay);
        }
        StopCoroutine(skip);
        StartCoroutine(Finish());
    }

    private IEnumerator ClickWaitDialog(IEnumerator prtSen, string senten)
    {
        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        StopCoroutine(prtSen);
        textDialog.text = senten;
        StartCoroutine(Finish());
    }

    private IEnumerator Finish()
    {
        StopCoroutine(curSenten);
        StopCoroutine(skip);
        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        isPrinted = true;
        GameManager.instance.IsStop = false;
        HideButton();
        yield break;
    }

    //  화면의 설정 버튼과 링크 버튼을 on/off하는  함수
    private void HideButton()
    {
        settingMenu.SetActive(!settingMenu.activeSelf);
        linkMenu.SetActive(!linkMenu.activeSelf);
    }

    //  설정 창을 on/off하는  함수
    private void SettingUISetActive()
    {
        settingUI.SetActive(!settingUI.activeSelf);
    }

    //  링크 창을 on/off하는  함수
    private void LinkUISetActive()
    {
        linkUI.SetActive(!linkUI.activeSelf);
    }

    //  게임 씬을 다시 불러오는 함수
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //  인트로 씬으로 돌아가는 함수
    public void BackIntro()
    {
        SceneManager.LoadScene(0);
    }

    //  노션 링크를 여는 함수
    private void ToNotion()
    {
        Application.OpenURL("https://bit.ly/3wHQ04P");
    }

    //  배경음악을 on/off 하고 버튼의 색을 바꾸는 함수
    private void BgmEnabled(bool flag)
    {        
        SoundManager.instance.BgmPlay(flag);
        if (flag)
        {            
            bgmOnText.color = Color.white;
            bgmOffText.color = myGrey;
        }
        else
        {
            bgmOnText.color = myGrey;
            bgmOffText.color = Color.white;
        }
    }

    //  모든 임무를 끝낼시 나오는 안내창을 on/off 하는 함수
    public void FinishUISetActive()
    {
        finishUI.SetActive(!finishUI.activeSelf);
    }
}
