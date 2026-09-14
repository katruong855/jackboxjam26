using UnityEngine;
using PurrNet;
using TMPro;

public class Timer : NetworkBehaviour
{    
    [SerializeField] private TMP_Text timerText;
    private bool DEBUG = true;

    //false = OwnerAuth
    //3 = Reoncile interval
    //The reconcile interval deciphers how often it will force align all clients
    private SyncTimer timer = new();


    protected override void OnSpawned()
    {
        timer.onTimerSecondTick += OnTimerSecondTick;
        //This starts the timer with 30 seconds countdown
        if(isServer)
            timer.StartTimer(60f);
            if(DEBUG) Debug.Log("Timer started.");
        
    }

    protected override void OnDespawned()
    {
        timer.onTimerSecondTick -= OnTimerSecondTick;
    }

    private void OnTimerSecondTick()
    {
        //You can also get .remaining to get the precise float value
        //For displaying timers, the remainingInt makes it easy
        timerText.text = timer.remainingInt.ToString() + " Seconds";
    }

    private void PauseGameTimer() 
    {
        //Pauses the timer and will sync the remaining time because it's set to true
        timer.PauseTimer(true);
    }

    private void ResumeGameTimer()
    {
        //Will resume the timer from where it was paused
        timer.ResumeTimer();
    }
}
