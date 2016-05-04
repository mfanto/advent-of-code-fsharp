open System.IO
open System.Text.RegularExpressions

let vowels = ['a'; 'e'; 'i'; 'o'; 'u']
let badStrings = ["ab"; "cd"; "pq"; "xy"]

let explode line =
  [for c in line -> c]
  
let (|DoubleLetter|_|) input =
  let m = Regex("(\w)\1{1}").Match(input)
  if m.Success then Some true else None

let countLetter line c =
  let rec step remaining count =
    match remaining with 
    | [] -> count
    | h::t -> step t (if h = c then count + 1 else count)
  step (explode line) 0

let hasThreeVowels line =
  vowels |> List.sumBy (fun c -> countLetter line c) >= 3
 
let hasDoubleLetter line =
  match line with
  | DoubleLetter line -> true
  | _ -> false

let noBadStrings (line:string) =
  badStrings |> List.exists (fun b -> line.Contains(b)) = false

let isNice line = 
  hasThreeVowels line && hasDoubleLetter line && noBadStrings line
    
let start =
  File.ReadLines("input.txt")
  |> Seq.sumBy (fun line -> if isNice line then 1 else 0)
  |> printfn "%d"