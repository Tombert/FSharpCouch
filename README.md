# FSharpCouch
Just a simple wrapper for doing CouchDB from FSharp

## Usage 

```
open FSCouch

let dbHandle = newConnection "url.of.couch.db" "mycooldatabasename"
put dbHandle "id123" """ {"some": "json"} """ |> Async.RunSynchronously

let result = get dbHandle "id123" |> Async.RunSynchrously

```
