open System.IO

let toTuple (line:string) =
  let split =line.Split[|'x'|]
  (System.Int32.Parse split.[0], System.Int32.Parse split.[1], System.Int32.Parse split.[2])
  
let readLines (path:string) = seq {
  use sr = new StreamReader(path)
  while not sr.EndOfStream do
    yield sr.ReadLine()
}

let perimeterLength (x,y) =
  x+x+y+y
    
let volumn (l,w,h) =
  l*w*h
  
let smallestPerimeter (l,w,h) =
  [|(l,w);(w,h);(h,l)|]
  |> Seq.map (fun (x,y) -> perimeterLength (x, y))
  |> Seq.min

let getGifts =
  readLines "input.txt"
  |> Seq.map (fun line -> toTuple line)
  |> Seq.sumBy (fun (l,w,h) -> (smallestPerimeter (l, w, h) + volumn (l, w, h)))
    
printfn "%d" getGifts