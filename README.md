# Antidote.uFuzzy

Binding for npm https://www.npmjs.com/package/@leeoniya/ufuzzy package

## Installation

```
# using nuget
dotnet add package Antidote.uFuzzy
```

# or with paket
paket add Antidote.uFuzzy --project /path/to/project.fsproj
```

You also need to install `uFuzzy.js` package.

```
# using Femto
dotnet femto --resolve

# using NPM
npm i @leeoniya/ufuzzy
# using yarn
yarn add @leeoniya/ufuzzy
```

## Usage

Exemple 1:

```fs
open Antidote.uFuzzy
open Fable.Core

let haystack =
    [|
        "Apple"
        "Orange"
        "Banana"
    |]

let ufzzy = ufuzzy.Create(haystack)

let idxs = fuse.filter("apple")
let result = idxs |> Array.map(fun x -> haystack.[int x])
```

## To publish

*For maintainers only*

```ps1
cd Antidote.uFuzzy
dotnet pack -c Release
dotnet nuget push .\bin\Release\Antidote.uFuzzy.X.X.X.snupkg -s nuget.org -k <nuget_key>
dotnet nuget push .\bin\Release\Antidote.uFuzzy.X.X.X.nupkg -s nuget.org -k <nuget_key>
```
