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
    
    [Header("��ȭ ���� ��� ����")]
    [SerializeField] private float delay;

    [Header("����� ��ư")]
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

    //  ���� ��ȣ�ۿ�� ��ȭâ�� ����ϱ� ���� �Լ�    
    public void StartDialog(ref string name, ref string senten)
    {        
        HideButton();
        //  ��ȭâ�� ��µǴ� ���ȿ� ĳ���Ͱ� �������� ���ϰ� isStop�� true
        GameManager.instance.IsStop = true;
        //  ��ȭâ ����� ���� �ڷ�ƾ ����, ��ȭâ�� ����� ����� �̸��� ����� ���� ����
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

    //  ȭ���� ���� ��ư�� ��ũ ��ư�� on/off�ϴ�  �Լ�
    private void HideButton()
    {
        settingMenu.SetActive(!settingMenu.activeSelf);
        linkMenu.SetActive(!linkMenu.activeSelf);
    }

    //  ���� â�� on/off�ϴ�  �Լ�
    private void SettingUISetActive()
    {
        settingUI.SetActive(!settingUI.activeSelf);
    }

    //  ��ũ â�� on/off�ϴ�  �Լ�
    private void LinkUISetActive()
    {
        linkUI.SetActive(!linkUI.activeSelf);
    }

    //  ���� ���� �ٽ� �ҷ����� �Լ�
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //  ��Ʈ�� ������ ���ư��� �Լ�
    public void BackIntro()
    {
        SceneManager.LoadScene(0);
    }

    //  ��� ��ũ�� ���� �Լ�
    private void ToNotion()
    {
        Application.OpenURL("https://bit.ly/3wHQ04P");
    }

    //  ��������� on/off �ϰ� ��ư�� ���� �ٲٴ� �Լ�
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

    //  ��� �ӹ��� ������ ������ �ȳ�â�� on/off �ϴ� �Լ�
    public void FinishUISetActive()
    {
        finishUI.SetActive(!finishUI.activeSelf);
    }
}
