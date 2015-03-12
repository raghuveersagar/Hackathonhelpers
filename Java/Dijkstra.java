import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.InputMismatchException;
import java.util.Scanner;

class vertex implements Comparable {
	public vertex parent;
	public int distance;
	public int vertex_number;

	public int compareTo(Object o) {

		return this.distance - ((vertex) o).distance;
	}

}

public class Dijkstra {

	private int[][] adj_matrix;
	private vertex[] graph;
	int v, e;

	public void printMatrix(int[][] adj) {
		for (int i = 0; i < v; i++) {
			for (int j = 0; j < v; j++) {
				System.out.print(" " + String.valueOf(adj[i][j]) + " ");
			}
			System.out.println();
		}
	}

	public void printMatrix() {
		for (int i = 0; i < v; i++) {
			for (int j = 0; j < v; j++) {
				System.out.print(" " + String.valueOf(adj_matrix[i][j]) + " ");
			}
			System.out.println();
		}
	}

	public void default_Matrix() {
		for (int i = 0; i < v; i++) {
			for (int j = 0; j < v; j++) {
				if (i == j)
					adj_matrix[i][j] = 0;
				else
					adj_matrix[i][j] = Integer.MAX_VALUE;
			}
			//System.out.println();
		}
	}

	/*
	 * Read input from Std In and create the adjacency matrix
	 */
	void readInput(Scanner sc) {

		String info;
		try {
			if (sc.hasNextLine()) {
				// info = sc.nextLine();

				v = sc.nextInt();
				e = sc.nextInt();

			}

			adj_matrix = new int[v][v];
			graph = new vertex[v];
			vertex temp;
			for (int i = 0; i < v; i++) {
				temp = new vertex();
				temp.vertex_number = i;
				graph[i] = temp;
			}

			default_Matrix();
			int i = -1;
			int temp1 = -1;
			int temp2 = -1;
			while (sc.hasNextLine()) {
				i++;
				if (i == e)
					break;
				temp1 = sc.nextInt();
				temp2 = sc.nextInt();

				// else
				adj_matrix[temp1][temp2] = sc.nextInt();

			}

		}

		catch (InputMismatchException ie) {

			String msg = "Wrong Input format \nPlease use the following format\n5 10\n0 1 10\n0 3 5\n1 3 2\n3 1 3\n1 2 1\n3 4 2\n3 2 9\n2 4 4\n4 2 6\n4 0 7";
			System.err.println(msg);
			throw ie;

		}
	}

	/*
	 * Update all distances and predecessors for each vertex
	 */
	void initializegraph(int source) {
		for (vertex v : graph) {
			v.distance = Integer.MAX_VALUE;
			v.parent = null;

		}
		graph[source].distance = 0;

	}

	void relaxation(vertex u, vertex v, int weight) {
		// System.out.println("v.distance"+v.distance);
		// System.out.println("u.distance"+u.distance);
		if (v.distance > u.distance + weight) {
			// System.out.println("Updating "+v.vertex_number);
			v.distance = u.distance + weight;
			v.parent = u;

		}

	}

	/*
	 * Implemetation of Dijkstra single source shortest path algorithm Observed
	 * under timed environment.
	 */
	void dijkstra_algo() {

		ArrayList<vertex> priority_q = null;

		vertex[] temp_g = graph;

		System.out.println("Source vertex :" + temp_g[0].vertex_number);
		System.out
				.println("Length of shortest path to all other vertices(including itself) in increasing order of vertices(0....n) ");

		initializegraph(temp_g[0].vertex_number);
		priority_q = new ArrayList<vertex>(Arrays.asList(graph)); // Create
																	// priority
																	// to queue
																	// to
																	// extract
																	// node with
																	// minimum
																	// distance
		Collections.sort(priority_q);
		vertex[] result_matrix = new vertex[v];
		vertex temp_v = null;
		while (!priority_q.isEmpty()) {

			// temp_v= new vertex();
			temp_v = priority_q.remove(0);

			// System.out.println("temp_v"+temp_v.vertex_number+" ");
			result_matrix[temp_v.vertex_number] = (temp_v);
			for (vertex v : graph) { // Loop through all vertices and update new
										// distances
				if (temp_v.vertex_number != v.vertex_number) {

					int weight = adj_matrix[temp_v.vertex_number][v.vertex_number];

					if (weight != Integer.MAX_VALUE) {
						relaxation(temp_v, v, weight);
					}

				}

			}
			Collections.sort(priority_q);

		}

		for (vertex entry : result_matrix)
			System.out.print(entry.distance + " ");
		//System.out.println();

	}

	public static void main(String[] args) {

		Scanner sc;

		sc = new Scanner(System.in);

		Dijkstra d = new Dijkstra();
		d.readInput(sc);
		long startTime = System.currentTimeMillis();
		d.dijkstra_algo();// Call the shortest path algorithm
		long estimatedTime = System.currentTimeMillis() - startTime;
		System.out.println("\nExecution time in ms " + estimatedTime);

	}

}
