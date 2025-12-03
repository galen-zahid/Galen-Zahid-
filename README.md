**FINAL README.md (ENGLISH VERSION)**

**You can copy–paste directly to GitHub.**

---

# FSM Manufacturing Monitoring & Control System

### **(C# Real-Time Simulation + Verilog HDL for FPGA Implementation)**

## 📌 Overview

This repository contains **two independent implementations** of a manufacturing process *Finite State Machine (FSM)*:

1. **C# (.NET WinForms Simulation)**
   A real-time FSM simulator featuring GUI visualization, live plotting, and virtual sensor/actuator processing — designed for educational Model-Based Design (MBD).

2. **Verilog HDL (FPGA Hardware Implementation)**
   A hardware-synthesizable FSM that can be deployed on FPGA boards such as Cyclone IV/V or Artix-7.

Both implementations follow the **same system specification**, including 6 states, 9 sensor inputs, and 6 actuator outputs with a hardware-style **priority encoder**.

---

# 📁 Repository Structure

```
FsmGraph/
├── CSharp/                         # C# WinForms Simulation
│   ├── FsmLogic.cs
│   ├── Form1.cs
│   ├── Form1.Designer.cs
│   ├── Program.cs
│   ├── FsmGraph.csproj
│   └── ...
│
├── verilog/                       # Verilog HDL for FPGA
│   ├── manufacturing_fsm.v        # Main FSM module
│   ├── tb_manufacturing_fsm.v     # Testbench
│   ├── manufacturing_fsm.vcd      # Simulation waveform
│   └── README_VERILOG.md          # (optional) dedicated HDL documentation
│
└── 2042241044_Galen Zahid Wajendra_Tugas Eldig.pdf
```

---

# 🧠 FSM State Definitions

| Code | State Name     |
| ---- | -------------- |
| 000  | IDLE           |
| 001  | NORMAL_OP      |
| 010  | METAL_HANDLING |
| 011  | RETURN_NORMAL  |
| 100  | COOLING_ACTIVE |
| 101  | EMERGENCY_STOP |

These states are used consistently in both C# and HDL implementations.

---

# ⚙️ System Features

## **1. C# Simulation Features**

* Real-time FSM execution
* Randomized sensor input generator
* Live plotting using ScottPlot:

  * FSM state transitions
  * Sensor readings
  * Actuator outputs
* Clean and intuitive WinForms GUI
* Full MBD workflow demonstration
* Ideal for visualization & teaching

---

## **2. Verilog HDL Features**

* Fully synthesizable RTL FSM
* Modular hardware design
* Priority logic embedded at gate level
* Testbench included
* VCD waveform for debugging
* FPGA-ready pin assignment structure

---

# 🔥 Priority Encoder (Shared Logic)

Priority is evaluated **from highest to lowest**:

1. **Emergency Stop**
2. Overcurrent
3. High Temperature
4. Object Too Close
5. Low Pressure
6. Metal Detected
7. Normal Operation

Both C# and HDL versions implement this same hierarchy for actuator control.

---

# 🚀 Running Instructions

## **C# (.NET) Version**

### Requirements:

* .NET 8 SDK
* Visual Studio 2022
* ScottPlot.WinForms (auto-restored)

### Run:

```
cd CSharp
dotnet restore
dotnet run
```

---

## **Verilog HDL Version**

### Requirements:

* Icarus Verilog / ModelSim / Vivado
* GTKWave (for VCD viewing)

### Run Simulation:

```
cd verilog
iverilog -o fsm manufacturing_fsm.v tb_manufacturing_fsm.v
vvp fsm
gtkwave manufacturing_fsm.vcd
```

### FPGA Deployment:

* Import `manufacturing_fsm.v` into Quartus/Vivado
* Assign physical pins
* Generate bitstream & flash to FPGA

---

# 🧮 Mathematical & Computational Model

### **State Transition Function**

```
S(t+1) = f(S(t), Inputs)
```

### **Boolean Equations**

Actuator logic generated from:

* Truth tables
* Karnaugh Map simplification
* Priority-based logic minimization

### **Matrix Representation**

FSM transitions can be expressed as matrix transformations for academic study.

### **Quantum Model (Theory Only)**

State encoding as 3-qubit basis vectors |000⟩ to |101⟩
Transition as operator U
Actuator state as measurement M(|q⟩)

*(not used in implementation — for educational exploration only)*

---

# 👥 Project Team

### **Student Developer**

**Galen Zahid Wajendra**
2042241044 – D4 Instrumentation Engineering Technology

### **Supervising Lecturers**

* **Ir. Dwi Oktavianto Wahyu Nugroho, S.T., M.T.** – Digital Electronics Lecturer
* **Ahmad Radhy, S.Si., M.Si.** – Instrumentation Engineering Dept.
* **Fitri Adi Iskandarianto, S.T., M.T.** – Instrumentation Engineering Dept.

### **Institution**

* Department of Instrumentation Engineering
* Faculty of Vocational Studies
* Institut Teknologi Sepuluh Nopember (ITS)

---

# 📄 License

Academic project for the **Digital Electronics** course at ITS.
Free to use for research and educational purposes.

---

# 🔍 Additional Notes

* Both implementations are based on the same FSM specification
* C# version is ideal for debugging, visualization, and MBD
* Verilog version is optimized for real hardware execution
* Waveform (`.vcd`) is included for validation

--
