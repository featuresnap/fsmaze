# fsmaze
A library to explore maze generation functionality using F#, loosely inspired by [Mazes for Programmers](https://pragprog.com/book/jbmaze/mazes-for-programmers)

## Maze Representation
The maze is represented by a `GridBuilder` which maintains:
- The number of rows and columns in the maze
- The links (openings) between grid locations

### Adding links
Links between cells are represented in a `Dictionary<int * int, HashMap<int * int>`. The GridBuilder allows mutation of the list through the `AddLink` method. While I originally sought an immutable representation of links, it did not seem the best choice for rendering performance, given ~n^2 of lookups to check where links existed.

### Making Illegal States Unrepresentable when adding links
The `AddLink` method signature originally required 2 coordinates: 
```
member self.AddLink(cellFrom: int * int, cellTo: int * int)
```

This would require validation for adjacency of the two linked cells' coordinates. By introducing a `Direction` type, we can require the caller to express an offset direction from an origin cell, and the `offset` function (rather than the caller) computes the destination cell's coordinates. 