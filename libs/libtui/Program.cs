/*
using Wasmtime;

using var engine = new Engine();
using var linker = new Linker(engine);
using var store = new Store(engine);

using var module = Module.FromText(
                engine,
                "hello",
                "(module (func $hello (import \"\" \"hello\")) (func (export \"run\") (call $hello)))"
            );

linker.Define(
              "",
              "hello",
              Function.FromCallback(store, () => Console.WriteLine("Hello from C#!"))
          );

var instance = linker.Instantiate(store, module);
var run = instance.GetFunction(store, "run");
run?.Invoke(store);
*/
//App.Lanuch(new Surface());
//Console.WriteLine("Hellow tu!");