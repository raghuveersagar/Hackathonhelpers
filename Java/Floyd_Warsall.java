import java.util.InputMismatchException;
import java.util.Scanner;

public class Floyd_Warsall {

	private int[][] adj_matrix;

	int v, e;

	public void default_Matrix() {
		for (int i = 0; i < v; i++) {
			for (int j = 0; j < v; j++) {
				if (i == j)
					adj_matrix[i][j] = 0;
				else
					adj_matrix[i][j] = Integer.MAX_VALUE;
			}
			// System.out.println();
		}
	}

	/*
	 * Read Input and create Adjacency matrix
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
				// if(!weighted)
				// adj_matrix[temp1][temp2]=1;
				// else
				adj_matrix[temp1][temp2] = sc.nextInt();

			}

		} catch (InputMismatchException iex) {
			String msg = "Wrong Input format \nPlease use the following format\n5 10\n0 1 10\n0 3 5\n1 3 2\n3 1 3\n1 2 1\n3 4 2\n3 2 9\n2 4 4\n4 2 6\n4 0 7";
			System.err.println(msg);
			throw iex;

		}

	}

	public void printMatrix() {
		for (int i = 0; i < v; i++) {
			for (int j = 0; j < v; j++) {
				int x = adj_matrix[i][j];
				String temp_s;
				if (x == Integer.MAX_VALUE)
					temp_s = " oo ";
				else
					temp_s = " " + String.valueOf(x) + " ";
				System.out.format(" %5s", temp_s);
			}
			System.out.println();
		}
	}

	public void printMatrix(int[][] adj) {
		for (int i = 0; i < v; i++) {
			for (int j = 0; j < v; j++) {
				int x = adj[i][j];
				String temp_s;
				if (x == Integer.MAX_VALUE)
					temp_s = " oo ";
				else
					temp_s = " " + String.valueOf(x) + " ";
				System.out.format(" %8s", temp_s);
			}
			System.out.println();
		}
	}

	private int min(int x, int y) {
		return x < y ? x : y;
	}

	int sum(int x, int y) {
		return (x == Integer.MAX_VALUE || y == Integer.MAX_VALUE) ? Integer.MAX_VALUE
				: (x + y);
	}

	/*
	 * The Floyd Warshall Algorithm Implementation
	 */

	void FloydWarshall_algo() {

		int[][] D = new int[v][v];
		D = adj_matrix;
		int temp_int = 0;

		for (int k = 0; k < v; k++) {

			for (int i = 0; i < v; i++) {
				for (int j = 0; j < v; j++) {
					D[i][j] = min(D[i][j], sum(D[i][k], D[k][j]));
				}

			}

			// System.out.println();
			// printMatrix(D);

		}

		System.out.println("All pairs shortest path matrix:\n");
		printMatrix(D);

	}

	public static void main(String[] args) {

		Scanner sc = new Scanner(System.in);
		Floyd_Warsall f = new Floyd_Warsall();
		f.readInput(sc);
		// f.printMatrix();
		long startTime = System.currentTimeMillis();
		f.FloydWarshall_algo();
		long estimatedTime = System.currentTimeMillis() - startTime;
		System.out.println("\nExecution time in ms " + estimatedTime);

	}

}
