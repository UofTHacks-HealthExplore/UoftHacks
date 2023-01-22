using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.Networking;

using System.Threading.Tasks;
using TMPro;
public class FuckYou : MonoBehaviour
{
    public enum Type {GetResponse, GenerateResponse, GetQuestions, SendResponse};
    private string baseURL = "https://hear-with-capital-a-and-r.herokuapp.com/";
    public string curText;

    public Text translatedDisplay;
    public TMP_Text transcriptText;
    bool running;

    bool send;
    public string response;

    public string q;
    public List<string> r;

    private void Update()
    {
        // transform.position = receivedPos; //assigning receivedPos in SendAndReceiveData()
        // translatedDisplay.text = curText;
        // print(text);
    }

    private void Start()
    {
        
    }

    string ConvertURL(string s)
    {
        string newS = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == ' ')
            {
                newS += "%20";
            }
            else if (s[i].ToString() == "'")
            {
                newS += "%27";
            }
            else
            {
                newS += s[i];
            }
        }
        return newS;
    }

    // void Main()
    // {
    //     RunAsync().Wait();
    // }

    IEnumerator GetRequest(string uri, Type type, int num)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            print(webRequest.downloadHandler.text);
            response = webRequest.downloadHandler.text;
            // translatedDisplay.text += response;

            if (type == Type.GetQuestions && num <= 5){
                r.Add(response);
                GetRequest(baseURL + "responses", Type.GetQuestions, num + 1);
            }
            else if (type == Type.GetResponse){
                q = response;
            }
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }


    public void GenerateResponse(string text){
        string url = baseURL + "gen_response";
        StartCoroutine(GetRequest(url, Type.GenerateResponse, 0));
    }

    public void GetQuestions(){
        r = new List<string>();
        string url = baseURL + "responses/1";
        StartCoroutine(GetRequest(url, Type.GetQuestions, 1));
    }

    public void GetResponse(){
        string url = baseURL + "question";
        StartCoroutine(GetRequest(url, Type.GetResponse, 0));
    }

    public void SendResponse(int choice){
        StartCoroutine(GetRequest(baseURL + "responses/" + choice +"/send", Type.SendResponse, 0));
    }
}


