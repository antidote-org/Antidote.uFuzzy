module Tests.Fuse

open Mocha
open Node
open Fable.Core
open Fable.Core.Testing
open Fable.Core.JsInterop
open Antidote.UFuzzy


let haystack = [|
    "puzzle"
    "Super Awesome Thing (now with stuff!)"
    "FileName.js"
    "/feeding/the/catPic.jpg"
|]

let needle = "feed cat"

describe """uFuzzy.search""" (fun () ->

    it "works with strings" (fun () ->

        let uf = uFuzzy.Create( Antidote.UFuzzy.UFuzzy.IUFuzzyOptions() )

        let idxs = uf.filter(haystack, needle)
        let result = idxs |> Seq.map(fun x -> haystack.[int x]) |> Seq.toArray
        Assert.AreEqual(result.[0] , "/feeding/the/catPic.jpg")
    )
)
