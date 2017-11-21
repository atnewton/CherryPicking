namespace FSharpPortableLibrary

type SearchWord() =

      let rec findByIndexOfRecursive (textToSearch:string) (keyword:string) (startingPosition:int) =
          let index = textToSearch.IndexOf(keyword, startingPosition)
          if index >= 0 then 
            let rest = findByIndexOfRecursive textToSearch keyword (index + 1)
            index::rest
          else []

      member this.CherryPickAll(textToSearch:string, keyWord:string, startingPosition:int) = (findByIndexOfRecursive textToSearch keyWord startingPosition);