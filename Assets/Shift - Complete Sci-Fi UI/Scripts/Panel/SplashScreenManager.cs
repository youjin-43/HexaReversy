using UnityEngine;

namespace Michsky.UI.Shift
{
    public class SplashScreenManager : MonoBehaviour
    {
        [Header("Resources")]
        public GameObject splashScreen;
        public CanvasGroup Loading;


        private Animator splashScreenAnimator;

        private TimedEvent ssTimedEvent;

        [Header("Settings")]
        public bool disableSplashScreen;
        public bool enablePressAnyKeyScreen;
        public bool enableLoginScreen;
        public bool showOnlyOnce = true;

        void OnEnable()
        {
            if (showOnlyOnce && GameObject.Find("[Shift UI - Splash Screen Helper]") != null) { disableSplashScreen = true; }
            if (splashScreenAnimator == null) { splashScreenAnimator = splashScreen.GetComponent<Animator>(); }
            if (ssTimedEvent == null) { ssTimedEvent = splashScreen.GetComponent<TimedEvent>(); }


            if (enableLoginScreen == true && enablePressAnyKeyScreen == true && disableSplashScreen == false)
            {
                splashScreen.SetActive(true);
            }

            if (showOnlyOnce == true && disableSplashScreen == false)
            {
                GameObject tempHelper = new GameObject();
                tempHelper.name = "[Shift UI - Splash Screen Helper]";
                DontDestroyOnLoad(tempHelper);
            }
        }

        public void LoginScreenCheck()
        {
            Debug.Log("LoginScreenCheck실행됨 ");
            if (enableLoginScreen == true && enablePressAnyKeyScreen == true)
                splashScreenAnimator.Play("Press Any Key to Login");

            else if (enableLoginScreen == false && enablePressAnyKeyScreen == true)
            {
                splashScreenAnimator.Play("Press Any Key to Loading");
                ssTimedEvent.StartIEnumerator();
            }

            else if (enableLoginScreen == false && enablePressAnyKeyScreen == false)
            {
                splashScreenAnimator.Play("Loading");
                ssTimedEvent.StartIEnumerator();
            }
        }

        private void Update()
        {
            if (Loading.alpha == 1)
            {
                Debug.Log("상대 찾는중 ");
            }
        }
    }
}