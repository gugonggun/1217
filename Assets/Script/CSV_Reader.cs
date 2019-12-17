using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSV_Reader : Singleton<CSV_Reader>
{
    private string[] text_cols; //가로행 텍스트
    private string[] text_rows; //세로행 텍스트
    [SerializeField] private TextAsset text_asset;
    private List<Dialog> list_dialog;
    private int count;

    //각 대화의 아이디, 챕터, 에피소드 ,이름 ,대화등을 담는 클래스 
    private class Dialog
    {
        private string id; 
        private int chapter;
        private int episode;
        private string name;
        private string text;

        //각 데이터 반환형
        public int Chapter
        {
            get { return chapter; }
        }

        public int Episode
        {
            get { return episode; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Text
        {
            get { return text; }
        }

        //데이터 셋팅
        public void SetInit(string _id, int _chapter, int _episode, string _name, string _text)
        {
            this.id = _id;
            this.chapter = _chapter;
            this.episode = _episode;
            this.name = _name;
            this.text = _text;
        }
    }

    //CSV데이터 읽어오기
    public void GetCSVData(string _path)
    {
        text_cols = text_asset.text.Split('\n');
        list_dialog = new List<Dialog>();
        for (int i = 1; i < text_cols.Length - 1; i++)
        {
            text_rows = text_cols[i].Split(';');
            Dialog dummy = new Dialog();
            dummy.SetInit(text_rows[0], StringChangeInt(text_rows[1]), StringChangeInt(text_rows[2]), text_rows[3], text_rows[4] + text_rows[5] + text_rows[6]);
            list_dialog.Add(dummy);
        }
    }

    //대화창 텍스트 반환
    //굳이 외부 클래스가 안에 담긴 내용을 알필요 없게끔 데이터만 반환한다.
    public void GetDialogText(ref int _chapter, ref int _episode, ref string _name, ref string _text)
    {
        _chapter = this.list_dialog[count].Chapter;
        _episode = this.list_dialog[count].Episode;
        _name = this.list_dialog[count].Name;
        _text = this.list_dialog[count].Text;
        count++;
    }

    //String to Int 
    private int StringChangeInt(string _string)
    {
        return int.Parse(_string);
    }
}
