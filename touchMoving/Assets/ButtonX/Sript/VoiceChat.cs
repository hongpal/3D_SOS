using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceChat : MonoBehaviour {

    public string microphone;//마이크 이름

    private List<string> allMicrophones = new List<string>();//마이크들 저장
    private AudioSource audioSource;

    void Start()
    {
        //오디오 소스
        audioSource = GetComponent<AudioSource>();

        //사용가능한 마이크들 찾기
        for (int i = 0; i < Microphone.devices.Length; i++)
        {
            if (microphone == null)//첫번째 마이크를 기본마이크로 설정
            {
                microphone = Microphone.devices[i];
            }
            allMicrophones.Add(Microphone.devices[i]);
        }

        StartMicrophone();
    }

    void StartMicrophone()//마이크 시작
    {
        audioSource.Stop();

        //녹음 시작 (마이크 이름, 루프여부, 샘플레이트)
        audioSource.clip = Microphone.Start(microphone, true, 10, 44100);
        audioSource.loop = true;

        Debug.Log(Microphone.IsRecording(microphone).ToString());

        if (Microphone.IsRecording(microphone))
        { //마이크가 녹음하고 있는지 확인 (될때까지 루프)
            while (!(Microphone.GetPosition(microphone) > 0)){ }

            Debug.Log("녹음 시작 : " + microphone);
            audioSource.Play();
        }
        else
        {
            Debug.Log("녹음 실패 : " + microphone);
        }
    }
}
