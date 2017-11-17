using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class Timer {

    private int length;
    private Stopwatch myTimer = new Stopwatch();

	public Timer(int milliseconds)
    {
        length = milliseconds;
        myTimer.Start();
    }
	
    public bool hasElapsed()
    {
        long timePassed = myTimer.ElapsedMilliseconds;
        if (timePassed >= length)
            return true;
        else
            return false;
    }

    public bool hasElapsed(int milliseconds)
    {
        long timePassed = myTimer.ElapsedMilliseconds;
        if (timePassed >= milliseconds)
            return true;
        else
            return false;
    }

    public void reset(int milliseconds)
    {
        myTimer.Stop();
        myTimer.Reset();
        myTimer.Start();
        length = milliseconds;
    }

    public long timePassed()
    {
        return myTimer.ElapsedMilliseconds;
    }

    public int timeRemaining()
    {
        long timePassed = myTimer.ElapsedMilliseconds;
        return (int)(length - timePassed);
    }
}
