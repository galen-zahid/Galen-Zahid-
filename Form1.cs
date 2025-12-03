using ScottPlot;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace FsmGraph
{
    public partial class Form1 : Form
    {
        private Timer timer = new();
        private double timeNow = 0;

        private List<double> timeList = new();
        private List<double> stateList = new();

        // Sensor lists sesuai paper LaTeX
        private List<double> metalDetectedList = new();
        private List<double> highTempList = new();
        private List<double> overcurrentList = new();
        private List<double> errorList = new();
        private List<double> doneList = new();
        private List<double> readyList = new();
        private List<double> tempNormalList = new();
        private List<double> resetBtnList = new();
        private List<double> emergencyList = new();

        // Actuator lists
        private List<double> ConvList = new();
        private List<double> ServoList = new();
        private List<double> FanList = new();
        private List<double> LightList = new();
        private List<double> BuzzList = new();
        private List<double> ValveList = new();

        public Form1()
        {
            InitializeComponent();

            formsPlot1.Plot.Title("FSM State");
            formsPlot2.Plot.Title("Sensors");
            formsPlot3.Plot.Title("Actuators");

            timer.Interval = 1000;
            timer.Tick += UpdateLoop;
            timer.Start();
        }

        private void UpdateLoop(object sender, EventArgs e)
        {
            var data = FsmLogic.StepAuto();

            timeList.Add(timeNow);
            stateList.Add(data.state);

            // Store sensor values sesuai paper LaTeX
            metalDetectedList.Add(data.metal_detected);
            highTempList.Add(data.high_temp);
            overcurrentList.Add(data.overcurrent);
            errorList.Add(data.error);
            doneList.Add(data.done);
            readyList.Add(data.ready);
            tempNormalList.Add(data.temp_normal);
            resetBtnList.Add(data.reset_btn);
            emergencyList.Add(data.emergency);

            // Store actuator values
            ConvList.Add(data.Conv);
            ServoList.Add(data.Servo);
            FanList.Add(data.Fan);
            LightList.Add(data.Light);
            BuzzList.Add(data.Buzz);
            ValveList.Add(data.Valve);

            timeNow += 25;

            // state plot
            formsPlot1.Plot.Clear();
            formsPlot1.Plot.AddScatter(timeList.ToArray(), stateList.ToArray(), label: "State");
            formsPlot1.Plot.SetAxisLimits(yMin: -1, yMax: 6);
            formsPlot1.Plot.Legend();
            formsPlot1.Refresh();

            // sensor plot - pilih 6 sensor utama untuk plot
            formsPlot2.Plot.Clear();
            formsPlot2.Plot.AddScatter(timeList.ToArray(), metalDetectedList.ToArray(), label: "Metal");
            formsPlot2.Plot.AddScatter(timeList.ToArray(), highTempList.ToArray(), label: "HighTemp");
            formsPlot2.Plot.AddScatter(timeList.ToArray(), overcurrentList.ToArray(), label: "Overcurrent");
            formsPlot2.Plot.AddScatter(timeList.ToArray(), emergencyList.ToArray(), label: "Emergency");
            formsPlot2.Plot.AddScatter(timeList.ToArray(), readyList.ToArray(), label: "Ready");
            formsPlot2.Plot.AddScatter(timeList.ToArray(), tempNormalList.ToArray(), label: "TempNorm");
            formsPlot2.Plot.SetAxisLimits(yMin: -0.2, yMax: 1.2);
            formsPlot2.Plot.Legend();
            formsPlot2.Refresh();

            // actuator plot
            formsPlot3.Plot.Clear();
            formsPlot3.Plot.AddScatter(timeList.ToArray(), ConvList.ToArray(), label: "Conv");
            formsPlot3.Plot.AddScatter(timeList.ToArray(), ServoList.ToArray(), label: "Servo");
            formsPlot3.Plot.AddScatter(timeList.ToArray(), FanList.ToArray(), label: "Fan");
            formsPlot3.Plot.AddScatter(timeList.ToArray(), LightList.ToArray(), label: "Light");
            formsPlot3.Plot.AddScatter(timeList.ToArray(), BuzzList.ToArray(), label: "Buzz");
            formsPlot3.Plot.AddScatter(timeList.ToArray(), ValveList.ToArray(), label: "Valve");
            formsPlot3.Plot.SetAxisLimits(yMin: -0.2, yMax: 1.2);
            formsPlot3.Plot.Legend();
            formsPlot3.Refresh();

            // ===========================
            // PANEL INFO SEBELAH KANAN
            // ===========================
            infoLabel.Text =
                $"STATE: {data.state} ({data.StateName})\n\n" +
                "Sensors:\n" +
                $"  Metal Detected : {data.metal_detected}\n" +
                $"  High Temp      : {data.high_temp}\n" +
                $"  Overcurrent    : {data.overcurrent}\n" +
                $"  Error          : {data.error}\n" +
                $"  Done           : {data.done}\n" +
                $"  Ready          : {data.ready}\n" +
                $"  Temp Normal    : {data.temp_normal}\n" +
                $"  Reset Button   : {data.reset_btn}\n" +
                $"  Emergency      : {data.emergency}\n\n" +
                "Actuators:\n" +
                $"  Conv  : {data.Conv}\n" +
                $"  Servo : {data.Servo}\n" +
                $"  Fan   : {data.Fan}\n" +
                $"  Light : {data.Light}\n" +
                $"  Buzz  : {data.Buzz}\n" +
                $"  Valve : {data.Valve}\n";

            if (data.state == 5)
                infoLabel.ForeColor = System.Drawing.Color.Red;
            else
                infoLabel.ForeColor = System.Drawing.Color.Black;
        }
    }
}