using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Networking;
using Firebase.Storage;
using Firebase.Extensions;
using System.Windows;
using System.Threading;

public class QuestionScript : MonoBehaviour
{
    public RawImage rawImage;
    public Image QuestionImage, QuestionTextImage;
    FirebaseStorage storage;
    StorageReference storageReference;
    float cntdnw;
    int Count_correct;
    float timer = 0;
    float timer2 = 0;
    public bool click = false;
    public double b = 60;
    public static int scoreCounter;
    public static int counter;
    DatabaseReference reference;
    public static bool isPaused;
    public GameObject QuestionMenu;
    public Text Load, LoadObrazok, AnswerA,  AnswerB, AnswerC, AnswerD, AnswerAObrazok, AnswerBObrazok, AnswerCObrazok, AnswerDObrazok, AnswerStatus, Score, Number_correct, count_question, Definition, ImageText;
    public String CheckAnswerA, CheckAnswerB, CheckAnswerC, CheckAnswerD, QuestionCount, odpoved;
    public Text Spravne, Nespravne;
    public int nespravneCislo = 0;
    public GameObject Green, Green2, Green3, Green4, Red, Red2, Red3, Red4,Green5, Green6, Green7, Green8, Red5, Red6, Red7, Red8;
    public Sprite GreenRectangle, RedRectangle;

    public Button AnswerAbtn, AnswerBbtn, AnswerCbtn, AnswerDbtn, AnswerAbtnObrazok, AnswerBbtnObrazok, AnswerCbtnObrazok, AnswerDbtnObrazok;



    //neche sa  mi to studovat -1 nie 0 nespravne 1 sprave
    private short odpovedal=-1;
    public bool prisla_otazka=false;
    
   

    // Start is called before the first frame update
    void Start()
    {
     
        Load.gameObject.SetActive(false);
        QuestionTextImage.gameObject.SetActive(false);
        LoadObrazok.gameObject.SetActive(false);
        QuestionImage.gameObject.SetActive(false);
        Green.SetActive(false);
        Green2.SetActive(false);
        Green3.SetActive(false);
        Green4.SetActive(false);
        Red.SetActive(false);
        Red4.SetActive(false);
        Red2.SetActive(false);
        Red3.SetActive(false);
        Green5.SetActive(false);
        Green6.SetActive(false);
        Green7.SetActive(false);
        Green8.SetActive(false);
        Red5.SetActive(false);
        Red6.SetActive(false);
        Red7.SetActive(false);
        Red8.SetActive(false);
        AnswerAbtn.gameObject.SetActive(false);
        AnswerBbtn.gameObject.SetActive(false);
        AnswerCbtn.gameObject.SetActive(false);
        AnswerDbtn.gameObject.SetActive(false);

        AnswerAbtnObrazok.gameObject.SetActive(false);
        AnswerBbtnObrazok.gameObject.SetActive(false);
        AnswerCbtnObrazok.gameObject.SetActive(false);
        AnswerDbtnObrazok.gameObject.SetActive(false);
        AnswerAbtn.onClick.AddListener(() => ButtonClicked(1));
        AnswerBbtn.onClick.AddListener(() => ButtonClicked(2));
        AnswerCbtn.onClick.AddListener(() => ButtonClicked(3));
        AnswerDbtn.onClick.AddListener(() => ButtonClicked(4));

        AnswerAbtnObrazok.onClick.AddListener(() => ButtonClicked(1));
        AnswerBbtnObrazok.onClick.AddListener(() => ButtonClicked(2));
        AnswerCbtnObrazok.onClick.AddListener(() => ButtonClicked(3));
        AnswerDbtnObrazok.onClick.AddListener(() => ButtonClicked(4));

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        counter = 0;
        scoreCounter = 0;
        QuestionMenu.SetActive(false);
        Definition.enabled = false;
        ImageText.enabled = false;
        Debug.Log(MainMenu.code);

       //  StartCoroutine(LoadImage("https://i.pinimg.com/236x/ab/9e/74/ab9e740d9285ac0122acb96a82621899.jpg")); //Fetch file from the link


    }

