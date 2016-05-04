open System.Security.Cryptography
open System.Threading.Tasks
open System

let monitor = new Object();
let mutable results = [];

let byteToHex array =
    array
    |> Array.map (fun (x:byte) -> System.String.Format("{0:x2}", x))
    |> String.concat System.String.Empty 

let md5 (input:string) (i:int) =
  (input + i.ToString()) 
  |> System.Text.Encoding.ASCII.GetBytes
  |> HashAlgorithm.Create("MD5").ComputeHash 
  |> byteToHex

let saveMatch i =
  lock monitor (fun () -> results <- List.append results [i])
    
let check input i prefix =
  let hash = md5 input i 
  match hash with
  | h when h.StartsWith prefix -> saveMatch i
  | _ -> ()
  
let mine input difficulty =
  let prefix = String.replicate difficulty "0"
  Parallel.ForEach([0 .. 10000000], (fun i -> 
    check input i prefix
  )) |> ignore
  results
  |> List.min
  |> printfn "The answer is %d"
  
let test input i difficulty = 
  let prefix = String.replicate difficulty "0"
  let hash = md5 input i
  match hash.StartsWith prefix with
  | true -> printfn "%d %s - Matches" i hash
  | false -> printfn "%d %s - Does not match" i hash
  

mine "ckczppom" 6
  
//test "ckczppom" 3938038 6