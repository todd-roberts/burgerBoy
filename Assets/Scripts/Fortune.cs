public class Fortune : Pickup
{
    private FortuneUI _fortuneUI;

    private void Start()
    {
        _fortuneUI = FindObjectOfType<FortuneUI>();
    }

    protected override void OnPickup()
    {
        _fortuneUI.ShowFortune();
    }
}