    void OdpovedStatus(int A, int B, int C, int D,string odpoved)
    {
        {

            int good = 0;
            int bad = 0;
            Debug.Log(odpoved);
          
            Debug.Log(Definition.text);
            if (Definition.text !="New Text")
            {

                Definition.enabled = true;
            }
        
        
            if (odpoved.Contains("A") && A == 1)
            {
                good++;
            }
            else if (odpoved.Contains("A") && A == 0)
            {
                bad++;
            }
            if (odpoved.Contains("B") && B == 1)
            {
                good++;
            }
            else if (odpoved.Contains("B") && B == 0)
            {
                bad++;
            }
            if (odpoved.Contains("C") && C == 1)
            {
                good++;
            }
            else if (odpoved.Contains("C") && C == 0)
            {
                bad++;
            }
            if (odpoved.Contains("D") && D == 1)
            {
                good++;
            }
            else if (odpoved.Contains("D") && D == 0)
            {
                bad++;
            }
            Debug.Log(bad.ToString() + " " + good.ToString());


            if (bad==0 && good>0)
            {
                Debug.Log(bad + " " + good);
                AnswerStatus.text = "Spr�vna odpove�!";
                AnswerStatus.color = Color.green;          
                Definition.color = Color.green;
                scoreCounter++;
                Score.text = scoreCounter.ToString();






                //GUI.Label(new Rect(5, 20, 80, 100), "DOBRA ODPOVED");
              
                    Spravne.text = scoreCounter.ToString();

                

                //nechce sa mi to studovat

                odpovedal =1;
                QuestionMenu.SetActive(false);
            
            }
            else {
                AnswerStatus.text = "Nespr�vna odpove�!";
                AnswerStatus.color = Color.red;

                Definition.color = Color.red;
                //  GUI.Label(new Rect(5, 20, 80, 100), "ZLA ODPOVED");
                nespravneCislo++;
                Nespravne.text = nespravneCislo.ToString();


                //nechce sa mi to studovat

                odpovedal =0;
                 QuestionMenu.SetActive(false);
            
            }
        }
     
        // Time.timeScale = 0f;

    }

    public int get_odpoved()
    {
      
           
        
        return odpovedal;
    }

    public void reset()
    {
        
        Definition.enabled = false;
   
        AnswerStatus.enabled = false;


        odpovedal=-1;
        QuestionMenu.SetActive(true);
    }

  

    public IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(5f);

