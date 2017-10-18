namespace FsharpCouch

module FSCouch =
    open System.Net.Http

    let client = new HttpClient()
    let sendRequest (httpmethod: string) (data: string) (url:string) = async {
        let request =
            new System.Net.Http.StringContent (data, System.Text.Encoding.UTF8)
        let response = 
            match httpmethod with
            | "GET" ->
                client.GetAsync url
            | "PUT" ->
                client.PutAsync(url, request)
            | "POST" -> 
                client.PostAsync(url, request)
            | "DELETE" -> 
                client.DeleteAsync (url)
            | _ ->
                failwith "method not supported"
        let! httpresponse = response |> Async.AwaitTask 
        let! content = httpresponse.Content.ReadAsStringAsync() |> Async.AwaitTask
        return content

    }

    type ConnectionHandle = {
        ConnectionString : string
        DatabaseName : string
    }

    let newConnection connstr dbname =
        {
            ConnectionString = connstr
            DatabaseName = dbname
        }

    let get (dbhandle: ConnectionHandle) id = async {
        let url = sprintf "%s/%s/%s" (dbhandle.ConnectionString) (dbhandle.DatabaseName) (id)
        let! response = sendRequest "GET" "" url
        return response
    }


    let put (dbhandle: ConnectionHandle) id data = async {
        let url = sprintf "%s/%s/%s" (dbhandle.ConnectionString) (dbhandle.DatabaseName) (id)
        let! response = sendRequest "PUT" data url
        return response
    }

    let delete (dbhandle: ConnectionHandle) id data = async {
        let url = sprintf "%s/%s/%s" (dbhandle.ConnectionString) (dbhandle.DatabaseName) (id)
        let! response = sendRequest "DELETE" data url
        return response
    }
