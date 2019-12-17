using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Chat_System : MonoBehaviour
{
    [SerializeField] private Text text_charater_name; //캐릭터 네임
    [SerializeField] private Text text_chat_dialogue; //대화 내용
    [SerializeField] private Text text_chapter; //챕터 표시
    [SerializeField] private Text text_episode; //에피소드 표시

    [SerializeField] private float appear_text_value; //첫 텍스트 대기 시간
    [SerializeField] private float repeat_text_value; //텍스트 타이핑 시간

    private int chapter_value;
    private int episode_value;
    private string charater_name;
    private string dialog_text;

    private StringBuilder dialog_stringbuilder;

    private int read_text_value; //현재 읽은 텍스트 길이 값

    private void Awake()
    {
        CSV_Reader.Instance.GetCSVData("prologue.csv");
        dialog_stringbuilder = new StringBuilder();
        setting();
    }

    //이름과 채팅 내용 및 에피소드 등을 출력한다.
    private void setDialog(string _name, string _text, int _chapter, int _episode)
    {
        this.text_charater_name.text = _name;
        this.text_chapter.text = "Chapter " + _chapter.ToString();
        this.text_episode.text = "Episode " + _episode.ToString();
        InvokeRepeating("renewalText", appear_text_value , repeat_text_value);
    }

    public void DialogSkip()
    {
        //채팅로그가 쳐지는 도중이라면 캔슬되도록.
        if (IsInvoking("renewalText"))
        {
            CancelInvoke("renewalText");
            this.text_chat_dialogue.text = dialog_text;
        }
        else
        {
            CancelInvoke("renewalText");
            this.text_chat_dialogue.text = "";
            //stringbullder 비우기
            dialog_stringbuilder.Remove(0, this.read_text_value);
            dialog_stringbuilder.Clear();

            read_text_value = 0;
            setting();
        }
        
    }

    private void setting()
    {
        //데이터를 가져온다.
        CSV_Reader.Instance.GetDialogText(ref this.chapter_value, ref this.episode_value, ref this.charater_name, ref dialog_text);
        setDialog(charater_name, dialog_text, chapter_value, episode_value);
    }

    //텍스트 갱신
    private void renewalText()
    {
        //현재 텍스트가 전체 출력이 덜됬을경우 
        if (read_text_value < dialog_text.Length)
        {
            dialog_stringbuilder.Append(dialog_text[read_text_value++]);
            this.text_chat_dialogue.text = dialog_stringbuilder.ToString();
        }
        //전체 출력됐으면 멈추도록 설정
        else
        {
            CancelInvoke("renewalText");
        }
    }
    

}