        // Code to execute after the delay
    }
    public void loadData() {
        FirebaseDatabase.DefaultInstance.GetReference(MainMenu.code).ValueChanged += QuestionScript_ValueChanged;
        QuestionMenu.SetActive(true);

        // Time.timeScale = 0f;
    }

    private void Counter_Correct_Answers(int cislo)
    {
        if (cislo == 1)
        {
            Count_correct++;
            Debug.Log(Count_correct.ToString());
        }
    }
    IEnumerator LoadImage(string MediaUrl)
    {
        
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl); //Create a request
            yield return request.SendWebRequest(); //Wait for the request to complete
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                rawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                // setting the loaded image to our object
            }
       
    }

    public void ButtonClicked(int buttonNo)
    {

        Time.timeScale = 0;
        if (CheckAnswerA == "1" && buttonNo == 1)
        {
            if (AnswerAbtn.gameObject.activeSelf)
            {
                AnswerAbtn.GetComponent<Button>().image.sprite = GreenRectangle;
            Green2.SetActive(true);
        }
        else
        {
            AnswerAbtnObrazok.GetComponent<Button>().image.sprite = GreenRectangle;
            Green6.SetActive(true);
        }
        odpovedal = 1;
            scoreCounter++;
            Spravne.text = scoreCounter.ToString();
        }
        else if (CheckAnswerB == "1" && buttonNo == 2)
        {
            if (AnswerBbtn.gameObject.activeSelf)
            {
                AnswerBbtn.GetComponent<Button>().image.sprite = GreenRectangle;
            Green3.SetActive(true);
        }
        else
        {
            AnswerBbtnObrazok.GetComponent<Button>().image.sprite = GreenRectangle;
            Green7.SetActive(true);
        }
        odpovedal = 1;
            scoreCounter++;
            Spravne.text = scoreCounter.ToString();
        }
        else if (CheckAnswerC == "1" && buttonNo == 3)
        {
            if (AnswerCbtn.gameObject.activeSelf)
            {
                AnswerCbtn.GetComponent<Button>().image.sprite = GreenRectangle;
            Green.SetActive(true);
        }
        else
        {
            AnswerCbtnObrazok.GetComponent<Button>().image.sprite = GreenRectangle;
            Green5.SetActive(true);
        }
        odpovedal = 1;
            scoreCounter++;
            Spravne.text = scoreCounter.ToString();

        }
        else if (CheckAnswerD == "1" && buttonNo == 4)
        {
            if (AnswerAbtn.gameObject.activeSelf)
            {
                AnswerDbtn.GetComponent<Button>().image.sprite = GreenRectangle;
            Green4.SetActive(true);
        }
        else
        {
            AnswerDbtnObrazok.GetComponent<Button>().image.sprite = GreenRectangle;
            Green8.SetActive(true);
        }
        odpovedal = 1;
            scoreCounter++;
            Spravne.text = scoreCounter.ToString();

        }
        else if (CheckAnswerA == "0" && buttonNo == 1)
        {
            if (AnswerAbtn.gameObject.activeSelf)
            {
                AnswerAbtn.GetComponent<Button>().image.sprite = RedRectangle;
            Red.SetActive(true);
        }
        else
        {
            AnswerAbtnObrazok.GetComponent<Button>().image.sprite = RedRectangle;
            Red5.SetActive(true);
        }
        odpovedal = 0;
            nespravneCislo++;
            Nespravne.text = nespravneCislo.ToString();
        }
        else if (CheckAnswerB == "0" && buttonNo == 2)
        {
            if (AnswerBbtn.gameObject.activeSelf)
            {
                AnswerBbtn.GetComponent<Button>().image.sprite = RedRectangle;
                Red4.SetActive(true);
            }
            else {
                AnswerBbtnObrazok.GetComponent<Button>().image.sprite = RedRectangle;
                Red8.SetActive(true);
            }
           
            odpovedal = 0;
            nespravneCislo++;
            Nespravne.text = nespravneCislo.ToString();

        }
        else if (CheckAnswerC == "0" && buttonNo == 3)
        {
            if (AnswerCbtn.gameObject.activeSelf)
            {
                AnswerCbtn.GetComponent<Button>().image.sprite = RedRectangle;
            Red2.SetActive(true);
        }
        else
        {
            AnswerCbtnObrazok.GetComponent<Button>().image.sprite = RedRectangle;
            Red6.SetActive(true);
        }
        odpovedal = 0;
            nespravneCislo++;
            Nespravne.text = nespravneCislo.ToString();

        }
        else if (CheckAnswerD == "0" && buttonNo == 4)
        {
            if (AnswerDbtn.gameObject.activeSelf)
            {
                AnswerDbtn.GetComponent<Button>().image.sprite = RedRectangle;
            Red3.SetActive(true);
        }
        else
        {
            AnswerDbtnObrazok.GetComponent<Button>().image.sprite = RedRectangle;
            Red7.SetActive(true);
        }
        odpovedal = 0;
            nespravneCislo++;
            Nespravne.text = nespravneCislo.ToString();

        }


        Score.text = scoreCounter.ToString();



        
        StartCoroutine(ResumeAfterNSeconds(3.0f));
        //



    }

    float timer5 = 0;
    IEnumerator ResumeAfterNSeconds(float timePeriod)
    {
        yield return new WaitForEndOfFrame();
        timer5 += Time.unscaledDeltaTime;
        if (timer5 < timePeriod)
            StartCoroutine(ResumeAfterNSeconds(3.0f));
        else
        {
            Time.timeScale = 1;                //Resume
            timer5 = 0;
            QuestionMenu.SetActive(false);
        }
    }


    private void QuestionScript_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        rawImage.color = Color.black;
        QuestionCount = e.Snapshot.Child("otazka").ChildrenCount.ToString();
        Load.text = e.Snapshot.Child("otazka").Child((counter-1).ToString()).GetValue(true).ToString();
        LoadObrazok.text = e.Snapshot.Child("otazka").Child((counter - 1).ToString()).GetValue(true).ToString();
        AnswerA.text = e.Snapshot.Child("odpovedA").Child((counter - 1).ToString()).GetValue(true).ToString();
        AnswerB.text = e.Snapshot.Child("odpovedB").Child((counter - 1).ToString()).GetValue(true).ToString();
        AnswerC.text = e.Snapshot.Child("odpovedC").Child((counter - 1).ToString()).GetValue(true).ToString();
        AnswerD.text = e.Snapshot.Child("odpovedD").Child((counter - 1).ToString()).GetValue(true).ToString();
        AnswerAObrazok.text = e.Snapshot.Child("odpovedA").Child((counter - 1).ToString()).GetValue(true).ToString();
        AnswerBObrazok.text = e.Snapshot.Child("odpovedB").Child((counter - 1).ToString()).GetValue(true).ToString();
        AnswerCObrazok.text = e.Snapshot.Child("odpovedC").Child((counter - 1).ToString()).GetValue(true).ToString();
        AnswerDObrazok.text = e.Snapshot.Child("odpovedD").Child((counter - 1).ToString()).GetValue(true).ToString();
        CheckAnswerA = e.Snapshot.Child("odpovedASpravnost").Child((counter - 1).ToString()).GetValue(true).ToString();
        CheckAnswerB = e.Snapshot.Child("odpovedBSpravnost").Child((counter - 1).ToString()).GetValue(true).ToString();
        CheckAnswerC = e.Snapshot.Child("odpovedCSpravnost").Child((counter - 1).ToString()).GetValue(true).ToString();
        CheckAnswerD = e.Snapshot.Child("odpovedDSpravnost").Child((counter - 1).ToString()).GetValue(true).ToString();
        if (e.Snapshot.Child("teoria").Child((counter - 1).ToString()).Exists)
        {
            
            Definition.text = e.Snapshot.Child("teoria").Child((counter - 1).ToString()).GetValue(true).ToString();
        }

        if (e.Snapshot.Child("image").Child((counter - 1).ToString()).Exists)
        {

            LoadObrazok.gameObject.SetActive(true);
            QuestionImage.gameObject.SetActive(true);

            AnswerAbtnObrazok.gameObject.SetActive(true);
            AnswerBbtnObrazok.gameObject.SetActive(true);
            AnswerCbtnObrazok.gameObject.SetActive(true);
            AnswerDbtnObrazok.gameObject.SetActive(true);

            ImageText.text = e.Snapshot.Child("image").Child((counter - 1).ToString()).GetValue(true).ToString();
            ImageText.enabled = true;
            rawImage.color = Color.white;
            StartCoroutine(LoadImage("https://firebasestorage.googleapis.com/v0/b/last-city-8afa4.appspot.com/o/images%2F" + e.Snapshot.Child("image").Child((counter - 1).ToString()).GetValue(true).ToString() + "?alt=media"));
        }
        else {
            Load.gameObject.SetActive(true);
            QuestionTextImage.gameObject.SetActive(true);
            AnswerAbtn.gameObject.SetActive(true);
            AnswerBbtn.gameObject.SetActive(true);
            AnswerCbtn.gameObject.SetActive(true);
            AnswerDbtn.gameObject.SetActive(true);
        }
       

        cntdnw = 60.0f;
        Counter_Correct_Answers(Int32.Parse(CheckAnswerA));
        Counter_Correct_Answers(Int32.Parse(CheckAnswerB));
        Counter_Correct_Answers(Int32.Parse(CheckAnswerC));
        Counter_Correct_Answers(Int32.Parse(CheckAnswerD));
      //  Number_correct.text = Count_correct.ToString();
        count_question.text = "Otázka "+counter.ToString() + " / " + QuestionCount;
        Debug.Log("Load Success" + counter.ToString());
    }

    // Update is called once per frame

    public void render_otazka()
    {
        Definition.enabled = false;  
        loadData();
        counter++;
        Count_correct = 0;
        timer = 0;
        ImageText.enabled = false;
    }

    void Update()
    {
        if(odpovedal==-1)
        {
           
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OdpovedStatus(Int32.Parse(CheckAnswerA), Int32.Parse(CheckAnswerB), Int32.Parse(CheckAnswerC), Int32.Parse(CheckAnswerD),odpoved);
            }
            if (cntdnw > 0)
            {
                cntdnw -= Time.deltaTime;
            }

           
                 b = System.Math.Round(cntdnw, 0);
             
            
            if (cntdnw < 0)
            {

               
                Debug.Log("Completed");
            }

              
            




            timer += Time.deltaTime;

        
            if ( timer > 10)
            {
                
                prisla_otazka=true;

                /*Definition.enabled = false;

            
                loadData();
                counter++;
                Count_correct = 0;
                timer = 0;
                ImageText.enabled = false;*/

            }
            
        }
        else
        {
            prisla_otazka=false;
        }
    }
       
}
