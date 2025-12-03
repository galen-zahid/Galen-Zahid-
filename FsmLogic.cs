using System;

namespace FsmGraph
{
    public static class FsmLogic
    {
        // State encoding sesuai paper LaTeX:
        // 0: IDLE (000)
        // 1: NORMAL_OP (001)
        // 2: METAL_HANDLING (010)
        // 3: RETURN_NORMAL (011)
        // 4: COOLING_ACTIVE (100)
        // 5: EMERGENCY_STOP (101)

        private static int currentState = 0; // IDLE
        private static Random rand = new Random();

        private static int StableRandom()
        {
            return rand.NextDouble() < 0.25 ? 1 : 0;
        }

        public static (int state,
                       int metal_detected, int high_temp, int overcurrent,
                       int error, int done, int ready, int temp_normal,
                       int reset_btn, int emergency,
                       int Conv, int Servo, int Fan, int Light, int Buzz, int Valve,
                       string StateName)
               StepAuto()
        {
            // Sensor simulation sesuai paper LaTeX
            int metal_detected = StableRandom();    // Metal detection
            int high_temp = StableRandom();         // High temperature
            int overcurrent = StableRandom();       // Overcurrent
            int error = StableRandom();             // System error
            int done = StableRandom();              // Process done
            int ready = StableRandom();             // System ready
            int temp_normal = StableRandom();       // Temperature normal
            int reset_btn = StableRandom();         // Reset button
            int emergency = rand.NextDouble() < 0.05 ? 1 : 0; // Emergency (5%)

            // ========================
            // FSM TRANSITIONS LOGIC sesuai paper LaTeX
            // ========================
            switch (currentState)
            {
                case 0: // IDLE
                    if (ready == 1)
                        currentState = 1; // IDLE → NORMAL_OP
                    break;

                case 1: // NORMAL_OP
                    if (metal_detected == 1)
                        currentState = 2; // NORMAL_OP → METAL_HANDLING
                    else if (high_temp == 1)
                        currentState = 4; // NORMAL_OP → COOLING_ACTIVE
                    else if (error == 1)
                        currentState = 0; // NORMAL_OP → IDLE
                    break;

                case 2: // METAL_HANDLING
                    if (done == 1)
                        currentState = 3; // METAL_HANDLING → RETURN_NORMAL
                    break;

                case 3: // RETURN_NORMAL
                    if (temp_normal == 1)
                        currentState = 1; // RETURN_NORMAL → NORMAL_OP
                    break;

                case 4: // COOLING_ACTIVE
                    if (temp_normal == 1)
                        currentState = 1; // COOLING_ACTIVE → NORMAL_OP
                    break;

                case 5: // EMERGENCY_STOP
                    if (reset_btn == 1)
                        currentState = 0; // EMERGENCY_STOP → IDLE
                    break;
            }

            // Emergency override - priority sesuai tabel priority encoding
            if (emergency == 1)
                currentState = 5;

            // ========================
            // ACTUATORS sesuai paper LaTeX
            // ========================
            // Priority 1 (Highest): Emergency Stop - All STOP + Alarm
            // Priority 2: Overcurrent - Motor STOP + Alarm
            // Priority 3: High Temperature - Fan ON + Warning
            // Priority 4: Object Too Close - STOP + Warning
            // Priority 5: Low Pressure - RUN + Valve OPEN
            // Priority 6: Metal Detected - RUN + Servo MOVE
            // Priority 7: Normal Operation - RUN Normal

            int Conv = 0, Servo = 0, Fan = 0, Light = 0, Buzz = 0, Valve = 0;

            // Priority encoding logic
            if (emergency == 1) // Priority 1
            {
                Conv = 0;
                Servo = 0;
                Fan = 0;
                Valve = 0;
                Light = 1;
                Buzz = 1;
            }
            else if (overcurrent == 1) // Priority 2
            {
                Conv = 0;
                Servo = 0;
                Fan = 0;
                Valve = 0;
                Light = 1;
                Buzz = 1;
            }
            else if (high_temp == 1) // Priority 3
            {
                Conv = 0;
                Servo = 0;
                Fan = 1;
                Valve = 0;
                Light = 1;
                Buzz = 0;
            }
            else if (metal_detected == 1) // Priority 6
            {
                Conv = 1;
                Servo = 1;
                Fan = 0;
                Valve = 0;
                Light = 0;
                Buzz = 0;
            }
            else // Priority 7: Normal Operation
            {
                Conv = 1;
                Servo = 0;
                Fan = 0;
                Valve = 0;
                Light = 0;
                Buzz = 0;
            }

            string stateName = currentState switch
            {
                0 => "IDLE",
                1 => "NORMAL_OP",
                2 => "METAL_HANDLING",
                3 => "RETURN_NORMAL",
                4 => "COOLING_ACTIVE",
                5 => "EMERGENCY_STOP",
                _ => "UNKNOWN"
            };

            return (currentState,
                    metal_detected, high_temp, overcurrent,
                    error, done, ready, temp_normal, reset_btn, emergency,
                    Conv, Servo, Fan, Light, Buzz, Valve,
                    stateName);
        }
    }
}