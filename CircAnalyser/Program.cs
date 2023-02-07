using CircAnalyser.Gates;
using CircAnalyser.Triggers;

// Declearing components
OR Or1 = new(), Or2 = new();
AND And1 = new();

// link cricut 
Or1["OUT1"] += And1["IN1"];
Or2["OUT1"] += And1["IN2"];

// initialize input
Or1["IN1"].Lo();
Or1["IN2"].Hi();
Or2["IN1"].Lo();
Or2["IN2"].Hi();

// start simulation
CircAnalyser.Components.TickAndUpdateAll(Or1, Or2);

// check output ((A OR B) AND (C OR D))
Console.WriteLine(And1);


//AndGate.Update();
//Console.WriteLine(AndGate);

//D d = new();
//Console.WriteLine(d);
//d["D"].Hi();
//d.Tick();
//Console.WriteLine(d);
//d["D"].Lo();
//d.Tick();
//Console.WriteLine(d);
//d.Tick();
//Console.WriteLine(d);

//T t = new();
//Console.WriteLine(t);
//t["T"].Hi();
//t.Tick();
//Console.WriteLine(t);
//t.Tick();
//Console.WriteLine(t);
//t.Tick();
//Console.WriteLine(t);
//t.Tick();
//Console.WriteLine(t);