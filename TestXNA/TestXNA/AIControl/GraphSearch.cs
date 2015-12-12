using System.Collections.Generic;

public class Vertex
{


    public string id; // Label

    public float x; // Horizontal location on screen

    public float y; // Vertical location on screen

    public int type;

    // Construcutor

    public Vertex(string id, float x, float y, int type)

    {

        this.id = id;

        this.x = x;

        this.y = y;

        this.type = type;
    }

    public override string ToString()

    {

        return (id + " – " + x.ToString() + " – " + y.ToString());

    }

}

public class Edge

{

    public Vertex Vertex1; // Vertex one

    public Vertex Vertex2; // Vertex two

    public float Distance; // DIstance or similar

    public float Cost; // Cost or multiplier factor

    // Contructor

    public Edge(Vertex Vertex1, Vertex Vertex2, float distance, float cost)

    {

        this.Vertex1 = Vertex1;

        this.Vertex2 = Vertex2;

        this.Distance = distance;

        this.Cost = cost;

    }

}

class Dijkstra

{

    public List<Vertex> Vertices;

    public List<Edge> edges;

    // Constructor 
    public Dijkstra()

    {

        Vertices = new List<Vertex>(); // Holds the Vertices

        edges = new List<Edge>(); // Holds the connections

    }


    // Dijkstra calculation algorithm 
    public void Execute()

    {

        while (Vertices.Count > 0)

        {

            // For each smallset Vertex

            Vertex smallest = ExtractSmallest();


            // Get the adjacents 



            List<Vertex> adjacentVertices = AdjacentRemainingVertices(smallest);
            // for each adjacent Vertex calculate the distance

            int size = adjacentVertices.Count;

            for (int i = 0; i < size; ++i)

            {

                Vertex adjacent = adjacentVertices.ElementAt(i);

                float distance = Distance(smallest, adjacent) + smallest.distanceFromStart;

                if (distance < adjacent.distanceFromStart)
                {

                    adjacent.distanceFromStart = distance;

                    adjacent.previous = smallest;

                }

            }

        }

    }
}
