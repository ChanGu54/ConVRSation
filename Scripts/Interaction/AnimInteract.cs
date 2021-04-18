using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimInteract : MonoBehaviour
{
    // Start is called before the first frame update
    private float time;
    private GameObject luggage;
    private DF2ClientTester dialogflow;

    private TextMeshPro questText;
    private Animator animator;

    public GameObject quest_Prefab;
    private string cur_questanim;
    public GameObject arrow_Prefab;

    public Transform[] quest_mark_target;

    [Tooltip("오브젝트를 찾는 방식 결정")]
    public GameObject[] interaction;

    private GameObject[] quest_mark;

    [Tooltip("튜토리얼 씬에서만 사용하는 컨트롤러 애니메이션")]
    public Animator ctrleranim;
    public GameObject Tori;
    public GameObject beerRoot;
    public GameObject btn;


    [Tooltip("택시 씬에서만 사용하는 Value")]
    public Transform CameraPos;
    public GameObject taxiDriver;
    public GameObject Leo;

    public int flag = 0;

    public int _procedure
    {
        get
        {
            return GameManager.procedure;
        }
        set
        {
            Debug.Log("Procedure : " + value);

            GameManager.procedure = value;
            string[] str;
            switch (value)
            {
                case 30:
                    str = new string[]{ "튜토리얼을 시작하겠습니다.", "조작법은 양쪽 컨트롤러 모두 동일합니다.", "조이스틱 버튼을 통해 이동 가능합니다." };
                    StartCoroutine(QuestTextChange(3f, str));
                    break;
                case 31:
                    questText.text = "화살표 방향으로 이동하세요!";
                    cur_questanim = "QuestAppear";
                    ctrleranim.gameObject.SetActive(true);
                    quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 1.0f, 0) + quest_mark_target[19].transform.position, quest_mark_target[19].transform.rotation);
                    ctrleranim.Play("조이스틱", -1, 0f);
                    break;
                case 32:
                    Destroy(quest_mark[0]);
                    ctrleranim.gameObject.SetActive(false);
                    cur_questanim = "Default";
                    str = new string[] { "잘하셨습니다!", "다음은 대화 방법입니다.", "가까운 거리에서 사람과 마주치면 대화가 활성화됩니다." };
                    StartCoroutine(QuestTextChange(3f, str));
                    break;
                case 33:
                    GameObject.Find("MixedRealityPlayspace").transform.position = new Vector3(0, 0, 0);
                    cur_questanim = "QuestAppear";
                    questText.text = "Tori에게 말을 걸어보세요!";
                    quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 3.2f, 0) + quest_mark_target[20].transform.position, quest_mark_target[20].transform.rotation);
                    Tori.SetActive(true);
                    break;
                case 34:
                    Destroy(quest_mark[0]);
                    questText.text = "화면에 표시된 버튼을 누르는 동안만 발화가 가능합니다</b>(발화가 끝나면 버튼에서 손을 떼어주세요)";
                    ctrleranim.gameObject.SetActive(true);
                    ctrleranim.Play("터치스크린(대화상황)", -1, 0f);
                    break;
                case 35:
                    questText.text = "잘하셨습니다!";
                    ctrleranim.gameObject.SetActive(false);
                    cur_questanim = "Default";
                    break;
                case 36:
                    quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 1.0f, 0) + quest_mark_target[21].transform.position, quest_mark_target[21].transform.rotation);
                    questText.text = "책상 위를 가리킨 후, 화면에 표시된 버튼을 눌러주면 상호작용이 가능합니다.";
                    ctrleranim.gameObject.SetActive(true);
                    cur_questanim = "QuestAppear";
                    ctrleranim.Play("Select버튼", -1, 0f);
                    break;
                case 37:
                    Destroy(quest_mark[0]);
                    questText.text = "잘하셨습니다!";
                    ctrleranim.gameObject.SetActive(false);
                    cur_questanim = "Default";
                    break;
                case 38:
                    quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 1.0f, 0) + quest_mark_target[21].transform.position, quest_mark_target[21].transform.rotation);
                    questText.text = "화면의 버튼을 느낌표 아래를 가리킨 상태로 클릭하여 상호작용하세요!";
                    ctrleranim.gameObject.SetActive(true);
                    cur_questanim = "QuestAppear";
                    ctrleranim.Play("Select버튼", -1, 0f);
                    break;
                case 39:
                    Destroy(quest_mark[0]);
                    questText.text = "잘하셨습니다!";
                    ctrleranim.gameObject.SetActive(false);
                    cur_questanim = "Default";
                    break;
                case 40:
                    beerRoot.SetActive(true);
                    quest_mark[0] = Instantiate(arrow_Prefab, new Vector3(0, 1.0f, 0) + quest_mark_target[22].transform.position, quest_mark_target[22].transform.rotation, beerRoot.transform);
                    quest_mark[1] = Instantiate(quest_Prefab, new Vector3(0, 1.0f, 0) + quest_mark_target[21].transform.position, quest_mark_target[21].transform.rotation, beerRoot.transform);
                    questText.text = "박스를 화면의 버튼을 클릭하여 테이블 위로 올리세요!";
                    ctrleranim.gameObject.SetActive(true);
                    cur_questanim = "QuestAppear";
                    ctrleranim.Play("Select버튼", -1, 0f);
                    btn.SetActive(true);
                    break;
                case 41:
                    beerRoot.SetActive(false);
                    Destroy(quest_mark[0]);
                    Destroy(quest_mark[1]);
                    questText.text = "튜토리얼이 종료되었습니다. 진행하려면 버튼을 클릭하세요.";
                    ctrleranim.gameObject.SetActive(false);
                    btn.SetActive(true);
                    break;
                case 0:
                    questText.text = "Talk to Airport Check-In counter officer!";
                    quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 3.2f, 0) + quest_mark_target[0].transform.position, quest_mark_target[0].transform.rotation);
                    quest_mark[1] = Instantiate(quest_Prefab, new Vector3(0, 3.2f, 0) + quest_mark_target[1].transform.position, quest_mark_target[1].transform.rotation);
                    break;
                case 1:
                    Destroy(quest_mark[0]);
                    Destroy(quest_mark[1]);
                    quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 3.2f, 0) + quest_mark_target[2].transform.position, quest_mark_target[2].transform.rotation);
                    quest_mark[1] = Instantiate(quest_Prefab, new Vector3(0, 3.2f, 0) + quest_mark_target[3].transform.position, quest_mark_target[3].transform.rotation);
                    questText.text = "Interact with security check worker!";
                    break;
                case 2:
                    Destroy(quest_mark[0]);
                    Destroy(quest_mark[1]);
                    if (flag == 0)
                    {
                        quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 1.6f, 0) + quest_mark_target[4].transform.position, quest_mark_target[4].transform.rotation);
                    }
                    else if (flag == 1)
                    {
                        quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 1.6f, 0) + quest_mark_target[5].transform.position, quest_mark_target[5].transform.rotation);
                    }                   
                    questText.text = "Put your luggage on the basket!";
                    break;
                case 3:
                    Destroy(quest_mark[0]);
                    if (flag == 0)
                    {
                        quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 1.6f, 0) + quest_mark_target[6].transform.position, quest_mark_target[6].transform.rotation);
                    }
                    else if (flag == 1)
                    {
                        quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 1.6f, 0) + quest_mark_target[7].transform.position, quest_mark_target[7].transform.rotation);
                    }
                    questText.text = "Stand on the yellow line!";
                    StartCoroutine(AnimConversion("노란선", "security_노란선넘지마"));
                    break;
                case 4:
                    Destroy(quest_mark[0]);
                    if (flag == 0)
                    {
                        quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 0.5f, 0) + quest_mark_target[8].transform.position, quest_mark_target[4].transform.rotation);
                    }
                    else if (flag == 1)
                    {
                        quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 0.5f, 0) + quest_mark_target[9].transform.position, quest_mark_target[5].transform.rotation);
                    }
                    questText.text = "Walk through the scanner!"; 
                    break;
                case 5:
                    Destroy(quest_mark[0]);
                    if (flag == 0)
                    {
                        quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 1.6f, 0) + quest_mark_target[4].transform.position, quest_mark_target[8].transform.rotation);
                    }
                    else if (flag == 1)
                    {
                        quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 1.6f, 0) + quest_mark_target[5].transform.position, quest_mark_target[9].transform.rotation);
                    }
                    questText.text = "Put your cellphone on the basket again!";
                    StartCoroutine(AnimConversion("주머니빼", "security_주머니에있는거빼"));
                    break;
                case 6:
                    Destroy(quest_mark[0]);
                    if (flag == 0)
                    {
                        quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 0.5f, 0) + quest_mark_target[8].transform.position, quest_mark_target[4].transform.rotation);
                    }
                    else if (flag == 1)
                    {
                        quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 0.5f, 0) + quest_mark_target[9].transform.position, quest_mark_target[5].transform.rotation);
                    }
                    questText.text = "Walk through the scanner again!";
                    break;
                case 7:
                    Destroy(quest_mark[0]);
                    quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 1.6f, 0) + quest_mark_target[10].transform.position, quest_mark_target[10].transform.rotation);
                    questText.text = "Go to the duty-free shop and talk to the staff!";
                    break;
                case 8:
                    Destroy(quest_mark[0]);
                    quest_mark[0] = Instantiate(arrow_Prefab, new Vector3(0, 2f, 0) + quest_mark_target[11].transform.position, quest_mark_target[11].transform.rotation);
                    questText.text = "Choose what you want to buy and put it on the counter!";
                    break;
                case 9:
                    //quest_mark[0] = Instantiate(arrow_Prefab, quest_mark_target[11].transform.position, quest_mark_target[11].transform.rotation);
                    //questText.text = "Choose what you want to buy and put it on the counter!";
                    break;
                case 10:
                    Destroy(quest_mark[0]);
                    break;
                case 11: //The end
                    break;
                case 12:
                    Destroy(quest_mark[0]);
                    Destroy(quest_mark[1]);
                    quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 2.0f, 0) + quest_mark_target[12].transform.position, quest_mark_target[13].transform.rotation);
                    quest_mark[1] = Instantiate(quest_Prefab, new Vector3(0, 2.0f, 0) + quest_mark_target[13].transform.position, quest_mark_target[13].transform.rotation);
                    questText.text = "Place your bag up here!";
                    break; 
                case 20:
                    questText.text = "Go to foreigner immigration counter.";
                    quest_mark[0] = Instantiate(quest_Prefab, new Vector3(0, 3.2f, 0) + quest_mark_target[14].transform.position, quest_mark_target[14].transform.rotation);
                    quest_mark[1] = Instantiate(quest_Prefab, new Vector3(0, 3.2f, 0) + quest_mark_target[15].transform.position, quest_mark_target[15].transform.rotation);
                    break;
                case 21:
                    Destroy(quest_mark[0]);
                    Destroy(quest_mark[1]);
                    quest_mark[0] = Instantiate(arrow_Prefab, new Vector3(0, 1.0f, 0) + quest_mark_target[16 + flag].transform.position, quest_mark_target[16 + flag].transform.rotation);
                    questText.text = "Hand your passport to the staff.";
                    break;
                case 22:
                    Destroy(quest_mark[0]);
                    questText.text = "Talk to the staff.";
                    break;
                case 23: //left
                    quest_mark[0] = Instantiate(arrow_Prefab, new Vector3(0, 1.0f, 0) + quest_mark_target[16 + flag].transform.position, quest_mark_target[16 + flag].transform.rotation);
                    questText.text = "Hand your reservation confirmation to the staff.";
                    break;
                case 24: //right
                    quest_mark[0] = Instantiate(arrow_Prefab, new Vector3(0, 1.0f, 0) + quest_mark_target[16 + flag].transform.position, quest_mark_target[16 + flag].transform.rotation);
                    questText.text = "Take your Passport.";
                    break;
                case 25: //left
                    quest_mark[0] = Instantiate(arrow_Prefab, new Vector3(0, 1.0f, 0) + quest_mark_target[16 + flag].transform.position, quest_mark_target[16 + flag].transform.rotation);
                    questText.text = "Take your Passport, reservation confirmation and return ticket.";
                    break;
                case 26:
                    quest_mark[0] = Instantiate(arrow_Prefab, new Vector3(0, 1.0f, 0) + quest_mark_target[16 + flag].transform.position, quest_mark_target[16 + flag].transform.rotation);
                    questText.text = "Hand your return ticket to the staff.";
                    break;
                case 27:
                    Destroy(quest_mark[0]);
                    quest_mark[0] = Instantiate(arrow_Prefab, new Vector3(0, 1.0f, 0) + quest_mark_target[18].transform.position, quest_mark_target[18].transform.rotation);
                    questText.text = "Find and take your backpack at the luggage claim.";
                    break;
                case 28:
                    Destroy(quest_mark[0]);
                    questText.text = "퇴근!";
                    break;
                case 49:
                    GameObject.Find("MixedRealityPlayspace").transform.SetParent(CameraPos);
                    cur_questanim = "Default";
                    questText.text = "Tell the taxi driver you're going to LaLa Hotel.";
                    break;
                case 50:
                    var asd = GameObject.Find("MainCamera").GetComponent<RaycastTargeting>();
                    asd.Conv_With_Dialogflow(taxiDriver);
                    questText.text = "Tell the taxi driver you're going to LaLa Hotel.";
                    cur_questanim = "QuestAppear";
                    break;
                case 51:
                    questText.text = "Ask the taxi driver how long it takes to get there.";
                    break;
                case 52:
                    questText.text = "Ask the taxi driver how much it will price.";
                    break;
                case 53:
                    questText.text = "Feel free to talk to the taxi driver.";
                    break;
                case 54:
                    quest_mark[0] = quest_mark[0] = Instantiate(arrow_Prefab, new Vector3(0, 1.0f, 0) + quest_mark_target[23].transform.position, Quaternion.Euler(new Vector3(0, 90, 0) + quest_mark_target[23].transform.rotation.eulerAngles), quest_mark_target[23].transform);
                    dialogflow.content = "give_card";
                    dialogflow.SendText();
                    questText.text = "Please hand the card over to the taxi driver to pay the fare."; //arrive_hotel
                    break;
                case 55:
                    Destroy(quest_mark[0]);
                    cur_questanim = "Default";
                    break;
                case 56:
                    quest_mark[0] = Instantiate(arrow_Prefab, new Vector3(-0.005f, 0, 0) + quest_mark_target[23].transform.position, quest_mark_target[18].transform.rotation, quest_mark_target[23].transform);
                    dialogflow.content = "return_card";
                    dialogflow.SendText();
                    questText.text = "Please get your card back."; //return_card
                    cur_questanim = "QuestAppear";
                    break;
                case 57:
                    questText.text = "The End."; //arrive_hotel
                    break;
            }
            animator.Play(cur_questanim, -1, 0f);
        }
    }

    void Start()
    {
        time = 0;
        quest_mark = new GameObject[2];
        dialogflow = GameObject.Find("Dialogflow").GetComponent<DF2ClientTester>();
        animator = GameObject.Find("Quest").GetComponent<Animator>();
        questText = animator.transform.Find("QuestContents").GetComponent<TextMeshPro>();


        string name = SceneManager.GetActiveScene().name;

        if (name.Equals("출국"))
        {
            cur_questanim = "QuestAppear";
            _procedure = 0;
        }
        else if (name.Equals("입국"))
        {
            cur_questanim = "QuestAppear";
            _procedure = 20;
        }
        else if (name.Equals("튜토리얼"))
        {
            cur_questanim = "Default";
            _procedure = 30;
            //_procedure = 36;
        }
        else if (name.Equals("택시"))
        {
            cur_questanim = "QuestAppear";
            _procedure = 49;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(SceneManager.GetActiveScene().name == "택시")
        //{
        //    GameObject.Find("MixedRealityPlayspace").transform.SetParent(CameraPos);
        //}

        if (_procedure == 0) //Talking with Adela
        {
            if (GameManager.talking_target != null)
            {
                if (GameManager.talking_target.name == "Adela1")
                {
                    if (GameManager.tmp.text.Contains("place your bag up here"))
                    {
                        _procedure = 1;
                        StartCoroutine(AnimConversion("GoToLine", "GoToLine_Big-3"));
                        _procedure = 12;
                    }
                }
            }
        }
        else if (_procedure == 1) //Security Check - 첫 대화
        {
            if (GameManager.talking_target.name == "Brianna14")
            {
                if(GameManager.tmp.text.Contains("put all your metal objects"))
                {
                    flag = 0;
                    _procedure = 2;
                }
            }
            else if(GameManager.talking_target.name == "Maison12")
            {
                if (GameManager.tmp.text.Contains("put all your metal objects"))
                {
                    flag = 1;
                    _procedure = 2;
                }
            }
        }

        else if(_procedure == 2) // 짐 올려놓기1
        {
            return;
        }
        
        else if(_procedure == 3) // Beyond the yellow line
        {
            return;
        }

        else if(_procedure == 4) // 스캐너 통과
        {
            //StartCoroutine(AnimConversion("팔벌려", "security_checkpoint_팔벌려"));
            return;
        }

        else if (_procedure == 5) // 짐 올려놓기2
        {
            return;
        }
        else if(_procedure == 22)
        {
            if (GameManager.tmp.text.Contains("Have a nice trip!"))
            {
                _procedure = 24;
            }
            else if (GameManager.tmp.text.Contains("Give me your reservation confirmation."))
            {
                _procedure = 23;
            }
            else if (GameManager.tmp.text.Contains("Do you have a return ticket?"))
            {
                _procedure = 26;
            }
            else if (GameManager.tmp.text.Contains("Take your stuff. bye."))
            {
                _procedure = 25;
            }
        }

        else if (_procedure == 33) // 짐 올려놓기2
        {
            var audio = Tori.GetComponent<AudioSource>();
            if (!audio.isPlaying && audio.clip != null)
                _procedure = 34;
            return;
        }
        else if (_procedure == 34) // 짐 올려놓기2
        {
            if (GameManager.tmp.text.Contains("how to give and receive things"))
            {
                _procedure = 35;
            }
            return;
        }
        else if (_procedure == 35) // 짐 올려놓기2
        {
            time += Time.deltaTime;
            var audio = Tori.GetComponent<AudioSource>();
            if (!audio.isPlaying && audio.clip != null && time > 1)
            {
                time = 0;
                _procedure = 36;
            }
            return;
        }

        else if (_procedure == 36) // 짐 올려놓기2
        {
            if (GameManager.tmp.text.Contains("press the same button to get"))
            {
                _procedure = 37;
            }
            return;
        }
        else if (_procedure == 37) // 짐 올려놓기2
        {
            time += Time.deltaTime;
            var audio = Tori.GetComponent<AudioSource>();
            if (!audio.isPlaying && audio.clip != null && time > 1)
            {
                time = 0;
                _procedure = 38;
            }
            return;
        }
        else if (_procedure == 38) // 짐 올려놓기2
        {
            if (GameManager.tmp.text.Contains("move the box on that table to this table"))
            {
                _procedure = 39;
            }
            return;
        }
        else if (_procedure == 39) // 짐 올려놓기2
        {
            time += Time.deltaTime;
            var audio = Tori.GetComponent<AudioSource>();
            if (!audio.isPlaying && audio.clip != null && time > 1)
            {
                time = 0;
                _procedure = 40;
            }
            return;
        }
        else if (_procedure == 49) // 짐 올려놓기2
        {
            time += Time.deltaTime;
            if (time > 0.5f)
            {
                time = 0;
                _procedure = 50;
            }
            return;
        }
        else if (_procedure == 50) // 짐 올려놓기2
        {
            if (GameManager.tmp.text.Contains("Okay, I'll take you safe."))
            {
                _procedure = 51;
            }
            return;
        }
        else if (_procedure == 51) // 짐 올려놓기2
        {
            if (GameManager.tmp.text.Contains("About 30 minutes. Don't worry."))
            {
                _procedure = 52;
            }
            return;
        }
        else if (_procedure == 52) // 짐 올려놓기2
        {
            if (GameManager.tmp.text.Contains("You can only pay by card."))
            {
                time += Time.deltaTime;
                var audio = Leo.GetComponent<AudioSource>();
                if (!audio.isPlaying && audio.clip != null && time > 1)
                {
                    time = 0;
                    dialogflow.content = "blahblah_1";
                    dialogflow.SendText();
                    _procedure = 53;

                }
            }
            return;
        }
        else if (_procedure == 53) // 자유로운 대화
        {
            time += Time.deltaTime;
            if (time > 10f) //120초 해야됨!
            {
                time = 0;
                _procedure = 54;
            }
            return;
        }
        else if (_procedure == 54) //  카드 주는 Interaction
        {
            return;
        }
        else if (_procedure == 55) // 잠시대기
        {
            time += Time.deltaTime;
            if (time > 2f)
            {
                time = 0;
                _procedure = 56;
            }
            return;
        }
        else if (_procedure == 56) // 카드 돌려받는 Interaction
        {
            return;
        }
        else if (_procedure == 57) // 카드 돌려받는 Interaction
        {

            return;
        }
    }

    public void Interaction_Basket()
    {
        if(_procedure == 2)
        {
            if(flag == 0)
            {
                luggage = GameObject.Find("Basket_Left").transform.Find("MacBook_Left").gameObject;
            }
            else if(flag == 1)
            {
                luggage = GameObject.Find("Basket_Right").transform.Find("MacBook_Right").gameObject;
            }

            luggage.SetActive(true);
            _procedure = 3;

            dialogflow.content = "beyond_yellowline";
            dialogflow.SendText();
        }
        else if(_procedure == 5)
        {
            if (flag == 0)
            {
                luggage = GameObject.Find("Basket_Left").transform.Find("Phone_Left").gameObject;
            }
            else if (flag == 1)
            {
                luggage = GameObject.Find("Basket_Right").transform.Find("Phone_Right").gameObject;
            }

            luggage.SetActive(true);
            _procedure = 6;

            dialogflow.content = "WalkThroughScanner_Again";
            dialogflow.SendText();
        }
    }

    public static IEnumerator AnimConversion(string param_name, string anim_name)
    {
        GameManager.animator.SetBool(param_name, true);
        yield return null;

        while (!GameManager.animator.GetCurrentAnimatorStateInfo(0).IsName(anim_name))
            yield return null;
        while (GameManager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;

        GameManager.animator.SetBool(param_name, false);
    }

    IEnumerator QuestTextChange(float time, string[] content)
    {
        foreach(string str in content)
        {
            questText.text = str;
            yield return new WaitForSeconds(time);
        }
        _procedure += 1;
        yield return null;
    }
}
