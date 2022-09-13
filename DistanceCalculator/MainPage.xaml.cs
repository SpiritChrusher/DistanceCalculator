using System.Diagnostics;

namespace DistanceCalculator;

public partial class MainPage : ContentPage
{
	int count = 0;
	Stopwatch clock;


    public MainPage()
	{
		InitializeComponent();
		clock = new Stopwatch();
	}

	private void OnTimerClicked(object sender, EventArgs e)
	{
        if (Accelerometer.Default.IsSupported)
        {
            if (!Accelerometer.Default.IsMonitoring)
            {
                // Turn on accelerometer
                Accelerometer.Default.ReadingChanged += Accelerometer_ReadingChanged;
                Accelerometer.Default.Start(SensorSpeed.UI);
            }
        }

        count++;
        clock.Start();
        if (count%2 is 0)
		{
            clock.Stop();
            TimerBtn.Text = $"Resume";
            ShowTime.Text = clock.Elapsed.ToString();
        }
		else
		{
            clock.Start();
            TimerBtn.Text = $"Stop";
            ShowTime.Text = clock.Elapsed.ToString();
        }

        SemanticScreenReader.Announce(TimerBtn.Text);
	}

    private void OnStopClicked(object sender, EventArgs e)
    {
        clock.Reset();
        TimerBtn.Text = $"Start";

        // Turn off accelerometer
        //Accelerometer.Default.Stop();
        //Accelerometer.Default.ReadingChanged -= Accelerometer_ReadingChanged;

        SemanticScreenReader.Announce(TimerBtn.Text);
    }

    private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
    {
        // Update UI Label with accelerometer state
        AccelLabel.TextColor = Colors.Green;
        AccelLabel.Text = $"Accel: {e.Reading}";
    }
}

