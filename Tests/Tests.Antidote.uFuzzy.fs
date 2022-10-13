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

        let result = uf.filter(haystack, needle)

        Assert.AreEqual(result.Count, 1)
    )
)
