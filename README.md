

## ✨ Features

* **Tokenizer → Parser → Evaluator** — clean separation of responsibilities
* **Strategy + Registry** design pattern
* Handles **unary operations** (currently supports `+` and `-`)
* **Variadic function** support (e.g., `max`, `min` with variable argument lists)
* **Function overloading**
* Custom **exceptions** (partially implemented)
* Simple **REPL** interface

---

## 🚀 Highlights & Usage

### Registering Functions

You can register functions with **fixed** or **variable** arity.

#### 📌 Fixed-Arity Functions

```csharp
var func = new FunctionSpec
{
    Symbol = "pow",

    Operation = args =>
    {
        if (args.Length == 2)
            return Math.Pow(args[1], args[0]);
        else
            throw new Exception("Invalid number of arguments");
    },

    FixedArity = true,
    Arity = 2
};
```

---

#### 📌 Variable-Arity Functions

```csharp
new FunctionSpec
{
    Symbol = "random",

    Overloads = new Dictionary<int, Func<double[], double>>
    {
        // Dictionary keys indicate the overload arity
        [0] = args => new Random().Next(),
        [1] = args => new Random().Next((int)args[0]),
        [2] = args => new Random().Next((int)args[1], (int)args[0]),
    },

    FixedArity = false
};
```

**Or:**

```csharp
new FunctionSpec
{
    // Functions like max can accept any number of arguments greater than 1
    Symbol = "max",

    Operation = args => args.Max(),

    FixedArity = false,
    MinArity = 1
};
```

---

## 🧪 Examples

### Max Function

**Input:**

```text
max(2,3,4,5,6,7)
```

**Output:**

```text
7
```

---

### Sqrt Function

**Input:**

```text
sqrt(4)
```

**Output:**

```text
2
```

---

## TODOs
```text
- Implement a programming language

```



