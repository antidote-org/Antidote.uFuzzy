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

let fruitList =
    [
        "Apple"
        "Orange"
        "Banana"
    ]

let fuse = fuse.Create(ResizeArray fruitList)

let result = fuse.search("apple")
// { item: 'Apple', refIndex: 0 }
```

Exemple 2:

```fs
open AntiDote.uFuzzy
open Fable.Core

let books =
    [
        {|
            title = "Old Man's War"
            author =
                {|
                    firstName = "John"
                    lastName = "Scalzi"
                |}
        |}
        {|
            title = "The Lock Artist"
            author =
                {|
                    firstName = "Steve"
                    lastName = "Hamilton"
                |}
        |}
    ]

let fuse =
    fuse.Create(
        ResizeArray books,
        Fuse.IFuseOptions<_>(
            keys =
                [|
                    U3.Case2 "title"
                    U3.Case2 "author.firstName"
                |]
        )
    )

let result = fuse.search("jon")

// The result is typed, using the input list type
result.[0].item.author.firstName // John
result.[0].item.author.lastName // Scalzi
```

## To publish

*For maintainers only*

```ps1
cd Antidote.uFuzzy
dotnet pack -c Release
dotnet nuget push .\bin\Release\Antidote.uFuzzy.X.X.X.snupkg -s nuget.org -k <nuget_key>
dotnet nuget push .\bin\Release\Antidote.uFuzzy.X.X.X.nupkg -s nuget.org -k <nuget_key>
```
