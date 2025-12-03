module tb_manufacturing_fsm;

    reg clk, rst;
    reg metal_detected, high_temp, overcurrent, error;
    reg done, ready, temp_normal, reset_btn, emergency;
    wire conveyor, servo, fan, warning_light, buzzer, valve;

    manufacturing_fsm uut (
        .clk(clk),
        .rst(rst),
        .metal_detected(metal_detected),
        .high_temp(high_temp),
        .overcurrent(overcurrent),
        .error(error),
        .done(done),
        .ready(ready),
        .temp_normal(temp_normal),
        .reset_btn(reset_btn),
        .emergency(emergency),
        .conveyor(conveyor),
        .servo(servo),
        .fan(fan),
        .warning_light(warning_light),
        .buzzer(buzzer),
        .valve(valve)
    );

    // Clock generator
    always #5 clk = ~clk;

    initial begin
        // Dumpfile untuk GTKWave
        $dumpfile("manufacturing_fsm.vcd");
        $dumpvars(0, tb_manufacturing_fsm);

        // Initialize
        clk = 0; rst = 1;
        metal_detected = 0; high_temp = 0; overcurrent = 0; error = 0;
        done = 0; ready = 0; temp_normal = 1; reset_btn = 0; emergency = 0;
        
        #20 rst = 0;
        
        // Test normal operation
        #10 metal_detected = 1;
        #10 metal_detected = 0;
        
        // Test cooling activation
        #10 high_temp = 1;
        #20 temp_normal = 1;
        #10 high_temp = 0;
        
        // Test emergency stop
        #10 emergency = 1;
        #20 reset_btn = 1;
        #10 emergency = 0;
        #10 reset_btn = 0;

        #100 $finish;
    end

    initial begin
        $monitor("T=%t | State=%b | Conv=%b | Servo=%b | Fan=%b | Warn=%b | Buzz=%b",
                 $time, uut.current_state, conveyor, servo, fan, warning_light, buzzer);
    end
endmodule
