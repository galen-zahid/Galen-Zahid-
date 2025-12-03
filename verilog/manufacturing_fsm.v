module manufacturing_fsm (
    input wire clk,
    input wire rst,
    input wire metal_detected,
    input wire high_temp, 
    input wire overcurrent,
    input wire error,
    input wire done,
    input wire ready,
    input wire temp_normal,
    input wire reset_btn,
    input wire emergency,
    output reg conveyor,
    output reg servo,
    output reg fan,
    output reg warning_light,
    output reg buzzer,
    output reg valve
);

// State encoding
localparam [2:0] 
    IDLE           = 3'b000,
    NORMAL_OP      = 3'b001,
    METAL_HANDLING = 3'b010,
    RETURN_NORMAL  = 3'b011,
    COOLING_ACTIVE = 3'b100,
    EMERGENCY_STOP = 3'b101;

reg [2:0] current_state, next_state;

// State transition logic
always @(*) begin
    case (current_state)
        IDLE: begin
            if (emergency) next_state = EMERGENCY_STOP;
            else next_state = NORMAL_OP;
        end
        
        NORMAL_OP: begin
            if (emergency || overcurrent) next_state = EMERGENCY_STOP;
            else if (metal_detected) next_state = METAL_HANDLING;
            else if (high_temp) next_state = COOLING_ACTIVE;
            else next_state = NORMAL_OP;
        end
        
        METAL_HANDLING: begin
            if (emergency || error) next_state = EMERGENCY_STOP;
            else if (high_temp) next_state = COOLING_ACTIVE;
            else if (done) next_state = RETURN_NORMAL;
            else next_state = METAL_HANDLING;
        end
        
        RETURN_NORMAL: begin
            if (emergency) next_state = EMERGENCY_STOP;
            else if (high_temp) next_state = COOLING_ACTIVE;
            else if (ready) next_state = NORMAL_OP;
            else next_state = RETURN_NORMAL;
        end
        
        COOLING_ACTIVE: begin
            if (emergency) next_state = EMERGENCY_STOP;
            else if (temp_normal) next_state = NORMAL_OP;
            else next_state = COOLING_ACTIVE;
        end
        
        EMERGENCY_STOP: begin
            if (reset_btn) next_state = IDLE;
            else next_state = EMERGENCY_STOP;
        end
        
        default: next_state = IDLE;
    endcase
end

// State register
always @(posedge clk or posedge rst) begin
    if (rst) current_state <= IDLE;
    else current_state <= next_state;
end

// Output logic
always @(*) begin
    conveyor = 0;
    servo = 0;
    fan = 0;
    warning_light = 0;
    buzzer = 0;
    valve = 0;
    
    case (current_state)
        NORMAL_OP: begin
            conveyor = 1;
            valve = ~high_temp;
        end
        
        METAL_HANDLING: begin
            conveyor = 1;
            servo = 1;
        end
        
        RETURN_NORMAL: begin
            conveyor = 1;
        end
        
        COOLING_ACTIVE: begin
            fan = 1;
            warning_light = 1;
            buzzer = 1;
        end
        
        EMERGENCY_STOP: begin
            warning_light = 1;
            buzzer = 1;
        end
    endcase
end

endmodule
