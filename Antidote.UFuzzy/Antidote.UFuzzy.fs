// ts2fable 0.8.0
module rec Antidote.UFuzzy

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS

type Array<'T> = System.Collections.Generic.IList<'T>
type ReadonlyArray<'T> = System.Collections.Generic.IReadOnlyList<'T>

[<Import("default", "@leeoniya/ufuzzy")>]
let uFuzzy: UFuzzyStatic = jsNative

[<AllowNullLiteral>]
type IExports =
    abstract UFuzzy: UFuzzyStatic

[<AllowNullLiteral>]
type UFuzzy =
    /// initial haystack filter, can accept idxs from previous prefix/typeahead match as optimization
    abstract filter: haystack: Array<string> * needle: string * ?idxs: UFuzzy.HaystackIdxs -> UFuzzy.HaystackIdxs
    /// collects stats about pre-filtered matches, does additional filtering based on term boundary settings, finds highlight ranges
    abstract info: idxs: UFuzzy.HaystackIdxs * haystack: ResizeArray<string> * needle: string -> UFuzzy.Info
    /// performs final result sorting via Array.sort(), relying on Info
    abstract sort: info: UFuzzy.Info * haystack: ResizeArray<string> * needle: string -> UFuzzy.InfoIdxOrder
    /// utility for splitting needle into terms following defined interSplit/intraSplit opts. useful for out-of-order permutes
    abstract split: needle: string -> UFuzzy.Terms

[<AllowNullLiteral>]
type UFuzzyStatic =
    [<EmitConstructor>]
    abstract Create: UFuzzy.IUFuzzyOptions -> UFuzzy
    /// util for creating out-of-order permutations of a needle terms array
    abstract permute: arr: ResizeArray<obj> -> ResizeArray<ResizeArray<obj>>
    /// util for replacing common diacritics/accents
    abstract latinize: strings: ResizeArray<string> -> ResizeArray<string>
    /// util for highlighting matched substr parts of a result
    abstract highlight: ``match``: string * ranges: ResizeArray<float> * pre: string * suf: string -> string

module UFuzzy =

    /// needle's terms
    type Terms =
        Array<string>

    /// subset of idxs of a haystack array
    type HaystackIdxs =
        Array<float>

    /// sorted order in which info facets should be iterated
    type InfoIdxOrder =
        Array<float>

    /// partial RegExp
    type PartialRegExp =
        string

    /// what should be considered acceptable term bounds
    [<RequireQualifiedAccess>]
    type BoundMode =
        /// will match 'man' substr anywhere. e.g. tasmania
        | Any = 0
        /// will match 'man' at whitespace, punct, case-change, and alpha-num boundaries. e.g. mantis, SuperMan, fooManBar, 0007man
        | Loose = 1
        /// will match 'man' at whitespace, punct boundaries only. e.g. mega man, walk_man, man-made, foo.man.bar
        | Strict = 2

    [<RequireQualifiedAccess>]
    type IntraMode =
        /// allows any number of extra char insertions within a term, but all term chars must be present for a match
        | MultiInsert = 0
        /// allows for a single-char substitution, transposition, insertion, or deletion within terms (excluding first and last chars)
        | SingleError = 1

    [<RequireQualifiedAccess>]
    type OptionsIntraSub =
        | N0 = 0
        | N1 = 1
    [<AllowNullLiteral>]
    [<Global>]
    type IUFuzzyOptions [<ParamObject; Emit("$0")>]
        (
            ?interLft: BoundMode,
            ?interRgt: BoundMode,
            ?interSplit: PartialRegExp,
            ?intraChars: PartialRegExp,
            ?intraIns: float,
            ?intraMode: IntraMode,
            ?intraSub: OptionsIntraSub,
            ?intraTrn: OptionsIntraSub,
            ?intraDel: OptionsIntraSub,
            ?intraFilt:(string -> string -> float -> bool),
            ?sort: (Info -> Array<string> -> string -> InfoIdxOrder)
        ) =

        member val interLft: BoundMode option = jsNative with get, set
        member val interRgt: BoundMode option = jsNative with get, set
        member val intraSplit: PartialRegExp option = jsNative with get, set
        member val intraChars: PartialRegExp option = jsNative with get, set
        member val intraIns: float option = jsNative with get, set
        member val intraMode: IntraMode option = jsNative with get, set
        member val intraSub: OptionsIntraSub option = jsNative with get, set
        member val intraTrn: OptionsIntraSub option = jsNative with get, set
        member val intraDel: OptionsIntraSub option = jsNative with get, set
        member val intraFilt: (string -> string -> float -> bool) option = jsNative with get, set
        member val sort: (Info -> ResizeArray<string> -> string -> InfoIdxOrder) option = jsNative with get, set

    [<AllowNullLiteral>]
    type Info =
        /// matched idxs from haystack
        abstract idx: HaystackIdxs with get, set
        /// match offsets
        abstract start: ResizeArray<float> with get, set
        /// number of left BoundMode.Strict term boundaries found
        abstract interLft2: ResizeArray<float> with get, set
        /// number of right BoundMode.Strict term boundaries found
        abstract interRgt2: ResizeArray<float> with get, set
        /// number of left BoundMode.Loose term boundaries found
        abstract interLft1: ResizeArray<float> with get, set
        /// number of right BoundMode.Loose term boundaries found
        abstract interRgt1: ResizeArray<float> with get, set
        /// total number of extra chars matched within all terms. higher = matched terms have more fuzz in them
        abstract intraIns: ResizeArray<float> with get, set
        /// total number of chars found in between matched terms. higher = terms are more sparse, have more fuzz in between them
        abstract interIns: ResizeArray<float> with get, set
        /// total number of matched contiguous chars (substrs but not necessarily full terms)
        abstract chars: ResizeArray<float> with get, set
        /// number of exactly-matched terms (intra = 0) where both lft and rgt landed on a BoundMode.Loose or BoundMode.Strict boundary
        abstract terms: ResizeArray<float> with get, set
        /// offset ranges within match for highlighting: [startIdx0, endIdx0, startIdx1, endIdx1,...]
        abstract ranges: ResizeArray<ResizeArray<float>> with get, set
