public class Timer
{
    private float totalTime = 0;
    private float curTime = 0;

    public Timer(float totalTime)
    {
        this.totalTime = totalTime;
    }
    void UpdateTime(float passTime)
    {
        curTime += passTime;
    }
    public bool IsDone
    {
        get { return curTime > totalTime; }
    }
}