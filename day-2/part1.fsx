open System.IO

let toTuple (line:string) =
  let split =line.Split[|'x'|]
  (System.Int32.Parse split.[0], System.Int32.Parse split.[1], System.Int32.Parse split.[2])
  
let readLines (path:string) = seq {
  use sr = new StreamReader(path)
  while not sr.EndOfStream do
    yield sr.ReadLine()
}

let surfaceArea (l,w,h) = 
  (2*l*w) + (2*w*h) + (2*h*l)
  
let smallestSide (l,w,h) =
  [|(l,w);(w,h);(h,l)|]
  |> Array.map (fun (x,y) -> x*y)
  |> Array.min

let getGifts =
  readLines "input.txt"
  |> Seq.map (fun line -> toTuple line)
  |> Seq.sumBy (fun (l,w,h) -> (surfaceArea (l, w, h) + smallestSide (l, w, h)))
    
printfn "%d" getGifts