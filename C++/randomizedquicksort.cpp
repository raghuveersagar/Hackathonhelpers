#include<time.h>
#include<stdlib.h>
#include<vector>
#include<string>
#include<iostream>
#include<sstream>
#include<algorithm>
#include<iterator>
using namespace std;


int number_of_partitions=0;
int number_of_comparisions=0;
void exchange(vector<int> &v, int a, int b) {

	int temp = v[a];
	v[a] = v[b];
	v[b] = temp;

}

void printvector_3(vector<int> &v) {
	copy(v.begin(), v.end(), ostream_iterator<int>(cout, " "));
	cout << endl;

}


int random_partition(vector<int> &v, int p, int r)
{

	number_of_partitions++;
	srand(time(NULL));
    int pivot_index = p + rand() % (r-p+1);
    exchange(v,pivot_index,r);



	int x = v[r];
	int i = p - 1;
	for (int j = p; j < r; j++) {
		if (v[j] <= x) {
			i = i + 1;
			exchange(v, i, j);
		}
		number_of_comparisions++;
	}
	exchange(v, i + 1, r);
	return i + 1;
}



void randomomized_quicksort(vector<int> &v, int p, int r)
{
    if(p < r) {
        int q = random_partition(v, p, r);
        randomomized_quicksort(v, r, q-1);
        randomomized_quicksort(v, q+1, r);
    }
}

int main()
{
	string inputLine = string();
		getline(cin, inputLine);
		istringstream stream(inputLine);
		vector<int> values;
		clock_t t1,t2;
		int n;
		while (stream >> n) {

			values.push_back(n);
		}
t1= clock();
		randomomized_quicksort(values,0,values.size()-1);
		t2=clock();
		float diff((float) t2 - (float) t1);
			float seconds = diff / CLOCKS_PER_SEC;
		cout << "Total time taken in milli-seconds " << 1000 * seconds << endl;
		cout << "Number of comparisions " << number_of_comparisions << endl;
		cout << "Number of partitions " << number_of_partitions << endl;
		//printvector_3(values);

}
