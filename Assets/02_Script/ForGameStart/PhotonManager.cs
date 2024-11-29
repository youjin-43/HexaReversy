//포톤 네트워크를 이용하기 위해 아래 두개가 필수 
using Photon.Pun; // Pun : 포톤 유니티 네트워크의 약자
using Photon.Realtime; // 실시간 통신? 을 위해서

using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
   
    public TextMeshProUGUI userID;
    public GameStartManager GSM;

    //현재 서버 상태 확인 
    //public TextMeshProUGUI ConnectionStatus;
    //void Update()
    //{
    //    ConnectionStatus.text = PhotonNetwork.NetworkClientState.ToString();
    //}


    //Connect 버튼에 연결 
    public void Connect()
    {
        //지역 kr로 고정.이 부분이 없으면 자동으로 지역을 찾는데,다른 지역에 걸릴 경우 네트워크를 통해 만날 수 없다.
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "kr";

        PhotonNetwork.AutomaticallySyncScene = true; //이 값이 true일 때 MasterClient는 PhotonNetwork.LoadLevel()을 호출 할 수 있고 모든 연결된 플레이어들은 동일한 레벨(씬)을 자동적으로 로드
        //PhotonNetwork.AutomaticallySyncScene = false;

        // 같은 버전의 유저끼리 접속 허용. 다른 버전 유저 차단
        //PhotonNetwork.GameVersion = version;

        //플레이어가 네트워크에서 사용할 닉네임을 설정. 서버로 보낼 나의 아이디. 
        //TODO : 이거 나중에 게임 매니저 만들어서 바꿔야할듯 
        PhotonNetwork.NickName = userID.text;

        // 포톤 서버와 통신 횟수 확인. 초당 30회. 30이 떠야 정상
        Debug.Log("포톤 서버와 통신 횟수 확인 : " + PhotonNetwork.SendRate);

        PhotonNetwork.ConnectUsingSettings(); // 세팅한걸로 커넥트. PhotonNetwork를 실제 연결하며 서버 접속. 이거 하면 OnConnectedToMaster()가 호출됨
    }

    // 포톤 서버에 접속 후 호출되는 콜백 함수
    public override void OnConnectedToMaster()
    {
        print("서버 접속 완료");
        // 로비 입장했는지 확인, False. 연결과 로비 입장은 다르기 때문에 False 가 정상.
        Debug.Log($"로비 입장 여부 = {PhotonNetwork.InLobby}");

        //TODO : 로컬 닉네임이랑 그냥 닉네임이랑 차이가 뭐지?? 
        //PhotonNetwork.LocalPlayer.NickName = userID.text;

        PhotonNetwork.JoinLobby(); //서버에 입장한 후 바로 로비로 입장 
    }

    //로비에 접속 후 호출되는 콜백 함수 
    public override void OnJoinedLobby()
    {
        // 로비 입장했는지 확인, True
        Debug.Log($"로비 입장 여부 = {PhotonNetwork.InLobby}");

        // 생성되어 있는 룸 중에서 랜덤하게 입장
        PhotonNetwork.JoinRandomRoom(); // TODO : 나중에는 내가 의도하는 곳으로

    }

    // 랜덤 룸 입장에 실패했을 떄 호출되는 콜백 함수. 랜덤 룸 입장에 실패할 경우엔 내가 직접 방을 만든다.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // 에러 메세지 출력
        Debug.Log($"랜덤 룸 입장 실패 {returnCode}:{message}");

        // 룸 속성 정의
        RoomOptions ro = new RoomOptions(); //ro 는 RoomOption
        ro.MaxPlayers = 2;      // 최대 동접자 수:2. 포톤이 20명까지 지원하지만 2인 대전 게임이니까 
        ro.IsOpen = true;        // 룸의 오픈 여부
        ro.IsVisible = true;     // 로비에서 룸 목록 노출 여부

        // 룸 생성
        //TODO : 닉네임 중복되지 않도록 처리해줘야함 
        //방의 이름이 중복되지 않도록 방 이름에 플레이어의 이름을 붙여 생성하도록 했다. 이름이 중복될 경우 방이 생성되지 않는다.
        PhotonNetwork.CreateRoom("My Room " + PhotonNetwork.NickName, ro); // 이름과 옵션(위에서 설정한)
    }

   
    // 룸 생성이 완료된 후 호출되는 콜백 함수
    public override void OnCreatedRoom()
    {
        Debug.Log("룸 생성");
        Debug.Log($"룸 이름 = {PhotonNetwork.CurrentRoom.Name}");
    }

    // 룸에 입장한 후 호출되는 콜백 함수
    public override void OnJoinedRoom()
    {
        //내가 방장인지 확인
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("내가 방장이다!!!");
        }
        else
        {
            Debug.Log("나는 클라이언트다!!");
        }
    

        Debug.Log($"룸 입장 여부 = {PhotonNetwork.InRoom}");
        Debug.Log($"현재 룸의 인원수 = {PhotonNetwork.CurrentRoom.PlayerCount}");


        //for 문 처럼 Player 마다 실행 
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName}, {player.Value.ActorNumber}"); //ActorNumber:몇번째로 들어왔냐
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            Debug.Log("현재 인원수: " + PhotonNetwork.CurrentRoom.PlayerCount);

            //룸 닫고 
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            Debug.Log("현재 방 오픈 여부: " + PhotonNetwork.CurrentRoom.IsOpen);

            Debug.Log("Game Start!");

            Invoke("SetFindedText", 3f);
            //TODO : Finded! -> 아니 근데 이거 체감상 6초가 아닌것 같은데 착각인가; 
            Invoke("GameStart", 4f); //게임 씬으로 이동 
           
        }
    }

    //Invoke에서 실행하려고 그냥 이렇게 만들음
    void SetFindedText()
    {
        GSM.SetFindedText();
    }
    void GameStart()
    {
        GSM.GameStart();//씬 이동 
    }
}
