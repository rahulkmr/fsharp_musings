#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"
#r "packages/Suave/lib/net40/Suave.dll"

open FSharp.Data
open Suave


type Species = HtmlProvider<"http://en.wikipedia.org/wiki/The_world's_100_most_threatened_species">

let species =
    [ for x in Species.GetSample().Tables.``Species list``.Rows ->
        x.Type, x.``Common name`` ]

let speciesSorted =
    species
        |> List.countBy fst
        |> List.sortByDescending snd

let html =
    [ yield "<html><body><ul>"
      for (category, count) in speciesSorted do
        yield sprintf "<li>Category <b>%s</b>: <b>%d</b>" category count
      yield "</ul></body></html>" ]
    |> String.concat "\n"

startWebServer defaultConfig (Successful.OK html)